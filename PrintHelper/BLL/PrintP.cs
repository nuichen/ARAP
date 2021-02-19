using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.BLL
{
   public  class PrintP:IBLL.IPrint 
    {
     

        void IBLL.IPrint.Print(string style_id,string xml, System.Data.DataTable tbmain, System.Data.DataTable tbdetail)
        {
            try
            {
                 
                PrintV pv = new PrintV();
                pv.Print(xml, tbmain, tbdetail);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
