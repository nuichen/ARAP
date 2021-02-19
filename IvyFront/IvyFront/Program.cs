using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Data;
using System.IO;
using System.Drawing;
using System.Threading;
using DB;
using IvyFront.Helper;


namespace IvyFront
{
    static class Program
    {
        //*****************************软件版权信息*************************************//
        public static string cus_id = "";//客户id
        public static string ivy_app_id = "1064"; //产品id
        public static string software_name = "智慧农贸";//软件名称
        public static string software_version = "v_2.1.20181208";//软件版本号
        public static string db_version = "v_1.0.0";//数据版本号
        public static string merchant_name = "亿家惠";//商家名
        //*****************************软件版权信息*************************************//

        public static string app_id = "";
        public static string path = "";
        public static string conn = "";
        public static string oper_id = "";
        public static string oper_name = "";
        public static string oper_type = "";
        public static string branch_no = "";
        public static string jh = "";
        public static string com_guid = "";//机器码
        public static string mo_ling = "0";//抹零:0不抹零;1抹角;2抹分;3抹半块
        public static string weight_model = "1";//称重模式:1手工取重;2自动称重
        public static string input_cus_model = "1";//输客户模式:1每次重输客户; 2默认取上个客户; 
        public static int print_count = 1;//打印份数
        public static string can_input_qty = "1";//仅非称重商品手输数量
        public static string is_continue_weight = "0";//是否连续称重
        public static DB.IDB db = null;//数据库连接常开
        public static IBLL.IReadWeight ReadWeight;
        public static bool is_run = true;//数据上传线程运行标志
        public static bool is_login = false;//登录标志
        public static bool is_connect = true;//实付连接上服务端标志
        public static int item_count = 0;//商品服务器更新数量
        public static int sup_count = 0;//供应商客户服务器更新数量
        public static int cus_price_count = 0;//客户价格服务器更新数量
        public static int sup_price_count = 0;//供应商价格服务器更新数量
        public static Thread th = null;
        public static string errMsg = "";
        //public static Forms.frmMenu mainMenu;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            path = Application.StartupPath;
            //注册表
            Registry();
            /*
            IBLL.IPayBLL pbll = new BLL.PayBLL(); 
            string sheet_no = "";
            Program.jh = "001";
            pbll.MicroPay("81231000525321", "W", 0.01M, "135495986333499595", out sheet_no);
            */

            //处理未捕获的异常   
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常   
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            try
            {
                //更新软件
                if (!Debugger.IsAttached)
                {
                    SoftUpdate.Update();
                }
                

                //验证程序
                //if (SoftUpdate.VerifySoft() != 1)
                //{
                //    MessageBox.Show("没有找到加密狗!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    System.Environment.Exit(0);
                //}

                //创建数据库文件
                try
                {
                    if (!System.IO.Directory.Exists(path + "\\logs"))
                    {
                        System.IO.Directory.CreateDirectory(path + "\\logs");
                    }
                    if (!System.IO.Directory.Exists(path + "\\setting"))
                    {
                        System.IO.Directory.CreateDirectory(path + "\\setting");
                    }
                    if (System.IO.Directory.Exists(path + "\\data") == false)
                    {
                        System.IO.Directory.CreateDirectory(path + "\\data");
                    }
                    string db_file = Application.StartupPath + "\\data\\data";
                    if (System.IO.File.Exists(db_file) == false)
                    {
                        SQLiteByHandClose.CreateFile(db_file);
                    }
                    conn = "Data Source=" + db_file + ";Version=3;Pooling=False;Max Pool Size=100;";
                    var db = new DB.SQLiteByHandClose(conn);
                    db.Open();
                    Program.db = db;
                    Program.db.ExecuteScalar("PRAGMA synchronous = OFF;", null);
                    create_table.bt_sysn_info.Upgrade(db);
                    create_table.bt_par_setting.Upgrade(db);
                    create_table.bi_t_item_cls.Upgrade(db);
                    create_table.bi_t_item_info.Upgrade(db);
                    create_table.bi_t_supcust_info.Upgrade(db);
                    create_table.ic_t_branch_stock.Upgrade(db);
                    create_table.ot_pay_flow.Upgrade(db);
                    create_table.ic_t_inout_store_detail.Upgrade(db);
                    create_table.ic_t_inout_store_master.Upgrade(db);
                    create_table.sa_t_operator_i.Upgrade(db);
                    create_table.sm_t_salesheet.Upgrade(db);
                    create_table.sm_t_salesheet_detail.Upgrade(db);
                    create_table.t_order_detail.Upgrade(db);
                    create_table.bi_t_branch_info.Upgrade(db);
                    create_table.sys_t_sheet_no.Upgrade(db);
                    create_table.bi_t_cust_price.Upgrade(db);
                    create_table.bi_t_sup_item.Upgrade(db);
                    create_table.sys_t_system.Upgrade(db);
                    create_table.t_print_log.Upgrade(db);
                    create_table.t_clear_db_log.Upgrade(db);
                    create_table.t_click_log.Upgrade(db);
                }
                catch (Exception ex)
                {
                    Log.writeLog("创建数据库失败",ex.ToString(),null);
                    var frm = new Forms.MsgForm(ex.GetMessage());
                    frm.ShowDialog();
                    return;
                    //throw ex;
                }

                //检测打印机参数设置
                if (Appsetting.is_print == "1" && Appsetting.print_name == "")
                {
                    var frm = new Forms.MsgForm("请先设置打印机");
                    frm.ShowDialog();
                    return;
                }

                //删除过期数据
                IBLL.ISysBLL bll = new BLL.SysBLL();
                if (1 == 1)
                {
                    try
                    {
                        bll.DeleteOldData();
                    }
                    catch (Exception ex)
                    {
                        Log.writeLog("删除过期数据失败", ex.ToString(), null);
                        var frm = new Forms.MsgForm(ex.GetMessage());
                        frm.ShowDialog();
                    }
                }
                
                //
                ReadWeight = new BLL.ReadWeight_Common();
                is_run = true;
                try
                {
                    Thread t = new Thread(() => {
                        while (true) 
                        {
                            try
                            {
                                IBLL.IClientBLL bll2 = new BLL.ClientBLL();
                                //判断网络连接状态
                                is_connect = bll2.CheckConnect();

                                for (var i = 0; i < 30; i++)
                                {
                                    Thread.Sleep(1000);
                                    if (!is_run)
                                    {
                                        System.Environment.Exit(0);
                                        break;
                                    }
                                }
                                
                                //30秒自动上传销售和采购数据
                                if (is_connect && is_login)
                                {
                                    int errId = 0;
                                    string errMsg = "";
                                    bll2.UpLoadSale(out errId, out errMsg);

                                    errId = 0;
                                    errMsg = "";
                                    bll2.UpLoadInOut(out errId, out errMsg);
                                }
                            }
                            catch (Exception ex) 
                            {
                                MessageBox.Show("网络异常[2001]:" + ex.GetMessage());
                            }
                        }
                    });
                    t.Start();
                }
                catch (Exception ex)
                {
                    Log.writeLog("连接服务器异常", ex.ToString(), null);
                    var frm = new Forms.MsgForm(ex.GetMessage());
                    frm.ShowDialog();
                }
                //判断基础档案更新
                try
                {
                    Thread t = new Thread(() =>
                    {
                        while (true)
                        {
                            try
                            {
                                IBLL.IClientBLL bll2 = new BLL.ClientBLL();
                                int errId = 0;
                                string errMsg = "";
                                item_count = bll2.GetItemCount(out errId, out errMsg);
                                sup_count = bll2.GetSupCusCount(out errId, out errMsg);
                                cus_price_count = bll2.GetCusPriceCount("", "", out errId, out errMsg);
                                sup_price_count = bll2.GetSupPriceCount("", "", out errId, out errMsg);

                                
                                if (is_connect && is_login)
                                {
                                    errId = 0;
                                    errMsg = "";
                                    bll2.DownLoadItemCls(out errId, out errMsg);

                                    errId = 0;
                                    errMsg = "";
                                    bll2.DownLoadItem(out errId, out errMsg);

                                    errId = 0;
                                    errMsg = "";
                                    bll2.DownLoadSupCus(out errId, out errMsg);

                                    errId = 0;
                                    errMsg = "";
                                    bll2.DownLoadCusPrice("", "", out errId, out errMsg);

                                    errId = 0;
                                    errMsg = "";
                                    bll2.DownLoadSupPrice("", "", out errId, out errMsg);
                                }
                                
                                Thread.Sleep(5 * 60 * 1000);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("网络异常[2002]:" + ex.GetMessage());
                            }
                        }
                    });
                    t.Start();
                }
                catch (Exception ex)
                {
                    Log.writeLog("判断基础档案更新异常", ex.ToString(), null);
                }
                //
                try
                {
                    
                    var lst = bll.GetParSettingList();
                    foreach (Model.bt_par_setting item in lst)
                    {
                        if (item.par_id == "mo_ling") mo_ling = item.par_val;
                        else if (item.par_id == "weight_model") weight_model = item.par_val;
                        else if (item.par_id == "branch_no") branch_no = item.par_val;
                        else if (item.par_id == "jh") jh = item.par_val;
                        else if (item.par_id == "input_cus_model") input_cus_model = item.par_val;
                        else if (item.par_id == "print_count") print_count = Conv.ToInt(item.par_val);
                        else if (item.par_id == "can_input_qty") can_input_qty = item.par_val;
                        else if (item.par_id == "is_continue_weight") is_continue_weight = item.par_val;
                    }
                    if (branch_no.Length == 0)
                    {
                        if (new Forms.frmInitSetting().ShowDialog() == DialogResult.OK)
                        {
                            if (new Forms.WaitForm("正在加载数据，请稍候", "1").ShowWait())
                            {
                                if (new Forms.NewLoginForm().Login())
                                {
                                    Application.Run(new Forms.frmMenu());
                                }
                            }
                        }
                        else
                        {
                            System.Environment.Exit(0);
                        }
                    }
                    else 
                    {
                        if (new Forms.WaitForm("正在加载数据，请稍候", "1").ShowWait())
                        {
                            if (new Forms.NewLoginForm().Login())
                            {
                                Application.Run(new Forms.frmMenu());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.writeLog("系统异常", ex.ToString(), null);
                    var frm = new Forms.MsgForm(ex.GetMessage());
                    frm.ShowDialog();
                }

            }
            catch (Exception e) 
            {
                string str = "";
                string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";

                if (e != null)
                {
                    str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                         e.GetType().Name, e.Message, e.StackTrace);
                }
                else
                {
                    str = string.Format("应用程序线程错误:{0}", e);
                }
                Log.writeLog("Main", str);

                MessageBox.Show("系统错误！Message:" + e.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


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

            Log.writeLog("Application_ThreadException", str);
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

            Log.writeLog("Application_ThreadException", str);
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
                {//不存在
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
                        {key,guid}
                    };

                    regisHelper.WriteToRegistFile(dic, RegistFileName);

                    Program.com_guid = guid;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册表异常");
            }
        }
        #endregion
    }
}
