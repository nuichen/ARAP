using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class CountDetail : ICountDetail
    {

        string GetNewOrderCode(DB.IDB db)
        {

            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='BL'";
            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = 0;
                int.TryParse(obj.ToString(), out index);
                index += 1;
                if (index > 9999)
                {
                    index = 0;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='BL'";
                db.ExecuteScalar(sql, null);
                return "BL00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }

        }

        bool ICountDetail.Insert(string branch_no, System.Data.DataTable tb, out string sheet_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                //
                string sql = "select a.*,case when b.pack_num is null then 0 else b.pack_num end as pack_num from bi_t_item_info a " +
                    "left join bi_t_item_pack_detail b on a.item_no=b.item_no";
                var tbGoods = d.ExecuteToTable(sql, null);
                Dictionary<string, DataRow> goodsList = new Dictionary<string, DataRow>();
                foreach (DataRow row in tbGoods.Rows)
                {
                    string item_no = row["item_no"].ToString();
                    DataRow r;
                    if (goodsList.TryGetValue(item_no, out r) == false)
                    {
                        goodsList.Add(row["item_no"].ToString(), row);
                    }
                }
                //
                db.BeginTran();
                //
                sheet_no = GetNewOrderCode(d);

                //
                int index = 0;
                decimal amt = 0;

                List<co_t_order_child> lines = new List<co_t_order_child>();
                foreach (DataRow row in tb.Rows)
                {

                    DataRow goodsRow;
                    if (goodsList.TryGetValue(row["item_no"].ToString(), out goodsRow) == true)
                    {
                        index++;
                        co_t_order_child l = new co_t_order_child();
                        l.sheet_no = sheet_no;
                        l.item_no = row["item_no"].ToString();
                        l.unit_no = goodsRow["unit_no"].ToString();
                        l.unit_factor = Conv.ToDecimal(goodsRow["pack_num"]);
                        l.in_price = Conv.ToDecimal(goodsRow["sale_price"].ToString());
                        l.order_qnty = Conv.ToDecimal(row["qty"]);
                        l.sub_amount = l.in_price * l.order_qnty;
                        amt += l.sub_amount;
                        l.real_qty = 0;
                        l.tax_rate = 0;
                        l.pay_percent = 0;
                        l.other1 = "";
                        l.other2 = "";
                        l.other3 = "";
                        l.num1 = 0;
                        l.num2 = 0;
                        l.num3 = 0;
                        l.barcode = goodsRow["barcode"].ToString();
                        l.sheet_sort = index;
                        l.discount = 1;
                        l.voucher_no = "";
                        l.out_qty = 0;
                        l.packqty = 0;
                        l.sgqty = 0;
                        l.ordmemo = "";

                        lines.Add(l);

                    }

                }
                if (index == 0)
                {
                    throw new Exception("导出时，检查到合法行数为0");
                }
                //
                if (1 == 1)
                {
                    co_t_order_main m = new co_t_order_main();
                    m.sheet_no = sheet_no;
                    m.p_sheet_no = "";
                    m.sup_no = "";
                    m.order_man = "";
                    m.oper_id = "1001";
                    m.valid_date = System.DateTime.Now;
                    m.oper_date = System.DateTime.Now;
                    m.total_amount = amt;
                    m.paid_amount = 0;
                    m.trans_no = "L";
                    m.order_status = "0";
                    m.approve_flag = "0";
                    m.agree_inhand = "";
                    m.coin_code = "RMB";
                    m.branch_no = branch_no;
                    m.sale_way = "A";
                    m.memo = "";
                    m.other1 = "";
                    m.other2 = "";
                    m.cm_branch = "00";
                    m.approve_man = "";
                    m.approve_date = System.DateTime.MinValue;
                    m.num1 = 0;
                    m.num2 = 0;
                    m.num3 = 0;
                    m.ask_no = "";
                    m.co_sheetno = "";
                    d.Insert(m);
                }
                //
                foreach (co_t_order_child l in lines)
                {
                    d.Insert(l, "flow_id");
                }
                //
                db.CommitTran();
                return true;
            }
            catch (Exception)
            {
                db.RollBackTran();
                sheet_no = "";
                // Log.writeLog(ex.Message, ex.StackTrace);
                throw;
            }
            finally
            {
                db.Close();
            }
        }
    }
}