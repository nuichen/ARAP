using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    interface IGoodsCls
    {
        List<Model.goods_cls> GetList(string parent_cls_no,string keyword, int pageSize, int pageIndex, out int total);
        DataTable GetDt(string parent_cls_no, string keyword, int pageSize, int pageIndex, out int total);
        List<Model.goods_cls> GetListForMenu();
        List<body.goods_cls> GetListForSupCustGroup();
        DataTable GetDtForSupCustGroup();
        Model.goods_cls GetClsInfo(string cls_no);
        void Add(int parent_cls_id, string cls_no, string cls_name, string is_show_mall, string cus_group);
        void Change(string cls_id, string cls_no, string cls_name, string is_show_mall, string cus_group);
        void Delete(string cls_id);
        void Stop(string cls_id);
        void Restart(string cls_id);
        void GetIcon(string cls_id,out string img_url,out string img_full_url);

        string GetGroupCls(string group_no);
        void SaveGroupCls(string group_no, string item_clsnos);
    }
}
