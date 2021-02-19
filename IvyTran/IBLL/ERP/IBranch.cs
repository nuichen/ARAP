using System.Data;

namespace IvyTran.IBLL.ERP
{
    interface IBranch
    {
        DataTable GetBranch(string branch_no, string batch_no, string item_no, int type, int? is_message);
        DataTable GetBranchStock(string item_no, string batch_no);
        System.Data.DataTable GetList();
        System.Data.DataTable GetListByParCode(string par_code);
        System.Data.DataTable GetItem(string branch_no);
        string MaxCode(string par_code);
        void Add(Model.bi_t_branch_info item);
        void Change(Model.bi_t_branch_info item);
        void Delete(string branch_no);

        DataTable QuickSearchList(string keyword);
    }
}
