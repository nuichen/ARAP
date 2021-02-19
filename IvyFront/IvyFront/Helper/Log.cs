using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IvyFront.Helper;

namespace IvyFront
{
    class Log
    {
        public static void writeLog(string position, string message, params string[] paras)
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            List<string> mesList = new List<string>();
            mesList.Add("");
            mesList.Add("");
            mesList.Add("**************************" + position + DateTime.Now.ToString() + "*************************");
            if (paras != null)
            {
                for (var i = 0; i < paras.Length; i++)
                {
                    mesList.Add("Paramter " + i.ToString() + ":" + paras[i]);
                }
            }
            mesList.Add(message);
            mesList.Add("**************************************************************************************");

            File.AppendAllLines(Program.path + "\\logs\\" + fileName, mesList, Encoding.UTF8);
        }

        public static void writeLog(Exception ex)
        {
            writeLog("Exception", ex.GetMessage(), null);
        }

    }
}
