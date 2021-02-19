using ReadWriteContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IvyBack.Helper
{
    class Request : IRequest
    {

        private string url = "";
        public Request()
        {
            url = AppSetting.svr;
        }
        public Request(string ip, string port)
        {
            url = "http://" + ip + ":" + port;
        }
        private string returnJson = "";//返回Json
        private static bool doing = false;
        private IWriteContext wc;
        private IReadContext rc;

        public string request(string par_url)
        {
            while (1 == 1)
            {
                System.Threading.Thread.Sleep(100);
                if (doing == false)
                {
                    try
                    {
                        doing = true;
                        string url = this.url + par_url;
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                        req.KeepAlive = false;
                        req.Timeout = 5 * 60 * 1000;
                        req.ReadWriteTimeout = 5 * 60 * 1000;
                        req.CookieContainer = AppSetting.CookieContainer;
                        //
                        ReadWriteContext.IWriteContext wc = new ReadWriteContext.WriteContextByJson();
                        wc.Append("none", "");
                        byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(wc.ToString());
                        req.Method = "POST";
                        Stream requestStream = req.GetRequestStream();
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                        requestStream.Close();
                        //
                        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                        res.Cookies = req.CookieContainer.GetCookies(req.RequestUri);
                        AppSetting.CookieContainer.Add(res.Cookies);
                        Stream stream = res.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        string str = reader.ReadToEnd();
                        stream.Close();
                        reader.Close();
                        //
                        doing = false;
                        //
                        returnJson = str;
                        rc = new ReadWriteContext.ReadContextByJson(returnJson);
                        return returnJson;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        doing = false;
                    }
                }
            }
        }
        public string request(string par_url, string context)
        {
            while (1 == 1)
            {
                System.Threading.Thread.Sleep(100);
                if (doing == false)
                {
                    try
                    {
                        doing = true;
                        string url = this.url + par_url;
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                        req.KeepAlive = false;
                        req.Timeout = 10 * 60 * 1000;
                        req.ReadWriteTimeout = 10 * 60 * 1000;
                        req.CookieContainer = AppSetting.CookieContainer;
                        //
                        byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(context);
                        req.Method = "POST";
                        Stream requestStream = req.GetRequestStream();
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                        requestStream.Close();
                        //
                        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                        res.Cookies = req.CookieContainer.GetCookies(req.RequestUri);
                        AppSetting.CookieContainer.Add(res.Cookies);
                        Stream stream = res.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        string str = reader.ReadToEnd();
                        stream.Close();
                        reader.Close();
                        //
                        doing = false;
                        //
                        returnJson = str;
                        rc = new ReadWriteContext.ReadContextByJson(returnJson);
                        return returnJson;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        doing = false;
                    }
                }
            }
        }

        public IReadContext GetReadContext()
        {
            if (rc == null)
                rc = new ReadContextByJson(returnJson);
            return rc;
        }


    }
}
