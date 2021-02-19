using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using System.IO;

namespace b2bclient
{
    public class ImageHelper
    {
        public static Image getImage(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Timeout = 60 * 1000;
                req.ReadWriteTimeout = 60 * 1000;
                //
                req.Method = "GET";
                //
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream stream = res.GetResponseStream();
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                stream.Close();
                //
                return img;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("ImageHelper ->getImage()", ex.ToString(), url);
                return null;
            }

        }

        public static void UploadImage(Image img, out string img_path)
        {
            string url = AppSetting.svr + "/upload_img?t=upload";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = 60 * 1000;
            req.ReadWriteTimeout = 60 * 1000;
            //
            req.Headers.Add("img_type", "jpg");
            //
            System.IO.MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var bs = ms.ToArray();
            req.Method = "POST";
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(bs, 0, bs.Length);
            requestStream.Close();
            //
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();
            stream.Close();
            reader.Close();
            //
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(str);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            img_path = read.Read("img_path");

        }
    }
}
