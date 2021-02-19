using System;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL;
using IvyTran.IBLL.OnLine;
using Model;

namespace IvyTran.BLL.OnLine
{
    public class Advice : IAdvice
    {
        DataTable IAdvice.GetList(int pageSize, int pageIndex, out int total)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql_t = "select av_id from advice where mc_id = @mc_id ";
            var pars_t = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mc_id",Global.McId)
            };
            var dt_t = db.ExecuteToTable(sql_t, pars_t);
            total = dt_t.Rows.Count;
            int last = pageSize * (pageIndex - 1);
            string sql = "select top " + pageSize + " a.*,b.nickname from advice a left join wx_user b on a.openid = b.openid where a.mc_id = @mc_id and a.av_id not in (select top " + last + " av_id from advice where mc_id = @mc_id order by av_id) order by a.ask_date desc";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mc_id",Global.McId)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }
        void IAdvice.Reply(string av_id, string mc_reply)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "update advice  set mc_reply = @mc_reply ,reply_date = @reply_date where av_id = @av_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mc_reply",mc_reply),
                new System.Data.SqlClient.SqlParameter("@reply_date",DateTime.Now),
                new System.Data.SqlClient.SqlParameter("@av_id",av_id)
            };
            db.ExecuteScalar(sql, pars);
        }

        advice IAdvice.Select(string av_id, out string nickname)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select  a.*,b.nickname from advice a left join wx_user b on a.openid = b.openid where av_id = @av_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@av_id",av_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count == 0)
            {
                throw new ExceptionBase("非法操作！");
            }
            advice advice = new advice();
            DataRow row = dt.Rows[0];
            advice.av_id = row["av_id"].ToString();
            advice.use_ask = row["use_ask"].ToString();
            advice.mc_reply = row["mc_reply"].ToString();
            nickname = row["nickname"].ToString();
            return advice;
        }
    }
}