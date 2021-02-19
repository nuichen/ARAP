using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IvyTran.BLL.ERP;
using IvyTran.IBLL.ERP;

namespace IvyTran.Helper
{
    public class VerifyHelper
    {
        public bool verify_merchant(string mer_key, out string mer_id, out string errMsg)
        {
            ISysBLL bll = new SysBLL();
            var mer = bll.GetMerchantByKey(mer_key);
            if (mer == null)
            {
                mer_id = "";
                errMsg = "不存在商户";
                return false;
            }
            if (mer.status != "1")
            {
                mer_id = "";
                errMsg = "商户已停用";
                return false;
            }
            mer_id = mer.mer_id;
            errMsg = "";
            return true;
        }
    }
}