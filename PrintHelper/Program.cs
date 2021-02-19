using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrintHelper
{
  public static class Program
    {
        public static string path = "";
     
        
      public   static void Main()
        {
           
            path = Application.StartupPath;
            //
            try
            {
                
                
                //
                if (System.IO.Directory.Exists(path + "\\print_style") == false)
                {
                    System.IO.Directory.CreateDirectory(path + "\\print_style");
                }
                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message  );
            }
          
        }
    }
}
