using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tran
{
    public class LogHelper
    {
        public static void writeLog(string position, string message, params string[] paras)
        {
            try
            {
                var path = Program.path + "\\log\\";
                string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                List<string> mesList = new List<string>();
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

                File.AppendAllLines(path + fileName, mesList, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
    }
}
