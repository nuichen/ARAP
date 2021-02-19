using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.IBLL
{
    interface IBranch
    {
        Dictionary<string, Model.bi_t_branch_info> GetList();
        string GetBranch(string branch_no);
    }
}
