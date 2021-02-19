using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.IBLL
{
    public interface IPayBLL
    {
        void CreatePrePay(string ori_sheet_no, string pay_type, decimal pay_amt, out string sheet_no, out string prepay_id, out string qrcode_url);
        void QueryPayStatus(string sheet_no, out string status);
        void CancelPay(string sheet_no);
        void MicroPay(string ori_sheet_no, string pay_type, decimal pay_amt, string barcode, out string sheet_no, out string status);
        void Query(string sheet_no, string pay_type, out string status);
    }
}
