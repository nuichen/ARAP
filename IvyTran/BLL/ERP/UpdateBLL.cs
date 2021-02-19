using System;
using System.Runtime.InteropServices;
using System.Threading;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class UpdateBLL : IUpdate
    {
        public bool IsOk()
        {
            try
            {
                Request r = new Request();

                r.request("http://www.baidu.com");

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex);
                return false;
            }
        }

        public void AutoUpdate()
        {
            try
            {
                //是否联网
                if (IsOk())
                {
                    UpdataHelper.F_0001_0010();
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex);
                throw ex;
            }
        }

        public void Update()
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    Thread.Sleep(1000);

                   // Application.Restart();
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog(ex);
                }
            });
            t.Start();
        }

        public string GetServerVer()
        {
            ISys bll = new Sys();
            return bll.Read("ser_ver");
        }

    }

    public class UpdataHelper
    {
        [DllImport("IVYGetValue.dll")]
        public static extern int F_0001_0010();
    }
}
