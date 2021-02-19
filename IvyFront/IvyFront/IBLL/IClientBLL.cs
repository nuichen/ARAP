using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.IBLL
{
   public interface IClientBLL
    {
       int GetItemCount(out int errId, out string errMsg);
       void DownLoadItem(out int errId, out string errMsg);
       void DownLoadStock(out int errId, out string errMsg);
       void DownLoadItemCls(out int errId, out string errMsg);
       void DownLoadOper(out int errId, out string errMsg);
       int GetSupCusCount(out int errId, out string errMsg);
       void DownLoadSupCus(out int errId, out string errMsg);
       void DownLoadBranch(out int errId, out string errMsg);
       int GetCusPriceCount(string cust_id, string item_no, out int errId, out string errMsg);
       void DownLoadCusPrice(string cust_id, string item_no, out int errId, out string errMsg);
       int GetSupPriceCount(string sup_id, string item_no, out int errId, out string errMsg);
       void DownLoadSupPrice(string sup_id, string item_no, out int errId, out string errMsg);
       void DownLoadSystemPars(out int errId, out string errMsg);
       void DownLoadPayWay(out int errId, out string errMsg);
       void UpLoadSale(out int errId, out string errMsg);
       void UpLoadInOut(out int errId, out string errMsg);
       int GetNoUpLoadSaleCount();
       int GetNoUpLoadInOutCount();
       bool ChangePwd(string branch_no, string oper_id, string pwd, string new_pwd);
       bool CheckConnect();
       decimal GetCusItemPrice(string cust_id, string item_no);
       void GetCusBalance(string cust_id, out decimal balance, out decimal credit_amt);
    }
}
