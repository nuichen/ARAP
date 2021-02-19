using System.Data;

namespace IvyTran.IBLL.ERP
{
    public interface ICountDetail
    {
        bool Insert(string branch_no, DataTable tb, out string sheet_no);
    }
}