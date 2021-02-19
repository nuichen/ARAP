using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Server.svr
{
    public class pay:BaseService
    {

        protected override void ProcessRequestPostHandler(string t, Dictionary<string, object> kv, HttpContext context)
        {
            try
            {
                IBLL.IPayBLL bll = new BLL.PayBLL();
                Helper.VerifyHelper vbll = new Helper.VerifyHelper();
                switch (t)
                {
                    case"create_prepay":
                        if (ExistsKeys(kv, "ori_sheet_no", "mer_key", "jh", "pay_type", "pay_amt", "sign") == false)
                        {
                            WriteInvalidParameters(context);
                            break;
                        }
                        var ori_sheet_no = ObjectToString(kv, "ori_sheet_no");
                        string mer_key = ObjectToString(kv, "mer_key");
                        string jh = ObjectToString(kv, "jh");
                        string pay_type = ObjectToString(kv, "pay_type");
                        decimal pay_amt = ObjectToDecimal(kv, "pay_amt");
                        string sign = ObjectToString(kv, "sign");
                        var sheet_no = "";
                        var prepay_id = "";
                        var qrcode_url = "";
                        var mer_id = "";
                        var errMsg = "";
                        if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg)) 
                        {
                            throw new ExceptionBase(1001,"商家验证失败:" + errMsg);
                        }
                        string str = "jh=" + jh + "&mer_key=" + mer_key + "&ori_sheet_no=" + ori_sheet_no + "&pay_amt=" + pay_amt.ToString("0.00") + "&pay_type=" + pay_type;
                        str = MD5Helper.ToMD5(str);
                        if (str.ToLower() != sign.ToLower())
                        {
                            throw new ExceptionBase(1002,"签名不正确");
                        }
                        bll.CreatePrePay(ori_sheet_no, mer_id, jh, pay_type, pay_amt, out sheet_no, out prepay_id, out qrcode_url);
                        WriteContext.Append("errId", "0");
                        WriteContext.Append("errMsg", "");
                        WriteContext.Append("sheet_no", sheet_no);
                        WriteContext.Append("prepay_id", prepay_id);
                        WriteContext.Append("qrcode_url", qrcode_url);
                        Write(context, WriteContext.ToString());
                        break;
                    case "cancel_prepay":
                        if (ExistsKeys(kv, "mer_key", "sheet_no", "sign") == false)
                        {
                            WriteInvalidParameters(context);
                            return;
                        }
                        mer_key = ObjectToString(kv, "mer_key");
                        sheet_no = ObjectToString(kv, "sheet_no");
                        sign = ObjectToString(kv, "sign");
                        mer_id = "";
                        errMsg = "";
                        if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg)) 
                        {
                            throw new ExceptionBase(1001,"商家验证失败:" + errMsg);
                        }
                        str = "mer_key=" + mer_key + "&sheet_no=" + sheet_no;
                        str = MD5Helper.ToMD5(str);
                        if (str.ToLower() != sign.ToLower())
                        {
                            throw new ExceptionBase(1002,"签名不正确");
                        }
                        //
                        bll.CancelPay(sheet_no);
                        //
                        WriteContext.Append("errId", "0");
                        WriteContext.Append("errMsg", "");
                        Write(context, WriteContext.ToString());
                        break;

                    case "commit_prepay":
                        if (ExistsKeys(kv, "mer_key", "sheet_no", "sign") == false)
                        {
                            WriteInvalidParameters(context);
                            return;
                        }
                        mer_key = ObjectToString(kv, "mer_key");
                        sheet_no = ObjectToString(kv, "sheet_no");
                        sign = ObjectToString(kv, "sign");
                        mer_id = "";
                        errMsg = "";
                        if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg)) 
                        {
                            throw new ExceptionBase(1001,"商家验证失败:" + errMsg);
                        }
                        str = "mer_key=" + mer_key + "&sheet_no=" + sheet_no;
                        str = MD5Helper.ToMD5(str);
                        if (str.ToLower() != sign.ToLower())
                        {
                            throw new ExceptionBase(1002,"签名不正确");
                        }
                        //
                        bll.CommitPay(sheet_no);
                        WriteContext.Append("errId", "0");
                        WriteContext.Append("errMsg", "");
                        Write(context, WriteContext.ToString());
                        break;
                    case "query_pay_status":
                        if (ExistsKeys(kv, "mer_key", "sheet_no", "sign") == false)
                        {
                            WriteInvalidParameters(context);
                            return;
                        }
                        mer_key = ObjectToString(kv, "mer_key");
                        sheet_no = ObjectToString(kv, "sheet_no");
                        sign = ObjectToString(kv, "sign");
                        mer_id = "";
                        errMsg = "";
                        if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg)) 
                        {
                            throw new ExceptionBase(1001,"商家验证失败:" + errMsg);
                        }
                        str = "mer_key=" + mer_key + "&sheet_no=" + sheet_no;
                        str = MD5Helper.ToMD5(str);
                        if (str.ToLower() != sign.ToLower())
                        {
                            throw new ExceptionBase(1002,"签名不正确");
                        }
                        var status = bll.QueryPayStatus(sheet_no);
                        //
                        WriteContext.Append("errId", "0");
                        WriteContext.Append("errMsg", "");
                        WriteContext.Append("status", status);
                        Write(context, WriteContext.ToString());
                        break;
                    case "close":
                        if (ExistsKeys(kv, "mer_key", "sheet_no", "sign", "pay_type") == false)
                        {
                            WriteInvalidParameters(context);
                            return;
                        }
                        sheet_no = ObjectToString(kv, "sheet_no");
                        mer_key = ObjectToString(kv, "mer_key");
                        pay_type = ObjectToString(kv, "pay_type");
                        sign = ObjectToString(kv, "sign");
                        var close_sheet_no = "";
                        int errId = 0;
                        mer_id = "";
                        errMsg = "";
                        if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg)) 
                        {
                            throw new ExceptionBase(1001,"商家验证失败:" + errMsg);
                        }
                        str = "mer_key=" + mer_key + "&pay_type=" + pay_type + "&sheet_no=" + sheet_no;
                        str = MD5Helper.ToMD5(str);
                        if (str.ToLower() != sign.ToLower())
                        {
                            throw new ExceptionBase(1002,"签名不正确");
                        }

                        bll.ClosePrePay(sheet_no, mer_id, pay_type,out close_sheet_no, out errId, out errMsg);
                        //
                        WriteContext.Append("errId", errId.ToString());
                        WriteContext.Append("errMsg", errMsg);
                        WriteContext.Append("sheet_no", close_sheet_no);
                        Write(context, WriteContext.ToString());
                        break;
                    case "micro_pay":
                        if (ExistsKeys(kv, "ori_sheet_no", "mer_key", "jh", "pay_type", "pay_amt", "barcode", "sign") == false)
                        {
                            WriteInvalidParameters(context);
                            break;
                        }
                        ori_sheet_no = ObjectToString(kv, "ori_sheet_no");
                        mer_key = ObjectToString(kv, "mer_key");
                        jh = ObjectToString(kv, "jh");
                        pay_type = ObjectToString(kv, "pay_type");
                        pay_amt = ObjectToDecimal(kv, "pay_amt");
                        sign = ObjectToString(kv, "sign");
                        var barcode = ObjectToString(kv, "barcode");
                        sheet_no = "";
                        mer_id = "";
                        errId = 0;
                        errMsg = "";
                        if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg))
                        {
                            throw new ExceptionBase(1001, "商家验证失败:" + errMsg);
                        }
                        str = "barcode=" + barcode + "&jh=" + jh + "&mer_key=" + mer_key + "&ori_sheet_no=" + ori_sheet_no + "&pay_amt=" + pay_amt.ToString("0.00") + "&pay_type=" + pay_type;
                        str = MD5Helper.ToMD5(str);
                        if (str.ToLower() != sign.ToLower())
                        {
                            throw new ExceptionBase(1002, "签名不正确");
                        }
                        status = "0";
                        bll.MicroPay(ori_sheet_no, mer_id, jh, pay_type, pay_amt, barcode, out sheet_no,out status, out errId, out errMsg);
                        WriteContext.Append("errId", errId.ToString());
                        WriteContext.Append("errMsg", errMsg);
                        WriteContext.Append("sheet_no", sheet_no);
                        WriteContext.Append("status", status);//支付状态
                        Write(context, WriteContext.ToString());
                        break;
                    case "query":
                        if (ExistsKeys(kv, "mer_key", "sheet_no", "pay_type", "sign") == false)
                        {
                            WriteInvalidParameters(context);
                            return;
                        }
                        mer_key = ObjectToString(kv, "mer_key");
                        sheet_no = ObjectToString(kv, "sheet_no");
                        sign = ObjectToString(kv, "sign");
                        pay_type = ObjectToString(kv, "pay_type");
                        mer_id = "";
                        errMsg = "";
                        if (!vbll.verify_merchant(mer_key, out mer_id, out errMsg))
                        {
                            throw new ExceptionBase(1001, "商家验证失败:" + errMsg);
                        }
                        str = "mer_key=" + mer_key + "&pay_type=" + pay_type + "&sheet_no=" + sheet_no;
                        str = MD5Helper.ToMD5(str);
                        if (str.ToLower() != sign.ToLower())
                        {
                            throw new ExceptionBase(1002, "签名不正确");
                        }
                        status = bll.Query(mer_id, sheet_no, pay_type);//查询支付状态
                        //
                        WriteContext.Append("errId", "0");
                        WriteContext.Append("errMsg", "");
                        WriteContext.Append("status", status);
                        Write(context, WriteContext.ToString());
                        break;
                }

            }
            catch (Exception e)
            {
                LogHelper.writeLog("pay ->ProcessRequestPostHandler()", e.ToString(), t);
                WriteException(context, e);
            }
        }

    }
}