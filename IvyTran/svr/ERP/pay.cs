using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class pay : BaseService
    {
        IPayBLL bll = new PayBLL();
        Helper.VerifyHelper vbll = new Helper.VerifyHelper();
        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }

        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);
            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }
            return web.NmJson();
        }

        public void create_prepay(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ori_sheet_no", "mer_key", "jh", "pay_type", "pay_amt", "sign") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            var ori_sheet_no = w.Read("ori_sheet_no");
            string mer_key = w.Read("mer_key");
            string jh = w.Read("jh");
            string pay_type = w.Read("pay_type");
            decimal pay_amt = ObjectToDecimal(kv, "pay_amt");
            string sign = w.Read("sign");
            var sheet_no = "";
            var prepay_id = "";
            var qrcode_url = "";
            var mer_id = "";
            var errMsg = "";
            if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg))
            {
                throw new ExceptionBase(1001, "商家验证失败:" + errMsg);
            }
            string str = "jh=" + jh + "&mer_key=" + mer_key + "&ori_sheet_no=" + ori_sheet_no + "&pay_amt=" + pay_amt.ToString("0.00") + "&pay_type=" + pay_type;
            str = MD5Helper.ToMD5(str);
            if (str.ToLower() != sign.ToLower())
            {
                throw new ExceptionBase(1002, "签名不正确");
            }
            bll.CreatePrePay(ori_sheet_no, mer_id, jh, pay_type, pay_amt, out sheet_no, out prepay_id, out qrcode_url);
            w.Write("sheet_no", sheet_no);
            w.Write("prepay_id", prepay_id);
            w.Write("qrcode_url", qrcode_url);
        }
        public void cancel_prepay(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "mer_key", "sheet_no", "sign") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            var mer_key = w.Read("mer_key");
            var sheet_no =w.Read("sheet_no");
            var sign = w.Read("sign");
            var mer_id = "";
            var errMsg = "";
            if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg))
            {
                throw new ExceptionBase(1001, "商家验证失败:" + errMsg);
            }
            string str = "mer_key=" + mer_key + "&sheet_no=" + sheet_no;
            str = MD5Helper.ToMD5(str);
            if (str.ToLower() != sign.ToLower())
            {
                throw new ExceptionBase(1002, "签名不正确");
            }
            bll.CancelPay(sheet_no);
        }
        public void commit_prepay(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "mer_key", "sheet_no", "sign") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string mer_key = w.Read("mer_key");
            string sheet_no = w.Read("sheet_no");
            string sign = w.Read("sign");
            string mer_id = "";
            string errMsg = "";
            if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg))
            {
                throw new ExceptionBase(1001, "商家验证失败:" + errMsg);
            }
            string str = "mer_key=" + mer_key + "&sheet_no=" + sheet_no;
            str = MD5Helper.ToMD5(str);
            if (str.ToLower() != sign.ToLower())
            {
                throw new ExceptionBase(1002, "签名不正确");
            }
            //
            bll.CommitPay(sheet_no);
        }
        public void query_pay_status(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "mer_key", "sheet_no", "sign") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string mer_key = w.Read("mer_key");
            string sheet_no = w.Read("sheet_no");
            string sign = w.Read("sign");
            string mer_id = "";
            string errMsg = "";
            if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg))
            {
                throw new ExceptionBase(1001, "商家验证失败:" + errMsg);
            }
            string str = "mer_key=" + mer_key + "&sheet_no=" + sheet_no;
            str = MD5Helper.ToMD5(str);
            if (str.ToLower() != sign.ToLower())
            {
                throw new ExceptionBase(1002, "签名不正确");
            }
            var status = bll.QueryPayStatus(sheet_no);
            w.Write("status", status);
        }
        public void close(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "mer_key", "sheet_no", "sign", "pay_type") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string sheet_no = w.Read("sheet_no");
            string mer_key = w.Read("mer_key");
            string pay_type = w.Read("pay_type");
            string sign = w.Read("sign");
            var close_sheet_no = "";
            int errId = 0;
            string mer_id = "";
            string errMsg = "";
            if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg))
            {
                throw new ExceptionBase(1001, "商家验证失败:" + errMsg);
            }
            string str = "mer_key=" + mer_key + "&pay_type=" + pay_type + "&sheet_no=" + sheet_no;
            str = MD5Helper.ToMD5(str);
            if (str.ToLower() != sign.ToLower())
            {
                throw new ExceptionBase(1002, "签名不正确");
            }
            bll.ClosePrePay(sheet_no, mer_id, pay_type, out close_sheet_no, out errId, out errMsg);
            w.Write("sheet_no", close_sheet_no);
        }
        public void micro_pay(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ori_sheet_no", "mer_key", "jh", "pay_type", "pay_amt", "barcode", "sign") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ori_sheet_no = w.Read("ori_sheet_no");
            string mer_key = w.Read("mer_key");
            string jh = w.Read("jh");
            string pay_type = w.Read("pay_type");
            decimal pay_amt = ObjectToDecimal(kv, "pay_amt");
            string sign = w.Read("sign");
            var barcode = w.Read("barcode");
            string sheet_no = "";
            string mer_id = "";
            int errId = 0;
            string errMsg = "";
            if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg))
            {
                throw new ExceptionBase(1001, "商家验证失败:" + errMsg);
            }
            string str = "barcode=" + barcode + "&jh=" + jh + "&mer_key=" + mer_key + "&ori_sheet_no=" + ori_sheet_no + "&pay_amt=" + pay_amt.ToString("0.00") + "&pay_type=" + pay_type;
            str = MD5Helper.ToMD5(str);
            if (str.ToLower() != sign.ToLower())
            {
                throw new ExceptionBase(1002, "签名不正确");
            }
            string status = "0";
            bll.MicroPay(ori_sheet_no, mer_id, jh, pay_type, pay_amt, barcode, out sheet_no, out status, out errId, out errMsg);
            w.Write("sheet_no", sheet_no);
            w.Write("status", status);//支付状态
        }
        public void query(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "mer_key", "sheet_no", "pay_type", "sign") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string mer_key = w.Read("mer_key");
            string sheet_no = w.Read("sheet_no");
            string sign = w.Read("sign");
            string pay_type = w.Read("pay_type");
            string mer_id = "";
            string errMsg = "";
            if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg))
            {
                throw new ExceptionBase(1001, "商家验证失败:" + errMsg);
            }
            string str = "mer_key=" + mer_key + "&pay_type=" + pay_type + "&sheet_no=" + sheet_no;
            str = MD5Helper.ToMD5(str);
            if (str.ToLower() != sign.ToLower())
            {
                throw new ExceptionBase(1002, "签名不正确");
            }
            string status = bll.Query(mer_id, sheet_no, pay_type);//查询支付状态
           w.Write("status", status);
        }
    }
}