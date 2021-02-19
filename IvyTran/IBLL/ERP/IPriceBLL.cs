using System.Data;

namespace IvyTran.IBLL.ERP
{
    public interface IPriceBLL
    {
        DataTable GetCustPriceList(string cust_id);
        DataTable GetSupPriceList(string sup_id);

        decimal GetCusItemPrice(string cust_id, string item_no, string type);
        decimal GetSupItemPrice(string sup_id, string item_no, string type);
        decimal GetLastInPrice(string item_no);
    }
}
