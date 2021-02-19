using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IvyBack.Helper;
using Model;


namespace IvyBack.IBLL
{
    public interface ISup
    {
        DataTable GetFactory();
        DataTable GetDataTable(string region_no, string keyword, int show_stop, int page_index, int page_size, out int total_count);
        Page<bi_t_supcust_info> GetDataTable(string region_no, string keyword, int show_stop, Page<bi_t_supcust_info> page);

        bi_t_supcust_info GetItem(string supcust_no);

        string GetMaxCode();

        void Add(bi_t_supcust_info sup);
        void Adds(List<bi_t_supcust_info> supcustInfos);
        void Update(bi_t_supcust_info sup);
        void Del(bi_t_supcust_info sup);

        DataTable QuickSearchList(string keyword);
        List<bi_t_supcust_info> GetALL();

        DataTable GetSupBindItem(string sup_no);
        void SaveSupBindItem(bi_t_supcust_bind_item bind_item);
    }
}
