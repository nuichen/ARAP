using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public   class Region:IRegion 
    {
        
        System.Data.DataTable IRegion.GetList(string is_cs)
        {
            string sql = "select * from bi_t_region_info where supcust_flag='"+is_cs+"' order by region_no";
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        System.Data.DataTable IRegion.GetNodeList(string is_cs)
        {
            string sql = "select * from bi_t_region_info where supcust_flag='" + is_cs + "' order by region_no";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            List<int> li = new List<int>();
            for (int i = 0; i < tb.Rows.Count; i++)
            {
               var tb2 = tb.AsEnumerable().Where(item => Conv.ToString(item["region_parent"]) == Conv.ToString(tb.Rows[i]["region_no"])).ToArray();
                if (tb2.Length > 0)
                {
                    li.Add(i);
                }
            }
            foreach(int j in li)
            {
                tb.Rows[j].Delete();
            }
            tb.AcceptChanges();


            return tb;
        }
        System.Data.DataTable IRegion.GetItem(string region_no)
        {
            string sql = "select * from bi_t_region_info where region_no='" + region_no + "'";
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        string IRegion.MaxCode()
        {
            string sql = "select  top 1  region_no from bi_t_region_info where len(region_no)=4 order by region_no desc ";
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                var val = tb.Rows[0]["region_no"].ToString();
                int index = Helper.Conv.ToInt16(val);
                index++;
                return index.ToString().PadLeft(4, '0');
            }
            else
            {
                return "0001";
            }
        }

        void IRegion.Add(Model.bi_t_region_info item)
        {
            string sql = "select * from bi_t_region_info where region_no='" + item.region_no + "'";

            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已经存在地区编号" + item.region_no);
            }
            db.Insert(item);
            if (0 == Conv.ToDecimal(db.ExecuteScalar("select count(1) from bi_t_region_info where region_no='00' and supcust_flag='"+item.supcust_flag+"' ", null)))
            {
                Model.bi_t_region_info defaultType = new Model.bi_t_region_info()
                {
                    region_no = "00",
                    region_name = "不定",
                    supcust_flag = item.supcust_flag,
                    display_flag = "1",
                    update_time = DateTime.Now,
                };
                db.Insert(defaultType);
            }
            //if (0 == Conv.ToDecimal(db.ExecuteScalar("select count(1) from bi_t_region_info where region_no='00' and supcust_flag='C' ", null)))
            //{
            //    Model.bi_t_region_info defaultType1 = new Model.bi_t_region_info()
            //    {
            //        region_no = "00",
            //        region_name = "不定",
            //        supcust_flag = "C",
            //        display_flag = "1",
            //        update_time = DateTime.Now,
            //    };
            //    db.Insert(defaultType1);
            //}
        }

        void IRegion.Change(Model.bi_t_region_info item)
        {
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(item, "region_no");
        }

        void IRegion.Delete(string region_no)
        {
            string sql = "select * from bi_t_region_info where region_parent like '%" + region_no + "%'";

            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("存在子分类，不能删除!");
            }
            //
            sql = "select top 1  * from bi_t_supcust_info where region_no='" + region_no + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("地区已被客户供应商引用，不能删除!");
            }
            //
            sql = "delete from bi_t_region_info where region_no='" + region_no + "'";
            db.ExecuteScalar(sql, null);
        }

    }
}
