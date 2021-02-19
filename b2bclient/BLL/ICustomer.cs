using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    interface ICustomer
    {
        List<string> GetNewCustomerNo();
        void Approve(string cus_no, string status, string oper_id, string cus_level, string supcust_group, string remark);
        body.customer GetCustomer(string cus_no);
        List<body.customer> GetCusList(string date1, string date2, string status, string salesman_id, string keyword, int pageSize, int pageIndex, out int total);
        DataTable GetCusDt(string date1, string date2, string status, string salesman_id, string keyword, int page_size, int page_no, out int total);
        bool GetFirstNewCustomer(out body.customer cus, out int un_read_num);
        void SignRead(string cus_no);
        List<body.key_value> GetSalesmanList();
        List<body.key_value> GetCusGroupList();
        void AddCustomer(string login_no,string cus_pwd, string cus_name, string cus_tel, string mobile, string cus_idcard, string cus_area, string contact_addr,
            string detail_addr, string cus_level, string remark, string supcust_group, string salesman_id, string cus_start_date, string cus_end_date, string is_branch);

        void UpdateCustomer(string cus_no, string login_no, string cus_name, string cus_tel, string mobile, string cus_idcard, string cus_area, string contact_addr,
            string detail_addr, string cus_level, string remark, string supcust_group, string salesman_id, string cus_start_date, string cus_end_date, string is_branch);

        List<body.salesman> GetSalesmanList(string keyword);
        void GetBalance(string cus_no, out decimal balance, out decimal credit_amt);
        void DeleteCustomer(string cus_no);

        DataTable SearchCusBalance(string keyword, string supcust_group);
    }
}
