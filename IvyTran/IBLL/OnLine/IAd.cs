using System.Data;
using Model;

namespace IvyTran.IBLL.OnLine
{
    public interface IAd
    {
        DataTable GetList(int pageSize,int pageIndex,out int total);
        void Add(ad ad);
        void Delete(string ad_id);
        void Change(ad ad);
        ad SelectById(string ad_id);
        void AddMemberAd(string ad_href, string ad_img_url);
    }
}