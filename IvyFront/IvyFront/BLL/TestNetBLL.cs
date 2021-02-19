using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyFront.Helper;

namespace IvyFront.BLL
{
    public class TestNetBLL
    {
        public bool CheckConnect(string svr, out string errMsg)
        {
            errMsg = "";
            try
            {
                bool is_connect = false;
                IRequest req = new TestWebServiceRequest(svr);
                var json = req.request("/common?t=connect_server", "");
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                if (read.Read("is_connect") == "1")
                {
                    is_connect = true;
                }
                return is_connect;
            }
            catch (Exception ex)
            {
                Log.writeLog("TestNetBLL ->CheckConnect()", ex.ToString(), null);
                errMsg = ex.GetMessage();
                return false;
            }
        }

    }
}
