using System.Collections.Generic;
using System.Data;

namespace IvyBack.IBLL.OnLine
{
    interface IAd
    {
        List<Model.ad> GetList(int pageSize,int pageIndex,out int total);
        DataTable GetDt(int pageSize, int pageIndex, out int total);
        void Add(Model.ad ad );
        void Change(Model.ad ad);
        void Delete(string ad_id);
        Model.ad Select(string ad_id);
        void AddMemberAd(string ad_href, string ad_img_url);
    }
}
