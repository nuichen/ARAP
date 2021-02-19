using System;
using System.IO;
using System.Net;
using System.Text;
using IvyTran.DAL;
using IvyTran.IBLL.OnLine;

namespace IvyTran.BLL.OnLine
{
    public class Message : IMessage
    {
        //private string url_get_temp_id = "https://api.weixin.qq.com/cgi-bin/template/get_all_private_template";
        private string url_send_message = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=";
        //private string openid = "oOYllw81AoORUn7AQMMzkw2e0YQU";
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

        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            Console.WriteLine(Url + (postDataStr == "" ? "" : "?") + postDataStr);

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        string IMessage.Create(string data, string open_type)
        {
            DB.IDB d = new DB.DBByAutoClose(AppSetting.conn);

            sys_acc accDAL = new sys_acc(d);
            var acc = accDAL.Select();
            if (acc == null)
            {
                throw new Exception("未初始化微信帐户");
            }
            //
            var res = this.request(AppSetting.wxsvr + "/wxapp?t=get_wx_app", "{\"appid\":\"" + acc.wx_appid +
                "\",\"secret\":\"" + acc.wx_secret + "\"}");
            //
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(res);
            if (read.Read("errId") != "0")
            {
                throw new ExceptionBase(read.Read("errMsg"));
            }
            var app = new body.wxapp(res);
            var res_send = this.request(url_send_message + app.token, data);
            return res_send;
        }
    }
}