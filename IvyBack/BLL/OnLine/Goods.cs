using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using IvyBack.Helper;
using IvyBack.IBLL.OnLine;

namespace IvyBack.BLL.OnLine
{
    public class Goods : IGoods
    {
        List<Model.goods> IGoods.GetList(string cls_no, string keyword, string theme, string is_no_show_mall, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/goods?t=select_key", "{\"cls_no\":\"" + cls_no + "\",\"keyword\":\"" + keyword + "\",\"theme\":\"" + theme + "\",\"is_no_show_mall\":\"" + is_no_show_mall + "\",\"pageIndex\":\"" + pageIndex + "\",\"pageSize\":\"" + pageSize + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var lst = new List<Model.goods>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var item = new Model.goods();
                    lst.Add(item);
                    item.goods_id = r.Read("goods_id");
                    item.goods_no = r.Read("goods_no");
                    item.goods_name = r.Read("goods_name");
                    item.is_show_mall = r.Read("is_show_mall");
                    item.status = r.Read("status");
                    item.cls_id = r.Read("cls_id");
                    item.cls_name = r.Read("cls_name");
                    item.themes = r.Read("themes");
                }
            }
            return lst;
        }

        DataTable IGoods.GetDt(string cls_no, string keyword, string theme, string is_no_show_mall, int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/OnLine/goods?t=select_key", "{\"cls_no\":\"" + cls_no + "\",\"keyword\":\"" + keyword + "\",\"theme\":\"" + theme + "\",\"is_no_show_mall\":\"" + is_no_show_mall + "\",\"pageIndex\":\"" + pageIndex + "\",\"pageSize\":\"" + pageSize + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            if (read.Read("datas").Length < 10)
            {
                return new DataTable();
            }
            var tb = Conv.GetDataTable(read.ReadList("datas"));

            return tb;
        }


        void IGoods.Add(string goods_no, string goods_name, string long_name, string cls_id, string small_img_url,
            string large_img_url, string detail_img_url, string themes, string text, string is_show_mall, List<Model.goods_std> lststd)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("goods_no", goods_no);
            write.Append("goods_name", goods_name);
            write.Append("long_name", long_name);
            write.Append("cls_id", cls_id);
            write.Append("small_img_url", small_img_url);
            write.Append("large_img_url", large_img_url);
            write.Append("detail_img_url", detail_img_url);
            write.Append("themes", themes);
            write.Append("text", text);
            var dt = new DataTable();
            dt.Columns.Add("prices");
            dt.Columns.Add("is_default");
            dt.Columns.Add("qty");
            foreach (Model.goods_std std in lststd)
            {
                dt.Rows.Add(std.prices, std.is_default, std.qty);
            }
            write.Append("datas", dt);
            var json = req.request("/OnLine/goods?t=add", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoods.Change(string goods_id, string goods_no, string goods_name, string long_name, string cls_id, string small_img_url,
            string large_img_url, string detail_img_url, string themes, string text, string is_show_mall, List<Model.goods_std> lststd)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("goods_id", goods_id);
            write.Append("goods_no", goods_no);
            write.Append("goods_name", goods_name);
            write.Append("long_name", long_name);
            write.Append("cls_id", cls_id);
            write.Append("small_img_url", small_img_url);
            write.Append("large_img_url", large_img_url);
            write.Append("detail_img_url", detail_img_url);
            write.Append("themes", themes);
            write.Append("text", text);
            write.Append("is_show_mall", is_show_mall);
            var dt = new DataTable();
            dt.Columns.Add("prices");
            dt.Columns.Add("is_default");
            dt.Columns.Add("qty");
            foreach (Model.goods_std std in lststd)
            {
                dt.Rows.Add(std.prices, std.is_default, std.qty);
            }
            write.Append("datas", dt);
            var json = req.request("/OnLine/goods?t=change", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoods.GetInfo(string goods_id, out string goods_no, out string goods_name, out string long_name, out string cls_id, out string cls_name,
            out string small_img_url, out string small_img_full_url, out string large_img_url, out string large_img_full_url, out string detail_img_url,
            out string detail_img_full_url, out string themes, out string text, out string status, out string is_show_mall, out List<Model.goods_std> lststd)
        {
            var req = new Request();
            var json = req.request("/OnLine/goods?t=get_info", "{\"goods_id\":\"" + goods_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            goods_no = read.Read("goods_no");
            goods_name = read.Read("goods_name");
            long_name = read.Read("long_name");
            cls_id = read.Read("cls_id");
            cls_name = read.Read("cls_name");
            small_img_url = read.Read("small_img_url");
            small_img_full_url = read.Read("small_img_full_url");
            large_img_url = read.Read("large_img_url");
            large_img_full_url = read.Read("large_img_full_url");
            detail_img_url = read.Read("detail_img_url");
            detail_img_full_url = read.Read("detail_img_full_url");
            themes = read.Read("themes");
            text = read.Read("text");
            status = read.Read("status");
            is_show_mall = read.Read("is_show_mall");
            List<Model.goods_std> lsttmp = new List<Model.goods_std>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var std = new Model.goods_std();
                    std.prices = r.Read("prices");
                    std.is_default = r.Read("is_default");
                    float qty = Conv.ToFloat(r.Read("qty"));
                    std.qty = qty;
                    lsttmp.Add(std);
                }
            }
            lststd = lsttmp;
        }

        void IGoods.GetInfo(string goods_no, out string goods_id, out string new_goods_no, out string goods_name, out string long_name, out string cls_id, out string cls_name,
            out string small_img_url, out string small_img_full_url, out string large_img_url, out string large_img_full_url, out string detail_img_url,
            out string detail_img_full_url, out string themes, out string text, out string status, out string is_show_mall, out List<Model.goods_std> lststd)
        {
            var req = new Request();
            var json = req.request("/OnLine/goods?t=get_info_by_no", "{\"goods_no\":\"" + goods_no + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            goods_id = read.Read("goods_id");
            new_goods_no = read.Read("goods_no");
            goods_name = read.Read("goods_name");
            long_name = read.Read("long_name");
            cls_id = read.Read("cls_id");
            cls_name = read.Read("cls_name");
            small_img_url = read.Read("small_img_url");
            small_img_full_url = read.Read("small_img_full_url");
            large_img_url = read.Read("large_img_url");
            large_img_full_url = read.Read("large_img_full_url");
            detail_img_url = read.Read("detail_img_url");
            detail_img_full_url = read.Read("detail_img_full_url");
            themes = read.Read("themes");
            text = read.Read("text");
            status = read.Read("status");
            is_show_mall = read.Read("is_show_mall");
            List<Model.goods_std> lsttmp = new List<Model.goods_std>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var std = new Model.goods_std();
                    std.prices = r.Read("prices");
                    std.is_default = r.Read("is_default");
                    float qty = Conv.ToFloat(r.Read("qty"));
                    std.qty = qty;
                    lsttmp.Add(std);
                }
            }
            lststd = lsttmp;
        }

        void IGoods.Delete(string goods_id)
        {
            var req = new Request();
            var json = req.request("/OnLine/goods?t=delete", "{\"goods_id\":\"" + goods_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoods.Stop(string goods_id)
        {
            var req = new Request();
            var json = req.request("/OnLine/goods?t=stop", "{\"goods_id\":\"" + goods_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoods.Restart(string goods_id)
        {
            var req = new Request();
            var json = req.request("/OnLine/goods?t=start", "{\"goods_id\":\"" + goods_id + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {

                throw new Exception(read.Read("errMsg"));
            }
        }

        System.Drawing.Image IGoods.getImage(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Timeout = 60 * 1000;
                req.ReadWriteTimeout = 60 * 1000;
                //
                req.Method = "GET";
                //
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream stream = res.GetResponseStream();
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                stream.Close();
                //
                return img;
            }
            catch (Exception)
            {
                return null;
            }

        }

        void IGoods.UploadImage(System.Drawing.Image img, out string img_path)
        {
            string url = AppSetting.svr + "/upload_img?t=upload";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = 60 * 1000;
            req.ReadWriteTimeout = 60 * 1000;
            //
            req.Headers.Add("img_type", "jpg");
            //
            System.IO.MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var bs = ms.ToArray();
            req.Method = "POST";
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(bs, 0, bs.Length);
            requestStream.Close();
            //
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();
            stream.Close();
            reader.Close();
            //
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(str);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            img_path = read.Read("img_path");

        }

        void IGoods.ChangePriceShort(string goods_id, string prices)
        {
            var req = new Request();
            var json = req.request("/OnLine/goods?t=change_price_short", "{\"goods_id\":\"" + goods_id + "\",\"prices\":\"" + prices + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoods.ChangeStock(string goods_id, string stock_qty)
        {

            var req = new Request();

            var json = req.request("/OnLine/goods?t=change_stock", "{\"goods_id\":\"" + goods_id + "\",\"stock_qty\":\"" + stock_qty + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {

                throw new Exception(read.Read("errMsg"));
            }
        }

        void IGoods.InputSpe(int goods_id, string color, string size, string prices)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("goods_id", goods_id.ToString());
            write.Append("color", color);
            write.Append("size", size);
            write.Append("prices", prices);


            var json = req.request("/OnLine/goods?t=input_spe", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        List<Model.theme> IGoods.GetThemeList()
        {
            var req = new Request();
            var json = req.request("/OnLine/goods?t=get_theme_list", "");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            var lst = new List<Model.theme>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var item = new Model.theme();
                    lst.Add(item);
                    item.theme_id = r.Read("theme_id");
                    item.theme_code = r.Read("theme_code");
                    item.theme_name = r.Read("theme_name");
                }
            }
            return lst;
        }
    }
}
