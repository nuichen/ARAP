using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Tran
{
    class Program
    {

        public static string path = "";
        public static string conn;
  
        public static DB.IDB db = null;//数据库连接常开
       
        private static void Register(Type interfaceType, Type classType, string port, string path)
        {
            string url = "http://127.0.0.1:" + port + path;
            ServiceHost host = new ServiceHost(classType);
            host.AddServiceEndpoint(interfaceType, Binding(), url);
            var behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = true;
            behavior.HttpGetUrl = new Uri(url);
            host.Description.Behaviors.Add(behavior);
            host.Open();
        }


        private static System.ServiceModel.BasicHttpBinding Binding()
        {
            var bing = new System.ServiceModel.BasicHttpBinding();
            bing.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas();
            bing.ReaderQuotas.MaxArrayLength = 999999999;
            bing.ReaderQuotas.MaxStringContentLength = 999999999;
            bing.MaxReceivedMessageSize = 999999999;
            return bing;
        }


        static void Main(string[] args)
        {
            path = Application.StartupPath;
            if (Directory.Exists(path + "\\images")==false)
            {
                Directory.CreateDirectory(path + "\\images");
            }
            if (Directory.Exists(path + "\\log")==false)
            {
                Directory.CreateDirectory(path + "\\log");
            }
            if (Directory.Exists(path + "\\db")==false)
            {
                Directory.CreateDirectory(path + "\\db");
            }
            if (System.IO.Directory.Exists(path + "\\setting") == false)
            {
                System.IO.Directory.CreateDirectory(path + "\\setting");
            }
            //
            System.Threading.ThreadPool.SetMinThreads(100,100);
            System.Threading.ThreadPool.SetMaxThreads(1000, 1000);
            //
            Console.WriteLine("开始");
           
            
            //
            if (1 == 1)
            {
                Register(typeof(svr.IServiceBase), typeof(svr.ServiceBase), Appsetting.port, "/servicebase");
                Register(typeof(svr.IServiceBase), typeof(svr.common), Appsetting.port, "/common");
                Register(typeof(svr.IServiceBase), typeof(svr.settle), Appsetting.port, "/settle");
            }

            while (true)
            {
                string val = Console.ReadLine();
                if (val == "exit")
                {
                    break;
                }
            }
           

        }

       

    }
}
