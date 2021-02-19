using IvyTran.IBLL.ERP;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DB;
using IvyTran.Helper;
using Model.BaseModel;
using NPinyin;
using System.Linq;

namespace IvyTran.BLL.ERP
{
    public class Cus : ICus
    {
        DataTable ICus.GetSendType()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            try
            {
                string sql = "select * from bi_t_send_type";
                return db.ExecuteToTable(sql, null);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        DataTable ICus.GetCarId(string id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            try
            {
                string sql = "select car_id from bi_t_supcust_info where supcust_flag='C' and supcust_no='" + id + "'";
                return db.ExecuteToTable(sql, null);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        DataTable ICus.GetCar()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            try
            {
                string sql = "select * from bi_t_supcust_car";
                return db.ExecuteToTable(sql, null);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        DataTable ICus.GetCar(string id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            try
            {
                string sql = "select * from bi_t_supcust_car where car_id='" + id + "'";
                return db.ExecuteToTable(sql, null);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        void ICus.AddCar(bi_t_supcust_car car)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            try
            {
                string sql = "select * from bi_t_supcust_car where car_id='" + car.car_id + "'";
                if (db.ExecuteToTable(sql, null).Rows.Count > 0)
                {
                    throw new Exception("该车辆已存在");
                }
                db.Insert(car);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        void ICus.ChangeCar(bi_t_supcust_car car)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            try
            {
                string sql = "select * from bi_t_supcust_car where car_id='" + car.car_id + "'";
                if (db.ExecuteToTable(sql, null).Rows.Count > 0)
                {
                    sql = "delete from bi_t_supcust_car where car_id='" + car.car_id + "'";
                    db.ExecuteToTable(sql, null);
                    db.Insert(car);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        void ICus.DeleteCar(string car_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            try
            {
              
                    string  sql = "delete from bi_t_supcust_car where car_id='" + car_id + "'";
                    db.ExecuteToTable(sql, null);

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
        bool ICus.UpdateCustType(DataTable tb1)
        {
            //DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "";
                foreach (DataRow row in tb1.Rows)
                {
                    string sql1 = @"update bi_t_supcust_info set sup_type='" + row["sup_type"].ToString() +
                                  "' where supcust_no='" + row["supcust_no"].ToString() + "'";
                    sql += sql1 + "\r\n";
                }
                //var tb = db.ExecuteToTable(sql, null);
                string connectionString = AppSetting.conn;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sql;
                            cmd.Transaction = transaction;
                            cmd.ExecuteNonQuery();
                            transaction.Commit(); //提交事务
                        }
                        return true;
                    }
                    catch(Exception e)
                    {
                        transaction.Rollback();
                        //throw new AggregateException(e);
                        //事务回滚
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }
            }
            
        }

        System.Data.DataTable IBLL.ERP.ICus.CustType(string cust_name, string cust_type,string flag,string region)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select supcust_no,sup_name,sup_type from [dbo].[bi_t_supcust_info] a 
left join bi_t_region_info  b on a.region_no=b.region_no where 1=1 ";
            int a = 0;
            if (int.TryParse(region, out a)&&!string.IsNullOrEmpty(region))
            {
                sql += " and b.region_no like '%" + region + "%'";
            }
            if (int.TryParse(region, out a)==false && !string.IsNullOrEmpty(region))
            {
                sql += " and b.region_name like '%" + region + "%'";
            }

            if (int.TryParse(cust_name, out a)&&!string.IsNullOrEmpty(cust_name))
            {
                sql += " and a.supcust_no like '%"+cust_name+"%'";

            }

            if (int.TryParse(cust_name, out a)==false && !string.IsNullOrEmpty(cust_name))
            {
                sql += " and a.sup_name like '%" + cust_name + "%'";
            }

            if (!string.IsNullOrEmpty(cust_type))
            {
                sql += " and a.sup_type='"+cust_type+"'";
            }

            if (!string.IsNullOrEmpty(flag))
            {
                sql += " and a.supcust_flag='" + flag + "'";
            }

            sql += " order by a.supcust_no";
            var tb = db.ExecuteToTable(sql, null);
            return tb;

        }
        System.Data.DataTable ICus.GetList(string region_no, string keyword, string is_jail, int show_stop, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select a.*,b.region_name from (select * from bi_t_supcust_info where supcust_flag='C')a
left join bi_t_region_info b on a.region_no=b.region_no  and b.supcust_flag='C'
where 1=1";
            if (region_no != "")
            {
                string sql1 = "select * from bi_t_region_info where supcust_flag='S' order by region_no";
                DataTable tb1 = db.ExecuteToTable(sql1, null);
                if (tb1.Rows.Count > 0)
                    region_no = GetRegionNo(region_no, tb1);
                else
                    region_no = "'" + region_no + "'";
                region_no = region_no.Substring(0, region_no.Length - 1);
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
            if (is_jail != "")
            {
                sql += " and a.is_jail = '1'";
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
        public string GetRegionNo(string region_no, DataTable tb1)
        {
            string region = "";/* + region_no + "',";*/
            DataRow[] tb2;
            tb2 = tb1.AsEnumerable().Where(item => Conv.ToString(item["region_parent"]) == region_no).ToArray();
            if (tb2.Length > 0)
            {
                foreach (DataRow dr in tb2)
                {
                    region += GetRegionNo(dr["region_no"].ToString(), tb1);
                }

            }
            else
            {
                region = "'" + region_no + "',";
            }
            return region;
        }
        DataTable IBLL.ERP.ICus.SupCustType(string flag)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @" select supcust_type,supcust_type_name from bi_t_supcust_type where supcust_flag='"+flag+"'";
           
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }


        System.Data.DataTable IBLL.ERP.ICus.GetItem(string supcust_no)
        {
            string sql = "select * from bi_t_supcust_info where supcust_flag='C' and supcust_no='" + supcust_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }

        string IBLL.ERP.ICus.MaxCode()
        {
            string sql = "select top 1 supcust_no from bi_t_supcust_info where supcust_flag='C' and len(supcust_no)=5 order by supcust_no desc ";
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

        void ICus.Add(bi_t_supcust_info item)
        {
            item.update_time = System.DateTime.Now;
            string sql = "select * from bi_t_supcust_info where supcust_flag='C' and supcust_no='" + item.supcust_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已经存在编码" + item.supcust_no);
            }
            db.Insert(item);
        }

        public void Adds(List<Model.bi_t_supcust_info> supcustInfos)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                var supInfos = new Dictionary<string, Model.bi_t_supcust_info>();
                var regionInfos = new Dictionary<string, bi_t_region_info>();

                {
                    var tb = d.ExecuteToTable("SELECT * FROM dbo.bi_t_supcust_info WHERE supcust_flag='C'", null);
                    foreach (DataRow row in tb.Rows)
                    {
                        var sup = row.DataRowToModel<Model.bi_t_supcust_info>();
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
                        throw new Exception($"客户编号:[{info.supcust_no}]，已存在");

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
                    info.supcust_flag = "C";
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

        void ICus.Change(bi_t_supcust_info item)
        {
            item.update_time = System.DateTime.Now;
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(item, "supcust_no,supcust_flag");
        }

        void ICus.Delete(string supcust_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "";
            //
            sql = "select top 1  * from sm_t_salesheet where cust_no='" + supcust_no + "'  ";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("客户已被批发销售单引用，不能删除!");
            }
            //

            sql = "select  top 1 * from ic_t_inout_store_master where trans_no='D' and supcust_no='" + supcust_no + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("客户已被单据引用，不能删除!");
            }
            //
            sql = "delete from bi_t_supcust_info where supcust_flag='C' and supcust_no='" + supcust_no + "'";
            db.ExecuteScalar(sql, null);
        }

        public DataTable QuickSearchList(string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select top 100 supcust_no,sup_name from bi_t_supcust_info ";
            sql += "where supcust_flag='C' and display_flag='1' and (supcust_no like '%'+@keyword+'%' or sup_name like '%'+@keyword+'%') ";
            sql += "order by supcust_no ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@keyword", keyword),
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }


        string GetMaxCode(DB.IDB db, string sheet_type)
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + sheet_type + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = Helper.Conv.ToInt(obj);
                index += 1;
                if (index > 9999)
                {
                    index = 1;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + sheet_type + "'";
                db.ExecuteScalar(sql, null);
                return sheet_type + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }

        }

        DataTable ICus.GetCustItemList(string date1, string date2, string cust_no)
        {
            var condition_sql = "";
            if (cust_no != "") condition_sql += " and a.sup_no=@sup_no ";
            string sql = "select a.*,c.oper_name,e.oper_name as approve_man_name,f.sup_name ";
            sql += "from rp_t_cust_item_master a ";
            sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C') f on a.sup_no=f.supcust_no ";
            sql += "left join sa_t_operator_i c on a.oper_id=c.oper_id ";
            sql += "left join sa_t_operator_i e on a.approve_man=e.oper_id ";
            sql += "where convert(varchar(10),a.oper_date,120)>=@date1 and convert(varchar(10),a.oper_date,120)<=@date2 " + condition_sql;
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@sup_no", cust_no),
                new System.Data.SqlClient.SqlParameter("@date1", date1),
                new System.Data.SqlClient.SqlParameter("@date2", date2)
            };
            var tb = db.ExecuteToTable(sql, pars);

            return tb;
        }


        void ICus.GetCustItem(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,c.oper_name,e.oper_name as approve_man_name,f.sup_name ";
            sql += "from rp_t_cust_item_master a ";
            sql += "left join(select supcust_no,sup_name from bi_t_supcust_info where supcust_flag= 'C') f on a.sup_no = f.supcust_no ";
            sql += "left join sa_t_operator_i c on a.oper_id=c.oper_id ";
            sql += "left join sa_t_operator_i e on a.approve_man=e.oper_id ";
            sql += " where a.sheet_no=@sheet_no ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@sheet_no", sheet_no)
            };
            tb1 = db.ExecuteToTable(sql, pars);

            sql = @"select a.flow_id,a.sheet_no,a.item_no,a.unit_no,a.sup_no
,b.item_subno,b.item_name,b.item_size,c.sup_name,t.send_id+'/'+t.send_name send_type
from rp_t_cust_item_detail a left join bi_t_item_info b on a.item_no=b.item_no 
left join(select supcust_no,sup_name 
from bi_t_supcust_info where supcust_flag= 'S') c on a.sup_no=c.supcust_no 
left join bi_t_send_type t on t.send_id=a.send_type
where a.sheet_no=@sheet_no and isnull(a.display_flag,'1')='1' 
order by a.flow_id ";
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@sheet_no", sheet_no)
            };
            tb2 = db.ExecuteToTable(sql, pars);
        }

        void ICus.AddCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines, out string sheet_no)
        {
            sheet_no = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                sheet_no = GetMaxCode(d, "CI");
                ord.sheet_no = sheet_no;
                ord.oper_date = DateTime.Now;
                //
                d.Insert(ord);

                StringBuilder sb = new StringBuilder();
                int index = 0;
                int i = 0;
                string item_nos = "";
                foreach (Model.rp_t_cust_item_detail line in lines)
                {
                    ++i;
                    sb.Append("insert into rp_t_cust_item_detail(flow_id,sheet_no,item_no,item_subno,item_name,sup_no,unit_no,display_flag,send_type) ");
                    sb.Append("values(");
                    sb.Append("'" + i + "'");
                    sb.Append(",'" + sheet_no + "'");
                    sb.Append(",'" + line.item_no + "'");
                    sb.Append(",'" + line.item_subno + "'");
                    sb.Append(",'" + line.item_name + "'");
                    sb.Append(",'" + line.sup_no + "'");
                    sb.Append(",'" + line.unit_no + "'");
                    sb.Append(",'1','" + line.send_type + "'");
                    sb.Append(");\r\n");

                    item_nos += line.item_no + ",";
                    if (index > 5000)
                    {
                        item_nos = "'" + item_nos.Replace(",", "','") + "'";
                        string sql = "update rp_t_cust_item_detail set display_flag='0' where sheet_no in(select sheet_no from rp_t_cust_item_master where sup_no=@sup_no) and item_no in(" + item_nos + ") ";
                        var pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@sup_no", ord.sup_no)
                        };
                        d.ExecuteScalar(sql, pars);

                        d.ExecuteScalar(sb.ToString(), null);
                        sb.Clear();
                        index = 0;
                        item_nos = "";
                    }
                }

                if (sb.Length > 0)
                {
                    item_nos = "'" + item_nos.Replace(",", "','") + "'";
                    string sql = "update rp_t_cust_item_detail set display_flag='0' where sheet_no in(select sheet_no from rp_t_cust_item_master where sup_no=@sup_no) and item_no in(" + item_nos + ") ";
                    var pars = new System.Data.SqlClient.SqlParameter[]
                    {
                            new System.Data.SqlClient.SqlParameter("@sup_no", ord.sup_no)
                    };
                    d.ExecuteScalar(sql, pars);

                    d.ExecuteScalar(sb.ToString(), null);
                    sb.Clear();
                    index = 0;
                    item_nos = "";
                }
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Cus.AddCustItem()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void ICus.ChangeCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select approve_flag from rp_t_cust_item_master where sheet_no='" + ord.sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + ord.sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + ord.sheet_no + "]");
                    }
                }
                sql = "delete from rp_t_cust_item_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_cust_item_master where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                d.Insert(ord);

                StringBuilder sb = new StringBuilder();
                int index = 0;
                int i = 0;
                string item_nos = "";
                foreach (Model.rp_t_cust_item_detail line in lines)
                {
                    ++i;
                    sb.Append("insert into rp_t_cust_item_detail(flow_id,sheet_no,item_no,item_subno,item_name,sup_no,unit_no,display_flag,send_type) ");
                    sb.Append("values(");
                    sb.Append("'" + i + "'");
                    sb.Append(",'" + ord.sheet_no + "'");
                    sb.Append(",'" + line.item_no + "'");
                    sb.Append(",'" + line.item_subno + "'");
                    sb.Append(",'" + line.item_name + "'");
                    sb.Append(",'" + line.sup_no + "'");
                    sb.Append(",'" + line.unit_no + "'");
                    sb.Append(",'1','"+line.send_type+"'");
                    sb.Append(");\r\n");

                    item_nos += line.item_no + ",";
                    if (index > 5000)
                    {
                        item_nos = "'" + item_nos.Replace(",", "','") + "'";
                        sql = "update rp_t_cust_item_detail set display_flag='0' where sheet_no in(select sheet_no from rp_t_cust_item_master where sup_no=@sup_no) and item_no in(" + item_nos + ") ";
                        var pars = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@sup_no", ord.sup_no)
                        };
                        d.ExecuteScalar(sql, pars);

                        d.ExecuteScalar(sb.ToString(), null);
                        sb.Clear();
                        index = 0;
                        item_nos = "";
                    }
                }

                if (sb.Length > 0)
                {
                    item_nos = "'" + item_nos.Replace(",", "','") + "'";
                    sql = "update rp_t_cust_item_detail set display_flag='0' where sheet_no in(select sheet_no from rp_t_cust_item_master where sup_no=@sup_no) and item_no in(" + item_nos + ") ";
                    var pars = new System.Data.SqlClient.SqlParameter[]
                    {
                            new System.Data.SqlClient.SqlParameter("@sup_no", ord.sup_no)
                    };
                    d.ExecuteScalar(sql, pars);

                    d.ExecuteScalar(sb.ToString(), null);
                    sb.Clear();
                    index = 0;
                    item_nos = "";
                }
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Cus.ChangeCustItem()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void ICus.DeleteCustItem(string sheet_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                string sql = "select approve_flag from rp_t_cust_item_master where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                }
                sql = "delete from rp_t_cust_item_detail where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_cust_item_master where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Cus.DeleteCustItem()", ex.ToString(), sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void ICus.CheckCustItem(string sheet_no, string approve_man)
        {
            DB.IDB d = new DB.DBByAutoClose(AppSetting.conn);
            try
            {
                string sql = "update rp_t_cust_item_master set approve_flag='1',approve_man=@approve_man,approve_date=getdate() ";
                sql += "where sheet_no=@sheet_no ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@approve_man", approve_man),
                    new System.Data.SqlClient.SqlParameter("@sheet_no", sheet_no)
                };
                d.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Cus.CheckCustItem()", ex.ToString(), sheet_no);
                throw ex;
            }
        }



    }
}
