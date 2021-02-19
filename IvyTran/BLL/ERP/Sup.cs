using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using NPinyin;
using System.Linq;

namespace IvyTran.BLL.ERP
{
    public class Sup : ISup
    {
        public DataTable GetList()
        {
            string sql = "select * from bi_t_supcust_info ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetCJ()
        {
            string sql = "select supcust_no+'/'+sup_name sup_name from bi_t_supcust_info where is_factory='1'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        System.Data.DataTable ISup.GetList(string region_no, string keyword, int show_stop, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);         
           string sql = @"select a.*,b.region_name from (select * from bi_t_supcust_info where (supcust_flag='S' or supcust_flag='P')) a
left join bi_t_region_info b on a.region_no=b.region_no and b.supcust_flag='S'
where 1=1";
            if (region_no != "")
            {
                string sql1 = "select * from bi_t_region_info where supcust_flag='S' order by region_no";
                DataTable tb1 = db.ExecuteToTable(sql1, null);
                if(tb1.Rows.Count>0)
                region_no = GetRegionNo(region_no,tb1);
                else
                    region_no = "'" + region_no + "'";
                region_no = region_no.Substring(0, region_no.Length-1);
                sql += " and a.region_no in( " + region_no + ")";
            }
            if (keyword != "")
            {
                sql += " and (a.supcust_no like '%@%' or sup_name like '%@%')".Replace("@", keyword);
            }
            if (show_stop != 1)
            {
                sql += " and a.display_flag='1'";
            }


            var tb = db.ExecuteToTable(sql, "a.supcust_no", null, page_size, page_index, out total_count);
            return tb;

        }
        /// <summary>
        /// 递归调用
        /// </summary>
        /// <param name="region_no"></param>
        /// <param name="tb1"></param>
        /// <returns></returns>
        public string GetRegionNo(string region_no,DataTable tb1)
        {
            string region = "";/* + region_no + "',";*/
            DataRow[] tb2;
                tb2 = tb1.AsEnumerable().Where(item => Conv.ToString(item["region_parent"]) == region_no).ToArray();
                if (tb2.Length > 0)
                {
                    foreach (DataRow dr in tb2)
                    {
                        region += GetRegionNo(dr["region_no"].ToString(),tb1);
                    }

                }
                else
                {
                    region = "'" + region_no + "',";
                }
            return region;
        }
        System.Data.DataTable ISup.GetItem(string supcust_no)
        {
            string sql = "select * from bi_t_supcust_info where supcust_flag='S' and supcust_no='" + supcust_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }

        string ISup.MaxCode()
        {
            string sql = "select  top 1 supcust_no from bi_t_supcust_info where (supcust_flag='S' or supcust_flag='P') and len(supcust_no)=5 order by supcust_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count == 0)
            {
                return "00001";
            }
            else
            {
                string result = System.Text.RegularExpressions.Regex.Replace(tb.Rows[0]["supcust_no"].ToString(), @"[^0-9]+", "");
                int index = result.ToInt32();
                index++;
                return index.ToString().PadLeft(5, '0');
            }
        }

        void ISup.Add( bi_t_supcust_info item)
        {
            item.update_time = System.DateTime.Now;
            string sql = "select * from bi_t_supcust_info where supcust_flag='S' and supcust_no='" + item.supcust_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已经存在编码" + item.supcust_no);
            }
            db.Insert(item);
        }

        public void Adds(List<bi_t_supcust_info> supcustInfos)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                var supInfos = new Dictionary<string, bi_t_supcust_info>();
                var regionInfos = new Dictionary<string, bi_t_region_info>();

                {
                    var tb = d.ExecuteToTable("SELECT * FROM dbo.bi_t_supcust_info WHERE supcust_flag='S'", null);
                    foreach (DataRow row in tb.Rows)
                    {
                        var sup = row.DataRowToModel<bi_t_supcust_info>();
                        supInfos.Add(sup.supcust_no, sup);
                    }

                    tb = d.ExecuteToTable("SELECT * FROM dbo.bi_t_region_info", null);
                    foreach (DataRow row in tb.Rows)
                    {
                        var region = row.DataRowToModel<bi_t_region_info>();

                        regionInfos.Add(region.region_no, region);
                        if (!regionInfos.TryGetValue(region.region_name, out _))
                        {
                            regionInfos.Add(region.region_name, region);
                        }
                    }

                }

                foreach (var info in supcustInfos)
                {
                    //检查重复
                    if (supInfos.TryGetValue(info.supcust_no, out _))
                        throw new Exception($"供应商编号:[{info.supcust_no}]，已存在");

                    //区域
                    if (regionInfos.TryGetValue(info.region_no, out var region))
                    {
                        info.region_no = region.region_no;
                    }
                    else
                    {
                        info.region_no = "00";
                    }

                    info.sup_pyname = Pinyin.GetInitials(info.sup_name);
                    info.supcust_flag = "S";
                    info.create_time = DateTime.Now;
                    info.update_time = DateTime.Now;
                }

                d.BulkCopy(supcustInfos);

                db.CommitTran();
            }
            catch (Exception)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ISup.Change(bi_t_supcust_info item)
        {
            item.update_time = System.DateTime.Now;
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(item, "supcust_no,supcust_flag");
        }

        void ISup.Delete(string supcust_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "";
            //

            sql = "select  top 1 * from ic_t_inout_store_master where trans_no='PI' and supcust_no='" + supcust_no + "' ";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("客户已被单据引用，不能删除!");
            }
            //
            sql = "select  top 1 * from ic_t_inout_store_master where trans_no='F' and supcust_no='" + supcust_no + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("客户已被单据引用，不能删除!");
            }
            //
            sql = "delete from bi_t_supcust_info where supcust_flag='S' and supcust_no='" + supcust_no + "'";
            db.ExecuteScalar(sql, null);
        }

        public DataTable QuickSearchList(string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"select supcust_no,sup_name from bi_t_supcust_info 
where supcust_flag='S' and display_flag='1' and 
(supcust_no like '%{keyword}%' or sup_name like '%{keyword}%') ";
            sql += "order by supcust_no ";
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public List<bi_t_supcust_info> GetALL()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @" SELECT * FROM dbo.bi_t_supcust_info WHERE supcust_flag='S' AND display_flag=1 ";

            List<bi_t_supcust_info> lis = db.ExecuteToList<bi_t_supcust_info>(sql, null);
            return lis;
        }

        public DataTable GetSupBindItem(string sup_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT * FROM dbo.bi_t_supcust_bind_item";

            if (!string.IsNullOrEmpty(sup_no))
            {
                sql += " WHERE supcust_no='" + sup_no + "' ";
            }

            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public void SaveSupBindItem(bi_t_supcust_bind_item bind_item)
        {
            DB.DBByHandClose db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                bind_item.update_time = DateTime.Now;

                d.ExecuteScalar(" DELETE FROM dbo.bi_t_supcust_bind_item WHERE supcust_no='" + bind_item.supcust_no + "' ", null);

                d.Insert(bind_item);

                db.CommitTran();
            }
            catch (Exception)
            {
                db.RollBackTran();
            }
            finally
            {
                db.Close();
            }
        }
    }

}
