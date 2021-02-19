using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using Model;
using IvyBack.Helper;
using System.Data;
using Model.BaseModel;
using Model.PaymentModel;

namespace IvyBack.BLL
{
    public class ARAP_SCPaymentBLL : IARAP_SCPaymentBLL
    {

        public DataTable GetDataTable(List<ReadWriteContext.IReadContext> lst)
        {

            DataTable dt = new DataTable();

            foreach (ReadWriteContext.IReadContext r in lst)
            {
                Dictionary<string, object> dic = r.ToDictionary();

                if (dt.Columns.Count < 1)
                {
                    foreach (string key in dic.Keys)
                    {
                        dt.Columns.Add(key);
                    }
                }

                DataRow dr = dt.NewRow();
                foreach (string key in dic.Keys)
                {
                    int i = dt.Columns.IndexOf(key);
                    dr[i] = dic[key];
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        public Page<ot_supcust_beginbalance> GetSupcustBeginbalance(string region_no, string keyword,string is_cs, Page<ot_supcust_beginbalance> page)
        {
            {
                JsonRequest r = new JsonRequest();

                r.Write("region_no", region_no);
                r.Write("keyword", keyword);
                r.Write("is_cs", is_cs);
                r.Write<ot_supcust_beginbalance>(page);

                r.request("/arap_scpayment?t=get_supcust_beginbalance");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

                page.Tb = r.GetDataTable();
                page.PageCount = r.ReadToInt("total_count");
                return page;
            }
            throw new NotImplementedException();
        }
        public Page<ot_supcust_beginbalance> GetSupcustList(string region_no, string keyword, string is_cs, Page<ot_supcust_beginbalance> page)
        {
            {
                JsonRequest r = new JsonRequest();

                r.Write("region_no", region_no);
                r.Write("keyword", keyword);
                r.Write("is_cs", is_cs);
                r.Write<ot_supcust_beginbalance>(page);

                r.request("/arap_scpayment?t=get_supcust_beginbalance_list");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

                page.Tb = r.GetDataTable();
                page.PageCount = r.ReadToInt("total_count");
                return page;
            }
            throw new NotImplementedException();
        }
        public void SavaSupcustInitial(List<ot_supcust_beginbalance> lr, string is_cs, string oper_id)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("is_cs", is_cs);
            r.Write("oper_id", oper_id);
            r.Write("lr", lr);
            r.request("/arap_scpayment?t=sava_supcust_initial");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        void IBLL.IARAP_SCPaymentBLL.CheckSupcustInitial(string supcust_flag)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();

            Helper.IRequest req = new Helper.Request();
            w.Append("supcust_flag",supcust_flag);
            var json = req.request("/arap_scpayment?t=CheckSupcustInitial", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
        void IBLL.IARAP_SCPaymentBLL.NotCheckSupcustInitial( string supcust_flag)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();

            Helper.IRequest req = new Helper.Request();
            w.Append("supcust_flag", supcust_flag);
            var json = req.request("/arap_scpayment?t=NotCheckSupcustInitial", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
        public DataTable GetAccountFlows(Model.rp_t_accout_payrec_flow flow)
        {
            JsonRequest r = new JsonRequest();

            r.Write<rp_t_accout_payrec_flow>("flow", flow);

            r.request("/arap_scpayment?t=GetAccountFlows");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        System.Data.DataTable IBLL.IARAP_SCPaymentBLL.GetNoticeList(DateTime date1, DateTime date2, string cus_no, string is_cs,string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("cus_no", cus_no);
            w.Append("is_cs", is_cs);
            w.Append("sheet_no", sheet_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/arap_scpayment?t=GetNoticeList", w.ToString());
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
                var tb = GetDataTable(r.ReadList("data"));

                return tb;
            }
        }
        DataTable IBLL.IARAP_SCPaymentBLL.GetCollectionNotice(List<rp_t_account_notice> lr)
        {
             Helper.JsonRequest r = new JsonRequest();
            r.Write("lr", lr);

            r.request("/arap_scpayment?t=GetCollectionNotice");
            r.WhetherSuccess();

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }
        DataTable IARAP_SCPaymentBLL.GetSupcustBeginbalanceModel(string supcust_no)
        {
            try
            {
                JsonRequest r = new JsonRequest();
                r.Write("supcust_no", supcust_no);
                r.request("/arap_scpayment?t=get_supcust_beginbalance_model");
                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
                DataTable dt = r.GetDataTable();
                return dt;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("CommonBLL.GetAllSupList()", ex.ToString(), null);
                throw ex;
            }
        }
        DataTable IARAP_SCPaymentBLL.GetSupcustList(string is_cs)
        {
            try
            {
                JsonRequest r = new JsonRequest();
                r.Write("is_cs", is_cs);
                r.request("/arap_scpayment?t=get_supcust_list");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

                

                //DataTable dt = new DataTable();
                //dt.Columns.Add("supcust_no");
                //dt.Columns.Add("sup_name");
                DataTable dt =r.GetDataTable();
                //if (read.Read("data") != null)
                //{
                //    foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                //    {
                //        dt.Rows.Add(r.Read("supcust_no"), r.Read("sup_name"));
                //    }
                //}
                return dt;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("CommonBLL.GetAllSupList()", ex.ToString(), null);
                throw ex;
            }
        }
        /// <summary>
        /// 保存客户通知单
        /// </summary>
        /// <param name="ord"></param>
        /// <param name="lines"></param>
        /// <param name="sheet_no"></param>
        void IBLL.IARAP_SCPaymentBLL.AddNotice(Model.rp_t_account_notice ord, List<Model.rp_t_account_notice_detail> lines, out string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);
            r.Write("lines", lines);

            r.request("/arap_scpayment?t=add_notice");
            r.WhetherSuccess();
            sheet_no = r.Read("sheet_no");
        }
        /// <summary>
        /// 删除客户通知单
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="update_time"></param>
        void IBLL.IARAP_SCPaymentBLL.DeleteNotice(string sheet_no, DateTime update_time)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("update_time", update_time.ToString("yyyy-MM-dd HH:mm:ss"));
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/arap_scpayment?t=delete_notice", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }

        }
        void IBLL.IARAP_SCPaymentBLL.ChangeNotice(Model.rp_t_account_notice ord, List<Model.rp_t_account_notice_detail> lines,out string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);
            r.Write("lines", lines);


            r.request("/arap_scpayment?t=change_notice");

            r.WhetherSuccess();
            sheet_no = r.Read("sheet_no");
        }
        void IBLL.IARAP_SCPaymentBLL.DeleteInitial(string keyword, string is_cs, DateTime update_time)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("keyword", keyword);
            w.Append("is_cs", is_cs);
            w.Append("update_time", update_time.ToString("yyyy-MM-dd HH:mm:ss"));
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/arap_scpayment?t=delete_initial", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
        /// <summary>
        /// 审核用户账期通知
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="approve_man"></param>
        /// <param name="update_time"></param>
        void IBLL.IARAP_SCPaymentBLL.CheckNotice(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/arap_scpayment?t=check_notice");
            r.WhetherSuccess();
        }
        System.Data.DataTable IBLL.IARAP_SCPaymentBLL.GetArApList(DateTime date1, DateTime date2, string supcust_form, string supcust_to, string sheet_id)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("supcust_form", supcust_form);
            w.Append("supcust_to", supcust_to);
            w.Append("sheet_id", sheet_id);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/arap_scpayment?t=GetArApList", w.ToString());
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
                var tb = GetDataTable(r.ReadList("data"));

                return tb;
            }
        }
        void IBLL.IARAP_SCPaymentBLL.GetArApOrder(string sheet_no , out System.Data.DataTable tb1)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.request("/arap_scpayment?t=GetArApOrder");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
        }
        void IBLL.IARAP_SCPaymentBLL.AddArAp(rp_t_arap_transformation ord, out string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);

            r.request("/arap_scpayment?t=AddArAp");
            r.WhetherSuccess();
            sheet_no = r.Read("sheet_no");
        }
        void IBLL.IARAP_SCPaymentBLL.DeleteArAp(string sheet_no, DateTime update_time)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("update_time", update_time.ToString("yyyy-MM-dd HH:mm:ss"));
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/arap_scpayment?t=DeleteArAp", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
        void IBLL.IARAP_SCPaymentBLL.CheckArAp(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/arap_scpayment?t=CheckArAp");
            r.WhetherSuccess();
        }
        void IBLL.IARAP_SCPaymentBLL.NotCheckArAp(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/arap_scpayment?t=NotCheckArAp");
            r.WhetherSuccess();
        }
        void IBLL.IARAP_SCPaymentBLL.ChangeArAp(rp_t_arap_transformation ord,out string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);

            r.request("/arap_scpayment?t=ChangeArAp");
            r.WhetherSuccess();
            sheet_no = r.Read("sheet_no");
        }
        void IBLL.IARAP_SCPaymentBLL.AddSupcustInitial(string supcust, string is_cs, string oper_id, int type)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("supcust", supcust);
            r.Write("is_cs", is_cs);
            r.Write("type", type);

            r.request("/arap_scpayment?t=add_supcust_initial");
            r.WhetherSuccess();
        }
        List<ot_supcust_beginbalance> IBLL.IARAP_SCPaymentBLL.GetSupcustInfoList(string is_cs)
        {
            try
            {
                JsonRequest r = new JsonRequest();
                r.Write("is_cs", is_cs);
                r.request("/arap_scpayment?t=get_supcust_info_list");
                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
//                List<bi_t_supcust_info> list = r.GetList<bi_t_supcust_info>();

                var dt = r.GetDataTable();
                List<ot_supcust_beginbalance> list =Helper. TableToList<ot_supcust_beginbalance>.ConvertToModel(dt);

                return list;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("CommonBLL.GetAllSupList()", ex.ToString(), null);
                throw ex;
            }
        }
        DataTable IARAP_SCPaymentBLL.GetPaymentList()
        {
            JsonRequest r = new JsonRequest();

            r.request("/arap_scpayment?t=get_payment_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            DataTable tb = r.GetDataTable();
            return tb;
        }
        DataTable IARAP_SCPaymentBLL.GetPaymentList(string sheet_no)
        {
            JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.request("/arap_scpayment?t=get_payment_info");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            DataTable tb = r.GetDataTable();
            return tb;
        }
        DataTable IARAP_SCPaymentBLL.GetRecpayRecordModel(string supcust_no, string is_cs)
        {
            JsonRequest r = new JsonRequest();

           
            r.Write("supcust_no", supcust_no);
            r.Write("is_cs", is_cs);
            r.request("/arap_scpayment?t=get_recpay_record_model");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            DataTable tb = r.GetDataTable();
            return tb;
        }
        System.Data.DataTable IBLL.IARAP_SCPaymentBLL.GetList(DateTime date1, DateTime date2, string cus_no, string is_cs)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("cus_no", cus_no);
            w.Append("is_cs", is_cs);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/arap_scpayment?t=get_list", w.ToString());
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
                var tb = GetDataTable(r.ReadList("data"));

                return tb;
            }
        }
        void IBLL.IARAP_SCPaymentBLL.GetOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("is_cs", is_cs);
            r.request("/arap_scpayment?t=get_order");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");

        }

        void IBLL.IARAP_SCPaymentBLL.SaveAgingGroup(DataTable dt)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();      
            w.Append("dt", dt);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/arap_scpayment?t=SaveAgingGroup", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);

            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        DataTable IBLL.IARAP_SCPaymentBLL.GetAgingGroup(string is_cs)
        {
            JsonRequest r = new JsonRequest();
            r.Write("is_cs", is_cs);
            r.request("/arap_scpayment?t=GetAgingGroup");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            DataTable tb = GetDataTable(r.ReadList("data"));
            return tb;
        }

    }
}
