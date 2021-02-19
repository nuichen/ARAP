using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IvyFront.cons
{
    class DataGridHelper 
    {
        System.Windows.Forms.DataGridView dg;
        public DataGridHelper(System.Windows.Forms.DataGridView dg)
        {
            this.dg = dg;
        }



      public   void AddColumn(string col_name, string col_caption, int width, int align)
        {
            System.Windows.Forms.DataGridViewColumn col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dg.Columns.Add(col);
            col.Name = col_name;
            col.DataPropertyName = col_name;
            col.HeaderText = col_caption;
            col.Width = width;
            if (align == 0)
            {
                col.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            }
            else if (align == 1)
            {
                col.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            }
            else if (align == 2)
            {
                col.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            }

        }

      public   void  SetStyle()
        {
            dg.EnableHeadersVisualStyles = false;
            //
            dg.BackgroundColor = Color.FromArgb(251, 251, 251);
            dg.BorderStyle = BorderStyle.None;
            dg.GridColor = Color.FromArgb(239, 239, 239);
            dg.RowHeadersDefaultCellStyle.Padding = new Padding(dg.RowHeadersWidth);
            dg.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dg.DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);

            //
            dg.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(239, 239, 239);
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dg.ColumnHeadersHeight = 40;
            dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //
            dg.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dg.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dg.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
            dg.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            //
            dg.RowPostPaint += this.dg_RowPostPaint;

        }

        private void dg_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dg.RowHeadersVisible == true)
            {
                var rec = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dg.RowHeadersWidth, e.RowBounds.Height);
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), dg.Font, Brushes.Black, rec, sf);

            }



        }

    }
}
