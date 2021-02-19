using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using IvyFront.Forms;

namespace IvyFront.Helper
{
    /// <summary>
    /// 软件安全验证
    /// </summary>
    public class SoftUpdate
    {

        /// <summary>
        /// 更新
        /// </summary>
        public static void Update()
        {
            try
            {
                IvyHelper.F_0001_0010();
            }
            catch (Exception ex)
            {
                Log.writeLog(ex);
            }

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
                Log.writeLog(ex);
                throw;
            }
        }

        /// <summary>
        /// 权限校验
        /// </summary>
        /// <returns></returns>
        public static bool PermissionsBalidation()
        {
            int flag = -1;//0:成功 -1:没有权限 -2：没有加密狗

            if (Program.oper_id.Equals("9999"))
            {
                flag = -1;
            }
            else
            {
                flag = 0;
                //验证加密狗
                //if (VerifySoft() != 1)
                //{
                //    flag = -2;
                //}
                //else
                //{
                //    flag = 0;
                //}
            }

            switch (flag)
            {
                case 0:
                    return true;
                case -1:
                    
                    return false;
                case -2:
                    new MsgForm("没有找到加密狗").ShowDialog();
                    return false;
                default:
                    return false;
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
                Log.writeLog(ex);
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
                Log.writeLog(ex);
                throw;
            }
        }

        public static int Exists()
        {
            try
            {
                return F_0001_0049(); //1有狗  
            }
            catch (Exception ex)
            {
                Log.writeLog(ex);
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
