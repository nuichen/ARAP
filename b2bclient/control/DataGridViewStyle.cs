using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace b2bclient.control
{
   public  class DataGridViewStyle
    {
       public static void SetStyle(System.Windows.Forms.DataGridView dg)
       {
           dg.EnableHeadersVisualStyles = false;
           //
           dg.BackgroundColor = Color.FromArgb(251, 251, 251);
           dg.BorderStyle = BorderStyle.None;
           dg.GridColor = Color.FromArgb(239, 239, 239);
           dg.RowHeadersDefaultCellStyle.Padding = new Padding(dg.RowHeadersWidth);
           dg.DefaultCellStyle.BackColor = Color.FromArgb(255,255,255);
           dg.DefaultCellStyle.ForeColor = Color.FromArgb(0,0,0);
           dg.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
           dg.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 0, 0);
           //
           dg.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
           dg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(239, 239, 239);
           dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
           dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           dg.ColumnHeadersHeight = 40;
           dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
           //
           dg.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
           dg.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
           dg.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
           dg.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);

       }

     

    }
}
