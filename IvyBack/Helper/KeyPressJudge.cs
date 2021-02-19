using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IvyBack.Helper
{
    public class KeyPressJudge
    {
        public static bool IsChinese(char str)
        {
            Regex rg = new Regex("^[\u4e00-\u9fa5]$");
            return rg.IsMatch(str.ToString());
        }
        public static bool IsChinese(string str)
        {
            Regex rg = new Regex("[\u4e00-\u9fa5]");
            return rg.IsMatch(str);
        }

        public static bool IsBackKey(char str)
        {
            return str.Equals('\b');
        }

        public static bool IsNum(char str)
        {
            if ((str >= '0' && str <= '9') || str == '.')
                return true;
            return false;
        }

        public static bool IsEnglish(char str)
        {
            if ((str >= 'A' && str <= 'Z') || (str >= 'a' && str <= 'z'))
                return true;
            return false;
        }

        public static bool IsEnter(char str)
        {
            if (str.Equals(Keys.Enter))
                return true;
            return false;
        }

        public static bool IsKeyBord(char str)
        {
            if (str.Equals(Keys.Shift) || str.Equals(Keys.ControlKey) || str.Equals(Keys.Alt) || str.Equals(Keys.Control) ||
                 str.Equals(Keys.Enter) || str == '\r' || str.Equals('\b') ||
                 new char[] { '\u0001', '\u001a', '\u0018', '\u0003', '\u0016' }.Contains(str))
                return true;
            return false;
        }

        public static bool IsStr(char str)
        {
            if (IsChinese(str) || IsBackKey(str) || IsNum(str) || IsEnglish(str) || IsEnter(str) || IsKeyBord(str))
                return true;
            return false;
        }

    }
}
