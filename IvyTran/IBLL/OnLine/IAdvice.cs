using System.Data;
using Model;

namespace IvyTran.IBLL.OnLine
{
    public interface IAdvice
    {
        DataTable GetList(int pageSize, int pageIndex, out int total);
        void Reply(string av_id, string mc_reply);
        advice Select(string av_id, out string nickname);
    }
}