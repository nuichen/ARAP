using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    interface IAdvice
    {
        List<Model.advice> GetList(int pageSize, int pageIndex, out int total);
        DataTable GetDt(int pageSize, int pageIndex, out int total);
        void Reply(string av_id, string mc_reply);
        Model.advice Select(string av_id);
    }
}
