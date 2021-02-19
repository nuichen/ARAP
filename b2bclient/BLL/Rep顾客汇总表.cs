using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    class Rep顾客汇总表:IRep顾客汇总表 
    {
        void IRep顾客汇总表.GetKey(string clear_key, string date1, string date2, string mobile, out string key)
        {
            var req = new Request();
            var json = req.request("/rep?t=顾客汇总表", "{\"clear_key\":\"" + clear_key +
                "\",\"date1\":\"" + date1 + "\",\"date2\":\"" + date2 +
                "\",\"mobile\":\"" + mobile + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }

        DataTable IRep顾客汇总表.GetData(string key, int pageSize, int pageIndex, out int total,string field,string fields)
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
            dt.Columns.Add("手机号");
            dt.Columns.Add("昵称");
            dt.Columns.Add("姓名");
            dt.Columns.Add("地址");
            dt.Columns.Add("单数");
            dt.Columns.Add("数量");
            dt.Columns.Add("金额");
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    dt.Rows.Add(
                        r.Read("手机号"),
                        r.Read("昵称"),
                        r.Read("姓名"),
                        r.Read("地址"),
                        r.Read("单数"),
                        r.Read("数量"),
                        r.Read("金额")
                        );

                }

            }
            return dt;
        }
    }
}
