using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IvyFront
{
    public class ReadXml
    {
        protected string context = "";
        XmlDocument doc = null;
        public ReadXml(string context)
        {
            this.context = context;
            doc = new XmlDocument();
            doc.LoadXml(context);
        }

        public bool Exist(string path)
        {
            if (doc.SelectSingleNode("xml/" + path) == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public string Read(string path)
        {
            XmlNode node = doc.SelectSingleNode("xml/" + path);
            if (node == null)
            {
                return "";
            }
            {
                return node.InnerXml;
            }
        }

        public List<ReadXml> ReadList(string path)
        {
            List<ReadXml> lst = new List<ReadXml>();
            //
            XmlNodeList nlst = doc.SelectNodes("xml/" + path);
            foreach (XmlNode node in nlst)
            {
                string xml = "<xml>" + "\r\n" + node.InnerXml + "\r\n" + "</xml>";
                lst.Add(new ReadXml(xml));
            }
            //
            return lst;
        }

    }
}
