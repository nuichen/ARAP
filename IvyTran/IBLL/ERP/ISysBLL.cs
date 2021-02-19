namespace IvyTran.IBLL.ERP
{
    public interface ISysBLL
    {
        body.merchant GetMerchantByKey(string key);
        body.merchant GetMerchantById(string id);
        body.wxpay GetMerWxpayById(string id);
        body.alipay GetMerAlipayById(string id);
    }
}