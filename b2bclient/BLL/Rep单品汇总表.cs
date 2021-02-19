using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    class Rep单品汇总表 : IRep单品汇总表
    {

        void IRep单品汇总表.GetKey(string clear_key, string date1, string date2, string goods_name, out string key)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("clear_key", clear_key);
            write.Append("date1", date1);
            write.Append("date2", date2);
            write.Append("goods_name", goods_name);

            var json = req.request("/rep?t=单品汇总表", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            key = read.Read("key");
        }

        DataTable IRep单品汇总表.GetData(string key, int pageSize, int pageIndex, out int total, string field, string fields)
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
            dt.Columns.Add("编号");
            dt.Columns.Add("品名");
            dt.Columns.Add("单位");
            dt.Columns.Add("规格");
            dt.Columns.Add("单数");
            dt.Columns.Add("数量");
            dt.Columns.Add("金额");
            if (read.Read("data") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                {
                    dt.Rows.Add(r.Read("编号"), r.Read("品名"), r.Read("单位"), r.Read("规格"), r.Read("单数"), r.Read("数量"), r.Read("金额"));
                }

            }
            return dt;
        }

    }
}
