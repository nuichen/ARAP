using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tran.svr
{
    class common : IServiceBase 
    {
        void IServiceBase.Request(string t, string pars, out string res)
        {
            try
            {
                ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(pars);
                var kv = r.ToDictionary();
                IBLL.ICommon bll = new BLL.Common();
                if (t == "connect_server")
                {
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("is_connect", "1");
                    res = w.ToString();
                    
                }
                else if (t == "get_item_count")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    int count = bll.GetItemCount(sysn_time);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("total_count", count.ToString());
                    res = w.ToString();
                   
                }
                else if (t == "get_item_list")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dt = bll.GetItemList(sysn_time);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("sysn_time", dtime);
                    w.Append("datas", dt);
                    res = w.ToString();
                }
                else if (t == "get_itemcls_list")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dt = bll.GetItemClsList(sysn_time);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("sysn_time", dtime);
                    w.Append("datas", dt);
                    res = w.ToString();
                }
                else if (t == "get_branch_list")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dt = bll.GetBranchList(sysn_time);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("sysn_time", dtime);
                    w.Append("datas", dt);
                    res = w.ToString();
                }
                else if (t == "get_stock_list")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time", "branch_no") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    string branch_no = r.Read("branch_no");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dt = bll.GetBranchStockList(sysn_time, branch_no);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("sysn_time", dtime);
                    w.Append("datas", dt);
                    res = w.ToString();
                }
                else if (t == "get_oper_list")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time", "branch_no") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    string branch_no = r.Read("branch_no");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dt = bll.GetOperList(sysn_time, branch_no);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("sysn_time", dtime);
                    w.Append("datas", dt);
                    res = w.ToString();
                }
                else if (t == "get_sup_count")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    int count = bll.GetSupCusCount(sysn_time);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("total_count", count.ToString());
                    res = w.ToString();

                }
                else if (t == "get_sup_list")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dt = bll.GetSupCusList(sysn_time);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("sysn_time", dtime);
                    w.Append("datas", dt);
                    res = w.ToString();

                }
                else if (t == "get_cus_price_count")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time", "cust_id", "item_no") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    var cust_id = r.Read("cust_id");
                    var item_no = r.Read("item_no");
                    int count = bll.GetCusPriceCount(sysn_time, cust_id, item_no);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("total_count", count.ToString());
                    res = w.ToString();

                }
                else if (t == "get_cus_price_list")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time", "cust_id", "item_no") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    var cust_id = r.Read("cust_id");
                    var item_no = r.Read("item_no");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dt = bll.GetCusPriceList(sysn_time, cust_id, item_no);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("sysn_time", dtime);
                    w.Append("datas", dt);
                    res = w.ToString();

                }
                else if (t == "get_sup_price_count")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time", "sup_id", "item_no") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    string sup_id = r.Read("sup_id");
                    string item_no = r.Read("item_no");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    int count = bll.GetSupPriceCount(sysn_time, sup_id, item_no);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("total_count", count.ToString());
                    res = w.ToString();

                }
                else if (t == "get_sup_price_list")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time", "sup_id", "item_no") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    string sup_id = r.Read("sup_id");
                    string item_no = r.Read("item_no");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dt = bll.GetSupPriceList(sysn_time, sup_id, item_no);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("sysn_time", dtime);
                    w.Append("datas", dt);
                    res = w.ToString();

                }
                else if (t == "get_system_pars")
                {
                    if (CommonHelper.ExistsKeys(kv, "sysn_time") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string sysn_time = r.Read("sysn_time");
                    var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var dt = bll.GetSystemPars(sysn_time);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("sysn_time", dtime);
                    w.Append("datas", dt);
                    res = w.ToString();

                }
                else if (t == "get_payway_list")
                {
                    var dt = bll.GetPayWayList();
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("datas", dt);
                    res = w.ToString();

                }
                else if(t == "update_pwd")
                {
                    if (CommonHelper.ExistsKeys(kv, "branch_no", "oper_id", "pwd", "new_pwd") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string branch_no = r.Read("branch_no");
                    string oper_id = r.Read("oper_id");
                    string pwd = r.Read("pwd").ToLower();//加密
                    string new_pwd = r.Read("new_pwd").ToLower();//加密
                    bll.UpdatePwd(branch_no, oper_id, pwd, new_pwd);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    res = w.ToString();
                }
                else if (t == "get_cus_item_price")
                {
                    if (CommonHelper.ExistsKeys(kv, "cust_id", "item_no") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string cust_id = r.Read("cust_id");
                    string item_no = r.Read("item_no");
                    var price = bll.GetCusItemPrice(cust_id, item_no);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("price", price.ToString());
                    res = w.ToString();
                }
                else if (t == "get_cus_balance")
                {
                    if (CommonHelper.ExistsKeys(kv, "cust_id") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    string cust_id = r.Read("cust_id");
                    decimal balance = 0;
                    decimal credit_amt = 0;
                    bll.GetBalance(cust_id, out balance, out credit_amt);
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    w.Append("balance", balance.ToString());
                    w.Append("credit_amt", credit_amt.ToString());
                    res = w.ToString();
                }
                else
                {
                    throw new Exception("未找到方法:" + t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("common()", ex.ToString(), t, pars);
                ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                w.Append("errId", "-1");
                w.Append("errMsg", ex.Message);
                res = w.ToString();
            }
        }
    }
}
