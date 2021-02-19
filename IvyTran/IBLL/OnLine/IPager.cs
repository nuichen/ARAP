using System.Data;

namespace IvyTran.IBLL.OnLine
{
    public interface IPager
    {
        void ClearData(string key);
        void GetData(string key, out DataTable dt, int pageSize, int pageIndex, out int total);
        void GetDataWithTotal(string key, out DataTable dt, int pageSize, int pageIndex, out int total, string field, string fields);
    }
}