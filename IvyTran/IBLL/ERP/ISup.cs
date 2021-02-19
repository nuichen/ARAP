using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    interface ISup
    {
        DataTable GetList();
        DataTable GetCJ();
        System.Data.DataTable GetList(string region_no, string keyword, int show_stop, int page_index, int page_size, out int total_count);
        System.Data.DataTable GetItem(string supcust_no);
        string MaxCode();
        void Add(bi_t_supcust_info item);
        void Adds(List<bi_t_supcust_info> supcustInfos);
        void Change(bi_t_supcust_info item);
        void Delete(string supcust_no);

        DataTable QuickSearchList(string keyword);

        List<bi_t_supcust_info> GetALL();

        DataTable GetSupBindItem(string sup_no);
        void SaveSupBindItem(bi_t_supcust_bind_item bind_item);
    }
}
