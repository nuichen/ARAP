namespace IvyTran.IBLL.ERP
{
    public interface IPayBLL
    {
        void CancelPay(string ord_id);
        void CommitPay(string ord_id);
        string QueryPayStatus(string ord_id);
        void CreatePrePay(string ori_sheet_no, string mer_id, string jh, string pay_type, decimal pay_amt, out string sheet_no, out string prepay_id, out string qrcode_url);
        string QueryWxPrePay(string sheet_no, string mer_id, out string errMsg);
        bool QueryAliPrePay(string sheet_no, string mer_id, out string errMsg);
        void ClosePrePay(string ord_id, string mer_id, string pay_type, out string sheet_no, out int errId, out string errMsg);

        void MicroPay(string ori_sheet_no, string mer_id, string jh, string pay_type, decimal pay_amt, string barcode, out string sheet_no, out string status, out int errId, out string errMsg);
        string Query(string mer_id, string sheet_no, string pay_type);
    }
}