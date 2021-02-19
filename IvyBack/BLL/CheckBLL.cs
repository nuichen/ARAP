using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IvyBack.IBLL;
using Model;
using IvyBack.Helper;
using System.IO;

namespace IvyBack.BLL
{
    public class CheckBLL : ICheckBLL
    {
        DataTable IBLL.ICheckBLL.GetCheckSheetList(DateTime date1, DateTime date2)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/check?t=get_checksheet_list", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                if (r.Read("data").Length < 10)
                {
                    return new DataTable();
                }
                IBLL.ICommonBLL bll = new BLL.CommonBLL();
                var tb = bll.GetDataTable(r.ReadList("data"));

                return tb;
            }
        }

        DataTable IBLL.ICheckBLL.GetCheckInitList(string branch_no)
        {
            Helper.IRequest req = new Helper.Request();
            
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("branch_no", branch_no);
            var json = req.request("/check?t=get_check_init_list", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                if (r.Read("data").Length < 10)
                {
                    return new DataTable();
                }
                IBLL.ICommonBLL bll = new BLL.CommonBLL();
                var tb = bll.GetDataTable(r.ReadList("data"));

                return tb;
            }
        }
        //------------------------------My Add Code-----------------------------------------
        //DataTable IBLL.ICheckBLL.GetCheckInitListByBranch_no(string branch_no)
        //{
        //    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
        //    //w.Append("branch_no", branch_no);
        //    Helper.IRequest req = new Helper.Request();
        //    var json = req.request("/check?t=GetCheckInitListByBranch_no", branch_no);
        //    ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
        //    if (r.Read("errId") != "0")
        //    {
        //        throw new Exception(r.Read("errMsg"));
        //    }
        //    else
        //    {
        //        if (r.Read("data").Length < 10)
        //        {
        //            return new DataTable();
        //        }
        //        IBLL.ICommonBLL bll = new BLL.CommonBLL();
        //        var tb = bll.GetDataTable(r.ReadList("data"));

        //        return tb;
        //    }
        //}
        //-----------------------------------------------------------------------
        DataTable IBLL.ICheckBLL.GetBranchStockList(string branch_no)
        {
            Helper.IRequest req = new Helper.Request();
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("branch_no", branch_no);
            var json = req.request("/check?t=get_branch_stock_list", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                if (r.Read("data").Length < 10)
                {
                    return new DataTable();
                }
                IBLL.ICommonBLL bll = new BLL.CommonBLL();
                var tb = bll.GetDataTable(r.ReadList("data"));

                return tb;
            }
        }

        void IBLL.ICheckBLL.GetCheckSheet(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);

            r.request("/check?t=get_checksheet");

            r.WhetherSuccess();
            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");
        }

        void IBLL.ICheckBLL.AddCheckSheet(Model.ic_t_check_master ord, List<Model.ic_t_check_detail> lines, out string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("branch_no", ord.branch_no);
            w.Append("check_no", ord.check_no);
            w.Append("meno", ord.meno);
            w.Append("oper_id", ord.oper_id);
            w.Append("deal_man", ord.deal_man);
            w.Append("cm_branch", ord.cm_branch);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss"));

            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("item_no");
            tb.Columns.Add("in_price", typeof(decimal));
            tb.Columns.Add("sale_price", typeof(decimal));
            tb.Columns.Add("stock_qty", typeof(decimal));
            tb.Columns.Add("real_qty", typeof(decimal));
            tb.Columns.Add("balance_qty", typeof(decimal));
            tb.Columns.Add("memo");
            tb.Columns.Add("other1");
            tb.Columns.Add("other2");
            tb.Columns.Add("num1", typeof(decimal));
            tb.Columns.Add("num2", typeof(decimal));
            tb.Columns.Add("num3", typeof(decimal));
            tb.Columns.Add("packqty", typeof(int));
            tb.Columns.Add("sgqty", typeof(decimal));
            foreach (Model.ic_t_check_detail line in lines)
            {
                tb.Rows.Add(line.sheet_no, line.item_no, line.in_price, line.sale_price, line.stock_qty, line.real_qty, line.balance_qty,
                    line.memo, line.other1, line.other2, line.num1, line.num2, line.num3, line.packqty, line.sgqty);
            }
            w.Append("lines", tb);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/check?t=add_checksheet", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            sheet_no = r.Read("sheet_no");
        }

        void IBLL.ICheckBLL.ChangeCheckSheet(Model.ic_t_check_master ord, List<Model.ic_t_check_detail> lines)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("branch_no", ord.branch_no);
            w.Append("check_no", ord.check_no);
            w.Append("meno", ord.meno);
            w.Append("oper_id", ord.oper_id);
            w.Append("deal_man", ord.deal_man);
            w.Append("cm_branch", ord.cm_branch);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Append("update_time", ord.update_time.ToString("yyyy-MM-dd HH:mm:ss"));
            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("item_no");
            tb.Columns.Add("in_price", typeof(decimal));
            tb.Columns.Add("sale_price", typeof(decimal));
            tb.Columns.Add("stock_qty", typeof(decimal));
            tb.Columns.Add("real_qty", typeof(decimal));
            tb.Columns.Add("balance_qty", typeof(decimal));
            tb.Columns.Add("memo");
            tb.Columns.Add("other1");
            tb.Columns.Add("other2");
            tb.Columns.Add("num1", typeof(decimal));
            tb.Columns.Add("num2", typeof(decimal));
            tb.Columns.Add("num3", typeof(decimal));
            tb.Columns.Add("packqty", typeof(int));
            tb.Columns.Add("sgqty", typeof(decimal));
            foreach (Model.ic_t_check_detail line in lines)
            {
                tb.Rows.Add(line.sheet_no, line.item_no, line.in_price, line.sale_price, line.stock_qty, line.real_qty, line.balance_qty,
                    line.memo, line.other1, line.other2, line.num1, line.num2, line.num3, line.packqty, line.sgqty);
            }
            w.Append("lines", tb);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/check?t=change_checksheet", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICheckBLL.DeleteCheckSheet(string sheet_no, DateTime update_time)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("update_time", update_time.ToString("yyyy-MM-dd HH:mm:ss"));
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/check?t=delete_checksheet", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }

        }


        void ICheckBLL.AddChectInitSheet(ic_t_check_init check_init, out string sheet_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_init>("check_init", check_init);

            r.request("/check?t=AddChectInitSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            sheet_no = r.Read("sheet_no");
        }

        void ICheckBLL.UpdateChectInitSheet(ic_t_check_init init)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_init>("init", init);

            r.request("/check?t=UpdateChectInitSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        void ICheckBLL.DeleteChectInitSheet(ic_t_check_init init)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_init>("init", init);

            r.request("/check?t=DeleteChectInitSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        ic_t_check_init ICheckBLL.GetCheckInitSheet(ic_t_check_init init)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_init>("init", init);

            r.request("/check?t=GetCheckInitSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            init = r.GetObject<ic_t_check_init>("data");

            return init;
        }

        DataTable ICheckBLL.GetCheckInitSheets(ic_t_check_init init)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_init>("init", init);

            r.request("/check?t=GetCheckInitSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }


        DataTable ICheckBLL.GetCheckBak(ic_t_check_bak bak)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_bak>("bak", bak);

            r.request("/check?t=GetCheckBak");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

        DataTable ICheckBLL.GetCheckItemBak(ic_t_check_bak bak)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_bak>("bak", bak);

            r.request("/check?t=GetCheckItemBak");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

        DataTable ICheckBLL.GetAllCheckItemBak(ic_t_check_bak bak)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_bak>("bak", bak);

            r.request("/check?t=GetAllCheckItemBak");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }


        void ICheckBLL.CheckPDSheet(string sheet_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/check?t=CheckPDSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }
        void ICheckBLL.CheckPCSheet(ic_t_check_init ini)
        {
            JsonRequest r = new JsonRequest();

            r.Write("ini", ini);

            r.request("/check?t=CheckPCSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        DataTable ICheckBLL.GetCheckFinish(ic_t_check_finish finish)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_finish>("finish", finish);

            r.request("/check?t=GetCheckFinish");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

        void ICheckBLL.UpdateCheckFinish(List<ic_t_check_finish> finishs)
        {
            JsonRequest r = new JsonRequest();

            r.Write<ic_t_check_finish>("finishs", finishs);

            r.request("/check?t=UpdateCheckFinish");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }
        public void CreateUnCheckSheet(string sheet_no)
        {
            JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.request("/check?t=CreateUnCheckSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
    }
}
