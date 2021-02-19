using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class report : BaseService
    {

        IReport reportBll = new Report();
        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }

        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);
            try
            {
                var T = reportBll.GetType();
                MethodInfo[] methods = T.GetMethods().Where(m => m.Name.Equals(t)).ToArray();
                if (methods.Length < 1) throw new Exception("请求地址异常!");

                ParameterInfo[] par = new ParameterInfo[] { };
                Object[] obj_par = new Object[] { };
                Dictionary<int, string> ref_dic = new Dictionary<int, string>();
                foreach (MethodInfo method in methods)
                {
                    if (ref_dic.Count > 0) ref_dic.Clear();
                    par = method.GetParameters();
                    obj_par = new Object[par.Length];
                    int num = 1;
                    for (int i = 0; i < par.Length; i++)
                    {
                        ParameterInfo parinfo = par[i];

                        if (parinfo.IsOut)
                        {
                            ref_dic.Add(i, parinfo.Name);
                        }
                        else if (parinfo.ParameterType.IsValueType)
                        {
                            if (!web.ExistsKeys(par[i].Name)) continue;
                        }

                        obj_par[i] = web.GetObject(parinfo);
                        num++;
                    }

                    if (num < par.Length && method == methods[methods.Length - 1])
                        throw new Exception("缺少参数");

                    try
                    {
                        object returnValue = method.Invoke(reportBll, obj_par);

                        //
                        Type t1 = returnValue.GetType();
                        if (t1.IsValueType || t1 == typeof(string))
                        {
                            web.WriteResult(returnValue);
                        }
                        else
                        {
                            if (returnValue is DataTable)
                            {
                                web.Write((DataTable)returnValue);
                            }
                            else
                            {
                                web.Write(returnValue);
                            }
                        }

                        web.WriteRefObject(obj_par, ref_dic);
                        web.WriteSuccess();
                        break;
                    }
                    catch (Exception e)
                    {
                        LogHelper.writeLog("匹配方法", e.ToString());
                        throw e;
                    }
                }

            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }

            return web.NmJson();
        }



    }
}