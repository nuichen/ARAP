using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyFront.IBLL;

namespace IvyFront.BLL
{
    public class PayBLL : IBLL.IPayBLL
    {
        /// <summary>
        /// 预支付
        /// </summary>
        /// <param name="ori_sheet_no"></param>
        /// <param name="pay_type"></param>
        /// <param name="pay_amt"></param>
        /// <param name="sheet_no"></param>
        /// <param name="prepay_id"></param>
        /// <param name="qrcode_url"></param>
        void IPayBLL.CreatePrePay(string ori_sheet_no, string pay_type, decimal pay_amt, out string sheet_no, out string prepay_id, out string qrcode_url)
        {
            sheet_no = "";
            prepay_id = "";
            qrcode_url = "";
            try
            {
                var req = new Request();
                string sign = "jh=" + Program.jh + "&mer_key=" + Appsetting.mer_key + "&ori_sheet_no=" + ori_sheet_no + "&pay_amt=" + pay_amt.ToString("0.00") + "&pay_type=" + pay_type;
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("ori_sheet_no", ori_sheet_no);
                write.Append("mer_key", Appsetting.mer_key);
                write.Append("jh", Program.jh);
                write.Append("pay_type", pay_type);
                write.Append("pay_amt", pay_amt.ToString());
                write.Append("sign", MD5Helper.ToMD5(sign));
                var json = req.request("/pay?t=create_prepay", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                sheet_no = read.Read("sheet_no");
                prepay_id = read.Read("prepay_id");
                qrcode_url = read.Read("qrcode_url");
            }
            catch (Exception ex)
            {
                Log.writeLog("PayBLL ->CreatePrePay()", ex.ToString(), ori_sheet_no);
                throw ex;
            }
        }

        //查询常春藤支付服务器微信支付状态（主要用于预支付）
        void IPayBLL.QueryPayStatus(string sheet_no, out string status)
        {
            status = "";
            try
            {
                var req = new Request();
                string sign = "mer_key=" + Appsetting.mer_key + "&sheet_no=" + sheet_no;
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sheet_no", sheet_no);
                write.Append("mer_key", Appsetting.mer_key);
                write.Append("sign", MD5Helper.ToMD5(sign));
                var json = req.request("/pay?t=query_pay_status", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                status = read.Read("status");
            }
            catch (Exception ex)
            {
                Log.writeLog("PayBLL ->QueryPayStatus()", ex.ToString(), sheet_no);
                throw ex;
            }
        }

        //取消常春藤支付服务器微信支付状态（主要用于预支付）
        void IPayBLL.CancelPay(string sheet_no)
        {
            try
            {
                var req = new Request();
                string sign = "mer_key=" + Appsetting.mer_key + "&sheet_no=" + sheet_no;
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sheet_no", sheet_no);
                write.Append("mer_key", Appsetting.mer_key);
                write.Append("sign", MD5Helper.ToMD5(sign));
                var json = req.request("/pay?t=cancel_pay", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("PayBLL ->CancelPay()", ex.ToString(), sheet_no);
                throw ex;
            }
        }

        /// <summary>
        /// 微信/支付宝扫码支付
        /// </summary>
        /// <param name="ori_sheet_no"></param>
        /// <param name="pay_type"></param>
        /// <param name="pay_amt"></param>
        /// <param name="barcode"></param>
        /// <param name="sheet_no">支付单号</param>
        /// <param name="status">支付状态</param>
        void IPayBLL.MicroPay(string ori_sheet_no, string pay_type, decimal pay_amt, string barcode, out string sheet_no, out string status)
        {
            sheet_no = "";
            status = "0";
            try
            {
                var req = new Request();
                string sign = "barcode=" + barcode + "&jh=" + Program.jh + "&mer_key=" + Appsetting.mer_key + "&ori_sheet_no=" + ori_sheet_no + "&pay_amt=" + pay_amt.ToString("0.00") + "&pay_type=" + pay_type;
               
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("ori_sheet_no", ori_sheet_no);
                write.Append("mer_key", Appsetting.mer_key);
                write.Append("jh", Program.jh);
                write.Append("pay_type", pay_type);
                write.Append("pay_amt", pay_amt.ToString());
                write.Append("barcode", barcode);
                write.Append("sign", MD5Helper.ToMD5(sign));
                var json = req.request("/pay?t=micro_pay", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                sheet_no = read.Read("sheet_no");
                status = read.Read("status");
            }
            catch (Exception ex)
            {
                Log.writeLog("PayBLL ->MicroPay()", ex.ToString(), ori_sheet_no);
                throw ex;
            }
        }

        /// <summary>
        /// 查询微信/支付宝服务器账单支付状态
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="status"></param>
        void IPayBLL.Query(string sheet_no, string pay_type, out string status)
        {
            status = "";
            try
            {
                var req = new Request();
                string sign = "mer_key=" + Appsetting.mer_key + "&pay_type=" + pay_type + "&sheet_no=" + sheet_no;
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sheet_no", sheet_no);
                write.Append("pay_type", pay_type);
                write.Append("mer_key", Appsetting.mer_key);
                write.Append("sign", MD5Helper.ToMD5(sign));
                var json = req.request("/pay?t=query", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                status = read.Read("status");
            }
            catch (Exception ex)
            {
                Log.writeLog("PayBLL ->Query()", ex.ToString(), sheet_no);
                throw ex;
            }
        }

    }
}
