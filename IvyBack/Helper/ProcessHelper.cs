using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IvyBack.Helper
{
    public class ProcessHelper
    {
        public static void UACReStart()
        {
            //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            //{
            //    UseShellExecute = true,
            //    WorkingDirectory = Environment.CurrentDirectory,
            //    FileName = Application.ExecutablePath,
            //    Verb = "runas"
            //};
            ////设置启动动作,确保以管理员身份运行

            //try
            //{
            //    System.Diagnostics.Process.Start(startInfo);
            //}
            //catch
            //{
            //    return;
            //}
            UACStartProcess(Application.ExecutablePath);
            System.Environment.Exit(0);
        }
        public static bool UACStartProcess(string file_name)
        {
            Process p = new Process();
            try
            {
                p.StartInfo.FileName = file_name;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.Verb = "runas";
                p.Start();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}