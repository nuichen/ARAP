using System;
using System.Collections.Generic;
using System.Data;
using DB;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class order : BaseService
    {
        IOrder bll = new Order();
        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }

        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);
            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }
            return web.NmJson();
        }

        public void GetPHSheets(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime endTime = w.ObjectToDate("endTime");
            DateTime startTime = w.ObjectToDate("startTime");
            DataTable tb = bll.GetPHSheets(startTime, endTime);
            w.Write(tb);
        }

        #region 采购统单

        public void GetReqOrderList(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string out_date = w.Read("out_date");
            var dt = bll.GetReqOrderList(date1, date2, out_date);
            w.Write(dt);
        }
        public void GetPHReqOrderList(WebHelper w, Dictionary<string, object> kv)
        {
            string ph_sheet_no = w.Read("ph_sheet_no");
            var dt = bll.GetPHReqOrderList(ph_sheet_no);
            w.Write(dt);
        }

        //获取送货日期列表
        public void get_ph_order_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "date1", "date2") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            var date1 = ObjectToString(kv, "date1");
            var date2 = ObjectToString(kv, "date2");

            var dt = bll.GetPHOrderList(date1, date2);
            w.Write("data", dt);
        }

        //过去销售订单明细
        public void get_req_order_detail(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sheet_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            var sheet_no = ObjectToString(kv, "sheet_no");

            var dt = bll.GetReqOrderDetail(sheet_no);
            w.Write("data", dt);
        }

        //生成采购订单商品汇总
        public void get_ph_req_order_diff(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            string is_bulk = w.Read("is_bulk");
            var dt = bll.GetReqOrderStockDiff(ph_sheet_no, is_bulk);
            w.Write("data", dt);
        }

        public void CreatePHOrder(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_nos = w.Read("sheet_nos");
            string oper_id = w.Read("oper_id");
            bll.CreatePHOrder(sheet_nos, oper_id,0.ToString());
        }

        //生成采购订单
        public void create_cg_order(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no", "op_type", "datas") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            string is_min_stock = w.Read("is_min_stock");
            string oper_id = w.Read("oper_id");
            DataTable datas = w.GetDataTable("datas");
            var op_type = ObjectToString(kv, "op_type");

            DataTable item_dt = new DataTable();
            item_dt.Columns.Add("item_no");
            item_dt.Columns.Add("unit_no");
            item_dt.Columns.Add("cg_rate");
            foreach (DataRow dr in datas.Rows)
            {
                var row = item_dt.NewRow();
                item_dt.Rows.Add(row);
                row["item_no"] = dr["item_no"];
                row["unit_no"] = dr["unit_no"];
                row["cg_rate"] = dr["cg_rate"];
            }

            bll.CreateCGOrder(ph_sheet_no, op_type, item_dt, is_min_stock, oper_id);
        }

        //查询物料运算明细
        public void SearchMaterialList(WebHelper w, Dictionary<string, object> kv)
        {
            IMrpBLL mrpBll = new MrpBLL();
            string ph_sheet_no = w.Read("ph_sheet_no");
            string cust_no = w.Read("cust_no");
            string keyword = w.Read("keyword");
            string only_show_nosup = w.Read("only_show_nosup");
            DataTable tb = mrpBll.SearchMaterialList(ph_sheet_no, cust_no, keyword, only_show_nosup);
            w.Write("data", tb);
        }

        //生成运算统单
        public void BindItemSup(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable lines = new DataTable();
            lines.Columns.Add("flow_id");
            lines.Columns.Add("sup_no");
            foreach (ReadWriteContext.IReadContext r2 in ReadContext.ReadList("data"))
            {
                var line = lines.NewRow();
                lines.Rows.Add(line);

                line["flow_id"] = r2.Read("flow_id");
                line["sup_no"] = r2.Read("sup_no");
            }
            IMrpBLL mrpBll = new MrpBLL();
            string ph_sheet_no = w.Read("ph_sheet_no");
            string oper_id = w.Read("oper_id");
            mrpBll.BindItemSup(ph_sheet_no, oper_id, lines);
        }

        //生成运算统单
        public void DoMrp(WebHelper w, Dictionary<string, object> kv)
        {
            IMrpBLL mrpBll = new MrpBLL();
            string ph_sheet_no = w.Read("ph_sheet_no");
            string oper_id = w.Read("oper_id");
            mrpBll.DoMrp(ph_sheet_no, oper_id);
        }
        public void CreateCgOrderByDtl(WebHelper w, Dictionary<string, object> kv)
        {
            IMrpBLL mrpBll = new MrpBLL();

            string ph_sheet_no = w.Read("ph_sheet_no");
            string op_type = w.Read("op_type");
            string item_nos = w.Read("item_nos");
            string is_min_stock = w.Read("is_min_stock");
            string oper_id = w.Read("oper_id");
            mrpBll.CreateCgOrderByDtl(ph_sheet_no, op_type, item_nos, is_min_stock, oper_id);
        }
        public void CreateCgOrderBySum(WebHelper w, Dictionary<string, object> kv)
        {
            IMrpBLL mrpBll = new MrpBLL();

            string ph_sheet_no = w.Read("ph_sheet_no");
            string op_type = w.Read("op_type");
            string item_nos = w.Read("item_nos");
            string is_min_stock = w.Read("is_min_stock");
            string oper_id = w.Read("oper_id");
            mrpBll.CreateCgOrderBySum(ph_sheet_no, op_type, item_nos, is_min_stock, oper_id);
        }


        //按供应商导出
        public void get_sup_req_order_detail(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            var dt = bll.GetSupReqOrderDetail(ph_sheet_no);
            w.Write("data", dt);
        }

        //按分类导出
        public void get_cls_req_order_detail(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            var dt = bll.GetClsReqOrderDetail(ph_sheet_no);
            w.Write("data", dt);
        }

        //按商品导出
        public void get_item_req_order_sum(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            var dt = bll.GetItemReqOrderSum(ph_sheet_no);
            w.Write("data", dt);
        }

        //供应商协拣
        public void get_sup_pick_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no", "sup_no", "item_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            string sup_no = w.Read("sup_no");
            string item_no = w.Read("item_no");
            DateTime startTime = w.ObjectToDate("startTime");
            DateTime endTime = w.ObjectToDate("endTime");

            var dt = bll.GetSupPickList(ph_sheet_no, sup_no, item_no, startTime, endTime);
            w.Write("data", dt);
        }
        #endregion

        public void DoBulkMrp(WebHelper w, Dictionary<string, object> kv)
        {
            IMrpBLL mrpBll = new MrpBLL();

            string sheet_nos = w.Read("sheet_nos");
            DateTime date1 = w.ObjectToDate("date1");
            DateTime date2 = w.ObjectToDate("date2");
            string ms_other = w.Read("ms_other");
            string oper_id = w.Read("oper_id");
            mrpBll.DoBulkMrp(sheet_nos, date1, date2, ms_other, oper_id);
        }   
        //按供应商协拣导出
        public void get_sup_pick_order_detail(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            DataTable dt = bll.GetSupPickOrderDetail(ph_sheet_no);
            w.Write("data", dt);
        }
        public void DoLockInventory(WebHelper w, Dictionary<string, object> kv)
        {
            IMrpBLL mrpBll = new MrpBLL();
            string sheet_nos = w.Read("sheet_nos");
            string cb_sheet_no = w.Read("cb_sheet_no");
            string lock_sheet_nos = w.Read("lock_sheet_nos");
            mrpBll.DoLockInventory(cb_sheet_no, sheet_nos, lock_sheet_nos);
        }

        public void CreateBulkCgOrder(WebHelper w, Dictionary<string, object> kv)
        {
            IMrpBLL mrpBll = new MrpBLL();

            string cb_sheet_no = w.Read("cb_sheet_no");
            string op_type = w.Read("op_type");
            string item_nos = w.Read("item_nos");
            string is_min_stock = w.Read("is_min_stock");
            string oper_id = w.Read("oper_id");
            mrpBll.CreateBulkCgOrder(cb_sheet_no, op_type, item_nos, is_min_stock, oper_id);
        }

        public void GetBulikItems(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable tb = bll.GetBulikItems();
            w.Write(tb);
        }

        public void SaveBulkItems(WebHelper w, Dictionary<string, object> kv)
        {
            List<bi_t_bulk_item> items = w.GetList<bi_t_bulk_item>("items");
            bll.SaveBulkItems(items);
        }

        public void GetBulkReqOrderList(WebHelper w, Dictionary<string, object> kv)
        {
            string cb_sheet_no = w.Read("cb_sheet_no");
            DataTable tb = bll.GetBulkReqOrderList(cb_sheet_no);
            w.Write(tb);
        }

        public void GetBulkOrderList(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.ObjectToDate("date1").Toyyyy_MM_ddStart();
            string date2 = w.ObjectToDate("date2").Toyyyy_MM_ddEnd();
            DataTable tb = bll.GetBulkOrderList(date1, date2);
            w.Write(tb);
        }

        public void GetBulkReqOrderStockDiff(WebHelper w, Dictionary<string, object> kv)
        {
            string cb_sheet_no = w.Read("cb_sheet_no");
            DataTable tb = bll.GetBulkReqOrderStockDiff(cb_sheet_no);
            w.Write(tb);
        }

        //按供应商导出
        public void GetBulkSupReqOrderDetail(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            var dt = bll.GetBulkSupReqOrderDetail(ph_sheet_no);
            w.Write("data", dt);
        }

        //按分类导出
        public void GetBulkClsReqOrderDetail(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            var dt = bll.GetBulkClsReqOrderDetail(ph_sheet_no);
            w.Write("data", dt);
        }

        //按商品导出
        public void GetBulkItemReqOrderSum(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ph_sheet_no = w.Read("ph_sheet_no");
            var dt = bll.GetBulkItemReqOrderSum(ph_sheet_no);
            w.Write("data", dt);
        }

        #region 智能定价   
        //获取商品分类
        public void GetItemCls(WebHelper w, Dictionary<string, object> kv)
        {
            var dt = bll.GetItemCls();
            w.Write("data", dt);
        }

        //获取左侧分类下商品
        public void GetLeftTable(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "item_clsno") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string item_clsno = ObjectToString(kv, "item_clsno");
            var dt = bll.GetLeftTable(item_clsno);
            w.Write("data", dt);
        }

        //获取商品价格
        public void GetItemPrice(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "item_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string item_no = ObjectToString(kv, "item_no");
            var dt = bll.GetItemPrice(item_no);
            w.Write("data", dt);
        }

        public void GetPriceHistory(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "item_name") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }

            string item_name = ObjectToString(kv, "item_name");
            var dt = bll.GetPriceHistory(item_name);
            w.Write("data", dt);
        }

        public void GetItemList(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "item_nos") == false)
            {
                var dt = bll.GetItemList();
                w.Write("data", dt);
            }
            else
            {
                List<string> item_nos = w.GetList<string>("item_nos");
                var dt = bll.GetItemList(item_nos);
                w.Write("data", dt);
            }
        }
        #endregion

        #region 快速开单—追加采购助手单  
        public void AddNewCGOrder(WebHelper w, Dictionary<string, object> kv)
        {
            List<Model.co_t_cg_order_detail> lines = w.GetList<Model.co_t_cg_order_detail>("lines");
            bll.AddCGOrder(lines);
        }
        #endregion

        //下载销售订单
        public void get_out_order(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sheet_no") == false)
            {
                w.WriteInvalidParameters();
                return;
            }

            DataTable tbMain;
            DataTable tbDetail;
            string sheet_no = ObjectToString(kv, "sheet_no");
            if (bll.GetOutPut(sheet_no, out tbMain, out tbDetail))
            {
                w.Write("data1", tbMain);
                w.Write("data2", tbDetail);
            }
        }

        //下载采购订单
        public void get_cg_order(WebHelper w, Dictionary<string, object> kv)
        {
            if (bll.GetCoOrder("", out var tbMain3, out var tbDetail3))
            {
                w.Write("data1", tbMain3);
                w.Write("data2", tbDetail3);
            }
        }

        //退货
        public void re_order(WebHelper w, Dictionary<string, object> kv)
        {
            //退货
            ic_t_inout_store_master main = new ic_t_inout_store_master()
            {
                branch_no = ObjectToString(kv, "branch_no"),
                trans_no = "D",
                db_no = "+",
                oper_id = "",
                display_flag = "1",
                supcust_no = ObjectToString(kv, "supcust_no"),
                approve_flag = "0",
                pay_date = DateTime.Now,
                oper_date = DateTime.Now,
                discount = 1M,
                coin_no = "RMB",
            };
            List<ic_t_inout_store_detail> details = new List<ic_t_inout_store_detail>();

            Dictionary<string, decimal> dic = new Dictionary<string, decimal>();
            foreach (ReadWriteContext.IReadContext r2 in ReadContext.ReadList("data"))
            {
                string item_no = r2.Read("item_no");
                if (dic.ContainsKey(item_no))
                    dic[item_no] += Conv.ToDecimal(r2.Read("qty"));
                else
                    dic.Add(item_no, Conv.ToDecimal(r2.Read("qty")));
            }
            Dictionary<string, string> dicItem = bll.GetItem();

            foreach (string key in dic.Keys)
            {
                var item = bll.GetItem(key);
                int pack = 1;
                if (item.item_size.Contains("*"))
                {
                    var strs = item.item_size.Split('*');
                    pack = Conv.ToInt(strs[strs.Length - 1]);
                }
                //添加
                details.Add(new ic_t_inout_store_detail()
                {
                    sheet_no = main.sheet_no,
                    item_no = key,
                    item_name = dicItem[key],
                    unit_no = item.unit_no,
                    unit_factor = 1.00M,
                    orgi_price = item.price,
                    cost_price = item.base_price,
                    barcode = item.barcode,
                    packqty = (int)Math.Floor(dic[key] / pack),
                    ret_qnty = dic[key],
                    sgqty = dic[key] % pack,
                    in_qty = dic[key],
                    is_tax = "0",
                    valid_date = DateTime.Now.AddMonths(1)
                });
            }
            bll.AddReOrder(main, details);
        }

        //上传采购入库单
        public void coll_order(WebHelper w, Dictionary<string, object> kv)
        {
            //收货入库         supcust_no = ObjectToString(kv, "supcust_no")
            var main = new ic_t_inout_store_master()
            {

                oper_id = "1001",
                trans_no = "A",
                branch_no = ObjectToString(kv, "branch_no"),
                approve_flag = "0",
                oper_date = DateTime.Now,
                db_no = "+",
                coin_no = "RMB",
                supcust_no = ObjectToString(kv, "supcust_no"),
                pay_date = DateTime.Now,
                old_no = ExistsKeys(kv, "p_sheet_no") ? ObjectToString(kv, "p_sheet_no") : "",
                display_flag = "1",
                discount = 0M,
                sale_no = "A",
                deal_man = "0"
            };
            List<ic_t_inout_store_detail> childs = new List<ic_t_inout_store_detail>();
            //childs = ReadContext.ReadList("data");

            var dic = new Dictionary<string, decimal>();
            var item_nos = "";
            foreach (ReadWriteContext.IReadContext r2 in ReadContext.ReadList("data"))
            {
                string item_no = r2.Read("item_no");
                item_nos += item_no + ",";
                if (dic.ContainsKey(item_no))
                    dic[item_no] += Conv.ToDecimal(r2.Read("qty"));
                else
                    dic.Add(item_no, Conv.ToDecimal(r2.Read("qty")));
            }
            var dicc1 = new Dictionary<string, string>();
            if (main.old_no != "")
            {
                var tb1 = bll.GetOrderDetail(main.old_no);

                string item_no1;
                foreach (DataRow r in tb1.Rows)
                {
                    string item_no = r["item_no"].ToString();
                    if (!dicc1.TryGetValue(item_no, out item_no1))
                    {
                        dicc1.Add(item_no, r["item_no"].ToString());
                    }
                }
            }

            if (item_nos.Length > 0) item_nos = item_nos.Substring(0, item_nos.Length - 1);
            var dicItem = bll.GetItemDic(item_nos);
            foreach (string key in dic.Keys)
            {
                if (dicc1.Count > 0)
                {
                    string item_name1;
                    if (!dicc1.TryGetValue(key, out item_name1))
                    {
                        continue;
                    }
                }
                var item = bll.GetItem(key);
                if (item == null)
                {
                    LogHelper.writeLog("coll_order()", "商品档案不存在", key);
                    throw new Exception("商品档案不存在[" + key + "]");
                }
                int pack = 1;
                if (!string.IsNullOrEmpty(item.item_size) && item.item_size.Contains("*"))
                {
                    var strs = item.item_size.Split('*');
                    pack = Conv.ToInt(strs[strs.Length - 1]);
                }
                //添加
                childs.Add(new ic_t_inout_store_detail()
                {
                    sheet_no = main.sheet_no,
                    item_no = item.item_no,
                    item_name = dicItem[key],
                    unit_no = item.unit_no,
                    in_qty = dic[key],
                    sgqty = dic[key] % pack,
                    packqty = (int)Math.Floor(dic[key] / pack),
                    unit_factor = 1.00M,
                    barcode = item.barcode,
                    is_tax = "0",
                    valid_date = DateTime.Now.AddMonths(1)
                });

            }

            if (childs.Count > 0)
            {
                bll.AddColOrder(main, childs);
            }
        }

        //上传销售出库单
        public void up_order(WebHelper w, Dictionary<string, object> kv)
        {
            sm_t_salesheet main1 = new sm_t_salesheet()
            {
                cust_no = ObjectToString(kv, "sup_no"),
                pay_way = "A",
                oper_id = "0",
                sale_man = "0",
                discount = 1M,
                coin_no = "RMB",
                approve_flag = "0",
                oper_date = DateTime.Now.AddDays(0),
                pay_date = DateTime.Now,
                branch_no = ObjectToString(kv, "branch_no"),
                //    is_tax = '0'
            };
            List<sm_t_salesheet_detail> details1 = new List<sm_t_salesheet_detail>();
            if (ExistsKeys(kv, "sheet_no") && !string.IsNullOrEmpty(ObjectToString(kv, "sheet_no")))
            {
                //有订单
                main1.voucher_no = ObjectToString(kv, "sheet_no");
                var tb = bll.GetOrderDetail(main1.voucher_no);
                Dictionary<string, string> dicSS = new Dictionary<string, string>();
                foreach (DataRow row in tb.Rows)
                {
                    string item_no = row["item_no"].ToString();
                    string temp;
                    if (dicSS.TryGetValue(item_no, out temp) == false)
                    {
                        dicSS.Add(item_no, item_no);
                    }
                }
                var dic = new Dictionary<string, decimal>();
                DB.IDB db = new DBByAutoClose(AppSetting.conn);
                var item_nos = "";
                foreach (ReadWriteContext.IReadContext r2 in ReadContext.ReadList("data"))
                {

                    string item_no = r2.Read("item_no");
                    item_nos += item_no + ",";
                    decimal qty = Conv.ToDecimal(r2.Read("qty"));
                    string temp = "";
                    if (item_no.StartsWith("PK") == true && dicSS.TryGetValue(item_no, out temp) == false)
                    {
                        var sql = "select * from bi_t_item_info where item_no='" + item_no + "'";
                        var dt = db.ExecuteToTable(sql, null);
                        if (dt.Rows.Count == 0)
                        {
                            throw new Exception("匹配不到主商品" + item_no);
                        }
                        var row = dt.Rows[0];
                        item_no = row["item_no"].ToString();
                        qty = qty * Conv.ToInt("1");
                    }
                    decimal nums = 0;
                    if (dic.TryGetValue(item_no, out nums))
                    {
                        //存在
                        nums += qty;
                        dic.Remove(item_no);
                        dic.Add(item_no, nums);
                    }
                    else
                    {
                        //不存在
                        dic.Add(item_no, qty);
                    }
                }
                if (item_nos.Length > 0) item_nos = item_nos.Substring(0, item_nos.Length - 1);

                var dicItem = bll.GetItemDic(item_nos);
                foreach (DataRow r in tb.Rows)
                {
                    string item_no = r["item_no"].ToString();
                    //
                    decimal nums = 0;
                    if (!dic.TryGetValue(item_no, out nums)) continue;
                    int pack = 1;
                    if (r["item_size"].ToString().Contains("*"))
                    {
                        var strs = r["item_size"].ToString().Split('*');
                        pack = Conv.ToInt(strs[strs.Length - 1]);
                    }
                    //添加
                    details1.Add(new sm_t_salesheet_detail()
                    {
                        sheet_no = main1.sheet_no,
                        item_no = item_no,
                        item_name = dicItem[item_no],
                        unit_no = r["unit_no"].ToString(),
                        unit_factor = Conv.ToDecimal(r["unit_factor"]),
                        sale_price = Conv.ToDecimal(r["in_price"]),
                        sale_qnty = nums,
                        other1 = r["other1"].ToString(),
                        other2 = r["other2"].ToString(),
                        other3 = r["other3"].ToString(),
                        num1 = Conv.ToDecimal(r["num1"]),
                        num2 = Conv.ToDecimal(r["num2"]),
                        num3 = Conv.ToDecimal(r["num3"]),
                        barcode = r["barcode"].ToString(),
                        sheet_sort = Conv.ToInt(r["sheet_sort"]),
                        discount = Conv.ToDecimal(r["discount"]),
                        voucher_no = main1.voucher_no,
                        packqty = (int)Math.Floor(nums / pack),
                        sgqty = nums % pack,
                        //  is_tax = "0"
                    });

                }
            }
            else
            {
                //无订单
                main1.voucher_no = "";
                var item_nos = "";
                main1.cust_no = ObjectToString(kv, "supcust_no");
                var dic = new Dictionary<string, decimal>();
                foreach (ReadWriteContext.IReadContext r2 in ReadContext.ReadList("data"))
                {
                    string item_no = r2.Read("item_no");
                    item_nos += item_no + ",";
                    //   string item_subno= r2.Read("item_subno");
                    if (dic.ContainsKey(item_no))
                        dic[item_no] += Conv.ToDecimal(r2.Read("qty"));
                    else
                        dic.Add(item_no, Conv.ToDecimal(r2.Read("qty")));
                }
                if (item_nos.Length > 0) item_nos = item_nos.Substring(0, item_nos.Length - 1);
                var dicItem = bll.GetItemDic(item_nos);
                foreach (string key in dic.Keys)
                {

                    var item = bll.GetItem(key);
                    if (item == null)
                    {
                        LogHelper.writeLog("coll_order()", "商品档案不存在", key);
                        throw new Exception("商品档案不存在[" + key + "]");
                    }

                    int pack = 1;
                    if (item.item_size.Contains("*"))
                    {
                        var strs = item.item_size.Split('*');
                        pack = Conv.ToInt(strs[strs.Length - 1]);
                    }
                    // Log.writeLog("key", "key值：" + dic[key] + "  pack：" + pack);
                    //添加
                    details1.Add(new sm_t_salesheet_detail()
                    {
                        sheet_no = main1.sheet_no,
                        item_no = key,
                        item_name = dicItem[key],
                        unit_no = item.unit_no,
                        real_price = item.price,
                        unit_factor = 1.00M,
                        sale_price = item.price,
                        barcode = item.barcode,
                        packqty = (int)Math.Floor(dic[key] / pack),
                        sale_qnty = dic[key],
                        sgqty = dic[key] % pack,
                        is_tax = "0"
                    });

                }
            }
            if (details1.Count > 0)
            {
                bll.AddOrder(main1, details1);
            }
        }

        //换货入库
        public void barter_order(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_inout_store_master main2 = new ic_t_inout_store_master()
            {
                branch_no = "0009",
                trans_no = "D",
                db_no = "+",
                oper_id = "",
                display_flag = "1",
                supcust_no = ObjectToString(kv, "supcust_no"),
                approve_flag = "0",
                pay_date = DateTime.Now,
                oper_date = DateTime.Now,
                discount = 1M,
                coin_no = "RMB",
            };
            List<ic_t_inout_store_detail> details2 = new List<ic_t_inout_store_detail>();
            Dictionary<string, decimal> dic2 = new Dictionary<string, decimal>();
            foreach (ReadWriteContext.IReadContext r2 in ReadContext.ReadList("data"))
            {
                string item_no = r2.Read("item_no");
                if (dic2.ContainsKey(item_no))
                    dic2[item_no] += Conv.ToDecimal(r2.Read("qty"));
                else
                    dic2.Add(item_no, Conv.ToDecimal(r2.Read("qty")));
            }

            var dicItem = bll.GetItem();
            foreach (string key in dic2.Keys)
            {

                var item = bll.GetItem(key);
                int pack = 1;
                if (item.item_size.Contains("*"))
                {
                    var strs = item.item_size.Split('*');
                    pack = Conv.ToInt(strs[strs.Length - 1]);
                }

                //添加
                details2.Add(new ic_t_inout_store_detail()
                {
                    sheet_no = main2.sheet_no,
                    item_no = key,
                    item_name = dicItem[key],
                    unit_no = item.unit_no,
                    unit_factor = 1.00M,
                    orgi_price = item.price,
                    cost_price = item.base_price,
                    barcode = item.barcode,
                    packqty = (int)Math.Floor(dic2[key] / pack),
                    ret_qnty = dic2[key],
                    sgqty = dic2[key] % pack,
                    in_qty = dic2[key],
                });
            }

            bll.AddReOrder(main2, details2);
        }

        //残次品入库
        public void bad_goods_out(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_inout_store_master main3 = new ic_t_inout_store_master()
            {
                branch_no = "0009",
                trans_no = "F",
                db_no = "-",
                oper_id = "",
                display_flag = "1",
                supcust_no = ObjectToString(kv, "supcust_no"),
                approve_flag = "0",
                pay_date = DateTime.Now,
                oper_date = DateTime.Now,
                discount = 1M,
                coin_no = "RMB",
                sale_no = "A"
            };
            List<ic_t_inout_store_detail> details3 = new List<ic_t_inout_store_detail>();

            Dictionary<string, decimal> dic3 = new Dictionary<string, decimal>();
            foreach (ReadWriteContext.IReadContext r2 in ReadContext.ReadList("data"))
            {
                string item_no = r2.Read("item_no");
                if (dic3.ContainsKey(item_no))
                    dic3[item_no] += Conv.ToDecimal(r2.Read("qty"));
                else
                    dic3.Add(item_no, Conv.ToDecimal(r2.Read("qty")));
            }
            var dicItem = bll.GetItem();
            foreach (string key in dic3.Keys)
            {

                var item = bll.GetItem(key);
                int pack = 1;
                if (item.item_size.Contains("*"))
                {
                    var strs = item.item_size.Split('*');
                    pack = Conv.ToInt(strs[strs.Length - 1]);
                }


                //添加
                details3.Add(new ic_t_inout_store_detail()
                {
                    sheet_no = main3.sheet_no,
                    item_no = key,
                    item_name = dicItem[key],
                    unit_no = item.unit_no,
                    unit_factor = 1.00M,
                    orgi_price = item.price,
                    cost_price = item.base_price,
                    barcode = item.barcode,
                    packqty = (int)Math.Floor(dic3[key] / pack),
                    sgqty = dic3[key] % pack,
                    in_qty = dic3[key],
                });
            }

            bll.AddReOrder(main3, details3);
        }
    }
}