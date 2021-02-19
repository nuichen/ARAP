using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using Model.SysModel;
using IvyTran.IBLL.ERP;
using IvyTran.BLL.ERP;

namespace IvyTran.Helper
{
    class LogHelper
    {
        private static string path = HttpContext.Current.Request.PhysicalApplicationPath + "\\log\\";
        public static void writeLog(string position, string message, params string[] paras)
        {

            sys_t_operator_log item = new sys_t_operator_log();
            string params_str = "";
            if (paras != null)
            {
                for (var i = 0; i < paras.Length; i++) params_str += "Paramter " + i.ToString() + ": " + paras[i];
            }
            item.func_id = position;
            item.module_name = position;
            item.log_info = message;
            item.memo = params_str;
            item.log_level = "WARNING";
            item.log_type = "系统日志";
            item.oper_date = DateTime.Now;
            item.oper_id = "";
            ISysLogBLL bll = new SysLogBLL();
            bll.WriteSysLog(item);
        }

        public static void writeLog(string msg)
        {
            try
            {
                sys_t_operator_log item = new sys_t_operator_log();
                string params_str = "";
                item.func_id = "";
                item.module_name = "";
                item.log_info = msg;
                item.memo = params_str;
                item.log_level = "WARNING";
                item.log_type = "系统日志";
                item.oper_date = DateTime.Now;
                item.oper_id = "";
                ISysLogBLL bll = new SysLogBLL();
                bll.WriteSysLog(item);
            }
            catch (Exception)
            {
                //throw ex;
            }
        }

        public static void writeLog(Exception ex)
        {
            writeLog(ex.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="message"></param>
        /// <param name="oper_id"></param>
        /// <param name="log_type">系统日志，操作日志，监控日志</param>
        /// <param name="log_level">WARNING,ERROR,EXCEPTION,</param>
        /// <param name="paras"></param>
        public static void writeSheetLog(string position, string message, string oper_id, string log_type = "操作日志", string log_level = "INFO", params string[] paras)
        {
            sys_t_operator_log item = new sys_t_operator_log();
            string params_str = "";
            if (paras != null)
            {
                for (var i = 0; i < paras.Length; i++) params_str += "Paramter " + i.ToString() + ": " + paras[i];
            }
            item.func_id = position;
            item.module_name = position;
            item.log_info = message;
            item.memo = params_str;
            item.log_level = log_level;
            item.log_type = log_type;
            item.oper_date = DateTime.Now;
            item.oper_id = oper_id;
            ISysLogBLL bll = new SysLogBLL();
            bll.WriteSysLog(item);
        }

    }
    #region 原日志
    /*class LogHelper
    {
        private static string path = HttpContext.Current.Request.PhysicalApplicationPath + "\\log\\";
        public static void writeLog(string position, string message, params string[] paras)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            List<string> mesList = new List<string>();
            mesList.Add("");
            mesList.Add("");
            mesList.Add("**************************" + position + DateTime.Now.ToString() + "*************************");
            if (paras != null)
            {
                for (var i = 0; i < paras.Length; i++) mesList.Add("Paramter " + i.ToString() + ": " + paras[i]);
            }
            mesList.Add(message);
            mesList.Add("**************************************************************************************");

            File.AppendAllLines(path + "\\" + fileName, mesList, Encoding.UTF8);
        }
        public static void writeLog(string msg)
        {
            try
            {
                string fileName = DateTime.Now.ToString("yyyyMMddHH") + ".txt";
                List<string> mesList = new List<string>();

                mesList.Add("**************************" + DateTime.Now.ToString() + "*************************");
                mesList.Add(msg);

                File.AppendAllLines(path + fileName, mesList, Encoding.UTF8);
            }
            catch (Exception)
            {
                //throw ex;
            }
        }
        public static void writeLog(Exception ex)
        {
            writeLog(ex.ToString());
        }
    }*/
    #endregion
}
