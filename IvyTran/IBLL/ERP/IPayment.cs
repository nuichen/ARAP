namespace IvyTran.IBLL.ERP
{
    interface IPayment
    {
        System.Data.DataTable GetList();
        System.Data.DataTable GetItem(string pay_way);
        void Add(Model.bi_t_payment_info  item);
        void Change(Model.bi_t_payment_info item);
        void Delete(string pay_way);
        System.Data.DataTable Getlist();
    }
}
