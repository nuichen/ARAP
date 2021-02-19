using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using Model.InOutModel;

namespace IvyTran.svr.ERP
{
    public class inout : BaseService
    {

        IInOutBLL bll = new InOutBLL();
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
        public void GetReceiveOrderDetail(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime start = w.Read("start").ToDateTime();
            DateTime over = w.Read("end").ToDateTime();
            string item_no = w.Read("item_no");
            string is_build = w.Read("is_build");
            DataTable dt = bll.GetReceiveOrderDetail(start, over, item_no, is_build);
            w.Write("dt", dt);
        }
        public void UpdateReceiveOrderDetail(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = w.GetDataTable("dt");
            bll.UpdateReceiveOrderDetail(dt);

        }
        public void get_deleteTZ(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string type = w.Read("type");
            bll.DeleteInOutTZ(sheet_no,type);
        }

        public void get_inoutTZ_sheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");

            DataTable tb = bll.GetInOutTZSheet(sheet_no);
            w.Write("data", tb);
        }
        public void get_intoutTZ_sheetD(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.InOutTZSheetDetail(sheet_no, out tb1, out tb2);
            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void get_intoutTZ_sheetD_D(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.InOutTZSheetDetail_D(sheet_no, out tb1, out tb2);
            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }

        public void get_inoutTZ_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string cust_no = w.Read("cust_no");
            var tb = bll.InOutTZList(date1, date2, cust_no);
            w.Write("data", tb);
        }
        public void add_inoutTZ(WebHelper w, Dictionary<string, object> kv)
        {
            List<Model.InOutModel.ic_t_inoutstore_recpay_detail> date1 = w.GetList<Model.InOutModel.ic_t_inoutstore_recpay_detail>("lr");
            ic_t_inoutstore_recpay_main main = w.GetObject<ic_t_inoutstore_recpay_main>("main");
            bll.add_InOutTZ(date1, main);
        }
        public void check_inoutTZ(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            DateTime update_time = w.Read("update_time").ToDateTime();
            CheckSheet cs = new CheckSheet();
            cs.CheckInOutZT(sheet_no, approve_man, update_time);
        }
        public void get_salesheetJG_sheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            DataTable tb = bll.GetSaleSheetJGSheet(sheet_no,Convert.ToDateTime(date1), Convert.ToDateTime(date2));
            w.Write("data", tb);
        }
        public void get_salesheetJG_sheetD(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetSaleSheetJGSheetDetail(sheet_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void get_salesheetTZ_sheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");

           DataTable tb= bll.GetSaleSheetTZSheet(sheet_no);
           w.Write("data", tb);
        }
        public void get_salesheetTZ_sheetD(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
             bll.GetSaleSheetTZSheetDetail(sheet_no, out tb1, out tb2);

             w.Write("tb1", tb1);
             w.Write("tb2", tb2);
        }
        public void get_salesheetTZ(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetSaleSheetTZ(sheet_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void get_salesheetTZ_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string cust_no = w.Read("cust_no");
            var tb = bll.GetSaleSheetTZList(date1, date2, cust_no);
            w.Write("data", tb);
        }
        public void add_TZ(WebHelper w, Dictionary<string, object> kv)
        {
            List<Model.InOutModel.sm_t_salesheet_recpay_detail> date1 = w.GetList<Model.InOutModel.sm_t_salesheet_recpay_detail>("lr");
           sm_t_salesheet_recpay_main main = w.GetObject<sm_t_salesheet_recpay_main>("main");
           bll.add_TZ(date1,main);
        }
        public void check_TZ(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            DateTime update_time = w.Read("update_time").ToDateTime();
            CheckSheet cs=new CheckSheet();
            cs.CheckZT(sheet_no,approve_man,update_time);
        }
        public void get_salesheet_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string cust_id = w.Read("cust_id");
            string sale_man = w.Read("sale_man");

            var tb = bll.GetSaleSheetList(date1, date2, cust_id, sale_man);
            w.Write("data", tb);
        }
      
        public void cg_to_Ss(WebHelper w, Dictionary<string, object> kv)
        {
            string str= w.Read("str");
            string sstr= w.Read("sstr");
            ArrayList al=new ArrayList();
            string[] s = str.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                if (!string.IsNullOrEmpty(s[i]))
                {
                    al.Add(s[i]);
                }
            }
            try
            {
                //bll.CGInSO(sstr,al);
                w.Write("b", true);
            }
            catch (Exception e)
            {
                w.Write("b", true);
                throw new Exception(e.ToString());
            }
            
            
        }
        public void get_simple_salesheet_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string cust_id = w.Read("cust_id");

            var tb = bll.GetSimpleSaleSheetList(date1, date2, cust_id);
            w.Write("data", tb);
        }
        public void get_salesheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetSaleSheet(sheet_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void add_salesheet(WebHelper w, Dictionary<string, object> kv)
        {
            body.sm_t_salesheet ord = w.GetObject<body.sm_t_salesheet>("ord");
            ord.approve_flag = "0";
            ord.source_flag = "";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;
            ord.old_no = "";
            ord.cust_cls = "";

            List<body.sm_t_salesheet_detail> lines = w.GetList<body.sm_t_salesheet_detail>("lines");
            foreach (var item in lines)
            {
                item.ly_sup_no = "";
                item.ly_rate = 0M;
                item.produce_day = DateTime.MinValue;
            }
            string sheet_no = "";
            bll.AddSaleSheet(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change_salesheet(WebHelper w, Dictionary<string, object> kv)
        {
            body.sm_t_salesheet ord = w.GetObject<body.sm_t_salesheet>("ord");
            ord.approve_flag = "0";
            ord.source_flag = "";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;
            ord.old_no = "";
            ord.cust_cls = "";

            List<body.sm_t_salesheet_detail> lines = w.GetList<body.sm_t_salesheet_detail>("lines");
            foreach (var item in lines)
            {
                item.ly_sup_no = "";
                item.ly_rate = 0M;
                item.produce_day = DateTime.MinValue;
            }
            bll.ChangeSaleSheet(ord, lines);
        }
        public void delete_salesheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteSaleSheet(sheet_no, update_time);
        }
        public void check_salesheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.CheckSaleSheet(sheet_no, approve_man, update_time);
        }
        public void GetSaleSheetExport(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string sup_no = w.Read("sup_no");
            DateTime oper_date = w.ObjectToDate("oper_date");

            DataTable tb = bll.GetSaleSheetExport(sheet_no, sup_no, oper_date);

            w.Write(tb);
        }


        public void GetImportSSSheet(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable tb = bll.GetImportSSSheet();
            w.Write(tb);
        }
        public void get_salesssheet_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string cust_id = w.Read("sup_no");
            string sale_man = w.Read("order_main");

            var tb = bll.GetSaleSSSheetList(date1, date2, cust_id, sale_man);
            w.Write(tb);
        }
        public void GetSaleSSSheetListPS(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string cust_id = w.Read("sup_no");
            string sale_man = w.Read("order_main");

            var tb = bll.GetSaleSSSheetListPS(date1, date2, cust_id, sale_man);
            w.Write(tb);
        }
        public void get_salesssheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetSaleSSSheet(sheet_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void add_salesssheet(WebHelper w, Dictionary<string, object> kv)
        {
            co_t_order_main ord = w.GetObject<co_t_order_main>();
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;

            List<co_t_order_child> lines = w.GetList<co_t_order_child>("lines");

            string is_gen_cg = w.ObjectToString("is_gen_cg");

            string sheet_no = "";
            bll.AddSaleSSSheet(ord, lines, is_gen_cg, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change_salesssheet(WebHelper w, Dictionary<string, object> kv)
        {
            co_t_order_main ord = w.GetObject<co_t_order_main>();
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;

            List<co_t_order_child> lines = w.GetList<co_t_order_child>("lines");

            bll.ChangeSaleSSSheet(ord, lines);
        }
        public void ChangeSaleSSheetGenPC(WebHelper w, Dictionary<string, object> kv)
        {
            Dictionary<string, string> dic = w.GetDic<string, string>();
            bll.ChangeSaleSSheetGenPC(dic);
        }
        public void delete_salesssheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteSaleSSSheet(sheet_no, update_time);
        }
        public void check_salesssheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.CheckSaleSSSheet(sheet_no, approve_man, update_time);
        }
        public void GetSaleSSDetailSum(WebHelper w, Dictionary<string, object> kv)
        {
            List<string> sheet_nos = w.GetList<string>("sheet_nos");
            DataTable tb = bll.GetSaleSSDetailSum(sheet_nos);
            w.Write(tb);
        }

        public void GetImportCGOrder(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable tb = bll.GetImportCGOrder();
            w.Write(tb);
        }
        public void get_cgorder_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string cust_id = w.Read("sup_no");
            string sale_man = w.Read("order_main");
            int type=Convert.ToInt32(w.Read("type"));
            var tb=new DataTable();
            if (type == 1)
            {
                tb = bll.GetCGOrderList(date1, date2, cust_id, sale_man,type);
            }
            else
            {
                tb = bll.GetCGOrderList(date1, date2, cust_id, sale_man);
            }
            w.Write(tb);
        }
        public void get_cgorder(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetCGOrder(sheet_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void add_cgorder(WebHelper w, Dictionary<string, object> kv)
        {
            co_t_order_main ord = w.GetObject<co_t_order_main>();
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;

            List<co_t_order_child> lines = w.GetList<co_t_order_child>("lines");

            string sheet_no = "";
            bll.AddCGOrder(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change_cgorder(WebHelper w, Dictionary<string, object> kv)
        {
            co_t_order_main ord = w.GetObject<co_t_order_main>();
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;

            List<co_t_order_child> lines = w.GetList<co_t_order_child>("lines");

            bll.ChangeCGOrder(ord, lines);
        }
        public void delete_cgorder(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteCGOrder(sheet_no, update_time);
        }
        public void check_cgorder(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.CheckCGOrder(sheet_no, approve_man, update_time);
        }

        public void get_inout_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string supcust_no = w.Read("supcust_no");
            string trans_no = w.Read("trans_no");

            var tb = bll.GetInOutList(date1, date2, supcust_no, trans_no);
            w.Write("data", tb);
        }
        public void get_simple_inout_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string supcust_no = w.Read("supcust_no");
            string trans_no = w.Read("trans_no");

            var tb = bll.GetSimpleInOutList(date1, date2, supcust_no, trans_no);
            w.Write("data", tb);
        }
        public void get_other_inout_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string trans_no = w.Read("trans_no");

            var tb = bll.GetOtherInOutList(date1, date2, trans_no);
            w.Write("data", tb);
        }
        public void get_inout(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string trans_no = w.Read("trans_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetInOut(sheet_no, trans_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void get_other_inout(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetInOut(sheet_no, out tb1, out tb2);
            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void add_inout(WebHelper w, Dictionary<string, object> kv)
        {
            Model.ic_t_inout_store_master ord = w.GetObject<Model.ic_t_inout_store_master>("ord");
            ord.approve_flag = "0";
            ord.display_flag = "1";
            ord.old_no = "";
            ord.approve_date = DateTime.MinValue;
            ord.max_change = 0m; //不确定
            ord.lock_man = "";
            ord.lock_date = DateTime.MinValue;

            List<body.ic_t_inout_store_detail> lines = w.GetList<body.ic_t_inout_store_detail>("lines");
            foreach (var item in lines)
            {
                item.cost_notax = 0m; //不确定
                item.ly_sup_no = "";
                item.ly_rate = 0m;
            }
            string sheet_no = "";
            bll.AddInOut(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change_inout(WebHelper w, Dictionary<string, object> kv)
        {
            Model.ic_t_inout_store_master ord = w.GetObject<Model.ic_t_inout_store_master>("ord");
            ord.approve_flag = "0";
            ord.display_flag = "1";
            ord.old_no = "";
            ord.approve_date = DateTime.MinValue;
            ord.max_change = 0m; //不确定
            ord.lock_man = "";
            ord.lock_date = DateTime.MinValue;

            List<body.ic_t_inout_store_detail> lines = w.GetList<body.ic_t_inout_store_detail>("lines");
            foreach (var item in lines)
            {
                item.cost_notax = 0m; //不确定
                item.ly_sup_no = "";
                item.ly_rate = 0m;
            }
            bll.ChangeInOut(ord, lines);
        }
        public void delete_inout(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteInOut(sheet_no, update_time);
        }
        /// <summary>
        /// 采购入库审核
        /// </summary>
        /// <param name="w"></param>
        /// <param name="kv"></param>
        public void check_inout(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.CheckInOut(sheet_no, approve_man, update_time);
        }

        public void AssAddCG(WebHelper w, Dictionary<string, object> kv)
        {
            Model.ic_t_inout_store_master ord = w.GetObject<Model.ic_t_inout_store_master>();
            ord.approve_flag = "0";
            ord.display_flag = "1";
            ord.old_no = "";
            ord.approve_date = DateTime.MinValue;
            ord.max_change = 0m; //不确定
            ord.lock_man = "";
            ord.lock_date = DateTime.MinValue;

            List<ic_t_inout_store_detail> lines = w.GetList<ic_t_inout_store_detail>("lines");
            foreach (var item in lines)
            {
                item.cost_notax = 0m; //不确定
                item.branch_no_d = "";
                item.ly_sup_no = "";
                item.ly_rate = 0m;
            }
            string sheet_no = "";
            bll.AssAddCG(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void AssGenCG(WebHelper w, Dictionary<string, object> kv)
        {
            string flow_id = w.Read("flow_id");
            string oper_id = w.Read("oper_id");
            bll.AssGenCG(flow_id, oper_id);

        }
        public void AssGenPlanCG(WebHelper w, Dictionary<string, object> kv)
        {
            string flow_id = w.Read("flow_id");
            string oper_id = w.Read("oper_id");
            bll.AssGenPlanCG(flow_id, oper_id);

        }

        public void ReceiveGenCG(WebHelper w, Dictionary<string, object> kv)
        {
            string flow_id = w.Read("flow_id");
            string oper_id = w.Read("oper_id");
            bll.ReceiveGenCG(flow_id, oper_id);

        }

        public void PickingGenSaleSheet(WebHelper w, Dictionary<string, object> kv)
        {
            string flow_id = w.Read("flow_id");
            string oper_id = w.Read("oper_id");
            bll.PickingGenSaleSheet(flow_id, oper_id);

        }

        public void InventoryAdjustment(WebHelper w, Dictionary<string, object> kv)
        {
            string flow_id = w.Read("flow_id");
            string oper_id = w.Read("oper_id");
            bll.InventoryAdjustment(flow_id, oper_id);
        }

        public void GenProcessDetail(WebHelper w, Dictionary<string, object> kv)
        {
            string flowIds = w.Read("flowIds");
            string oper_id = w.Read("oper_id");
            string fee_dept_no = w.Read("fee_dept_no");
            string pro_dept_no = w.Read("pro_dept_no");
            bll.GenProcessDetail(flowIds, oper_id,pro_dept_no,fee_dept_no);
        }
        #region 销售计划
        //获取销售计划上列表
        public void get_salespsheet_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string cust_id = w.Read("sup_no");
            string sale_man = w.Read("order_main");

            DataTable tb = bll.GetSaleSPSheetList(date1, date2, cust_id, sale_man);
            w.Write(tb);
        }
        //获取销售计划下列表
        public void get_salespsheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetSaleSPSheet(sheet_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        //添加销售计划
        public void add_salespsheet(WebHelper w, Dictionary<string, object> kv)
        {
            IvyTran.body.co_t_order_main ord = w.GetObject<IvyTran.body.co_t_order_main>();
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;

            List<IvyTran.body.co_t_order_child> lines = w.GetList<IvyTran.body.co_t_order_child>("lines");

            string sheet_no = "";
            bll.AddSaleSPSheet(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }

        //修改销售计划
        public void change_salespsheet(WebHelper w, Dictionary<string, object> kv)
        {
            IvyTran.body.co_t_order_main ord = w.GetObject<IvyTran.body.co_t_order_main>();
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;

            List<IvyTran.body.co_t_order_child> lines = w.GetList<IvyTran.body.co_t_order_child>("lines");

            bll.ChangeSaleSPSheet(ord, lines);
        }

        //删除销售计划
        public void delete_salespsheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteSaleSPSheet(sheet_no, update_time);
        }

        //审核销售计划
        public void check_salespsheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.CheckSaleSPSheet(sheet_no, approve_man, update_time);
        }

        //获取一周销售计划
        public void get_salespsheet_week(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string cust_id = w.Read("sup_no");
            string sale_man = w.Read("order_main");
            DataTable tb1, tb2;
            bll.GetSaleSPSheetWeek(date1, date2, cust_id, sale_man, out tb1, out tb2);
            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        #endregion
    }
}