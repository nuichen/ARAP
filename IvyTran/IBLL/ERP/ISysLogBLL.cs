using Model.SysModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IvyTran.IBLL.ERP
{
    public interface ISysLogBLL
    {
        void WriteSysLog(sys_t_operator_log item);
        DataTable GetSysLog(decimal flow_id);
        DataTable GetSysLogList(string date1, string date2, string log_type, string log_level, string oper_id, string keyword1, string keyword2, int page_no, int page_size, out int total);

        DataTable GetSysLogType(string log_tag);
    }
}
