using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using System.Runtime.InteropServices;
using IvyBack.Helper;
using System.Threading;
using System.Windows.Forms;

namespace IvyBack.BLL
{
    public class UpdateBLL : IUpdate
    {

        public string GetServerVer()
        {
            JsonRequest r = new JsonRequest();

            r.request("/update?t=GetServerVer");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            string var = r.ReadResult();

            return var;
        }
        public void Update()
        {
            JsonRequest r = new JsonRequest();

            r.request("/update?t=Update");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }


        public bool UpdateState()
        {
            try
            {
                //验证中间件版本
                string ser_ver = GetServerVer();
                if (ser_ver.Equals(GlobalData.ser_var))
                {
                    //验证数据库版本
                    IBLL.ISys bll = new BLL.SysBLL();
                    string db_ver = bll.Read("db_ver");
                    if (!db_ver.Equals(GlobalData.db_var))
                    {
                        throw new Exception("数据库版本错误");
                    }
                }
                else
                {
                    throw new Exception("中间件版本错误");
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog(ex);
                return false;
            }
        }
    }


}
