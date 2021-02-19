using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface ISys
    {
        void add(sys_t_system sys);
        void addDic(Dictionary<string, sys_t_system> sys);
        void update(sys_t_system sys);
        void updatedic(Dictionary<string, sys_t_system> sys);
        DataTable GetAll();
        string Read(string sys_var_id);
        void Write(string parId, string val);

        DataTable GetSheetNo();

        //机号管理
        void AddJH(global::Model.netsetup ns);
        void DelJH(global::Model.netsetup ns);
        DataTable GetJH();

        //权限管理
        DataTable GetAllGrant(global::Model.sa_t_oper_grant grant);
        void SaveGrant(List<global::Model.sa_t_oper_grant> grant);



    }
}
