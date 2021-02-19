using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Server
{
    public class LogHelper
    {
        public static void writeLog(string position, string message, params string[] paras)
        {
            try
            {
                if (!Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\logs\\"))
                {
                    Directory.CreateDirectory(HttpContext.Current.Request.PhysicalApplicationPath + "\\logs\\");
                }
                string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                List<string> mesList = new List<string>();
                mesList.Add("");
                mesList.Add("");
                mesList.Add("**************************" + position + DateTime.Now.ToString() + "*************************");
                if (paras != null) 
                {
                    for (var i = 0; i < paras.Length; i++) 
                    {
                        mesList.Add("paramter" + i + ":" + paras[i]);
                    }
                }
                mesList.Add(message);
                mesList.Add("**************************************************************************************");

                File.AppendAllLines(HttpContext.Current.Request.PhysicalApplicationPath + "\\logs\\" + fileName, mesList, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
    }
}
