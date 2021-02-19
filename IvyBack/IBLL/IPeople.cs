using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using IvyBack.Helper;
using System.Data;

namespace IvyBack.IBLL
{
    public interface IPeople
    {
        DataTable GetDataTable(string dep_no, string keyword, int show_stop, int page_index, int page_size, out int total_count);
        Page<bi_t_people_info> GetDataTable(string dep_no, string keyword, int show_stop, Page<bi_t_people_info> page);

        bi_t_people_info GetItem(string oper_id);

        string GetMaxCode();

        void Add(bi_t_people_info peo);
        void Update(bi_t_people_info peo);
        void Del(bi_t_people_info peo);

        DataTable QuickSearchList(string dept_no, string keyword);
    }
}
