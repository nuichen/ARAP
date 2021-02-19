using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.Helper
{
    public class sec
    {

        private string key = "zbwbestway";
        private int[] GetKey()
        {
            List<int> lst = new List<int>();
            foreach (char c in key)
            {

                lst.Add(Convert.ToInt32(c));
            }
            //lst.Reverse();//不用翻转
            return lst.ToArray();
        }

        public string des(string str)
        {
            int[] key = GetKey();
            string res = "";
            for (int i = 0; i < str.Length; i++)
            {
                int asc1 = Convert.ToInt32(str[i]);
                int asc2 = key[i % key.Length];

                char c = Convert.ToChar(asc2 - asc1);

                res = res + c.ToString();
            }
            return res;
        }



    }
}
