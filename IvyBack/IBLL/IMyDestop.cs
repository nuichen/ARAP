using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace IvyBack.IBLL
{
    public interface IMyDestop
    {
        DataTable GetAll();
        Dictionary<string, sys_t_oper_type> GetDic();
        List<sys_t_oper_type> GetOperTypeList();
        List<sys_t_oper_mylove> GetMyLove(string oper_id);
        void AddMyLove(sys_t_oper_mylove mylove);
        void deleteMyLove(sys_t_oper_mylove mylove);
    }
}
