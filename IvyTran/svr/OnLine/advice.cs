using System;
using System.Collections.Generic;
using IvyTran.BLL.OnLine;
using IvyTran.Helper;
using IvyTran.IBLL.OnLine;

namespace IvyTran.svr.OnLine
{
    public class advice : BaseService
    {
        IAdvice bll = new Advice();

        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }
        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);

            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }

            return web.NmJson();
        }

        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "pageSize", "pageIndex") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            int total = 0;
            int pageSize = ObjectToInt(kv, "pageSize");
            int pageIndex = ObjectToInt(kv, "pageIndex");
            var dt = bll.GetList(pageSize, pageIndex, out total);

            w.Write("datas", dt);
            w.Write("total", total);
        }

        public void reply(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "av_id", "reply") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string av_id = ObjectToString(kv, "av_id");
            string reply = ObjectToString(kv, "reply");
            bll.Reply(av_id, reply);
        }

        public void select(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "av_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            var av_id = ObjectToString(kv, "av_id");
            string nickname = "";
            var advice = bll.Select(av_id, out nickname);

            w.Write("nickname", nickname);
            w.Write("av_id", advice.av_id);
            w.Write("mc_reply", advice.mc_reply);
            w.Write("use_ask", advice.use_ask);
        }

    }
}