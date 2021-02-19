using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    class Rep失效订单明细:IRep失效订单明细 
    {
        void IRep失效订单明细.GetKey(string clear_key, string date1, string date2, string mobile, string goods_name, out string key)
        {
            var req = new Request();
            var json = req.request("/rep?t=失效订单明细", "{\"clear_key\":\"" + clear_key +
                "\",\"date1\":\"" + date1 + "\",\"date2\":\"" + date2 +
                "\",\"mobile\":\""+mobile+
                "\",\"goods_name\":\"" + goods_name + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }

        DataTable IRep失效订单明细.GetData(string key, int pageSize, int pageIndex, out int total,string field,string fields)
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
            dt.Columns.Add("单号");
            dt.Columns.Add("日期");
            dt.Columns.Add("下单时间");
            dt.Columns.Add("送达时间");
            dt.Columns.Add("手机");
            dt.Columns.Add("姓名");
            dt.Columns.Add("品名");
            dt.Columns.Add("属性");
            dt.Columns.Add("数量");
            dt.Columns.Add("单价");
            dt.Columns.Add("金额");
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    string ps = "";
                    ps = r.Read("属性1") + " " + r.Read("属性2") + " " + r.Read("属性3");
                    ps = ps.Trim();
                    dt.Rows.Add(
                        r.Read("单号"),
                        r.Read("日期"),
                        r.Read("下单时间"),
                        r.Read("送达时间"),
                        r.Read("手机"),
                        r.Read("姓名"),
                        r.Read("品名"),
                        ps,
                        r.Read("数量"),
                        r.Read("单价"),
                        r.Read("金额")
                        );
                }

            }
            return dt;
        }
    }
}
