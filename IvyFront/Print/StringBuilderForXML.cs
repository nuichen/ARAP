using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Print
{
   public class StringBuilderForXML
    {
        private System.Text.StringBuilder sb=new StringBuilder();
        public StringBuilderForXML()
        {
           
        }

        public void Append(string key,string value)
        {
            sb.Append("<" + key + ">");
            sb.Append(value);
            sb.Append("</" + key + ">");
        }

        public void Append(string context)
        {
            sb.Append(context);
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        public void Clear()
        {
            sb.Clear();
        }


    }
}
