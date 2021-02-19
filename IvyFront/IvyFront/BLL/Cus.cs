using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.BLL
{
    class Cus:IBLL.ICus 
    {
        List<Model.bi_t_supcust_info> IBLL.ICus.GetList(string keyword)
        {
            string sql = "select supcust_no,supcust_flag,ifnull(sup_name,'') sup_name,ifnull(sup_tel,'') sup_tel,ifnull(sup_pyname,'') sup_pyname,display_flag ";
            sql += "from bi_t_supcust_info where supcust_flag='C' and display_flag='1' ";
            if (keyword != "")
            {
                sql += "and (supcust_no like '%@a%' or sup_name like '%@a%')".Replace("@a",keyword);
            }
            var tb = Program.db.ExecuteToTable(sql, null);
            var lst = new List<Model.bi_t_supcust_info>();
            foreach (System.Data.DataRow row in tb.Rows)
            {
                var item = DB.ReflectionHelper.DataRowToModel<Model.bi_t_supcust_info>(row);
                lst.Add(item);
            }
            return lst;
        }

        public Dictionary<string, Model.bi_t_supcust_info> GetDic()
        {
            IBLL.ICus cusBLL = new BLL.Cus();
            var lst = cusBLL.GetList("");
            Dictionary<string, Model.bi_t_supcust_info> dic = new Dictionary<string, Model.bi_t_supcust_info>();
            foreach (Model.bi_t_supcust_info item in lst)
            {
                var it = new Model.bi_t_supcust_info();
                if (dic.TryGetValue(item.supcust_no, out it) == false)
                {
                    dic.Add(item.supcust_no, item);
                }
                
            }
            return dic;
        }

    }

}
