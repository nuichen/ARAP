using System;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public  class ItemCls : IItemCls
    {

        public System.Data.DataTable GetList()
        {
            string sql = @"SELECT c.item_clsno,  c.item_flag,c.item_clsname, c.display_flag,c.update_time,c.is_show_mall,g.SupCust_GroupName cus_group 
                                    FROM dbo.bi_t_item_cls c
                                    LEFT JOIN dbo.yjh_t_bind_group_cls b ON b.item_clsno = c.item_clsno
                                    LEFT JOIN dbo.bi_t_supcust_group g ON g.SupCust_GroupNo = b.SupCust_GroupNo
                                    WHERE c.item_flag = '0' order by c.item_clsno";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public System.Data.DataTable GetItem(string item_clsno)
        {
            string sql = "select * from bi_t_item_cls where item_flag='0' and item_clsno='" + item_clsno + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetListForMenu(string is_first_level)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var condition_sql = "";
            if (is_first_level == "1") condition_sql += " and len(cls_no)=4 ";
            string sql = "select cls_id,cls_no,cls_name,status,isnull(is_show_mall,'0') is_show_mall from goods_cls ";
            sql += "where isnull(is_show_mall,'0')='1' and status='1' " + condition_sql + "order by cls_no ";
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public string MaxCode(string par_code)
        {
            string sql = "select top 1  item_clsno from bi_t_item_cls where item_flag='0' and item_clsno like '" + par_code + "%' and len(item_clsno)=" + (par_code.Length + 2) +
                " order by item_clsno desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                var val = tb.Rows[0]["item_clsno"].ToString();
                val = val.Substring(par_code.Length);
                int index = Helper.Conv.ToInt16(val);
                index++;
                return par_code + index.ToString().PadLeft(2, '0');
            }
            else
            {
                return par_code + "01";
            }
        }

        public void Add(Model.bi_t_item_cls item)
        {
            item.update_time = System.DateTime.Now;
            string sql = "select * from bi_t_item_cls where item_flag='0' and item_clsno='" + item.item_clsno + "'";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已经存在分类编号" + item.item_clsno);
            }
            db.Insert(item);
        }

        public void Change(Model.bi_t_item_cls item)
        {
            item.update_time = System.DateTime.Now;
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            db.Update(item, "item_clsno,item_flag");
        }

        public void Delete(string item_clsno)
        {
            string sql = "select * from bi_t_item_cls where item_flag='0' and item_clsno like '" +
                item_clsno + "%' and item_clsno<>'" + item_clsno + "'";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("存在子分类，不能删除!");
            }
            //
            sql = "select  top 1 * from bi_t_item_info where item_clsno='" + item_clsno + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("存在商品资料,不能删除!");
            }
            //

            sql = "delete from bi_t_item_cls where item_flag='0' and item_clsno='" + item_clsno + "'";
            db.ExecuteScalar(sql, null);
        }

        public string GetGroupCls(string group_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select item_clsno from yjh_t_bind_group_cls where SupCust_GroupNo=@group_no order by item_clsno ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@group_no", group_no)
            };
            var dt = db.ExecuteToTable(sql, pars);
            string item_clsnos = "";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    item_clsnos += dr["item_clsno"].ToString() + ",";
                }
            }
            if (item_clsnos.Length > 0) item_clsnos = item_clsnos.Substring(0, item_clsnos.Length - 1);
            return item_clsnos;
        }
        public void SaveGroupCls(string group_no, string item_clsnos)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "delete from yjh_t_bind_group_cls where SupCust_GroupNo=@group_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@group_no", group_no)
                };
                db.ExecuteScalar(sql, pars);
                var item_clsnoarr = item_clsnos.Split(',');
                for (var i = 0; i < item_clsnoarr.Length; i++)
                {
                    sql = "insert into yjh_t_bind_group_cls(SupCust_GroupNo,item_clsno) values(@group_no,@item_clsno) ";
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@group_no", group_no),
                        new System.Data.SqlClient.SqlParameter("@item_clsno", item_clsnoarr[i])
                    };
                    db.ExecuteScalar(sql, pars);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("GoodsClass.SaveGroupCls()", ex.ToString(), group_no, item_clsnos);
            }
        }
    }
}
