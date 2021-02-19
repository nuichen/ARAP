using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IvyBack.IBLL;

namespace IvyBack.BLL
{
    public class CommonBLL : ICommonBLL
    {
        string ICommonBLL.IsServer()
        {
            try
            {
                Helper.JsonRequest r = new Helper.JsonRequest();

                r.request("/common?t=connect_server");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

                string is_connect = r.Read("is_connect");
                return is_connect;
            }
            catch (Exception)
            {
                return "0";
            }

        }
        string ICommonBLL.IsServer(string ip, string port)
        {
            try
            {
                Helper.JsonRequest r = new Helper.JsonRequest();

                r.request("/common?t=connect_server");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

                string is_connect = r.Read("is_connect");
                return is_connect;
            }
            catch (Exception)
            {
                return "0";
            }
        }

        List<Model.bi_t_supcust_info> ICommonBLL.GetCustList(string keyword)
        {
            try
            {
                Helper.IRequest req = new Helper.Request();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("page_index", "1");
                write.Append("page_size", "500");
                write.Append("region_no", "");
                write.Append("show_stop", "0");
                write.Append("keyword", keyword);

                var json = req.request("/cus?t=get_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                List<Model.bi_t_supcust_info> lst = new List<Model.bi_t_supcust_info>();
                if (read.Read("data") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                    {
                        Model.bi_t_supcust_info item = new Model.bi_t_supcust_info();
                        item.supcust_no = r.Read("supcust_no");
                        item.sup_name = item.supcust_no + "/" + r.Read("sup_name");
                        lst.Add(item);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("CommonBLL.GetCustList()", ex.ToString(), null);
                throw ex;
            }
        }

        List<Model.bi_t_supcust_info> ICommonBLL.GetSupList(string keyword)
        {
            try
            {
                Helper.IRequest req = new Helper.Request();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("page_index", "1");
                write.Append("page_size", "500");
                write.Append("region_no", "");
                write.Append("show_stop", "0");
                write.Append("keyword", keyword);

                var json = req.request("/sup?t=get_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                List<Model.bi_t_supcust_info> lst = new List<Model.bi_t_supcust_info>();
                if (read.Read("data") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                    {
                        Model.bi_t_supcust_info item = new Model.bi_t_supcust_info();
                        item.supcust_no = r.Read("supcust_no");
                        item.sup_name = item.supcust_no + "/" + r.Read("sup_name");
                        lst.Add(item);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("CommonBLL.GetSupList()", ex.ToString(), null);
                throw ex;
            }
        }

  

        DataTable ICommonBLL.GetAllSupList()
        {
            try
            {
                Helper.IRequest req = new Helper.Request();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("page_index", "1");
                write.Append("page_size", "10000");
                write.Append("region_no", "");
                write.Append("show_stop", "0");
                write.Append("keyword", "");

                var json = req.request("/sup?t=get_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("supcust_no");
                dt.Columns.Add("sup_name");
                if (read.Read("data") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                    {
                        dt.Rows.Add(r.Read("supcust_no"), r.Read("sup_name"));
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("CommonBLL.GetAllSupList()", ex.ToString(), null);
                throw ex;
            }
        }

        DataTable ICommonBLL.GetGoodsList()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("item_clsno", "");
            w.Append("keyword", "");
            w.Append("page_index", "1");
            w.Append("page_size", "20000");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/item?t=get_list", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                if (r.Read("data").Length < 10)
                {
                    return new DataTable();
                }
                IBLL.ICommonBLL bll = new BLL.CommonBLL();
                var tb = bll.GetDataTable(r.ReadList("data"));
                return tb;
            }
        }
        DataTable ICommonBLL.GetAllCustList(bool isJail)
        {
            try
            {
                Helper.IRequest req = new Helper.Request();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("page_index", "1");
                write.Append("page_size", "10000");
                write.Append("region_no", "");
                write.Append("show_stop", "0");
                write.Append("keyword", "");

                if (isJail)
                {
                    write.Append("is_jail", "1");
                }

                var json = req.request("/cus?t=get_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("supcust_no");
                dt.Columns.Add("sup_name");
                if (read.Read("data") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                    {
                        dt.Rows.Add(r.Read("supcust_no"), r.Read("sup_name"));
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("CommonBLL.GetAllCustList()", ex.ToString(), null);
                throw ex;
            }
        }
        DataTable ICommonBLL.GetBranchList()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("code_len", "4");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/branch?t=get_list", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                if (r.Read("data").Length < 10)
                {
                    return new DataTable();
                }
                IBLL.ICommonBLL bll = new BLL.CommonBLL();
                var tb = bll.GetDataTable(r.ReadList("data"));
                return tb;
            }
        }

        DataTable ICommonBLL.GetPeopleList()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("page_index", "1");
            w.Append("page_size", "10000");
            w.Append("dep_no", "");
            w.Append("show_stop", "0");
            w.Append("keyword", "");

            Helper.IRequest req = new Helper.Request();
            var json = req.request("/people?t=get_list", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                if (r.Read("data").Length < 10)
                {
                    return new DataTable();
                }
                IBLL.ICommonBLL bll = new BLL.CommonBLL();
                var tb = bll.GetDataTable(r.ReadList("data"));
                return tb;
            }
        }

        DataTable ICommonBLL.GetDataTable(List<ReadWriteContext.IReadContext> lst)
        {

            DataTable dt = new DataTable();

            foreach (ReadWriteContext.IReadContext r in lst)
            {
                Dictionary<string, object> dic = r.ToDictionary();

                if (dt.Columns.Count < 1)
                {
                    foreach (string key in dic.Keys)
                    {
                        dt.Columns.Add(key);
                    }
                }

                DataRow dr = dt.NewRow();
                foreach (string key in dic.Keys)
                {
                    int i = dt.Columns.IndexOf(key);
                    dr[i] = dic[key];
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }






    }
}
