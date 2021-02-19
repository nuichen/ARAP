using System;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL;
using IvyTran.IBLL.OnLine;
using Model;

namespace IvyTran.BLL.OnLine
{
    public class Ad : IAd
    {
        private string createOrdId()
        {
            string str = "";
            //
            string year = DateTime.Now.Year.ToString().Substring(2);//2位
            string day = DateTime.Now.DayOfYear.ToString().PadLeft(3, '0');//3位
            string time = Math.Floor(System.DateTime.Now.TimeOfDay.TotalMinutes).ToString().PadLeft(5, '0');//5位
            Random random = new Random();
            string rnd = random.Next(1, 999999).ToString().PadLeft(6, '0');//6位
            //
            str = year + day + time + rnd;//16位
            //
            return str;
        }

        DataTable IAd.GetList(int pageSize, int pageIndex, out int total)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            int last = (pageIndex - 1) * pageSize;
            string sql = "select * from ad where mc_id = @mc_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mc_id",Global.McId)
            };
            var dt = db.ExecuteToTable(sql, pars);
            total = dt.Rows.Count;
            string sql_s = "select top " + pageSize + " ad_id,isnull(ad_type,'0') ad_type,ad_name,title_img,detail_img,ad_text,goods_ids ";
            sql_s += "from ad where mc_id = @mc_id and ad_id not in (select top " + last + " ad_id from ad where mc_id = @mc_id )";
            var pars_s = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mc_id",Global.McId)
            };
            var dt_s = db.ExecuteToTable(sql_s, pars_s);
            return dt_s;
        }

        void IAd.Add(ad ad)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            ad.ad_id = createOrdId();
            ad.status = "1";
            ad.create_date = DateTime.Now;
            ad.mc_id = Global.McId;
            db.Insert(ad);
        }

        void IAd.Change(ad ad)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DAL.AdDAL adDAL = new DAL.AdDAL(db);
            var item = adDAL.SelectById(ad.ad_id);
            if (item == null)
            {
                throw new ExceptionBase("数据非法！");
            }
            string sql = "update ad set ad_name = @ad_name ,title_img = @title_img ,detail_img = @detail_img,ad_text = @ad_text ,goods_ids = @goods_ids where ad_id = @ad_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@ad_id",ad.ad_id),
                new System.Data.SqlClient.SqlParameter("@ad_name",ad.ad_name),
                new System.Data.SqlClient.SqlParameter("@title_img",ad.title_img),
                new System.Data.SqlClient.SqlParameter("@detail_img",ad.detail_img),
                new System.Data.SqlClient.SqlParameter("@ad_text",ad.ad_text),
                new System.Data.SqlClient.SqlParameter("@goods_ids",ad.goods_ids)
            };
            db.ExecuteScalar(sql, pars);
        }

        void IAd.Delete(string ad_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DAL.AdDAL adDAL = new DAL.AdDAL(db);
            var item = adDAL.SelectById(ad_id);
            if (item == null)
            {
                throw new Exception("数据非法！");
            }
            string sql = "delete from ad where ad_id = @ad_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@ad_id",ad_id)
            };
            db.ExecuteScalar(sql, pars);
        }

        ad IAd.SelectById(string ad_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DAL.AdDAL adDAL = new DAL.AdDAL(db);
            var item = adDAL.SelectById(ad_id);
            if (item == null)
            {
                throw new Exception("数据非法！");
            }
            return item;
        }

        void IAd.AddMemberAd(string ad_href, string ad_img_url)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = "insert into tb_member_ad(ad_img_url,ad_href_url,status,create_time) values(@ad_img_url,@ad_href_url,'1',getdate()) ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@ad_img_url",ad_img_url),
                new System.Data.SqlClient.SqlParameter("@ad_href_url",ad_href)
            };
            db.ExecuteScalar(sql, pars);
        }
    }
}