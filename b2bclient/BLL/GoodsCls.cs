using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    class GoodsCls:IGoodsCls 
    {
        List<Model.goods_cls> IGoodsCls.GetList(string parent_cls_no,string keyword, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=get_list", "{\"parent_cls_no\":\"" + parent_cls_no + "\",\"keyword\":\"" + keyword + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var lst = new List<Model.goods_cls>();
            if (read.Read("datas") != "")
            { 
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var item = new Model.goods_cls();
                    lst.Add(item);
                    item.cls_id = r.Read("cls_id");
                    item.cls_no = r.Read("cls_no");
                    item.cls_name = r.Read("cls_name");
                    item.status = r.Read("status");
                    item.is_show_mall = r.Read("is_show_mall");
                    item.cus_group = r.Read("cus_group");
                    item.supcust_groupname = r.Read("supcust_groupname");
                }
            }
            return lst;
        }

        DataTable IGoodsCls.GetDt(string parent_cls_no, string keyword, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=get_list", "{\"parent_cls_no\":\"" + parent_cls_no + "\",\"keyword\":\"" + keyword + "\",\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            if (read.Read("datas").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("datas"));

            return tb;
        }


        List<Model.goods_cls> IGoodsCls.GetListForMenu()
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=get_list_for_menu", "{\"is_first_level\":\"0\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            var lst = new List<Model.goods_cls>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var item = new Model.goods_cls();
                    lst.Add(item);
                    item.cls_id = r.Read("cls_id");
                    item.cls_no = r.Read("cls_no");
                    item.cls_name = r.Read("cls_name");
                    item.status = r.Read("status");
                    item.is_show_mall = r.Read("is_show_mall");
                }
            }
            return lst;
        }

        List<body.goods_cls> IGoodsCls.GetListForSupCustGroup()
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=get_list_for_menu", "{\"is_first_level\":\"1\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            var lst = new List<body.goods_cls>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var item = new body.goods_cls();
                    lst.Add(item);
                    item.is_check = false;
                    item.item_clsno = r.Read("cls_no");
                    item.item_clsname = r.Read("cls_name");
                }
            }
            return lst;
        }

        DataTable IGoodsCls.GetDtForSupCustGroup()
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=get_list_for_menu", "{\"is_first_level\":\"0\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            if (read.Read("datas").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("datas"));
            if (tb.Rows.Count > 0) 
            {
                tb.Columns.Add("is_check");
                foreach (DataRow dr in tb.Rows) 
                {
                    dr["is_check"] = "0";
                }
            }

            return tb;
        }

        Model.goods_cls IGoodsCls.GetClsInfo(string cls_no)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=get_cls_info", "{\"cls_no\":\"" + cls_no + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            else
            {
                var item = new Model.goods_cls();
                item.cls_id = read.Read("cls_id");
                item.cls_no = read.Read("cls_no");
                item.cls_name = read.Read("cls_name");
                item.status = read.Read("status");
                item.is_show_mall = read.Read("is_show_mall");
                item.cus_group = read.Read("cus_group");
                item.supcust_groupname = read.Read("supcust_groupname");
                return item;
            }
        }

        void IGoodsCls.Add(int parent_cls_id, string cls_no, string cls_name ,string img_url,string is_show_mall)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=add", "{\"parent_cls_id\":\"" + parent_cls_id + "\",\"cls_no\":\"" + cls_no + "\",\"cls_name\":\"" + cls_name + "\",\"icon_img_url\":\"" + img_url + "\",\"is_show_mall\":\"" + is_show_mall + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoodsCls.Change(string cls_id, string cls_no, string cls_name, string is_show_mall,string cus_group)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=change", "{\"cls_id\":\"" + cls_id + "\",\"cls_no\":\"" + cls_no + "\",\"cls_name\":\"" + cls_name + "\",\"cus_group\":\"" + cus_group + "\",\"is_show_mall\":\"" + is_show_mall + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoodsCls.Delete(string cls_id)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=delete", "{\"cls_id\":\"" + cls_id +  "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoodsCls.Stop(string cls_id)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=stop", "{\"cls_id\":\"" + cls_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoodsCls.Restart(string cls_id)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=restart", "{\"cls_id\":\"" + cls_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoodsCls.GetIcon(string cls_id,out string img_url,out string img_full_url)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=get_icon", "{\"cls_id\":\"" + cls_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            img_url = read.Read("icon_img_url");
            img_full_url = read.Read("icon_img_full_url");
        }

        string IGoodsCls.GetGroupCls(string group_no)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=get_group_cls", "{\"group_no\":\"" + group_no + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            var item_clsnos = read.Read("item_clsnos");
            return item_clsnos;
        }

        void IGoodsCls.SaveGroupCls(string group_no,string item_clsnos)
        {
            var req = new Request();
            var json = req.request("/goods_cls?t=save_group_cls", "{\"group_no\":\"" + group_no + "\",\"item_clsnos\":\"" + item_clsnos + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }
    }
}
