using System.Data;
using Model;

namespace IvyTran.IBLL.OnLine
{
    public interface ICustomer
    {
        DataTable GetNewMsg();
        void SignRead(string cus_no);
        void GetFirstNewCus(out customer cus, out int un_read_num);
        DataTable GetCusList(string date1, string date2, string status, string salesman_id, string keyword, int page_no, int page_size, out int total);
        customer GetCusInfo(string cus_no);
        void Approve(string cus_no,string status,string oper_id,string cus_level,string settle_type,string remark);
        DataTable GetSalesmanList();
        void UpdateCustomer(string cus_no,string login_no, string cus_name, string cus_tel, string mobile, string cus_idcard, string cus_area, string contact_addr,
            string detail_addr, string cus_level, string remark, string settle_type, string salesman_id, string start_date, string end_date, string is_branch);
        void AddCustomer(string login_no, string cus_pwd, string cus_name, string cus_tel, string mobile, string cus_idcard, string cus_area, string contact_addr,
            string detail_addr, string cus_level, string remark, string settle_type, string salesman_id, string start_date, string end_date, string is_branch);
        DataTable GetSalesmanList(string keyword);
        void GetCusBalance(string cus_no, out decimal balance, out decimal credit_amt);
        void Delete(string cus_no);
        DataTable GetCusGroupList();
        DataTable SearchCusBalanceList(string keyword, string supcust_group);
    }
}