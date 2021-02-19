using System;
using System.Collections.Generic;
using System.Data;
using IvyBack.Helper;
using IvyBack.IBLL.OnLine;
using Model;

namespace IvyBack.BLL.OnLine
{
    public class Order : IOrder
    {
        List<wm_order> IOrder.GetOrderNew(string key, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();

            var json = req.request("/OnLine/pager?t=get_data", "{\"key\":\"" + key + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }

            //
            total = Conv.ToInt(read.Read("total"));
            //
            var lst = new List<wm_order>();
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    var item = new wm_order();
                    lst.Add(item);
                    item.ord_id = r.Read("ord_id");
                    item.create_time = r.Read("create_time");
                    item.mobile = r.Read("mobile");
                    item.sname = r.Read("sname");
                    item.qty = r.Read("qty");
                    item.amount = r.Read("amount");
                    item.enable_qty = r.Read("enable_qty");
                    item.enable_amount = r.Read("enable_amount");
                    item.reach_time = r.Read("reach_time");
                    item.status = r.Read("status");
                    item.build_status = r.Read("build_status");
                    item.send_status = r.Read("send_status");
                    item.pay_type = r.Read("pay_type");
                    item.cus_no = r.Read("cus_no");
                    item.salesman_id = r.Read("salesman_id");
                    item.is_pay = r.Read("is_pay");
                    item.discount_amt = Conv.ToDecimal(r.Read("discount_amt"));
                }

            }
            return lst;
        }

        DataTable IOrder.GetOrderNewDt(string key, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();

            var json = req.request("/OnLine/pager?t=get_data", "{\"key\":\"" + key + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }

            //
            total = Conv.ToInt(read.Read("total"));
            if (read.Read("data").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("data"));

            return tb;
        }

        List<wm_order> IOrder.GetOrderPass(string key, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/pager?t=get_data", "{\"key\":\"" + key + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var lst = new List<wm_order>();
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    var item = new wm_order();
                    lst.Add(item);
                    item.ord_id = r.Read("ord_id");
                    item.create_time = r.Read("create_time");
                    item.mobile = r.Read("mobile");
                    item.sname = r.Read("sname");
                    item.qty = r.Read("qty");
                    item.amount = r.Read("amount");
                    item.enable_qty = r.Read("enable_qty");
                    item.enable_amount = r.Read("enable_amount");
                    item.reach_time = r.Read("reach_time");
                    item.status = r.Read("status");
                    item.build_status = r.Read("build_status");
                    item.send_status = r.Read("send_status");
                    item.pay_type = r.Read("pay_type");
                    item.cus_no = r.Read("cus_no");
                    item.salesman_id = r.Read("salesman_id");
                    item.is_pay = r.Read("is_pay");
                    item.discount_amt = Conv.ToDecimal(r.Read("discount_amt"));
                }

            }
            return lst;
        }

        DataTable IOrder.GetOrderPassDt(string key, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/pager?t=get_data", "{\"key\":\"" + key + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            if (read.Read("data").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("data"));

            return tb;
        }

        List<wm_order> IOrder.GetOrderDisable(string key, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/pager?t=get_data", "{\"key\":\"" + key + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var lst = new List<wm_order>();
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    var item = new wm_order();
                    lst.Add(item);
                    item.ord_id = r.Read("ord_id");
                    item.create_time = r.Read("create_time");
                    item.mobile = r.Read("mobile");
                    item.sname = r.Read("sname");
                    item.qty = r.Read("qty");
                    item.amount = r.Read("amount");
                    item.enable_qty = r.Read("enable_qty");
                    item.enable_amount = r.Read("enable_amount");
                    item.reach_time = r.Read("reach_time");
                    item.status = r.Read("status");
                    item.build_status = r.Read("build_status");
                    item.send_status = r.Read("send_status");
                    item.pay_type = r.Read("pay_type");
                    item.cus_no = r.Read("cus_no");
                    item.salesman_id = r.Read("salesman_id");
                    item.is_pay = r.Read("is_pay");
                    item.discount_amt = Conv.ToDecimal(r.Read("discount_amt"));
                }

            }
            return lst;
        }

        DataTable IOrder.GetOrderDisableDt(string key, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/pager?t=get_data", "{\"key\":\"" + key + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            if (read.Read("data").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("data"));

            return tb;
        }

        List<wm_order> IOrder.GetOrderComplete(string key, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/pager?t=get_data", "{\"key\":\"" + key + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var lst = new List<wm_order>();
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    var item = new wm_order();
                    lst.Add(item);
                    item.ord_id = r.Read("ord_id");
                    item.create_time = r.Read("create_time");
                    item.mobile = r.Read("mobile");
                    item.sname = r.Read("sname");
                    item.qty = r.Read("qty");
                    item.amount = r.Read("amount");
                    item.enable_qty = r.Read("enable_qty");
                    item.enable_amount = r.Read("enable_amount");
                    item.reach_time = r.Read("reach_time");
                    item.status = r.Read("status");
                    item.build_status = r.Read("build_status");
                    item.send_status = r.Read("send_status");
                    item.pay_type = r.Read("pay_type");
                    item.cus_no = r.Read("cus_no");
                    item.salesman_id = r.Read("salesman_id");
                    item.is_pay = r.Read("is_pay");
                    item.discount_amt = Conv.ToDecimal(r.Read("discount_amt"));
                }

            }
            return lst;
        }

        DataTable IOrder.GetOrderCompleteDt(string key, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/pager?t=get_data", "{\"key\":\"" + key + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            if (read.Read("data").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("data"));

            return tb;
        }


        void IOrder.Pass(string ord_id)
        {
            JsonRequest r = new JsonRequest();
            r.Write("ord_id", ord_id);

            r.request("/OnLine/order?t=pass");

            r.WhetherSuccess();
        }

        void IOrder.Preview(string ord_id)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=preview", "{\"ord_id\":\"" + ord_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IOrder.Disable(string ord_id)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=disable", "{\"ord_id\":\"" + ord_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IOrder.GetOrder(string ord_id, out wm_order ord, out List<wm_order_item> lines, out int un_read_num)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=get_order", "{\"ord_id\":\"" + ord_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            ord = new wm_order();
            un_read_num = Conv.ToInt(read.Read("un_read_num"));
            ord.ord_id = read.Read("ord_id");
            ord.create_time = read.Read("create_time");
            ord.mobile = read.Read("mobile");
            ord.mobile_is_new = read.Read("mobile_is_new");
            ord.sname = read.Read("sname");
            ord.sex = read.Read("sex");
            ord.address = read.Read("address");
            ord.qty = read.Read("qty");
            ord.amount = read.Read("amount");
            ord.enable_qty = read.Read("enable_qty");
            ord.enable_amount = read.Read("enable_amount");
            ord.status = read.Read("status");
            ord.build_status = read.Read("build_status");
            ord.send_status = read.Read("send_status");
            ord.pay_type = read.Read("pay_type");
            ord.reach_time = read.Read("reach_time");
            ord.cus_remark = read.Read("cus_remark");
            ord.cus_no = read.Read("cus_no");
            ord.salesman_id = read.Read("salesman_id");
            ord.is_pay = read.Read("is_pay");
            ord.take_fee = Conv.ToDecimal(read.Read("take_fee"));
            ord.discount_amt = Conv.ToDecimal(read.Read("discount_amt"));
            //
            lines = new List<wm_order_item>();
            if (read.Read("lines") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("lines"))
                {
                    var line = new wm_order_item();
                    lines.Add(line);
                    line.ord_id = r.Read("ord_id");
                    line.row_index = r.Read("row_index");
                    line.goods_id = r.Read("goods_id");
                    line.goods_no = r.Read("goods_no");
                    line.goods_name = r.Read("goods_name");
                    line.price = r.Read("price");
                    line.qty = r.Read("qty");
                    line.amount = r.Read("amount");
                    line.color = r.Read("color");
                    line.size = r.Read("size");
                    line.enable = r.Read("enable");
                }
            }
        }

        void IOrder.GetOrder(string ord_id, out wm_order ord, out DataTable lines, out int un_read_num)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=get_order", "{\"ord_id\":\"" + ord_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            ord = new wm_order();
            un_read_num = Conv.ToInt(read.Read("un_read_num"));
            ord.ord_id = read.Read("ord_id");
            ord.create_time = read.Read("create_time");
            ord.mobile = read.Read("mobile");
            ord.mobile_is_new = read.Read("mobile_is_new");
            ord.sname = read.Read("sname");
            ord.sex = read.Read("sex");
            ord.address = read.Read("address");
            ord.qty = read.Read("qty");
            ord.amount = read.Read("amount");
            ord.enable_qty = read.Read("enable_qty");
            ord.enable_amount = read.Read("enable_amount");
            ord.status = read.Read("status");
            ord.build_status = read.Read("build_status");
            ord.send_status = read.Read("send_status");
            ord.pay_type = read.Read("pay_type");
            ord.reach_time = read.Read("reach_time");
            ord.cus_remark = read.Read("cus_remark");
            ord.cus_no = read.Read("cus_no");
            ord.salesman_id = read.Read("salesman_id");
            ord.is_pay = read.Read("is_pay");
            ord.take_fee = Conv.ToDecimal(read.Read("take_fee"));
            ord.discount_amt = Conv.ToDecimal(read.Read("discount_amt"));
            //
            lines = new DataTable();
            if (read.Read("lines") != "")
            {
                lines = Conv.GetDataTable(read.ReadList("lines"));
            }
        }


        void IOrder.Send(string ord_id)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=send", "{\"ord_id\":\"" + ord_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IOrder.DisableRow(string ord_id, string row_index)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=disable_row", "{\"ord_id\":\"" + ord_id +
               "\",\"row_index\":\"" + row_index + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        List<string> IOrder.GetNewOrderCode()
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=get_new_msg", "");
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
                    lst.Add(r.Read("ord_id"));
                }
            }
            return lst;
        }

        bool IOrder.GetFirstNewOrder(out wm_order ord, out List<wm_order_item> lines, out int un_read_num)
        {

            var req = new Request();
            var json = req.request("/OnLine/order?t=get_first_new_order", "");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);

            if (read.Read("errId") == "-8")
            {
                ord = null;
                lines = null;
                un_read_num = 0;
                return false;
            }
            else if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }

            //
            ord = new wm_order();
            un_read_num = Conv.ToInt(read.Read("un_read_num"));
            ord.ord_id = read.Read("ord_id");
            ord.create_time = read.Read("create_time");
            ord.mobile = read.Read("mobile");
            ord.mobile_is_new = read.Read("mobile_is_new");
            ord.sname = read.Read("sname");
            ord.sex = read.Read("sex");
            ord.address = read.Read("address");
            ord.qty = read.Read("qty");
            ord.amount = read.Read("amount");
            ord.enable_qty = read.Read("enable_qty");
            ord.enable_amount = read.Read("enable_amount");
            ord.status = read.Read("status");
            ord.build_status = read.Read("build_status");
            ord.send_status = read.Read("send_status");
            ord.pay_type = read.Read("pay_type");
            ord.reach_time = read.Read("reach_time");
            ord.cus_remark = read.Read("cus_remark");
            ord.cus_no = read.Read("cus_no");
            ord.salesman_id = read.Read("salesman_id");
            ord.is_pay = read.Read("is_pay");
            ord.take_fee = Conv.ToDecimal(read.Read("take_fee"));
            ord.discount_amt = Conv.ToDecimal(read.Read("discount_amt"));
            //
            lines = new List<wm_order_item>();
            if (read.Read("lines") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("lines"))
                {
                    var line = new wm_order_item();
                    lines.Add(line);
                    line.ord_id = r.Read("ord_id");
                    line.row_index = r.Read("row_index");
                    line.goods_id = r.Read("goods_id");
                    line.goods_no = r.Read("goods_no");
                    line.goods_name = r.Read("goods_name");
                    line.price = r.Read("price");
                    line.qty = r.Read("qty");
                    line.amount = r.Read("amount");
                    line.enable = r.Read("enable");
                }
            }
            //
            return true;
        }

        bool IOrder.GetFirstNewOrder(out wm_order ord, out DataTable lines, out int un_read_num)
        {

            var req = new Request();
            var json = req.request("/OnLine/order?t=get_first_new_order", "");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);

            if (read.Read("errId") == "-8")
            {
                ord = null;
                lines = null;
                un_read_num = 0;
                return false;
            }
            else if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }

            //
            ord = new wm_order();
            un_read_num = Conv.ToInt(read.Read("un_read_num"));
            ord.ord_id = read.Read("ord_id");
            ord.create_time = read.Read("create_time");
            ord.mobile = read.Read("mobile");
            ord.mobile_is_new = read.Read("mobile_is_new");
            ord.sname = read.Read("sname");
            ord.sex = read.Read("sex");
            ord.address = read.Read("address");
            ord.qty = read.Read("qty");
            ord.amount = read.Read("amount");
            ord.enable_qty = read.Read("enable_qty");
            ord.enable_amount = read.Read("enable_amount");
            ord.status = read.Read("status");
            ord.build_status = read.Read("build_status");
            ord.send_status = read.Read("send_status");
            ord.pay_type = read.Read("pay_type");
            ord.reach_time = read.Read("reach_time");
            ord.cus_remark = read.Read("cus_remark");
            ord.cus_no = read.Read("cus_no");
            ord.salesman_id = read.Read("salesman_id");
            ord.is_pay = read.Read("is_pay");
            ord.take_fee = Conv.ToDecimal(read.Read("take_fee"));
            ord.discount_amt = Conv.ToDecimal(read.Read("discount_amt"));
            //
            lines = new DataTable();
            if (read.Read("lines") != "")
            {
                lines = lines = Conv.GetDataTable(read.ReadList("lines"));
            }
            //
            return true;
        }


        void IOrder.SignRead(string ord_id)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=sign_read", "{\"ord_id\":\"" + ord_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IOrder.GetOrderNew(string clear_key, out string key)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=get_order_new", "{\"clear_key\":\"" + clear_key + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }

        void IOrder.GetOrderPass(string date1, string date2, string keyword, string clear_key, out string key)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=get_order_pass", "{\"date1\":\"" + date1 + "\",\"date2\":\"" + date2 + "\",\"keyword\":\"" + keyword + "\",\"clear_key\":\"" + clear_key + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }

        void IOrder.GetOrderDisable(string date1, string date2, string keyword, string clear_key, out string key)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=get_order_disable", "{\"date1\":\"" + date1 + "\",\"date2\":\"" + date2 + "\",\"keyword\":\"" + keyword + "\",\"clear_key\":\"" + clear_key + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }

        void IOrder.GetOrderComplete(string date1, string date2, string keyword, string only_show_no_receive, string clear_key, out string key)
        {
            var req = new Request();
            var json = req.request("/OnLine/order?t=get_order_complete", "{\"date1\":\"" + date1 + "\",\"date2\":\"" + date2 + "\",\"keyword\":\"" + keyword + "\",\"only_show_no_receive\":\"" + only_show_no_receive + "\",\"clear_key\":\"" + clear_key + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }
    }
}