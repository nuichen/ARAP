using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace IvyFront
{
    public class Request
    {
        private static string url = "";
        public Request()
        {
            if (url == "")
            {
                url = Appsetting.svr;
            }

        }
        private static CookieContainer cookie = new CookieContainer();

        public string request(string par_url, string context)
        {
            int nun = 0;
            while (1 == 1)
            {
                if (nun >= 3)
                {
                    return "";
                }
                System.Threading.Thread.Sleep(100);
                try
                {
                    string url = Request.url + par_url;
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.Timeout = 60 * 1000;
                    req.ReadWriteTimeout = 60 * 1000;
                    req.CookieContainer = cookie;
                    //
                    byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(context);
                    req.Method = "POST";
                    Stream requestStream = req.GetRequestStream();
                    requestStream.Write(requestBytes, 0, requestBytes.Length);
                    requestStream.Close();
                    //
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    res.Cookies = req.CookieContainer.GetCookies(req.RequestUri);
                    cookie = new CookieContainer();
                    cookie.Add(res.Cookies);
                    Stream stream = res.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    string str = reader.ReadToEnd();
                    stream.Close();
                    reader.Close();
                    //
                    return str;

                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    nun++;
                }
            }
        }

    }
}

