using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.IBLL
{
    interface ICus
    {
        List<Model.bi_t_supcust_info> GetList(string keyword);
         
        Dictionary<string, Model.bi_t_supcust_info> GetDic();
    }
}
