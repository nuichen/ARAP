using System.Data;

namespace IvyTran.IBLL.ERP
{
    interface IDep
    {
        System.Data.DataTable GetList();
        DataTable GetListF(string name);
        System.Data.DataTable GetItem(string dep_no);
        string MaxCode(string par_code);
        void Add(Model.bi_t_dept_info  item);
        void Change(Model.bi_t_dept_info  item);
        void Delete(string dep_no);
    }
}
