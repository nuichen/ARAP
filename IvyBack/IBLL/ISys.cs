using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyBack.IBLL
{
    public interface ISys
    {
        void add(Model.sys_t_system sys);
        void add(Dictionary<string, Model.sys_t_system> sys_dic);
        void update(Model.sys_t_system sys);
        void updateDic(Dictionary<string, Model.sys_t_system> sys_dic);
        Dictionary<string, Model.sys_t_system> GetAllDic();
        string Read(string sys_var_id);

        DataTable GetSheetNo();

        //机号管理
        void AddJH(Model.netsetup ns);
        void DelJH(Model.netsetup ns);
        DataTable GetJH();

        //权限管理
        List<Model.sa_t_oper_grant> GetAllGrant(Model.sa_t_oper_grant grant);
        Dictionary<string, Model.sa_t_oper_grant> GetAllGrantDic(Model.sa_t_oper_grant grant);

        void SaveGrant(List<Model.sa_t_oper_grant> grant);
    }
}
