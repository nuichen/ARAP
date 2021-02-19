using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
namespace IvyBack.IBLL
{
    public interface IBranch
    {
        DataTable GetAllList(int code_len);
        DataTable GetAllList(string code);
        bi_t_branch_info GetItem(bi_t_branch_info branch);
        string GetMaxCode(string code);
        void Add(bi_t_branch_info branch);
        void Upload(bi_t_branch_info branch);
        void Del(bi_t_branch_info branch);

        DataTable QuickSearchList(string keyword);
    }
}
