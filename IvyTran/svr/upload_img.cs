using System;
using System.Collections.Generic;
using IvyTran.BLL.OnLine;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr
{
    public class upload_img : BaseService
    {
        IImageClient bll = new ImageClient();
        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }
        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);

            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }

            return web.NmJson();
        }

        public void upload(WebHelper w, Dictionary<string, object> kv)
        {
            var img_type = base.context.Request.Headers["img_type"];
            if (img_type == "")
            {
                w.WriteInvalidParameters();
                return;
            }
            //
            System.IO.Stream stream = context.Request.InputStream;
            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
            string img_path = "";
            bll.Upload(img, img_type, out img_path);
            //写入消息
            //if (AppSetting.is_main == "1")
            //{
            //    BLL.ImageAdd bll = new BLL.ImageAdd();
            //    bll.AddMsg(img_url, img_path);
            //}
            //

            w.Write("img_url", AppSetting.imgsvr + img_path);
            w.Write("img_path", img_path);

        }


    }
}