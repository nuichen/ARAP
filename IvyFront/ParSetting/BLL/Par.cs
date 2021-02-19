using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParSetting.BLL
{
    class Par : IBLL.IPar
    {

        private System.Text.Encoding coding = System.Text.Encoding.GetEncoding("gb2312");

        string IBLL.IPar.Read(string class_name, string par_name)
        {
            string file = Program.path + "\\setting\\par.ini";
            if (System.IO.File.Exists(file) == false)
            {
                return "";
            }
            if (System.IO.File.ReadAllText(file) == "")
            {
                return "";
            }

            string[] lines = System.IO.File.ReadAllLines(file, coding);
            if (lines == null)
            {
                return "";
            }
            if (lines.Length == 0)
            {
                return "";
            }

            int flag = 0;
            int index = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].ToLower() == ("[" + class_name + "]").ToLower())
                {
                    flag = 1;
                    index = i;
                    break;
                }
            }

            if (flag == 0)
            {
                return "";
            }
            else
            {
                int end_index = index;
                for (int i = index + 1; i < lines.Length; i++)
                {
                    if (lines[i].Trim().StartsWith("[") == true)
                    {
                        break;
                    }
                    end_index = i;

                }
                for (int i = index + 1; i <= end_index; i++)
                {
                    if (lines[i].Split('=')[0].ToLower() == par_name.ToLower())
                    {
                        return lines[i].Substring(lines[i].Split('=')[0].Length + 1);
                    }
                }
            }
            return "";
        }



        void IBLL.IPar.Write(string class_name, string par_name, string par_value)
        {
            string file = Program.path + "\\setting\\par.ini";
            if (System.IO.File.Exists(file) == false)
            {
                return;
            }
            if (System.IO.File.ReadAllText(file) == "")
            {
                return;
            }

            string[] lines = System.IO.File.ReadAllLines(file, coding);
            if (lines == null)
            {
                return;
            }
            if (lines.Length == 0)
            {
                return;
            }

            int flag = 0;
            int index = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].ToLower() == ("[" + class_name + "]").ToLower())
                {
                    flag = 1;
                    index = i;
                    break;
                }
            }

            if (flag == 0)
            {
                return;
            }
            else
            {
                int end_index = index;
                for (int i = index + 1; i < lines.Length; i++)
                {
                    if (lines[i].Trim().StartsWith("[") == true)
                    {
                        break;
                    }
                    end_index = i;
                }
                for (int i = index + 1; i <= end_index; i++)
                {
                    if (lines[i].Split('=')[0].ToLower() == par_name.ToLower())
                    {
                        lines[i] = lines[i].Split('=')[0] + "=" + par_value;
                    }
                }
            }

            string context = "";
            foreach (string line in lines)
            {
                context += line + "\r\n";
            }
            System.IO.File.WriteAllText(file, context, coding);
        }

    }
}
