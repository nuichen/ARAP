using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Server.IBLL
{
    public interface IAssApiBLL
    {
        DataTable GetItemInfoList(string last_req, int page_no, out int total);
        DataTable GetItemUnitList(string last_req, int page_no, out int total);
        DataTable GetItemBarcodeList(string last_req, int page_no, out int total);
        DataTable GetItemClsList(string last_req);
        DataTable GetSupList(string last_req, int page_no, out int total);
        DataTable GetItemStock(string branch_no, string keyword);
    }
}