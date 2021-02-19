namespace IvyTran.IBLL.ERP
{
    public interface IPayNotify
    {
        void Success(string pay_no, string ord_id);
        void Fail(string pay_no, string ord_id);
    }
}