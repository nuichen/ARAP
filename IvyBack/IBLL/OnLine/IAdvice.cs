using System.Collections.Generic;
using System.Data;

namespace IvyBack.IBLL.OnLine
{
    interface IAdvice
    {
        List<Model.advice> GetList(int pageSize, int pageIndex, out int total);
        DataTable GetDt(int pageSize, int pageIndex, out int total);
        void Reply(string av_id, string mc_reply);
        Model.advice Select(string av_id);
    }
}
