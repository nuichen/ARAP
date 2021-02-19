using System.Data;

namespace IvyTran.IBLL.ERP
{
    interface IPeople
    {
        System.Data.DataTable GetList(string dep_no, string keyword, int show_stop, int page_index, int page_size, out int total_count);
        System.Data.DataTable GetItem(string oper_id);
        string MaxCode();
        void Add(Model.bi_t_people_info item);
        void Change(Model.bi_t_people_info item);
        void Delete(string oper_id);

        DataTable QuickSearchList(string dept_no, string keyword);
    }
}
