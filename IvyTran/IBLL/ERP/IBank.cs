namespace IvyTran.IBLL.ERP
{
    interface IBank
    {
        System.Data.DataTable GetList();
        System.Data.DataTable GetSubjectList();
        System.Data.DataTable GetItem(string visa_id);
        void Add(Model.bi_t_bank_info item);
        void Change(Model.bi_t_bank_info item);
        void Delete(string visa_id);
    }
}
