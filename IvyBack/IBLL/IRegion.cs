using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace IvyBack.IBLL
{
    public interface IRegion
    {
        DataTable GetDataTable(string is_cs);
        List<bi_t_region_info> GetAllList(string is_cs);
        bi_t_region_info GetRegion(string region_no);
        void Add(bi_t_region_info region);
        void Del(bi_t_region_info region);
        void Upload(bi_t_region_info region);
        string GetMaxCode();
    }
}
