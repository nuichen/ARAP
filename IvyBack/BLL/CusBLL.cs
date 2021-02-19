using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using Model;
using IvyBack.Helper;
using System.Data;
using Model.BaseModel;


namespace IvyBack.BLL
{
    public class CusBLL : ICus
    {
        public DataTable SendType()
        {
            JsonRequest r = new JsonRequest();
            r.request("/cus?t=send_type");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            DataTable tb = r.GetDataTable("data");
            return tb;
        }
        public DataTable GetCarId(string id)
        {
            JsonRequest r = new JsonRequest();
            r.Write("id", id);
            r.request("/cus?t=get_supcar");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            DataTable tb = r.GetDataTable("data");
            return tb;
        }
        public DataTable GetCar()
        {
            JsonRequest r = new JsonRequest();
            r.request("/cus?t=get_car");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            DataTable tb = r.GetDataTable("data");
            return tb;
        }
        public DataTable GetCar(string id)
        {
            JsonRequest r = new JsonRequest();
            r.Write("id", id);
            r.request("/cus?t=get_carid");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            DataTable tb = r.GetDataTable("data");
            return tb;
        }
        public void AddCar(bi_t_supcust_car car)
        {
            JsonRequest r = new JsonRequest();
            r.Write("car", car);
            r.request("/cus?t=add_car");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
        public void ChangeCar(bi_t_supcust_car car)
        {
            JsonRequest r = new JsonRequest();
            r.Write("car", car);
            r.request("/cus?t=change_car");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void DeleteCar(string car_id)
        {
            JsonRequest r = new JsonRequest();
            r.Write("car_id", car_id);
            r.request("/cus?t=delete_car");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public bool Supcust_type(DataTable  dt)
        {
            JsonRequest r = new JsonRequest();
            r.Write("dt", dt);
            r.request("/cus?t=update_custType");

            //if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
            string b=r.Read("b");
            //DataTable tb = r.GetDataTable();
            return Convert.ToBoolean(b);
        }

        public DataTable SupCustType(string flag)
        {
            JsonRequest r = new JsonRequest();
            r.Write("flag",flag);
            r.request("/cus?t=supcust_type");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable("data");
            return tb;
        }

        public System.Data.DataTable Supcust_type(string sup_name, string flag, string type,string region)
        {
            JsonRequest r = new JsonRequest();
            r.Write("sup_name", sup_name);
            r.Write("flag", flag);
            r.Write("type", type);
            r.Write("region",region);

            r.request("/cus?t=cust_type");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }
        public System.Data.DataTable GetDataTable(string region_no, string keyword, int show_stop, int page_index, int page_size, out int total_count)
        {
            JsonRequest r = new JsonRequest();
            r.Write("page_index", page_index);
            r.Write("page_size", page_size);
            r.Write("region_no", region_no);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);

            r.request("/cus?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            total_count = r.ReadToInt("total_count");
            return tb;
        }
        public Page<bi_t_supcust_info> GetDataTable(string region_no, string keyword, int show_stop, Page<bi_t_supcust_info> page)
        {
            JsonRequest r = new JsonRequest();
            r.Write("region_no", region_no);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);
            r.Write<bi_t_supcust_info>(page);

            r.request("/cus?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page.Tb = r.GetDataTable();
            page.PageCount = r.ReadToInt("total_count");
            return page;
        }

        public bi_t_supcust_info GetItem(string supcust_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("supcust_no", supcust_no);

            r.request("/cus?t=get_item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bi_t_supcust_info sup = r.GetObject<bi_t_supcust_info>("data");

            return sup;
        }

        public string GetMaxCode()
        {
            JsonRequest r = new JsonRequest();

            r.request("/cus?t=max_code");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            string max_code = r.Read("code");

            return max_code;
        }

        public void Add(bi_t_supcust_info sup)
        {
            JsonRequest r = new JsonRequest();

            r.Write(sup);

            r.request("/cus?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
        public void Adds(List<bi_t_supcust_info> supcustInfos)
        {
            JsonRequest r = new JsonRequest();

            r.Write(supcustInfos);

            r.request("/cus?t=Adds");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Update(bi_t_supcust_info sup)
        {
            JsonRequest r = new JsonRequest();

            r.Write(sup);

            r.request("/cus?t=change");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }


        public void Del(Model.bi_t_supcust_info sup)
        {
            JsonRequest r = new JsonRequest();

            r.Write("supcust_no", sup.supcust_no);

            r.request("/cus?t=delete");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public DataTable QuickSearchList(string keyword)
        {
            JsonRequest r = new JsonRequest();

            r.Write("keyword", keyword);

            r.request("/cus?t=QuickSearchList");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }


        DataTable ICus.GetCustItemList(DateTime date1, DateTime date2, string cust_id)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("sup_no", cust_id);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus?t=get_custitem_list", w.ToString());
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

        void ICus.GetCustItem(string sheet_no, out DataTable tb1, out DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);

            r.request("/cus?t=get_custitem");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");
        }

        void ICus.AddCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines, out string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("sup_no", ord.sup_no);
            w.Append("memo", ord.memo);
            w.Append("oper_id", ord.oper_id);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss"));

            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("item_no");
            tb.Columns.Add("item_name");
            tb.Columns.Add("unit_no");
            tb.Columns.Add("sup_no");
            tb.Columns.Add("send_type");
            foreach (Model.rp_t_cust_item_detail line in lines)
            {
                tb.Rows.Add(line.sheet_no, line.item_no, line.item_name, line.unit_no, line.sup_no,line.send_type);
            }
            w.Append("lines", tb);

            Helper.IRequest req = new Helper.Request();


            var json = req.request("/cus?t=add_custitem", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            sheet_no = r.Read("sheet_no");
        }

        void ICus.ChangeCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("sup_no", ord.sup_no);
            w.Append("memo", ord.memo);
            w.Append("oper_id", ord.oper_id);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss"));

            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("item_no");
            tb.Columns.Add("item_name");
            tb.Columns.Add("unit_no");
            tb.Columns.Add("sup_no");
            tb.Columns.Add("send_type");
            foreach (Model.rp_t_cust_item_detail line in lines)
            {
                tb.Rows.Add(line.sheet_no, line.item_no, line.item_name, line.unit_no, line.sup_no, line.send_type);
            }
            //w.Append("lines", tb);
            w.Append("lines", tb);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus?t=change_custitem", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void ICus.DeleteCustItem(string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus?t=delete_custitem", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }

        }
        void ICus.CheckCustItem(string sheet_no, string approve_man)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("approve_man", approve_man);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus?t=check_custitem", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }

        }

    }
}
