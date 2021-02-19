using System;
using System.Collections.Generic;
using System.Data;
using IvyBack.Helper;
using IvyBack.IBLL.OnLine;
using Model;

namespace IvyBack.BLL.OnLine
{
    public class Customer : ICustomer
    {
        List<string> ICustomer.GetNewCustomerNo()
        {
            var req = new Request();
            var json = req.request("/OnLine/customer?t=get_new_msg", "");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            var lst = new List<string>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    lst.Add(r.Read("cus_no"));
                }
            }
            return lst;
        }

        void ICustomer.Approve(string cus_no, string status, string oper_id, string cus_level, string supcust_group, string remark)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("cus_no", cus_no);
            write.Append("status", status);
            write.Append("oper_id", oper_id);
            write.Append("cus_level", cus_level);
            write.Append("supcust_group", supcust_group);
            write.Append("remark", remark);
            var json = req.request("/OnLine/customer?t=approve", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        customer ICustomer.GetCustomer(string cus_no)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("cus_no", cus_no);
            var json = req.request("/customer?t=get_customer", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            var cus = new customer();
            cus.cus_no = read.Read("cus_no");
            cus.login_no = read.Read("login_no");
            cus.cus_level = read.Read("cus_level");
            cus.cus_start_date = Conv.ToDateTime(read.Read("cus_start_date"));
            cus.cus_end_date = Conv.ToDateTime(read.Read("cus_end_date"));
            cus.cus_name = read.Read("cus_name");
            cus.cus_tel = read.Read("cus_tel");
            cus.mobile = read.Read("mobile");
            cus.cus_idcard = read.Read("cus_idcard");
            cus.cus_area = read.Read("cus_area");
            cus.contact_address = read.Read("contact_address");
            cus.detail_address = read.Read("detail_address");
            cus.remark = read.Read("remark");
            cus.settle_type = read.Read("settle_type");
            cus.salesman_id = read.Read("salesman_id");
            cus.salesman_name = read.Read("salesman_name");
            cus.img_url = read.Read("img_url");
            cus.status = read.Read("status");
            cus.is_branch = read.Read("is_branch");
            cus.supcust_group = read.Read("supcust_group");
            return cus;
        }

        List<customer> ICustomer.GetCusList(string date1, string date2, string status, string salesman_id, string keyword, int page_size, int page_no, out int total)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("date1", date1);
            write.Append("date2", date2);
            write.Append("status", status);
            write.Append("salesman_id", salesman_id);
            write.Append("keyword", keyword);
            write.Append("page_size", page_size.ToString());
            write.Append("page_no", page_no.ToString());
            var json = req.request("/OnLine/customer?t=get_cus_list", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            total = Conv.ToInt(read.Read("total"));
            //
            var lst = new List<customer>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var cus = new customer();
                    cus.cus_no = r.Read("cus_no");
                    cus.login_no = r.Read("login_no");
                    cus.cus_level = r.Read("cus_level");
                    cus.cus_start_date = Conv.ToDateTime(r.Read("cus_start_date"));
                    cus.cus_end_date = Conv.ToDateTime(r.Read("cus_end_date"));
                    cus.cus_name = r.Read("cus_name");
                    cus.cus_tel = r.Read("cus_tel");
                    cus.mobile = r.Read("mobile");
                    cus.cus_idcard = r.Read("cus_idcard");
                    cus.cus_area = r.Read("cus_area");
                    cus.contact_address = r.Read("contact_address");
                    cus.detail_address = r.Read("detail_address");
                    cus.remark = r.Read("remark");
                    cus.settle_type = r.Read("settle_type");
                    cus.salesman_id = r.Read("salesman_id");
                    cus.salesman_name = r.Read("salesman_name");
                    cus.img_url = r.Read("img_url");
                    cus.create_time = Conv.ToDateTime(r.Read("create_time"));
                    cus.approve_time = Conv.ToDateTime(r.Read("approve_time"));
                    cus.msg_hand = r.Read("msg_hand");
                    cus.status = r.Read("status");
                    cus.is_branch = r.Read("is_branch");
                    cus.supcust_group = r.Read("supcust_group");
                    lst.Add(cus);
                }
            }
            return lst;
        }

        DataTable ICustomer.GetCusDt(string date1, string date2, string status, string salesman_id, string keyword, int page_size, int page_no, out int total)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("date1", date1);
            write.Append("date2", date2);
            write.Append("status", status);
            write.Append("salesman_id", salesman_id);
            write.Append("keyword", keyword);
            write.Append("page_size", page_size.ToString());
            write.Append("page_no", page_no.ToString());
            var json = req.request("/OnLine/customer?t=get_cus_list", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            total = Conv.ToInt(read.Read("total"));

            if (read.Read("datas").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("datas"));

            return tb;
        }


        bool ICustomer.GetFirstNewCustomer(out customer cus, out int un_read_num)
        {
            var req = new Request();
            var json = req.request("/OnLine/customer?t=get_first_new_cus", "");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") == "-8")
            {
                cus = null;
                un_read_num = 0;
                return false;
            }
            else if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            cus = new customer();
            un_read_num = Conv.ToInt(read.Read("un_read_num"));
            cus.cus_no = read.Read("cus_no");
            cus.login_no = read.Read("login_no");
            cus.cus_level = read.Read("cus_level");
            cus.cus_start_date = Conv.ToDateTime(read.Read("cus_start_date"));
            cus.cus_end_date = Conv.ToDateTime(read.Read("cus_end_date"));
            cus.cus_name = read.Read("cus_name");
            cus.cus_tel = read.Read("cus_tel");
            cus.mobile = read.Read("mobile");
            cus.cus_idcard = read.Read("cus_idcard");
            cus.cus_area = read.Read("cus_area");
            cus.contact_address = read.Read("contact_address");
            cus.detail_address = read.Read("detail_address");
            cus.remark = read.Read("remark");
            cus.settle_type = read.Read("settle_type");
            cus.salesman_id = read.Read("salesman_id");
            cus.salesman_name = read.Read("salesman_name");
            cus.img_url = read.Read("img_url");
            cus.status = read.Read("status");
            cus.is_branch = read.Read("is_branch");
            return true;
        }

        void ICustomer.SignRead(string cus_no)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("cus_no", cus_no);
            var json = req.request("/OnLine/customer?t=sign_read", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        List<key_value> ICustomer.GetSalesmanList()
        {
            var req = new Request();
            var json = req.request("/OnLine/customer?t=get_salesman_list", "");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            var lst = new List<key_value>();
            var item = new key_value();
            item.t_key = "";
            item.t_value = "全部";
            lst.Add(item);
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    item = new key_value();
                    item.t_key = r.Read("salesman_id");
                    item.t_value = r.Read("salesman_name");
                    lst.Add(item);
                }
            }
            return lst;
        }

        List<key_value> ICustomer.GetCusGroupList()
        {
            var req = new Request();
            var json = req.request("/OnLine/customer?t=get_group_list", "");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            var lst = new List<key_value>();
            var item = new key_value();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    item = new key_value();
                    item.t_key = r.Read("supcust_groupno");
                    item.t_value = r.Read("supcust_groupname");
                    lst.Add(item);
                }
            }
            return lst;
        }


        void ICustomer.UpdateCustomer(string cus_no, string login_no, string cus_name, string cus_tel, string mobile, string cus_idcard, string cus_area, string contact_addr,
            string detail_addr, string cus_level, string remark, string supcust_group, string salesman_id, string cus_start_date, string cus_end_date, string is_branch)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("cus_no", cus_no);
            write.Append("login_no", login_no);
            write.Append("cus_name", cus_name);
            write.Append("cus_tel", cus_tel);
            write.Append("mobile", mobile);
            write.Append("cus_idcard", cus_idcard);
            write.Append("cus_area", cus_area);
            write.Append("contact_addr", contact_addr);
            write.Append("detail_addr", detail_addr);
            write.Append("cus_level", cus_level);
            write.Append("remark", remark);
            write.Append("salesman_id", salesman_id);
            write.Append("cus_start_date", cus_start_date);
            write.Append("cus_end_date", cus_end_date);
            write.Append("is_branch", is_branch);
            write.Append("supcust_group", supcust_group);
            var json = req.request("/OnLine/customer?t=update_customer", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void ICustomer.AddCustomer(string login_no, string cus_pwd, string cus_name, string cus_tel, string mobile, string cus_idcard, string cus_area, string contact_addr,
            string detail_addr, string cus_level, string remark, string supcust_group, string salesman_id, string cus_start_date, string cus_end_date, string is_branch)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("login_no", login_no);
            write.Append("cus_pwd", cus_pwd);
            write.Append("cus_name", cus_name);
            write.Append("cus_tel", cus_tel);
            write.Append("mobile", mobile);
            write.Append("cus_idcard", cus_idcard);
            write.Append("cus_area", cus_area);
            write.Append("contact_addr", contact_addr);
            write.Append("detail_addr", detail_addr);
            write.Append("cus_level", cus_level);
            write.Append("remark", remark);
            write.Append("salesman_id", salesman_id);
            write.Append("cus_start_date", cus_start_date);
            write.Append("cus_end_date", cus_end_date);
            write.Append("is_branch", is_branch);
            write.Append("supcust_group", supcust_group);
            var json = req.request("/OnLine/customer?t=add_customer", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }


        List<salesman> ICustomer.GetSalesmanList(string keyword)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("keyword", keyword);
            var json = req.request("/OnLine/customer?t=select_salesman_list", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            var lst = new List<salesman>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var item = new salesman();
                    item.salesman_id = r.Read("salesman_id");
                    item.salesman_name = r.Read("salesman_name");
                    item.mobile = r.Read("mobile");
                    item.salesman_level = r.Read("salesman_level");
                    item.salesman_pwd = r.Read("salesman_pwd");
                    item.status = r.Read("status");
                    item.oper_id = r.Read("oper_id");
                    item.create_time = Conv.ToDateTime(r.Read("create_time"));
                    lst.Add(item);
                }
            }
            return lst;
        }

        void ICustomer.GetBalance(string cus_no, out decimal balance, out decimal credit_amt)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("cus_no", cus_no);
            var json = req.request("/OnLine/customer?t=get_balance", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            balance = Conv.ToDecimal(read.Read("balance"));
            credit_amt = Conv.ToDecimal(read.Read("credit_amt"));
        }

        void ICustomer.DeleteCustomer(string cus_no)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("cus_no", cus_no);
            var json = req.request("/OnLine/customer?t=delete_customer", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        DataTable ICustomer.SearchCusBalance(string keyword, string supcust_group)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("keyword", keyword);
            write.Append("supcust_group", supcust_group);
            var json = req.request("/OnLine/customer?t=search_cus_balance", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);

            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }

            if (read.Read("datas").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("datas"));

            return tb;
        }
    }
}