using System;
using System.Collections.Generic;
using System.Data;
using IvyBack.Helper;
using IvyBack.IBLL.OnLine;

namespace IvyBack.BLL.OnLine
{
    class Advice:IAdvice
    {
        List<Model.advice> IAdvice.GetList(int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/advice?t=get_list", "{\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var lst = new List<Model.advice>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var item = new Model.advice();
                    lst.Add(item);
                    item.av_id = r.Read("av_id");
                    item.use_ask = r.Read("use_ask");
                    item.mc_reply = r.Read("mc_reply");
                    item.ask_date = Conv.ToDateTime(r.Read("ask_date"));
                    item.reply_date = Conv.ToDateTime(r.Read("reply_date"));
                    item.nickname = r.Read("nickname");
                }
            }
            return lst;
        }

        DataTable IAdvice.GetDt(int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/advice?t=get_list", "{\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            if (read.Read("datas").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("datas"));

            return tb;
        }


        void IAdvice.Reply(string av_id,string reply)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("av_id", av_id);
            write.Append("reply", reply);
            var json = req.request("/OnLine/advice?t=reply", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }


        Model.advice IAdvice.Select(string av_id)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("av_id", av_id);
            var json = req.request("/OnLine/advice?t=select", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            Model.advice advice = new Model.advice();
            advice.av_id = read.Read("av_id");
            advice.nickname = read.Read("nickname");
            advice.mc_reply = read.Read("mc_reply");
            advice.use_ask = read.Read("use_ask");
            return advice;
        }
    }
}
