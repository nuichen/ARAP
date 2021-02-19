namespace IvyTran.BLL.OnLine
{
    public class message_sale_demo
    {
         /*
        通用：
        wx_openid:微信用户id
        template_id:模板id
        redirect_url:跳转页面链接，可传空值
        head_str:模板第一行,自定义内容
        foot_str:模板最后一行,自定义内容
        
        注：所有参数均为string类型
        */

        /*
        零售-外卖订单接单确认通知
        标题-下单成功提醒
        name:商家名称
        order_time：下单时间
        menu: 商品明细
        price：订单金额
        */
        public string get_order_success_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string name, string order_time, string menu, string price)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword1\":{" +
                                        "\"value\":\"" + name + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword2\":{" +
                                        "\"value\":\"" + order_time + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword3\":{" +
                                        "\"value\":\"" + menu + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword4\":{" +
                                        "\"value\":\"" + price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }

        /*
        零售-外卖订单拒单通知
        标题-订单取消通知
        order_id：订单编号
        price: 订单金额
        */
        public string get_order_cancel_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string order_id, string price)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword1\":{" +
                                        "\"value\":\"" + order_id + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword2\":{" +
                                        "\"value\":\"" + price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }

        /*
        零售-外卖订单送货通知
        标题-配送通知
        order_context：订单详情
        name: 门店名称
        mobile:联系电话
        */
        public string get_order_send_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string order_context, string name, string mobile)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword1\":{" +
                                        "\"value\":\"" + order_context + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword2\":{" +
                                        "\"value\":\"" + name + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword3\":{" +
                                        "\"value\":\"" + mobile + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }


        /*
        零售-外卖订单退款通知
        标题-退款通知
        name：门店名称
        order_id:订单编号
        order_context:订单详情
        price:退款金额
        */
        public string get_order_refund_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string name, string order_id, string order_context, string price)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword1\":{" +
                                        "\"value\":\"" + name + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword2\":{" +
                                        "\"value\":\"" + order_id + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword3\":{" +
                                        "\"value\":\"" + order_context + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword4\":{" +
                                        "\"value\":\"" + price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }


        /*
        零售-商城订单接单通知
        标题-接单通知
        order_id：订单编号
        price:预计总价
        order_type：订单类型
        time:接单时间
        */
        public string get_mall_order_accept_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string order_id, string price, string order_type, string time)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword1\":{" +
                                        "\"value\":\"" + order_id + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword2\":{" +
                                        "\"value\":\"" + price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword3\":{" +
                                        "\"value\":\"" + order_type + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword4\":{" +
                                        "\"value\":\"" + time + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }


        /*
        零售-自提订单到货通知
        标题-自提通知
        order_id：订单编号
        name:商品名称
        shop_name：自提门店
        add:门店地址
        time:营业时间
        */
        public string get_mall_order_self_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string order_id, string name, string shop_name, string add, string time)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword1\":{" +
                                        "\"value\":\"" + order_id + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword2\":{" +
                                        "\"value\":\"" + name + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword3\":{" +
                                        "\"value\":\"" + shop_name + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword4\":{" +
                                        "\"value\":\"" + add + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword5\":{" +
                                        "\"value\":\"" + time + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }




        /*
        零售-储值卡冲值成功通知
        标题-冲值成功提醒
        price：冲值金额
        time：冲值时间
        total_price：帐户总额
        */
        public string get_recharge_success_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string price, string time, string total_price)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword1\":{" +
                                        "\"value\":\"" + price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword2\":{" +
                                        "\"value\":\"" + time + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword3\":{" +
                                        "\"value\":\"" + total_price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }


        /*
        零售-储值卡消费通知
        标题-储值卡消费提醒
        name：姓名
        card_id:储值卡号
        use_price：消费金额
        ex_price:此前余额
        last_price:此后余额
        */
        public string get_card_charge_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string name, string card_id, string use_price, string ex_price, string last_price)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword1\":{" +
                                        "\"value\":\"" + name + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword2\":{" +
                                        "\"value\":\"" + card_id + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword3\":{" +
                                        "\"value\":\"" + use_price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword4\":{" +
                                        "\"value\":\"" + ex_price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"keyword5\":{" +
                                        "\"value\":\"" + last_price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }


        /*
        零售-会员登记成功通知
        标题-成为会员通知
        member_id：会员号
        add:地址
        name：登记姓名
        mobile:登记手机号
        time:有效期
        */
        public string get_member_success_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string member_id, string add, string name, string mobile, string time)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"cardNumber\":{" +
                                        "\"value\":\"" + member_id + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"address\":{" +
                                        "\"value\":\"" + add + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"VIPName\":{" +
                                        "\"value\":\"" + name + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"VIPPhone\":{" +
                                        "\"value\":\"" + mobile + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"expDate\":{" +
                                        "\"value\":\"" + time + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }

        /*
        零售-积分兑换成功通知
        标题-积分兑换成功通知
        name:产品名
        score：兑换积分
        time:兑换时间
        */
        public string get_scored_success_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string name, string score, string time)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"productType\":{" +
                                        "\"value\":\"" + "产品名" + "\"," +
                                        "\"color\":\"#222222\"" +
                                    "}," +
                                    "\"name\":{" +
                                        "\"value\":\"" + name + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"points\":{" +
                                        "\"value\":\"" + score + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"date\":{" +
                                        "\"value\":\"" + time + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }

        /*
        零售-消费成功通知
        标题-消费成功通知
        time:消费时间
        shop:消费门店
        type:消费类型
        price:消费金额
        score:积分增加
        */
        public string get_charge_success_demo(string wx_openid, string template_id, string redirect_url, string head_str, string foot_str, string time, string shop, string type, string price, string score)
        {
            string post_data = "{\"touser\":\"" + wx_openid + "\"," +
                                "\"template_id\":\"" + template_id + "\"," +
                                "\"url\":\"" + redirect_url + "\"," +
                                "\"data\":{" +
                                    "\"first\": {" +
                                        "\"value\":\"" + head_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"time\":{" +
                                        "\"value\":\"" + time + "\"," +
                                        "\"color\":\"#222222\"" +
                                    "}," +
                                    "\"org\":{" +
                                        "\"value\":\"" + shop + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"type\":{" +
                                        "\"value\":\"" + type + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"money\":{" +
                                        "\"value\":\"" + price + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"point\":{" +
                                        "\"value\":\"" + score + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}," +
                                    "\"remark\":{" +
                                        "\"value\":\"" + foot_str + "\"," +
                                        "\"color\":\"#173177\"" +
                                    "}}}";
            return post_data;
        }
    }
}