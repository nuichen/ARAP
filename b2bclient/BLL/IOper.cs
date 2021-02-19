using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace b2bclient.BLL
{
   public interface IOper
    {
       void Login(string oper_id, string pwd,out string mc_id,out string oper_type);
    }
}
