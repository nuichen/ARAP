using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Tran.IBLL
{
    public interface ICommon
    {
        int GetItemCount(string sysn_time);

        DataTable GetItemList(string sysn_time);

        DataTable GetItemClsList(string sysn_time);

        DataTable GetBranchStockList(string sysn_time, string branch_no);

        DataTable GetOperList(string sysn_time, string branch_no);

        DataTable GetSupCusList(string sysn_time);

        int GetSupCusCount(string sysn_time);

        DataTable GetCusPriceList(string sysn_time, string cust_id, string item_no);

        int GetCusPriceCount(string sysn_time, string cust_id, string item_no);

        DataTable GetSupPriceList(string sysn_time, string sup_id, string item_no);

        int GetSupPriceCount(string sysn_time, string sup_id, string item_no);

        DataTable GetSystemPars(string sysn_time);

        DataTable GetBranchList(string sysn_time);

        DataTable GetPayWayList();

        void UpdatePwd(string branch_no, string oper_id, string pwd, string new_pwd);

        decimal GetCusItemPrice(string cust_id, string item_no);

        void GetBalance(string cus_no, out decimal balance, out decimal credit_amt);
    }
}
