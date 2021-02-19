using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Drawing;
using IvyBack.Properties;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace IvyBack.cons
{
    /// <summary>
    /// 读取资源文件
    /// </summary>
    public static class ResourcesHelper
    {
        private static string path = Program.path + "\\Resource\\IvyResources.dll";
        private static Assembly ass;
        private static ResourceManager rm;
        private static ResourceManager ivy_rm;
        private static Dictionary<string, Bitmap> dic = new Dictionary<string, Bitmap>();


        static ResourcesHelper()
        {
            /*
            //
            if (!System.IO.Directory.Exists(Program.path + "\\Resource"))
            {
                System.IO.Directory.CreateDirectory(Program.path + "\\Resource");
            }
            //加载资源文件
            if (File.Exists(path))
            {
                ass = Assembly.LoadFile(path);
                rm = new ResourceManager("Properties.Resources", ass);
            }
           */

            //记录当前程序集的资源文件
            Resources res = new Resources();
            PropertyInfo[] infos = res.GetType().GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo[] styles = infos.Where(p => p.PropertyType == typeof(Bitmap)).ToArray();

            Assembly myAssem = Assembly.GetExecutingAssembly();
           
            ivy_rm = new ResourceManager("Ivyback.Properties.Resources", myAssem);
            //foreach (PropertyInfo p in styles)
            //{
            //    Bitmap map = (Bitmap)ivy_rm.GetObject(p.Name);
            //    dic.Add(p.Name, map);
            //}
             
        }


        public static void SetControlImg(Control frm)
        {
            try
            {
                if (ass == null) return;
                if (frm == null) return;

                Bitmap bit;
                foreach (Control c in frm.Controls)
                {

                    /* 第一种方式 根据Tag匹配图片
                    if (c.Tag != null)
                    {
                        var tag = c.Tag.ToString();
                        if (tag.IndexOf("[") > -1 && tag.IndexOf("]") > -1)
                        {
                            tag = tag.Substring(1, tag.Length - 1);
                            tag = tag.Substring(0, tag.Length - 1);

                            string[] name = tag.Split(':');
                        }
                    }
                    */


                    if (c.BackgroundImage != null)
                    {
                        bit = (Bitmap)c.BackgroundImage;

                        var img = dic.SingleOrDefault(d => IsSameImg(d.Value, bit)).Key;
                        if (!string.IsNullOrEmpty(img))
                        {
                            bit = (Bitmap)rm.GetObject(img);
                            if (bit != null)
                                c.BackgroundImage = bit;
                        }

                        bit = null;
                        img = null;
                    }

                    if (frm.HasChildren)
                    {
                        SetControlImg(c);
                    }
                }

            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }

        }
        public static void Clear()
        {
            ass = null;
            rm = null;
            dic = null;
            Program.ClearMemory();
        }

        public static Image GetImage(string name)
        {
            try
            {
                Image img = (Image)ivy_rm.GetObject(name);
                //if (rm != null)
                //{
                //    var obj = (Image)rm.GetObject(name);
                //    if (obj != null)
                //        img = obj;
                //}

                return img;
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                return null;
            }
        }

        #region 判断图片是否一致
        /// <summary>
        /// 判断图片是否一致
        /// </summary>
        /// <param name="img">图片一</param>
        /// <param name="bmp">图片二</param>
        /// <returns>是否一致</returns>
        public static bool IsSameImg(Bitmap img, Bitmap bmp)
        {
            //大小不一致
            if (img != null && bmp != null && img.Width == bmp.Width && img.Height == bmp.Height)
            {
                //将图片一锁定到内存
                BitmapData imgData_i = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                IntPtr ipr_i = imgData_i.Scan0;
                int length_i = imgData_i.Width * imgData_i.Height * 3;
                byte[] imgValue_i = new byte[length_i];
                Marshal.Copy(ipr_i, imgValue_i, 0, length_i);
                img.UnlockBits(imgData_i);
                //将图片二锁定到内存
                BitmapData imgData_b = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                IntPtr ipr_b = imgData_b.Scan0;
                int length_b = imgData_b.Width * imgData_b.Height * 3;
                byte[] imgValue_b = new byte[length_b];
                Marshal.Copy(ipr_b, imgValue_b, 0, length_b);
                bmp.UnlockBits(imgData_b);
                //长度不相同
                if (length_i != length_b)
                {
                    return false;
                }
                else
                {
                    //循环判断值
                    for (int i = 0; i < length_i; i++)
                    {
                        //不一致
                        if (imgValue_i[i] != imgValue_b[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
