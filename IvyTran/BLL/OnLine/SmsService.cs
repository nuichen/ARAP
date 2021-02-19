using System.IO;
using System.Net;

namespace IvyTran.BLL.OnLine
{
    public class SmsService
    {
        private string request(string url, string context)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = 60 * 1000;
            req.ReadWriteTimeout = 60 * 1000;
            //
            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(context);
            req.Method = "POST";
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();
            //
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();
            stream.Close();
            reader.Close();
            return str;

        }

        public void Send_Ver_Code_Msg(string to_mobile, string ver_code, int disable_mins)
        {
            if (AppSetting.mc_id == "" || AppSetting.uid == "") return;

            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("mc_id", AppSetting.mc_id);
            write.Append("mc_uid", AppSetting.uid);
            write.Append("to_mobile", to_mobile);
            write.Append("ver_code", ver_code);
            write.Append("disable_mins", disable_mins.ToString());
            var res = this.request(AppSetting.sms_api + "/common?t=send_ver_code_msg", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(res);
            if (read.Read("errId") != "0")
            {
                throw new ExceptionBase(read.Read("errMsg"));
            }
        }

        public void Send_One_Recharge_Msg(string to_mobile, decimal amount, decimal give_amount, int disable_mins)
        {
            if (AppSetting.mc_id == "" || AppSetting.uid == "") return;

            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("mc_id", AppSetting.mc_id);
            write.Append("mc_uid", AppSetting.uid);
            write.Append("to_mobile", to_mobile);
            write.Append("amount", amount.ToString());
            write.Append("give_amount", give_amount.ToString());
            write.Append("disable_mins", disable_mins.ToString());
            var res = this.request(AppSetting.sms_api + "/common?t=send_one_recharge_msg", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(res);
            if (read.Read("errId") != "0")
            {
                throw new ExceptionBase(read.Read("errMsg"));
            }
        }

        public void Send_One_Pay_Msg(string to_mobile, decimal amount, decimal give_amount, int disable_mins)
        {
            if (AppSetting.mc_id == "" || AppSetting.uid == "") return;

            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("mc_id", AppSetting.mc_id);
            write.Append("mc_uid", AppSetting.uid);
            write.Append("to_mobile", to_mobile);
            write.Append("amount", amount.ToString());
            write.Append("give_amount", give_amount.ToString());
            write.Append("disable_mins", disable_mins.ToString());
            var res = this.request(AppSetting.sms_api + "/common?t=send_one_pay_msg", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(res);
            if (read.Read("errId") != "0")
            {
                throw new ExceptionBase(read.Read("errMsg"));
            }
        }

        public void Send_One_Acc_Msg(string to_mobile, string user_acc, string user_pwd, int disable_mins)
        {
            if (AppSetting.mc_id == "" || AppSetting.uid == "") return;

            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("mc_id", AppSetting.mc_id);
            write.Append("mc_uid", AppSetting.uid);
            write.Append("to_mobile", to_mobile);
            write.Append("user_acc", user_acc);
            write.Append("user_pwd", user_pwd);
            write.Append("disable_mins", disable_mins.ToString());
            var res = this.request(AppSetting.sms_api + "/common?t=send_one_acc_msg", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(res);
            if (read.Read("errId") != "0")
            {
                throw new ExceptionBase(read.Read("errMsg"));
            }
        }
    }
}