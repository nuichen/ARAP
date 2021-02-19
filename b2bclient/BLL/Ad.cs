using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    class Ad:IAd
    {
        List<Model.ad> IAd.GetList(int pageSize,int pageIndex,out int total)
        {
            var req = new Request();
            var json = req.request("/ad?t=get_list", "{\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            total = Conv.ToInt(read.Read("total"));
            //
            var lst = new List<Model.ad>();
            if (read.Read("datas") != "")
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var item = new Model.ad();
                    lst.Add(item);
                    item.ad_id = r.Read("ad_id");
                    item.ad_type = Conv.ToInt(r.Read("ad_type"));
                    item.ad_name = r.Read("ad_name");
                    item.title_img = r.Read("title_img");
                    item.detail_img = r.Read("detail_img");
                    item.ad_text = r.Read("ad_text");
                    item.goods_ids = r.Read("goods_ids");
                }
            }
            return lst;
        }

        DataTable IAd.GetDt(int pageSize, int pageIndex, out int total)
        {
            var req = new Request();
            var json = req.request("/ad?t=get_list", "{\"pageSize\":\"" + pageSize + "\",\"pageIndex\":\"" + pageIndex + "\"}");
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

        void IAd.Add(Model.ad ad)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("ad_name", ad.ad_name);
            write.Append("ad_type", ad.ad_type.ToString());
            write.Append("title_img", ad.title_img);
            write.Append("detail_img", ad.detail_img);
            write.Append("ad_text", ad.ad_text);
            write.Append("goods_ids", ad.goods_ids); 
            var json = req.request("/ad?t=add", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IAd.Change(Model.ad ad)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("ad_id", ad.ad_id);
            write.Append("ad_name", ad.ad_name);
            write.Append("title_img", ad.title_img);
            write.Append("detail_img", ad.detail_img);
            write.Append("ad_text", ad.ad_text);
            write.Append("goods_ids", ad.goods_ids);
            var json = req.request("/ad?t=change", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        void IAd.Delete(string ad_id)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("ad_id", ad_id);
            var json = req.request("/ad?t=delete", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

        Model.ad IAd.Select(string ad_id)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("ad_id", ad_id);
            var json = req.request("/ad?t=select", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            var ad = new Model.ad();
            ad.ad_id = read.Read("ad_id");
            ad.ad_type = Conv.ToInt(read.Read("ad_type"));
            ad.ad_name = read.Read("ad_name");
            ad.title_img = read.Read("title_img");
            ad.detail_img = read.Read("detail_img");
            ad.ad_text = read.Read("ad_text");
            ad.goods_ids = read.Read("goods_ids");
            return ad;
        }

        void IAd.AddMemberAd(string ad_href,string ad_img_url)
        {
            var req = new Request();
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("ad_href", ad_href);
            write.Append("ad_img_url", ad_img_url);
            var json = req.request("/ad?t=add_member_ad", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }
    }
}
