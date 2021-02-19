using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public  class PFPrice : IPFPrice
    {
        void IPFPrice.RetailPrice(string sheet_nos, string is_markup_rate)
        {

            string sql = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                foreach (string sheet_no in sheet_nos.Split(';'))
                {
                    sql =
                        @"select a.flow_id,a.sale_qnty,a.item_no,b.cust_no sup_id,c.cust_level cust from sm_t_salesheet_detail a 
            left join sm_t_salesheet b on a.sheet_no = b.sheet_no
            left join bi_t_supcust_info c on b.cust_no = c.supcust_no  and c.supcust_flag='C'
where a.sheet_no='" + sheet_no + "'";
                    DataTable tb = d.ExecuteToTable(sql, null);
                    decimal amt = 0;
                    foreach (System.Data.DataRow row in tb.Rows)
                    {
                        string item_no = row["item_no"].ToString();
                        string cust = row["cust"].ToString();
                        string sup_id = row["sup_id"].ToString();
                        string sql1 = "select price,base_price,base_price2,base_price3 from bi_t_item_info where item_no='" + item_no + "'";
                        DataTable dt2 = d.ExecuteToTable(sql1, null);
                        decimal real_price = 0;
                        switch (cust)
                        {
                            case "0":
                                // Helper.Conv.ToDecimal(dt2.Rows[0]["base_price3"]);
                                real_price = Helper.Conv.ToDecimal(dt2.Rows[0]["price"].ToString());
                                break;
                            case "1":
                                real_price = Helper.Conv.ToDecimal(dt2.Rows[0]["base_price"].ToString());
                                break;
                            case "2":
                                real_price = Helper.Conv.ToDecimal(dt2.Rows[0]["base_price2"].ToString());
                                break;
                            case "3":
                                real_price = Helper.Conv.ToDecimal(dt2.Rows[0]["base_price3"].ToString());
                                break;
                        }

                        int flow_id = (int)Helper.Conv.ToDecimal(row["flow_id"].ToString());
                        decimal sale_qnty = Helper.Conv.ToDecimal(row["sale_qnty"].ToString());
                        //decimal real_price = Helper.Conv.ToDecimal(row["real_price"].ToString());
                        decimal sale_money = sale_qnty * real_price;
                        //Helper.Conv.ToDecimal(row["sale_money"].ToString());

                        sql = "update sm_t_salesheet_detail set real_price='" + real_price +
                              "',sale_money='" + sale_money +
                              "' where flow_id=" + flow_id;
                        d.ExecuteScalar(sql, null);
                        amt += sale_money;

                    }
                    sql = "update sm_t_salesheet set real_amount=" + amt +
                          ",total_amount=" + amt +
                          " where sheet_no='" + sheet_no + "'";
                    d.ExecuteScalar(sql, null);
                    //
                    db.CommitTran();
                }
            }

            catch (Exception)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }
        }
        System.Data.DataTable IPFPrice.GetUnApproveList(DateTime date1, DateTime date2, string cus_no)
        {
            string sql = @"select a.*,cus.sup_name,
branch.branch_name,(a.branch_no +'/'+ branch.branch_name) as branch_no_a,
oper.oper_name, (a.oper_id +'/'+ oper.oper_name) as oper_id_a
 from sm_t_salesheet a
left join (select * from bi_t_supcust_info where supcust_flag='C') cus on a.cust_no=cus.supcust_no
left join (select * from bi_t_branch_info ) branch on a.branch_no=branch.branch_no
left join (select * from sa_t_operator_i) oper on a.oper_id=oper.oper_id
where a.approve_flag='0' and (a.oper_date BETWEEN '" + date1.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + date2.ToString("yyyy-MM-dd") + " 23:59:59') ";
            if (cus_no != "")
            {
                sql += " and a.cust_no='" + cus_no + "'";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        void IPFPrice.SetPrice(string sheet_nos, string is_markup_rate)
        {
            if (sheet_nos == "")
            {
                return;
            }

            string sql = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                sql = @"select d.item_no,d.valid_price,i.markup_rate
 from dbo.ic_t_inout_store_detail  d
 LEFT JOIN dbo.bi_t_item_info i ON d.item_no=i.item_no
 WHERE d.flow_id in
(
select max(flow_id) as flow_id from dbo.ic_t_inout_store_detail a
left join dbo.ic_t_inout_store_master b on a.sheet_no=b.sheet_no
where b.trans_no='A' and b.approve_flag='1' group by a.item_no
)
";
                var tbprice = d.ExecuteToTable(sql, null);
                Dictionary<string, decimal> dic = new Dictionary<string, decimal>();
                foreach (System.Data.DataRow row in tbprice.Rows)
                {
                    string item_no = row["item_no"].ToString();
                    decimal price = Helper.Conv.ToDecimal(row["valid_price"].ToString());
                    if ("1".Equals(is_markup_rate))
                    {
                        price = price + price * row["markup_rate"].ToDecimal() / 100;
                    }
                    dic.Add(item_no, price);
                }
                //
                foreach (string sheet_no in sheet_nos.Split(';'))
                {
                    sql = "select * from sm_t_salesheet_detail where sheet_no='" + sheet_no + "'";
                    var tb = d.ExecuteToTable(sql, null);
                    decimal amt = 0;
                    foreach (System.Data.DataRow row in tb.Rows)
                    {
                        string item_no = row["item_no"].ToString();
                        int flow_id = (int)Helper.Conv.ToDecimal(row["flow_id"].ToString());
                        decimal sale_qnty = Helper.Conv.ToDecimal(row["sale_qnty"].ToString());
                        decimal real_price = Helper.Conv.ToDecimal(row["real_price"].ToString());
                        decimal sale_money = Helper.Conv.ToDecimal(row["sale_money"].ToString());
                        decimal price = 0;
                        if (dic.TryGetValue(item_no, out price) == true)
                        {
                            real_price = price;
                            sale_money = sale_qnty * price;
                            sql = "update sm_t_salesheet_detail set real_price=" + real_price +
                                ",sale_money=" + sale_money +
                                " where flow_id=" + flow_id;
                            d.ExecuteScalar(sql, null);
                            amt += sale_money;
                        }
                        else
                        {
                            amt += sale_money;
                        }
                    }
                    sql = "update sm_t_salesheet set real_amount=" + amt +
                        ",total_amount=" + amt +
                        " where sheet_no='" + sheet_no + "'";
                    d.ExecuteScalar(sql, null);
                }
                //
                db.CommitTran();
            }
            catch (Exception)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void IPFPrice.SetPrice(string sheet_nos, DataTable import_tb, string is_markup_rate)
        {
            if (sheet_nos == "" || import_tb == null || import_tb.Rows.Count < 1)
            {
                return;
            }

            string sql = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                Dictionary<string, decimal> dic = new Dictionary<string, decimal>();

                //
                sql = @"SELECT item_subno,item_no,markup_rate FROM dbo.bi_t_item_info";

                Dictionary<string, string> item_dic = new Dictionary<string, string>();
                var item_tb = d.ExecuteToTable(sql, null);
                foreach (DataRow row in item_tb.Rows)
                {
                    item_dic.Add(row["item_subno"].ToString(), row["item_no"].ToString());
                }

                foreach (DataRow dr in import_tb.Rows)
                {
                    string item_subno = dr["货号"].ToString();
                    string item_no;
                    if (item_dic.TryGetValue(item_subno, out item_no))
                    {
                        decimal price = dr["采购价格"].ToDecimal();
                        if ("1".Equals(is_markup_rate))
                        {
                            price = price + price * dr["markup_rate"].ToDecimal() / 100;
                        }

                        dic.Add(item_no, price);
                    }
                }

                //
                foreach (string sheet_no in sheet_nos.Split(';'))
                {
                    sql = "select * from sm_t_salesheet_detail where sheet_no='" + sheet_no + "'";
                    var tb = d.ExecuteToTable(sql, null);
                    decimal amt = 0;
                    foreach (System.Data.DataRow row in tb.Rows)
                    {
                        string item_no = row["item_no"].ToString();
                        int flow_id = (int)Helper.Conv.ToDecimal(row["flow_id"].ToString());
                        decimal sale_qnty = Helper.Conv.ToDecimal(row["sale_qnty"].ToString());
                        decimal real_price = Helper.Conv.ToDecimal(row["real_price"].ToString());
                        decimal sale_money = Helper.Conv.ToDecimal(row["sale_money"].ToString());
                        decimal price = 0;
                        if (dic.TryGetValue(item_no, out price) == true)
                        {
                            real_price = price;
                            sale_money = sale_qnty * price;
                            sql = "update sm_t_salesheet_detail set real_price=" + real_price +
                                ",sale_money=" + sale_money +
                                " where flow_id=" + flow_id;
                            d.ExecuteScalar(sql, null);
                            amt += sale_money;
                        }
                        else
                        {
                            amt += sale_money;
                        }
                    }
                    sql = "update sm_t_salesheet set real_amount=" + amt +
                        ",total_amount=" + amt +
                        " where sheet_no='" + sheet_no + "'";
                    d.ExecuteScalar(sql, null);
                }
                //
                db.CommitTran();
            }
            catch (Exception)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
