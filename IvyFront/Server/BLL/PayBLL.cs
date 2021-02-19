using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.IBLL;

namespace Server.BLL
{
    public class PayBLL:IPayBLL
    {
        private string create_sheet_no()
        {
            var s_no = "";
            var dt = DateTime.Now;
            Random rd = new Random();
            s_no = dt.ToString("yyyy").Substring(3) + dt.DayOfYear.ToString().PadLeft(3, '0') + dt.ToString("HHmmss") + rd.Next(9999).ToString().PadLeft(4, '0');

            return s_no;
        }

        //取消支付
        void IPayBLL.CancelPay(string ord_id)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            string sql = "select 1 from ot_pay_record where sheet_no=@sheet_no ";
            var pars = new System.Data.SqlClient.SqlParameter[] 
            {
                new System.Data.SqlClient.SqlParameter("@sheet_no",ord_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count == 0)
            {
                throw new ExceptionBase("不存在支付记录");
            }
            sql = "update ot_pay_record set status='2' where sheet_no=@sheet_no ";
            pars = new System.Data.SqlClient.SqlParameter[] 
            {
                new System.Data.SqlClient.SqlParameter("@sheet_no",ord_id)
            };
            db.ExecuteScalar(sql,pars);
        }

        //提交支付
        void IPayBLL.CommitPay(string ord_id)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            string sql = "select 1 from ot_pay_record where sheet_no=@sheet_no ";
            var pars = new System.Data.SqlClient.SqlParameter[] 
            {
                new System.Data.SqlClient.SqlParameter("@sheet_no",ord_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count == 0)
            {
                throw new ExceptionBase("不存在支付记录");
            }
            sql = "update ot_pay_record set status='1' where sheet_no=@sheet_no ";
            pars = new System.Data.SqlClient.SqlParameter[] 
            {
                new System.Data.SqlClient.SqlParameter("@sheet_no",ord_id)
            };
            db.ExecuteScalar(sql, pars);
        }

        //查询支付状态
        string IPayBLL.QueryPayStatus(string ord_id)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            string sql = "select status from ot_pay_record where sheet_no=@sheet_no ";
            var pars = new System.Data.SqlClient.SqlParameter[] 
            {
                new System.Data.SqlClient.SqlParameter("@sheet_no",ord_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count == 0)
            {
                throw new ExceptionBase("不存在支付记录");
            }
            return dt.Rows[0]["status"].ToString();
        }

        /// <summary>
        /// 结算提交订单
        /// </summary>
        /// <param name="flow_no">收银单号</param>
        /// <param name="mer_id">商家id</param>
        /// <param name="jh">机台</param>
        /// <param name="pay_type">支付方式: W微信支付; Z支付宝支付</param>
        /// <param name="pay_amt">支付金额</param>
        /// <param name="prepay_id">微信预支付id</param>
        /// <param name="qrcode_url">微信/支付宝支付二维码内容</param>
        void IPayBLL.CreatePrePay(string ori_sheet_no, string mer_id, string jh,string pay_type, decimal pay_amt, out string sheet_no, out string prepay_id, out string qrcode_url)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            sheet_no = "";
            prepay_id = "";
            qrcode_url = "";
            try
            {
                sheet_no = mer_id + create_sheet_no();//商家编号+14位单号
                //微信支付生成微信预支付订单
                if (pay_type == "W")
                {
                    try
                    {
                        IBLL.ISysBLL bll = new BLL.SysBLL();
                        body.wxpay acc = bll.GetMerWxpayById(mer_id);
                        if (acc == null)
                        {
                            throw new Exception("未初始化微信商家帐户");
                        }
                        var wxconfig = new Wxpay.WxpayConfig(acc.wx_appid, acc.wx_secret, acc.wx_mcid, acc.wx_paykey);
                        var wxorder = new Wxpay.WxpayOrder(wxconfig);
                        var errMsg = "";
                        var res = wxorder.Pay4Qrcode(Appsetting.pay_notify_url, sheet_no, pay_amt, "commongoods", Appsetting.server_ip, out errMsg, out prepay_id, out qrcode_url);
                        if (!res)
                        {
                            LogHelper.writeLog("PayBLL.CreatePrePay(1)", "微信预付账单异常", acc.wx_appid, acc.wx_secret, acc.wx_mcid, acc.wx_paykey, sheet_no, Appsetting.pay_notify_url, pay_amt.ToString(), Appsetting.server_ip, errMsg, prepay_id, qrcode_url);
                            throw new Exception("微信预付账单生成失败:" + errMsg);
                        }
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
                else if (pay_type == "Z")
                {
                    try
                    {
                        IBLL.ISysBLL bll = new BLL.SysBLL();
                        body.alipay acc = bll.GetMerAlipayById(mer_id);
                        if (acc == null)
                        {
                            throw new Exception("未初始化支付宝商家帐户");
                        }
                        Com.Alipay.AliPay alibll = new Com.Alipay.AliPay();
                        var errMsg = "";
                        var res = alibll.Alipay_PreCreate(acc.app_id, acc.rsa1_private, acc.rsa1,sheet_no, "commongoods", pay_amt, acc.pid, out qrcode_url, out errMsg);
                        if (!res)
                        {
                            throw new Exception("支付宝预付账单生成失败:" + errMsg);
                        }
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
                //
                string sql = "insert into ot_pay_record(sheet_no,mer_id,jh,pay_type,pay_scene,pay_amt,status,create_time,ori_sheet_no)";
                sql += "values(@sheet_no,@mer_id,@jh,@pay_type,@pay_scene,@pay_amt,'0',getdate(),@ori_sheet_no) ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sheet_no",sheet_no),
                    new System.Data.SqlClient.SqlParameter("@ori_sheet_no",ori_sheet_no),
                    new System.Data.SqlClient.SqlParameter("@mer_id",mer_id),
                    new System.Data.SqlClient.SqlParameter("@jh",jh),
                    new System.Data.SqlClient.SqlParameter("@pay_type",pay_type),
                    new System.Data.SqlClient.SqlParameter("@pay_amt",pay_amt),
                    new System.Data.SqlClient.SqlParameter("@pay_scene","pre_pay")
                };

                db.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PayBLL.CreatePrePay()", ex.ToString(), sheet_no, ori_sheet_no, mer_id, pay_type, jh, pay_amt.ToString());
                throw ex;
            }
        }

        void IPayBLL.ClosePrePay(string ord_id,string mer_id,string pay_type,out string sheet_no, out int errId, out string errMsg)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            sheet_no = "";
            errId = 0;
            errMsg = "";
            try
            {
                sheet_no = mer_id + create_sheet_no();
                
                //
                //关闭订单成功后更新数据库预支付单状态
                if (errId == 0)
                {
                    IPayNotify bll = new PayNotify();
                    bll.Fail("", ord_id);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PayBLL.ClosePrePay()", ex.ToString(), ord_id, sheet_no, mer_id);
                throw ex;
            }
        }

        /// <summary>
        /// 查询订单微信支付状态
        /// </summary>
        /// <param name="sheet_no">单号</param>
        string IPayBLL.QueryWxPrePay(string sheet_no, string mer_id, out string errMsg)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            errMsg = "";
            try
            {
                IBLL.ISysBLL bll = new BLL.SysBLL();
                var acc = bll.GetMerWxpayById(mer_id);
                if (acc == null)
                {
                    throw new Exception("未初始化微信商家帐户");
                }
                var wxconfig = new Wxpay.WxpayConfig(acc.wx_appid, acc.wx_secret, acc.wx_mcid, acc.wx_paykey);
                var wxorder = new Wxpay.WxpayOrder(wxconfig);
                var res = wxorder.QueryOrderStatusByOrderId(sheet_no, out errMsg);
                return res;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PayBLL.QueryWxPrePay()", ex.ToString(), sheet_no);
                throw ex;
            }
        }

        /// <summary>
        /// 查询订单支付宝支付状态
        /// </summary>
        /// <param name="sheet_no">单号</param>
        bool IPayBLL.QueryAliPrePay(string sheet_no, string mer_id, out string errMsg)
        {
            errMsg = "";
            try
            {
                IBLL.ISysBLL bll = new BLL.SysBLL();
                body.alipay acc = bll.GetMerAlipayById(mer_id);
                if (acc == null)
                {
                    throw new Exception("未初始化支付宝商家帐户");
                }
                Com.Alipay.AliPay alibll = new Com.Alipay.AliPay();
                var res = alibll.Alipay_Query(acc.app_id, acc.rsa1_private, acc.rsa1, sheet_no, out errMsg);
                return res;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PayBLL.QueryAliPrePay()", ex.ToString(), sheet_no);
                throw ex;
            }
        }

        void IPayBLL.MicroPay(string ori_sheet_no, string mer_id, string jh, string pay_type, decimal pay_amt, string barcode,out string sheet_no,out string status, out int errId, out string errMsg) 
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            sheet_no = "";
            errId = 0;
            errMsg = "";
            status = "0";
            try
            {
                sheet_no = mer_id + create_sheet_no();//商家编号+14位单号
                //微信支付生成微信预支付订单
                if (pay_type == "W")
                {
                    try
                    {
                        Wxpay.MicroPay bll = new Wxpay.MicroPay();
                        int eid = 0;
                        string emsg = "";
                        bll.WXMicroPay(mer_id, sheet_no, pay_amt, barcode, jh, out eid, out emsg);
                        errId = eid;
                        errMsg = emsg;
                        if (eid == 0) {
                            status = "1";
                        }
                        else if (eid == 1)
                        {
                            status = "2";
                            throw new Exception("微信扫码支付失败[" + eid.ToString() + "]:" + errMsg);
                        }
                        else {
                            status = "0";
                        }
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
                else if (pay_type == "Z")
                {
                    try
                    {
                        IBLL.ISysBLL bll = new BLL.SysBLL();
                        body.alipay acc = bll.GetMerAlipayById(mer_id);
                        if (acc == null)
                        {
                            throw new Exception("未初始化支付宝商家帐户");
                        }
                        Com.Alipay.AliPay alibll = new Com.Alipay.AliPay();
                        var emsg = "";
                        var res = alibll.Alipay_BarcodePay(acc.app_id, acc.rsa1_private, acc.rsa1, sheet_no, "commongoods", pay_amt, barcode, acc.pid, out emsg);
                        status = "1";
                        if (!res)
                        {
                            status = "2";
                            errId = -1;
                            errMsg = emsg;
                            throw new Exception("支付宝扫码支付失败:" + emsg);
                        }
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
                //
                string sql = "insert into ot_pay_record(sheet_no,mer_id,jh,pay_type,pay_scene,pay_amt,status,create_time,ori_sheet_no)";
                sql += "values(@sheet_no,@mer_id,@jh,@pay_type,@pay_scene,@pay_amt,@status,getdate(),@ori_sheet_no) ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sheet_no",sheet_no),
                    new System.Data.SqlClient.SqlParameter("@ori_sheet_no",ori_sheet_no),
                    new System.Data.SqlClient.SqlParameter("@mer_id",mer_id),
                    new System.Data.SqlClient.SqlParameter("@jh",jh),
                    new System.Data.SqlClient.SqlParameter("@pay_type",pay_type),
                    new System.Data.SqlClient.SqlParameter("@pay_amt",pay_amt),
                    new System.Data.SqlClient.SqlParameter("@status",status),
                    new System.Data.SqlClient.SqlParameter("@pay_scene","barcode_pay")
                };

                db.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PayBLL.MicroPay()", ex.ToString(), sheet_no, ori_sheet_no, mer_id, pay_type, jh, pay_amt.ToString());
                throw ex;
            }
        }

        string IPayBLL.Query(string mer_id, string sheet_no, string pay_type)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            string status = "0";
            try
            {
                //微信支付生成微信预支付订单
                if (pay_type == "W")
                {
                    try
                    {
                        Wxpay.MicroPay bll = new Wxpay.MicroPay();
                        int eid = 0;
                        string emsg = "";
                        bll.WXMicroPayQuery(mer_id, sheet_no, out eid, out emsg);
                        if (eid == 0)
                        {
                            status = "1";
                        }
                        else if (eid == 1)
                        {
                            status = "2";
                            throw new Exception("微信扫码支付失败[" + eid.ToString() + "]:" + emsg);
                        }
                        else
                        {
                            status = "0";
                        }
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
                else if (pay_type == "Z")
                {
                    try
                    {
                        IBLL.ISysBLL bll = new BLL.SysBLL();
                        body.alipay acc = bll.GetMerAlipayById(mer_id);
                        if (acc == null)
                        {
                            throw new Exception("未初始化支付宝商家帐户");
                        }
                        Com.Alipay.AliPay alibll = new Com.Alipay.AliPay();
                        var emsg = "";
                        var res = alibll.Alipay_Query(acc.app_id, acc.rsa1_private, acc.rsa1, sheet_no, out emsg);
                        
                        if (!res)
                        {
                            throw new Exception("支付宝扫码支付失败:" + emsg);
                        }
                        else 
                        {
                            status = "1";
                        }
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                }
                //
                if (status != "1")
                {
                    string sql = "update ot_pay_record set status=@status where sheet_no=@sheet_no ";
                    var pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@sheet_no",sheet_no),
                        new System.Data.SqlClient.SqlParameter("@status",status)
                    };

                    db.ExecuteScalar(sql, pars);
                }
                return status;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PayBLL.Query()", ex.ToString(), sheet_no, mer_id, pay_type);
                throw ex;
            }
        }
    
    }
}