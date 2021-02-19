using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class check : BaseService
    {
        ICheckBLL bll = new CheckBLL();
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

        public void get_checksheet_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");

            var tb = bll.GetCheckSheetList(date1, date2);
            w.Write(tb);
        }
        public void get_check_init_list(WebHelper w, Dictionary<string, object> kv)
        {
            string branch_no = w.Read("branch_no");
            var tb = bll.GetCheckInitList(branch_no);
            w.Write(tb);
        }
        public void get_branch_stock_list(WebHelper w, Dictionary<string, object> kv)
        {
            string branch_no = w.Read("branch_no");
            var tb = bll.GetBranchStockList(branch_no);
            w.Write(tb);
        }
        public void get_checksheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetCheckSheet(sheet_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void add_checksheet(WebHelper w, Dictionary<string, object> kv)
        {
            Model.ic_t_check_master ord = w.GetObject<Model.ic_t_check_master>();
            ord.approve_flag = "0";
            ord.max_change = 0;

            List<Model.ic_t_check_detail> lines = w.GetList<Model.ic_t_check_detail>("lines");

            string sheet_no = "";
            bll.AddCheckSheet(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change_checksheet(WebHelper w, Dictionary<string, object> kv)
        {
            Model.ic_t_check_master ord = w.GetObject<Model.ic_t_check_master>();
            ord.approve_flag = "0";
            ord.max_change = 0;

            List<Model.ic_t_check_detail> lines = w.GetList<Model.ic_t_check_detail>("lines");

            bll.ChangeCheckSheet(ord, lines);
        }
        public void delete_checksheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteCheckSheet(sheet_no, update_time);
        }
        public void CheckPDSheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            bll.CheckPDSheet(sheet_no);
        }
        public void CheckPCSheet(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_check_init init = w.GetObject<ic_t_check_init>("ini");
            bll.CheckPCSheet(init);
        }
        public void AddChectInitSheet(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_check_init init = w.GetObject<ic_t_check_init>("check_init");
            string sheet_no = "";
            bll.AddChectInitSheet(init, out sheet_no);

            w.Write("sheet_no", sheet_no);

        }
        public void UpdateChectInitSheet(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_check_init init = w.GetObject<ic_t_check_init>("init");
            bll.UpdateChectInitSheet(init);
        }
        public void DeleteChectInitSheet(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_check_init init = w.GetObject<ic_t_check_init>("init");
            bll.DeleteChectInitSheet(init);
        }
        public void GetCheckInitSheet(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_check_init init = w.GetObject<ic_t_check_init>("init");
            DataTable tb = bll.GetCheckInitSheet(init);

            w.Write(tb);
        }
        public void GetCheckBak(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_check_bak bak = w.GetObject<ic_t_check_bak>("bak");
            DataTable tb = bll.GetCheckBak(bak);

            w.Write(tb);
        }
        public void GetCheckItemBak(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_check_bak bak = w.GetObject<ic_t_check_bak>("bak");
            DataTable tb = bll.GetCheckItemBak(bak);

            w.Write(tb);
        }
        public void GetCheckFinish(WebHelper w, Dictionary<string, object> kv)
        {
            ic_t_check_finish finish = w.GetObject<ic_t_check_finish>("finish");
            DataTable tb = bll.GetCheckFinish(finish);

            w.Write(tb);
        }
        public void UpdateCheckFinish(WebHelper w, Dictionary<string, object> kv)
        {
            List<ic_t_check_finish> finishs = w.GetList<ic_t_check_finish>("finishs");
            bll.UpdateCheckFinish(finishs);
        }
        public void CreateUnCheckSheet(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            bll.CreateUnCheckSheet(sheet_no);
        }
    }
}