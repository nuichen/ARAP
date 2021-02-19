using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    class Rep付款方式汇总表:IRep付款方式汇总表 
    {
        void IRep付款方式汇总表.GetKey(string clear_key, string date1, string date2, out string key)
        {
            var req = new Request();
            var json = req.request("/rep?t=付款方式汇总表", "{\"clear_key\":\"" + clear_key +
                "\",\"date1\":\"" + date1 + "\",\"date2\":\"" + date2  +"\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }

        DataTable IRep付款方式汇总表.GetData(string key, int pageSize, int pageIndex, out int total,string field,string fields)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("key", key);
            write.Append("pageSize", pageSize.ToString());
            write.Append("pageIndex", pageIndex.ToString());
            write.Append("field", field);
            write.Append("fields", fields);
            var json = req.request("/pager?t=get_data_with_total", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var dt = new DataTable();
            dt.Columns.Add("付款方式");
            dt.Columns.Add("单数");
            dt.Columns.Add("数量");
            dt.Columns.Add("金额");
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    string pay_type = "";
                    if (r.Read("付款方式") == "0")
                    {
                        pay_type = "微信支付";
                    }
                    else if (r.Read("付款方式") == "1")
                    {
                        pay_type = "信用支付";
                    }
                    else if (r.Read("付款方式") == "2")
                    {
                        pay_type = "支付宝支付";
                    }
                    else
                    {
                        pay_type = r.Read("付款方式");
                    }
                    dt.Rows.Add(pay_type,r.Read("单数"),r.Read("数量"),r.Read("金额") );

                }

            }
            return dt;
        }
    }
}
