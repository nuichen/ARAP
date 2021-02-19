using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Model.BaseModel;
using Model.FinanceModel;

namespace IvyTran.BLL.ERP
{
    public class ComplaintBLL : IBLL.ERP.IComplaintBLL
    {
        DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
        private static ComplaintBLL comBll = null;
        public static ComplaintBLL GetComplaintBLL()
        {
            if (comBll == null)
            {
                comBll = new ComplaintBLL();
            }
            return comBll;
        }
        public void AddComPlaint(rp_t_complaint_flow com_flow)
        {
            db.Insert(com_flow,"flow_id");
        }

        public void AddComPlaintType(bi_t_complaint_type com_type)
        {
            db.Insert(com_type);
        }

        public void DeleteComplaint(string flow_id)
        {
            string del_sql = "delete from rp_t_complaint_flow where flow_id=" + flow_id;
            db.ExecuteScalar(del_sql, null);
        }

        public void EditComPlaint(rp_t_complaint_flow com_flow)
        {
            db.Update(com_flow, "flow_id","complaint_type,supcust_no,ph_sheet_no,ms_other,oper_id,oper_date,anser,other2,other3");
        }

        public DataTable GetComplaintType()
        {
            string get_sql = "select * from bi_t_complaint_type";

            DataTable dt = db.ExecuteToTable(get_sql,null);
            return dt;
        }

        public DataTable GetCustInfo()
        {
            string get_sql = "select * from bi_t_supcust_info where supcust_flag='C'";

            DataTable dt = db.ExecuteToTable(get_sql, null);
            return dt;
        }

        public DataTable GetPhs()
        {
            string get_sql = "select top 5 * from ic_t_pspc_main";

            DataTable dt = db.ExecuteToTable(get_sql, null);
            return dt;
        }

        public DataTable GetComplaintFlow()
        {
            string get_sql = "select a.*,b.type_name,b.remark,c.sup_name from rp_t_complaint_flow a left join bi_t_complaint_type b on a.complaint_type=b.type_id left join bi_t_supcust_info c on a.supcust_no=c.supcust_no";

            DataTable dt = db.ExecuteToTable(get_sql, null);
            return dt;
        }

        public int GetTypeId()
        {
            string get_max_id_sql = "select isnull(max(type_id),0) as type_id from bi_t_complaint_type";
            DataTable dt = db.ExecuteToTable(get_max_id_sql, null);

            return dt.Rows.Count <= 0 ? 0 : Convert.ToInt32(dt.Rows[0]["type_id"]);
        }

        public void GetComPlaintInfo(string type_id, out DataTable dt)
        {
            string sql = "select a.*,c.type_name,b.sup_name from rp_t_complaint_flow a left join bi_t_supcust_info b on a.supcust_no=b.supcust_no left join bi_t_complaint_type c on a.complaint_type=c.type_id";

            dt = db.ExecuteToTable(sql, null);
        }
    }
}