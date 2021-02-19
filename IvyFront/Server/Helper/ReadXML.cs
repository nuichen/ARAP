using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Server
{
    public class ReadXML
    {
        private XmlDocument xdoc = new XmlDocument();
        public ReadXML(string xml)
        {
            xdoc.XmlResolver = null;
            xdoc.LoadXml(xml);
        }

        public string Read(string nodename)
        {
            var n = xdoc.SelectSingleNode("xml/" + nodename);
            if (n == null)
            {
                return "";
            }
            else
            {
                return n.InnerText;
            }
        }

        public SortedDictionary<string, string> ReadPars()
        {
            SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
            foreach (XmlNode node in xdoc.SelectSingleNode("xml").ChildNodes)
            {
                dic.Add(node.Name, node.InnerText);
            }
            return dic;
        }

    }
}