using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyTran.IBLL.ERP
{
    interface IComplaintBLL
    {
        void AddComPlaint(Model.FinanceModel.rp_t_complaint_flow com_flow);
        void EditComPlaint(Model.FinanceModel.rp_t_complaint_flow com_flow);
        void AddComPlaintType(Model.BaseModel.bi_t_complaint_type com_type);
        void GetComPlaintInfo(string type_id,out System.Data.DataTable dt);
        int GetTypeId();
        void DeleteComplaint(string flow_id);
        System.Data.DataTable GetCustInfo();
        System.Data.DataTable GetPhs();
        System.Data.DataTable GetComplaintType();
        System.Data.DataTable GetComplaintFlow();
    }
}
