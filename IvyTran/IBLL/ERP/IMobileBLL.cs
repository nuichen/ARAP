using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace IvyTran.IBLL.ERP
{
    public interface IMobileBLL
    {
        DataTable SearchMobileOperList(string oper_cls, string func_id, string keyword, string is_show_stop);
        DataTable GetFuncList();
        DataTable GetOperCls();
        DataTable GetSupcustList(string supcust_flag, string keyword);
        DataTable GetItemCls();
        string SaveMobileOper(Model.SysModel.sa_t_mobile_oper item);
        void SaveMobileOperDataMain(string oper_id, List<Model.SysModel.sa_t_mobile_data_main> lst);
        void SaveMobileOperDataDetail(string flow_id, string item_clsnos);
        DataTable GetOperDataMain(string oper_id);
        DataTable GetOperDataDetail(string flow_id);
        void DeleteMobileOper(string oper_id);
    }
}