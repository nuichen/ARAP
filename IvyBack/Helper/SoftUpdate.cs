using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using IvyBack.MainForm;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IvyBack.Helper
{
    /// <summary>
    /// 软件安全验证
    /// </summary>
    public class SoftUpdate
    {
        private SoftUpdate() { }
        public static SoftUpdate soft_update = new SoftUpdate();

        IBLL.ISys bll = new BLL.SysBLL();
        IBLL.IUpdate upbll = new BLL.UpdateBLL();

        frmLoad frm = new frmLoad();
        /// <summary>
        /// 更新
        /// </summary>
        public static void Upadte()
        {
            IvyHelper.F_0001_0010();
            return;
            
            soft_update.frm.title = "正在检测更新";

            Thread t = new Thread(() =>
            {
                try
                {
                    IvyHelper.F_0001_0010();

                    //Thread.Sleep(500);

                    //int ser_ver = Conv.ToInt(soft_update.upbll.GetServerVer().Split('_')[1]);
                    //int ord_ser_ver = Conv.ToInt(GlobalData.ser_var.Split('_')[1]);



                    //if (ser_ver < ord_ser_ver)
                    //{
                    //    soft_update.frm.title = "正在更新服务器";
                    //    //服务器版本 比标记版本低 需要更新
                    //    soft_update.upbll.Update();

                    //    int i = 1;
                    //    while (true)
                    //    {
                    //        Thread.Sleep(2000);//每隔2秒验证一次...............

                    //        i++;
                    //        if (soft_update.upbll.UpdateState())
                    //            break;

                    //        if (i > 20)
                    //        {
                    //            MsgForm.ShowFrom("更新失败!");
                    //            System.Environment.Exit(0);
                    //        }
                    //    }
                    //}
                    //else if (ser_ver > ord_ser_ver)
                    //{
                    //    //服务器版本 比标记版本高 程序版本较低
                    //    throw new Exception("程序版本较低，请更新程序！");
                    //}
                    //soft_update.frm.Invoke((MethodInvoker)delegate
                    //{
                    //    soft_update.frm.Close();
                    //});
                }
                catch (Exception ex)
                {
                    MsgForm.ShowFrom(ex);
                }
            });
            t.Start();
            soft_update.frm.ShowDialog();
        }

        /// <summary>
        /// 验证软件
        /// </summary>
        public static int VerifySoft()
        {
            try
            {
                return IvyHelper.Exists();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex);
                throw;
            }
        }

    }

    public class IvyHelper
    {

        public static int Encrypt(string str, out string str2)
        {
            try
            {
                StringBuilder ls_source = new StringBuilder(str);
                StringBuilder ls_enc = new StringBuilder((ls_source.Length / 128 + 1) * 256 + 1);
                int flag = F_0001_0022(ls_source, ls_enc);

                str2 = ls_enc.ToString();

                return flag;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex);
                throw;
            }
        }
        public static int Decode(string str, out string str2)
        {
            try
            {
                StringBuilder ls_enc = new StringBuilder(str);
                StringBuilder ls_dec = new StringBuilder(ls_enc.Length);
                int flag = F_0001_0048(ls_enc, ls_dec);

                str2 = ls_dec.ToString();

                return flag;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex);
                throw;
            }
        }

        public static int Exists()
        {
            try
            {
                return 1;
                ///   return F_0001_0049(); //1有狗  
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex);
                return 0;
            }
        }


        [DllImport("IVYGetValue.dll")]
        public static extern int F_0001_0010();
        [DllImport("IVYGetValue.dll")]
        static extern int F_0001_0022(StringBuilder as_from, StringBuilder as_to);
        [DllImport("IVYGetValue.dll")]
        static extern int F_0001_0048(StringBuilder as_from, StringBuilder as_to);
        [DllImport("IVYGetValue.dll")]
        static extern int F_0001_0049();

    }
}
