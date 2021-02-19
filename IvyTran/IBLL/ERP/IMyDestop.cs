using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface IMyDestop
    {
        DataTable GetAll();

        DataTable GetMyLove(string oper_id);
        void AddMyLove(sys_t_oper_mylove mylove);
        void deleteMyLove(sys_t_oper_mylove mylove);
    }
}
