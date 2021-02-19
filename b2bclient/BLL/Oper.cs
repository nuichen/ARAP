using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.BLL
{
    class Oper:IOper 
    {
        void IOper.Login(string oper_id, string pwd, out string mc_id ,out string oper_type)
        {
            var req = new Request();
            var json= req.request("/oper?t=login", "{\"oper_id\":\""+oper_id+"\",\"pwd\":\""+pwd+"\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            mc_id = read.Read("mc_id");
            oper_type = read.Read("oper_type");
            
          
        }

    }
}
