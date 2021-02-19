using IvyBack.Helper;
using IvyBack.SysForm;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IvyBack
{
    static class Program
    {
        public static Model.sa_t_operator_i oper;
        public static string path = "";
        public static string com_guid = "";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            path = Application.StartupPath;
            //注册表
            if (!Debugger.IsAttached)
            {
                Registry();
            }

            if(!Directory.Exists(path+ "\\Excel"))
            {
                Directory.CreateDirectory(path+ "\\Excel");
            }
           

            //
            Helper.AppSetting.path = Application.StartupPath;
            Helper.AppSetting.app_ini();
            //处理未捕获的异常   
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常   
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            try
            {
                //验证程序
                //if (SoftUpdate.VerifySoft() != 1)
                //{
                //    MessageBox.Show("没有找到加密狗!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    System.Environment.Exit(0);
                //}

                //检查服务器
                IBLL.ICommonBLL bll = new BLL.CommonBLL();
                if ("0".Equals(bll.IsServer()))
                {
                    //var frmFindServer = new MainForm.frmFindServer();
                    //frmFindServer.ShowDialog();
                    MsgForm.ShowFrom("无法连接到服务器，请检查服务器连接");
                    return;
                }

                //更新中间件 手动验证
                if (!Debugger.IsAttached)
                {
                    SoftUpdate.Upadte();
                }

                //检查机号
                frmRegister frmreg = new frmRegister();
                if (frmreg.RegisterJH() == DialogResult.Yes)
                {
                    if (args.Length == 0)
                    {
                        frmLogin app = new frmLogin();
                        if (app.ShowDialog() == DialogResult.Yes)
                        {
                            //Application.Run(new frmNav());
                            Application.Run(new frmMain());
                        }
                    }
                    else
                    {
                        sa_t_operator_i oper1 = new sa_t_operator_i();
                        if (args.Length == 1)
                        {
                            oper1.oper_id = args[0];
                            oper1.oper_pw = "";
                        }
                        if (args.Length == 2)
                        {
                            oper1.oper_id = args[0];
                            oper1.oper_pw = args[1];
                        }
                        IBLL.IOper login = new BLL.OperBLL();

                        if (login.Loginbyarap(oper1))
                        {
                            Program.oper = oper1;

                            InI.Writue("app", "oper_id", oper.oper_id);
                            Application.Run(new frmMain());
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string str = "";
                string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";

                str = string.Format("{3}异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                    e.GetType().Name, e.Message, e.StackTrace, strDateInfo);
                LogHelper.writeLog("Main", str);

                MessageBox.Show("系统错误！Message:" + e.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            System.Environment.Exit(0);
        }


        #region 异常处理
        /// <summary>
        ///这就是我们要在发生未处理异常时处理的方法，我这是写出错详细信息到文本，如出错后弹出一个漂亮的出错提示窗体，给大家做个参考
        ///做法很多，可以是把出错详细信息记录到文本、数据库，发送出错邮件到作者信箱或出错后重新初始化等等
        ///这就是仁者见仁智者见智，大家自己做了。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            string str = "";
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            Exception error = e.Exception as Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                     error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }

            LogHelper.writeLog("Application_ThreadException", str);
            MessageBox.Show("系统错误！Message:" + error.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.ExceptionObject as Exception;
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            if (error != null)
            {
                str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }

            LogHelper.writeLog("Application_ThreadException", str);
            MessageBox.Show("系统错误！Message:" + error.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表 
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程 
                if (process.Id != current.Id)
                {
                    //返回已经存在的进程
                    return process;

                }
            }
            return null;
        }

        private static void HandleRunningInstance(Process instance)
        {
            MessageBox.Show("已经在运行！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowWindowAsync(instance.MainWindowHandle, 1);  //调用api函数，正常显示窗口
            SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);
        #endregion

        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion

        #region 注册表>注册机器码
        static void Registry()
        {
            try
            {
                string RegistFileName = "IvySoft";
                //检查注册表信息是否存在
                RegistryHelper regisHelper = new RegistryHelper();
                if (!regisHelper.IsRegeditItemExist(RegistFileName))
                {
                    //不存在
                    regisHelper.CreateRegistFile(RegistFileName);
                }

                string key = "com_guid";
                if (regisHelper.IsRegeditKeyExit(RegistFileName, key))
                {
                    var str = regisHelper.ReadToRegistFile(key, RegistFileName);
                    Program.com_guid = str;
                }
                else
                {
                    string guid = Guid.NewGuid().ToString("N");
                    Dictionary<string, string> dic = new Dictionary<string, string>()
                    {
                        {key, guid}
                    };

                    regisHelper.WriteToRegistFile(dic, RegistFileName);

                    Program.com_guid = guid;
                }
            }
            catch (System.Security.SecurityException ex)
            {
                //权限不足
                //ProcessHelper.UACReStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

    }
}
