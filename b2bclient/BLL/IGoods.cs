using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;

namespace b2bclient.BLL
{
    interface IGoods
    {
        List<Model.goods> GetList(string cls_no, string keyword, string theme, string is_no_show_mall, int pageSize, int pageIndex, out int total);
        DataTable GetDt(string cls_no, string keyword, string theme, string is_no_show_mall, int pageSize, int pageIndex, out int total);
        void Add(string goods_no,string goods_name,string long_name, string cls_id,string small_img_url,
            string large_img_url,string detail_img_url,string themes, string text,string is_show_mall,List<Model.goods_std> lststd);

        void Change(string goods_id, string goods_no, string goods_name,string long_name, string cls_id, string small_img_url,
            string large_img_url, string detail_img_url,string themes, string text,string is_show_mall,List<Model.goods_std> lststd);

        void GetInfo(string goods_id, out string goods_no, out string goods_name, out string long_name, out string cls_id, out string cls_name,
            out string small_img_url, out string small_img_full_url, out string large_img_url, out string large_img_full_url, out string detail_img_url,
            out string detail_img_full_url,out string themes,out string text, out string status,out string is_show_mall,out List<Model.goods_std> lststd);

        void GetInfo(string goods_no, out string goods_id, out string new_goods_no, out string goods_name, out string long_name, out string cls_id,
            out string cls_name, out string small_img_url, out string small_img_full_url, out string large_img_url, out string large_img_full_url, out string detail_img_url,
            out string detail_img_full_url, out string themes, out string text, out string status, out string is_show_mall, out List<Model.goods_std> lststd);

        void Delete(string goods_id);

        void Stop(string goods_id);

        void Restart(string goods_id);

        Image getImage(string url);

        void UploadImage(Image img, out string img_path);

        void ChangePriceShort(string goods_id, string prices);

        void ChangeStock(string goods_id, string stock_qty);

        void InputSpe(int goods_id,string color, string size,string prices);

        List<body.theme> GetThemeList();
    }
}
