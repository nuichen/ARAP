using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IvyBack.IBLL;
using Model;
using IvyBack.Helper;
using Model.InOutModel;


namespace IvyBack.BLL
{
    public class InOutBLL : IInOutBLL
    {
        #region 收货明细

        public DataTable GetReceiveOrderDetail(DateTime start_time, DateTime end_time, string item_no, string is_build)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start", start_time);
            r.Write("end", end_time);
            r.Write("item_no", item_no);
            r.Write("is_build", is_build);

            r.request("/inout?t=GetReceiveOrderDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            return r.GetDataTable("dt");
        }
        public void UpdateReceiveOrderDetail(DataTable dt)
        {
            JsonRequest r = new JsonRequest();
            r.Write("dt", dt);
            r.request("/inout?t=UpdateReceiveOrderDetail");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }
        #endregion
        void IBLL.IInOutBLL.GetInOutZTSheetD_D(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_intoutTZ_sheetD_D");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");

        }
        DataTable IBLL.IInOutBLL.GetInOutZTSheet(string sheet_no)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.request("/inout?t=get_inoutTZ_sheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="type">0采购  1销售</param>
        void IBLL.IInOutBLL.DeleteZT(string sheet_no, string type)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);
            r.Write("type",type);
            r.request("/inout?t=get_deleteTZ");

            r.WhetherSuccess();
        }
        void IBLL.IInOutBLL.GetInOutZTSheetD(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_intoutTZ_sheetD");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");

        }
        DataTable IBLL.IInOutBLL.GetInOutZTList(DateTime date1, DateTime date2, string sup_no)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("date1", date1.Toyyyy_MM_dd());
            r.Write("date2", date2.Toyyyy_MM_dd());
            r.Write("cust_no", sup_no);

            r.request("/inout?t=get_inoutTZ_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }
        void IBLL.IInOutBLL.AddInOutZT(List<Model.InOutModel.ic_t_inoutstore_recpay_detail> lr, ic_t_inoutstore_recpay_main main)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("main", main);
            r.Write("lr", lr);
            r.request("/inout?t=add_inoutTZ");

        }
        void IBLL.IInOutBLL.CheckInOutZT(string sheet_no, string approve_man, string update_date)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_date", update_date);
            r.request("/inout?t=check_inoutTZ");
        }
        DataTable IBLL.IInOutBLL.GetSaleSheetJGSheet(DateTime date1,DateTime date2,string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("date1", date1.ToString("yyyy-MM-dd"));
            r.Write("date2", date2.ToString("yyyy-MM-dd"));
            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_salesheetJG_sheet");
            DataTable tb = r.GetDataTable();

            return tb;


        }
        void IBLL.IInOutBLL.GetSaleSheetJGSheetD(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_salesheetJG_sheetD");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");

        }

        DataTable IBLL.IInOutBLL.GetSaleSheetZTSheet(string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_salesheetTZ_sheet");
            DataTable tb = r.GetDataTable();

            return tb;

           
        }
       void IBLL.IInOutBLL.GetSaleSheetZTSheetD(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_salesheetTZ_sheetD");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");

        }
        void IBLL.IInOutBLL.GetSaleSheetZT(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_salesheetTZ");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");
        }
        DataTable IBLL.IInOutBLL.GetSaleSheetZTList(DateTime date1, DateTime date2, string sup_no)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("date1", date1.Toyyyy_MM_dd());
            r.Write("date2", date2.Toyyyy_MM_dd());
            r.Write("cust_no", sup_no);

            r.request("/inout?t=get_salesheetTZ_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }
        void  IBLL.IInOutBLL.AddZT(List<Model.InOutModel.sm_t_salesheet_recpay_detail> lr, sm_t_salesheet_recpay_main main)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("main", main);
            r.Write("lr", lr);
            r.request("/inout?t=add_TZ");

        }
        void IBLL.IInOutBLL.CheckZT(string sheet_no,string approve_man,string update_date)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_date", update_date);
            r.request("/inout?t=check_TZ");
        }
        DataTable IBLL.IInOutBLL.GetSaleSheetList(DateTime date1, DateTime date2, string cust_id, string sale_man)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("cust_id", cust_id);
            w.Append("sale_man", sale_man);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/inout?t=get_salesheet_list", w.ToString());
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
        bool IBLL.IInOutBLL.CgToSS(string sstr,string str)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            Helper.IRequest req = new Helper.Request();
            w.Append("sstr", sstr);
            w.Append("str",str);
            var json = req.request("/inout?t=cg_to_Ss", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                return Convert.ToBoolean(r.Read("b"));
            }
        }
        DataTable IBLL.IInOutBLL.GetSimpleSaleSheetList(DateTime date1, DateTime date2, string cust_id)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("cust_id", cust_id);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/inout?t=get_simple_salesheet_list", w.ToString());
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


        void IBLL.IInOutBLL.GetSaleSheet(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_salesheet");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");
        }

        void IBLL.IInOutBLL.AddSaleSheet(Model.sm_t_salesheet ord, List<Model.sm_t_salesheet_detail> lines, out string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);
            r.Write("lines", lines);

            r.request("/inout?t=add_salesheet");
            r.WhetherSuccess();

            sheet_no = r.Read("sheet_no");
        }

        void IBLL.IInOutBLL.ChangeSaleSheet(Model.sm_t_salesheet ord, List<Model.sm_t_salesheet_detail> lines)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);
            r.Write("lines", lines);

            r.request("/inout?t=change_salesheet");
            r.WhetherSuccess();
        }

        void IBLL.IInOutBLL.DeleteSaleSheet(string sheet_no, DateTime update_time)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("update_time", update_time.ToString("yyyy-MM-dd HH:mm:ss"));
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/inout?t=delete_salesheet", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }

        }
        void IBLL.IInOutBLL.CheckSaleSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/inout?t=check_salesheet");
            r.WhetherSuccess();
        }

        DataTable IInOutBLL.GetImportSSSheet()
        {
            Helper.JsonRequest r = new Helper.JsonRequest();

            r.request("/inout?t=GetImportSSSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }
        DataTable IBLL.IInOutBLL.GetSaleSSSheetList(DateTime date1, DateTime date2, string sup_no, string order_main)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("date1", date1.Toyyyy_MM_dd());
            r.Write("date2", date2.Toyyyy_MM_dd());
            r.Write("sup_no", sup_no);
            r.Write("order_main", order_main);

            r.request("/inout?t=get_salesssheet_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }
        void IBLL.IInOutBLL.GetSaleSSSheet(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_salesssheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");
        }
        void IBLL.IInOutBLL.AddSaleSSSheet(co_t_order_main ord, List<co_t_order_child> lines, string is_gen_cg, out string sheet_no)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write(ord);
            r.Write("lines", lines);
            r.Write("is_gen_cg", is_gen_cg);

            r.request("/inout?t=add_salesssheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            sheet_no = r.Read("sheet_no");
        }
        void IBLL.IInOutBLL.ChangeSaleSSSheet(Model.co_t_order_main ord, List<Model.co_t_order_child> lines)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write(ord);
            r.Write("lines", lines);

            r.request("/inout?t=change_salesssheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void ChangeSaleSSheetGenPC(Dictionary<string, string> dic)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write<string, string>(dic);

            r.request("/inout?t=ChangeSaleSSheetGenPC");
            r.WhetherSuccess();
        }

        void IBLL.IInOutBLL.DeleteSaleSSSheet(string sheet_no, DateTime update_time)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("update_time", update_time);

            r.request("/inout?t=delete_salesssheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }
        void IBLL.IInOutBLL.CheckSaleSSSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/inout?t=check_salesssheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }

        public DataTable GetSaleSSDetailSum(List<string> sheet_nos)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_nos", sheet_nos);

            r.request("/inout?t=GetSaleSSDetailSum");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

        DataTable IInOutBLL.GetSaleSheetExport(string sheet_no, string sup_no, DateTime oper_date)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("sup_no", sup_no);
            r.Write("oper_date", oper_date);

            r.request("/inout?t=GetSaleSheetExport");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

        DataTable IInOutBLL.GetImportCGOrder()
        {
            Helper.JsonRequest r = new Helper.JsonRequest();

            r.request("/inout?t=GetImportCGOrder");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }
        DataTable IInOutBLL.GetCGOrderList(DateTime date1, DateTime date2, string sup_no, string order_main,int type)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("date1", date1.Toyyyy_MM_dd());
            r.Write("date2", date2.Toyyyy_MM_dd());
            r.Write("sup_no", sup_no);
            r.Write("type", type);
            r.Write("order_main", order_main);

            r.request("/inout?t=get_cgorder_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

        public DataTable GetSaleSSSheetListPS(DateTime date1, DateTime date2, string sup_no, string order_main)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("date1", date1.Toyyyy_MM_dd());
            r.Write("date2", date2.Toyyyy_MM_dd());
            r.Write("sup_no", sup_no);
            r.Write("order_main", order_main);

            r.request("/inout?t=GetSaleSSSheetListPS");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

        void IInOutBLL.GetCGOrder(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_cgorder");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");

        }

        void IInOutBLL.AddCGOrder(co_t_order_main ord, List<co_t_order_child> lines, out string sheet_no)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write(ord);
            r.Write("lines", lines);

            r.request("/inout?t=add_cgorder");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            sheet_no = r.Read("sheet_no");
        }

        void IInOutBLL.ChangeCGOrder(co_t_order_main ord, List<co_t_order_child> lines)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write(ord);
            r.Write("lines", lines);

            r.request("/inout?t=change_cgorder");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        void IInOutBLL.DeleteCGOrder(string sheet_no, DateTime update_time)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("update_time", update_time);

            r.request("/inout?t=delete_cgorder");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        void IInOutBLL.CheckCGOrder(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/inout?t=check_cgorder");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }


        DataTable IBLL.IInOutBLL.GetInOutList(DateTime date1, DateTime date2, string supcust_no, string trans_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("supcust_no", supcust_no);
            w.Append("trans_no", trans_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/inout?t=get_inout_list", w.ToString());
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

        DataTable IBLL.IInOutBLL.GetSimpleInOutList(DateTime date1, DateTime date2, string supcust_no, string trans_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("supcust_no", supcust_no);
            w.Append("trans_no", trans_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/inout?t=get_simple_inout_list", w.ToString());
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

        DataTable IBLL.IInOutBLL.GetOtherInOutList(DateTime date1, DateTime date2, string trans_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("trans_no", trans_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/inout?t=get_other_inout_list", w.ToString());
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

        void IBLL.IInOutBLL.GetInOut(string sheet_no, string trans_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);
            r.Write("trans_no", trans_no);

            r.request("/inout?t=get_inout");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");
        }

        void IBLL.IInOutBLL.GetOtherInOut(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_other_inout");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");
        }


        void IBLL.IInOutBLL.AddInOut(Model.ic_t_inout_store_master ord, List<Model.ic_t_inout_store_detail> lines, out string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);
            r.Write("lines", lines);

            r.request("/inout?t=add_inout");
            r.WhetherSuccess();
            sheet_no = r.Read("sheet_no");
        }

        void IBLL.IInOutBLL.ChangeInOut(Model.ic_t_inout_store_master ord, List<Model.ic_t_inout_store_detail> lines)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);
            r.Write("lines", lines);

            r.request("/inout?t=change_inout");
            r.WhetherSuccess();
        }

        void IBLL.IInOutBLL.DeleteInOut(string sheet_no, DateTime update_time)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("update_time", update_time.ToString("yyyy-MM-dd HH:mm:ss"));
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/inout?t=delete_inout", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }

        }

        void IBLL.IInOutBLL.CheckInOut(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/inout?t=check_inout");
            r.WhetherSuccess();
        }

        void IInOutBLL.AssAddCG(ic_t_inout_store_master ord, List<ic_t_inout_store_detail> lines, out string sheet_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write(ord);
            r.Write("lines", lines);

            r.request("/inout?t=AssAddCG");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            sheet_no = r.Read("sheet_no");
        }

        void IInOutBLL.AssGenCG(string flow_id, string oper_id)
        {
            JsonRequest r = new JsonRequest();

            r.Write("flow_id", flow_id);
            r.Write("oper_id", oper_id);

            r.request("/inout?t=AssGenCG");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }

        void IInOutBLL.AssGenPlanCG(string flow_id, string oper_id)
        {
            JsonRequest r = new JsonRequest();

            r.Write("flow_id", flow_id);
            r.Write("oper_id", oper_id);

            r.request("/inout?t=AssGenPlanCG");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }

        void IInOutBLL.ReceiveGenCG(string flow_id, string oper_id)
        {
            JsonRequest r = new JsonRequest();

            r.Write("flow_id", flow_id);
            r.Write("oper_id", oper_id);

            r.request("/inout?t=ReceiveGenCG");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        void IInOutBLL.PickingGenSaleSheet(string flow_id, string oper_id)
        {
            JsonRequest r = new JsonRequest();

            r.Write("flow_id", flow_id);
            r.Write("oper_id", oper_id);

            r.request("/inout?t=PickingGenSaleSheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void InventoryAdjustment(string flow_id, string oper_id)
        {
            JsonRequest r = new JsonRequest();

            r.Write("flow_id", flow_id);
            r.Write("oper_id", oper_id);

            r.request("/inout?t=InventoryAdjustment");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void GenProcessDetail(string flowIds, string oper_id, string pro_dept_no, string fee_dept_no)
        {
            JsonRequest r = new JsonRequest();
            r.Write("flowIds", flowIds);
            r.Write("oper_id", oper_id);
            r.Write("pro_dept_no", pro_dept_no);
            r.Write("fee_dept_no", fee_dept_no);

            r.request("/inout?t=GenProcessDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
        /// <summary>
        /// 添加销售计划
        /// </summary>
        /// <param name="main"></param>
        /// <param name="child"></param>
        /// <returns>sheet_no</returns>
        public static string AddWeekPlan(body. co_t_order_main_ex main, List<body.co_t_order_child_ex> child)
        {
            var req = new JsonRequest();
            req.Write(main);
            req.Write("lines", child);

            req.request("/inout?t=add_salespsheet");

            if (!req.ReadSuccess()) throw new Exception(req.ReadMessage());

            return req.Read("sheet_no");
        }


        /// <summary>
        /// 修改销售计划
        /// </summary>
        /// <param name="main"></param>
        /// <param name="child"></param>
        public static void ModifyWeekPlan(body.co_t_order_main_ex main, List<body.co_t_order_child_ex> child)
        {
            var req = new JsonRequest();
            req.Write(main);
            req.Write("lines", child);

            req.request("/inout?t=change_salespsheet");
            if (!req.ReadSuccess())
                throw new Exception(req.ReadMessage());
        }

        /// <summary>
        /// 获取销售计划
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="sup_no"></param>
        /// <param name="order_main"></param>
        /// <returns></returns>
        public static DataTable GetSalespsheetList(DateTime date1, DateTime date2, string sup_no, string order_main)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("date1", date1.Toyyyy_MM_dd());
            r.Write("date2", date2.Toyyyy_MM_dd());
            r.Write("sup_no", sup_no);
            r.Write("order_main", order_main);

            r.request("/inout?t=get_salespsheet_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

        /// <summary>
        /// 获取销售计划详情
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="tb1"></param>
        /// <param name="tb2"></param>
        public static void GetSalespsheet(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);

            r.request("/inout?t=get_salespsheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");
        }

        /// <summary>
        /// 删除销售计划
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="update_time"></param>
        public static void DeleteSalespsheet(string sheet_no, DateTime update_time)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("update_time", update_time);

            r.request("/inout?t=delete_salespsheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }

        /// <summary>
        /// 审核销售计划
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="approve_man"></param>
        /// <param name="update_time"></param>
        public static void CheckSalespsheet(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/inout?t=check_salespsheet");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }

        /// <summary>
        /// 获取一周销售计划
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="custId"></param>
        /// <param name="orderMan"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        public static List<body.week_response> GetSalespsheetWeek(string date1, string date2, string custId, string orderMan, out DataTable dt1, out DataTable dt2)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("date1", date1);
            r.Write("date2", date2);
            r.Write("sup_no", custId);
            r.Write("order_main", orderMan);

            r.request("/inout?t=get_salespsheet_week");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            dt1 = r.GetDataTable("tb1");
            dt2 = r.GetDataTable("tb2");


            return r.GetList<body.week_response>("tb1");
        }

    }
}
