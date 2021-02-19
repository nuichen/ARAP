using System.IO;
using System.Net;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.OnLine
{
    public class ImageClient : IImageClient
    {
        void IImageClient.Upload(System.Drawing.Image img, string img_type, out string img_path)
        {
            string url = AppSetting.imgsvr + "/upload_img?t=upload";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = 60 * 1000;
            req.ReadWriteTimeout = 60 * 1000;
            //
            req.Headers.Add("img_type", img_type);
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
                throw new ExceptionBase(read.Read("errMsg"));
            }
            img_path = read.Read("img_path");
        }

        void IImageClient.Delete(string img_path)
        {
            string url = AppSetting.imgsvr + "/upload_img?t=delete";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = 60 * 1000;
            req.ReadWriteTimeout = 60 * 1000;
            //
            req.Headers.Add("img_path", img_path);
            //
            req.Method = "POST";
            byte[] bs = System.Text.UTF8Encoding.Default.GetBytes("");
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
                throw new ExceptionBase(read.Read("errMsg"));
            }
        }
    }
}