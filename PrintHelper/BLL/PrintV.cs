using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
namespace PrintHelper.BLL
{
    public class PrintV : Form, IBLL.IPrint
    {
        private ToolStripButton toolStripButton3;
        private PrintPreviewControl printPreviewControl1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripButton toolStripButton1;
        private ToolStrip toolStrip1;


        public PrintV()
        {
            this.InitializeComponent();
        }


        DataTable tbmain;
        DataTable tbdetail;
        DataTable tbstyle;
        string style_id = "";
        string xml = "";
        void IBLL.IPrint.Print(string style_id, string xml, System.Data.DataTable tbmain, System.Data.DataTable tbdetail)
        {
            try
            {

                this.style_id = style_id;
                this.xml = xml;
                this.tbmain = tbmain;
                this.tbdetail = tbdetail;
                row_count = this.tbdetail.Rows.Count;
                if (this.tbdetail.Columns.Contains("行号") == true)
                {
                    this.tbdetail.Columns.Remove("行号");
                    this.tbdetail.Columns.Add("行号");
                    this.tbdetail.Columns["行号"].SetOrdinal(0);
                }
                else
                {
                    this.tbdetail.Columns.Add("行号");
                    this.tbdetail.Columns["行号"].SetOrdinal(0);
                }
                //
                Helper.ReadXml r = new Helper.ReadXml(xml);
                barHeight = Convert.ToInt16(r.Read("Page/BarHeight"));
                pageWidth = Convert.ToInt16(r.Read("Page/Width"));
                pageHeight = Convert.ToInt16(r.Read("Page/Height"));
                ah = Convert.ToInt16(r.Read("Page/AHeight"));
                bh = Convert.ToInt16(r.Read("Page/BHeight"));
                ch = Convert.ToInt16(r.Read("Page/CHeight"));
                dh = Convert.ToInt16(r.Read("Page/DHeight"));
                eh = Convert.ToInt16(r.Read("Page/EHeight"));
                AppendEmptyRow = Convert.ToInt16(r.Read("Page/AppendEmptyRow"));
                rowHeight = ch;
                smallTotal = SmallTotal(xml);
                FontConverter fc = new FontConverter();
                gridFont = (Font)fc.ConvertFromString(r.Read("Page/GridFont"));
                gridLeft = Convert.ToInt16(r.Read("Page/GridLeft"));
                //
                tbstyle = new DataTable();
                tbstyle.Columns.Add("display");
                tbstyle.Columns.Add("colname");
                tbstyle.Columns.Add("colbyname");
                tbstyle.Columns.Add("width", typeof(int));
                tbstyle.Columns.Add("align");
                tbstyle.Columns.Add("format");
                tbstyle.Columns.Add("smalltotal");
                //

                foreach (Helper.ReadXml r2 in r.ReadList("Page/Column"))
                {
                    var row = tbstyle.NewRow();
                    tbstyle.Rows.Add(row);

                    row["display"] = r2.Read("Display");
                    row["colname"] = r2.Read("ColName");
                    row["colbyname"] = r2.Read("ColByname");

                    row["width"] = Convert.ToInt16(r2.Read("Width"));
                    row["align"] = r2.Read("Align");
                    row["format"] = r2.Read("Format");
                    row["SmallTotal"] = r2.Read("SmallTotal");

                }

                //
                this.ShowDialog();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintV));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripLabel1,
            this.toolStripComboBox1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(662, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton3.Text = "打印";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "页码";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // printPreviewControl1
            // 
            this.printPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl1.Location = new System.Drawing.Point(0, 25);
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.Size = new System.Drawing.Size(662, 456);
            this.printPreviewControl1.TabIndex = 1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton1.Text = "退出";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // PrintV
            // 
            this.ClientSize = new System.Drawing.Size(662, 481);
            this.Controls.Add(this.printPreviewControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PrintV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PrintV_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PrintDialog f = new PrintDialog();
            f.Document = pd;
            row_count = tbdetail.Rows.Count;
            row_index = 0;
            lstrow = new List<DataRow>();
            if (f.ShowDialog() == DialogResult.OK)
            {
                PageIndex = 1;
                pd.Print();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        System.Drawing.Printing.PrintDocument pd;
        private void PrintV_Load(object sender, EventArgs e)
        {
            PageIndex = 1;
            PageCount = this.GetPageCount(xml);
            PageSize = this.GetPageSize(xml);
            this.toolStripComboBox1.Items.Clear();
            for (int i = 1; i <= PageCount; i++)
            {
                this.toolStripComboBox1.Items.Add(i.ToString());
            }
            this.toolStripComboBox1.SelectedIndex = 0;
            //
            if (tbdetail != null)
            {
                int index = 0;
                foreach (DataRow row in tbdetail.Rows)
                {
                    index++;
                    row["行号"] = index;
                }
            }
            //
            pd = new System.Drawing.Printing.PrintDocument();
            pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("printPage", PageSize.Width, PageSize.Height);
            pd.PrintPage += this.pd_pagePrint;
            this.printPreviewControl1.Document = pd;
            this.printPreviewControl1.Zoom = 1;
        }

        public void Print(string xml, DataTable tbmain, DataTable tbdetail)
        {
            this.xml = xml;
            this.tbmain = tbmain;
            this.tbdetail = tbdetail;
            row_count = tbdetail.Rows.Count;
            if (this.tbdetail.Columns.Contains("行号") == true)
            {
                this.tbdetail.Columns.Remove("行号");
                this.tbdetail.Columns.Add("行号");
            }
            else
            {
                this.tbdetail.Columns.Add("行号");
            }
            PageCount = this.GetPageCount(xml);
            PageSize = this.GetPageSize(xml);
            //
            if (tbdetail != null)
            {
                int index = 0;
                foreach (DataRow row in tbdetail.Rows)
                {
                    index++;
                    row["行号"] = index;
                }
            }
            //
            Helper.ReadXml r = new Helper.ReadXml(xml);
            barHeight = Convert.ToInt16(r.Read("Page/BarHeight"));
            pageWidth = Convert.ToInt16(r.Read("Page/Width"));
            pageHeight = Convert.ToInt16(r.Read("Page/Height"));
            ah = Convert.ToInt16(r.Read("Page/AHeight"));
            bh = Convert.ToInt16(r.Read("Page/BHeight"));
            ch = Convert.ToInt16(r.Read("Page/CHeight"));
            dh = Convert.ToInt16(r.Read("Page/DHeight"));
            eh = Convert.ToInt16(r.Read("Page/EHeight"));
            AppendEmptyRow = Convert.ToInt16(r.Read("Page/AppendEmptyRow"));
            rowHeight = ch;
            smallTotal = SmallTotal(xml);
            FontConverter fc = new FontConverter();
            gridFont = (Font)fc.ConvertFromString(r.Read("Page/GridFont"));
            gridLeft = Convert.ToInt16(r.Read("Page/GridLeft"));
            //
            tbstyle = new DataTable();
            tbstyle.Columns.Add("display", typeof(bool));
            tbstyle.Columns.Add("colname");
            tbstyle.Columns.Add("colbyname");
            tbstyle.Columns.Add("width", typeof(int));
            tbstyle.Columns.Add("align");
            tbstyle.Columns.Add("format");
            tbstyle.Columns.Add("smalltotal", typeof(bool));
            //

            foreach (Helper.ReadXml r2 in r.ReadList("Page/Column"))
            {
                var row = tbstyle.NewRow();
                tbstyle.Rows.Add(row);

                row["display"] = Convert.ToBoolean(r2.Read("Display"));
                row["colname"] = r2.Read("ColName");
                row["colbyname"] = r2.Read("ColByname");

                row["width"] = Convert.ToInt16(r2.Read("Width"));
                row["align"] = r2.Read("Align");
                row["format"] = r2.Read("Format");
                row["SmallTotal"] = r2.Read("SmallTotal");

            }

            //
            //
            pd = new System.Drawing.Printing.PrintDocument();
            pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("printPage", PageSize.Width, PageSize.Height);
            pd.PrintPage += this.pd_pagePrint;
            PageIndex = 1;
            pd.Print();
        }

        private System.Drawing.Size GetPageSize(string xml)
        {
            Helper.ReadXml r = new Helper.ReadXml(xml);
            int w = Convert.ToInt16(r.Read("Page/Width"));
            int h = Convert.ToInt16(r.Read("Page/Height"));
            return new System.Drawing.Size(w, h);
        }


        private int SmallTotal(string xml)
        {
            Helper.ReadXml r = new Helper.ReadXml(xml);
            foreach (Helper.ReadXml r2 in r.ReadList("Page/Column"))
            {
                if (r2.Read("ColName") == "行号")
                {

                }
                else
                {
                    if (r2.Read("SmallTotal") == "1")
                    {
                        return 1;
                    }
                    else
                    {

                    }
                }
            }
            return 0;
        }

        private int GetPageCount(string xml)
        {


            int page_index = 1;
            int row_count = tbdetail.Rows.Count;

            while (1 == 1)
            {
                int top = 0;
                top += ah;
                if (page_index == 1)
                {
                    top += bh;
                }
                if (row_count > 0)
                {
                    top += rowHeight;
                }
                while (row_count > 0)
                {
                    if (pageHeight - top - rowHeight - smallTotal * rowHeight < eh)
                    {
                        top += smallTotal * rowHeight;
                        break;
                    }
                    top += rowHeight;
                    row_count--;
                }
                if (row_count == 0)
                {
                    if (pageHeight - top < dh + eh)
                    {
                        page_index++;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    page_index++;
                }
            }

            return page_index;

        }

        private void Draw(System.Drawing.Graphics g, int area, int pageIndex, int cur_top)
        {
            Helper.ReadXml r = new Helper.ReadXml(xml);

            int offset_y = 0;
            if (area == 1)
            {
                offset_y -= barHeight;
            }
            else if (area == 2)
            {
                offset_y -= ah + 2 * barHeight;
            }
            else if (area == 3)
            {
                offset_y -= ah + bh + barHeight * 3;
            }
            else if (area == 4)
            {
                offset_y -= ah + bh + ch + barHeight * 4;
            }
            else if (area == 5)
            {
                offset_y -= ah + bh + ch + dh + barHeight * 5;

            }
            if (area == 5)
            {
                cur_top = pageHeight - eh;
            }
            foreach (Helper.ReadXml r2 in r.ReadList("PrintObject1"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                top += offset_y + cur_top;
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                string context = r2.Read("Context");
                int Align = Convert.ToInt16(r2.Read("Align"));
                var sf = Helper.Conv.AlignToStringFormat(Align);
                FontConverter fc = new FontConverter();
                var font = (Font)fc.ConvertFromString(r2.Read("Font"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                int _BorderLeft = Convert.ToInt16(r2.Read("BorderLeft"));
                int _BorderRight = Convert.ToInt16(r2.Read("BorderRight"));
                int _BorderTop = Convert.ToInt16(r2.Read("BorderTop"));
                int _BorderBottom = Convert.ToInt16(r2.Read("BorderBottom"));
                Rectangle rec = new Rectangle(left, top, Width, Height);

                if (Area == area)
                {
                    g.DrawString(context, font, new SolidBrush(color), rec, sf);
                    if (_BorderLeft == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left, top + Height);
                    }
                    if (_BorderRight == 1)
                    {
                        g.DrawLine(Pens.Black, left + Width, top, left + Width, top + Height);
                    }
                    if (_BorderTop == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left + Width, top);
                    }
                    if (_BorderBottom == 1)
                    {
                        g.DrawLine(Pens.Black, left, top + Height, left + Width, top + Height);
                    }
                }

            }
            foreach (Helper.ReadXml r2 in r.ReadList("PrintObject2"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                top += offset_y + cur_top;
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                string Field = r2.Read("Field");
                string Format = r2.Read("Format");
                int Align = Convert.ToInt16(r2.Read("Align"));
                var sf = Helper.Conv.AlignToStringFormat(Align);
                FontConverter fc = new FontConverter();
                var font = (Font)fc.ConvertFromString(r2.Read("Font"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                int _BorderLeft = Convert.ToInt16(r2.Read("BorderLeft"));
                int _BorderRight = Convert.ToInt16(r2.Read("BorderRight"));
                int _BorderTop = Convert.ToInt16(r2.Read("BorderTop"));
                int _BorderBottom = Convert.ToInt16(r2.Read("BorderBottom"));
                Rectangle rec = new Rectangle(left, top, Width, Height);

                if (Area == area)
                {
                    if (tbmain.Rows.Count != 0)
                    {
                        if (tbmain.Columns.Contains(Field) == true)
                        {
                            DataRow row = tbmain.Rows[0];
                            DataColumn col = tbmain.Columns[Field];

                            if (tbmain.Columns[Field].DataType == typeof(byte[]))
                            {
                                System.IO.MemoryStream ms = new System.IO.MemoryStream((byte[])row[Field]);
                                Image img = Image.FromStream(ms);
                                g.DrawImage(img, rec);
                                continue;
                            }
                            string context = "";
                            if (Format == "")
                            {
                                context = row[Field].ToString();
                            }
                            else if (Format == "大写金额")
                            {
                                context = Helper.Conv.DaXie2(row[Field].ToString());
                            }
                            else
                            {
                                if (col.DataType == typeof(decimal))
                                {
                                    context = Helper.Conv.ToDecimal(row[Field]).ToString(Format);
                                }
                                else if (col.DataType == typeof(Int16))
                                {
                                    context = Helper.Conv.ToInt16(row[Field]).ToString(Format);
                                }
                                else if (col.DataType == typeof(Int32))
                                {
                                    context = Helper.Conv.ToInt32(row[Field]).ToString(Format);
                                }
                                else if (col.DataType == typeof(DateTime))
                                {
                                    context = Helper.Conv.ToDateTime(row[Field]).ToString(Format);
                                }
                                else
                                {
                                    context = row[Field].ToString();
                                }
                            }

                            g.DrawString(context, font, new SolidBrush(color), rec, sf);
                            if (_BorderLeft == 1)
                            {
                                g.DrawLine(Pens.Black, left, top, left, top + Height);
                            }
                            if (_BorderRight == 1)
                            {
                                g.DrawLine(Pens.Black, left + Width, top, left + Width, top + Height);
                            }
                            if (_BorderTop == 1)
                            {
                                g.DrawLine(Pens.Black, left, top, left + Width, top);
                            }
                            if (_BorderBottom == 1)
                            {
                                g.DrawLine(Pens.Black, left, top + Height, left + Width, top + Height);
                            }
                        }

                    }

                }
            }
            foreach (Helper.ReadXml r2 in r.ReadList("PrintObject3"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                top += offset_y + cur_top;
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                string Field = r2.Read("Field");
                string Format = r2.Read("Format");
                int Align = Convert.ToInt16(r2.Read("Align"));
                var sf = Helper.Conv.AlignToStringFormat(Align);
                FontConverter fc = new FontConverter();
                var font = (Font)fc.ConvertFromString(r2.Read("Font"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                int _BorderLeft = Convert.ToInt16(r2.Read("BorderLeft"));
                int _BorderRight = Convert.ToInt16(r2.Read("BorderRight"));
                int _BorderTop = Convert.ToInt16(r2.Read("BorderTop"));
                int _BorderBottom = Convert.ToInt16(r2.Read("BorderBottom"));
                Rectangle rec = new Rectangle(left, top, Width, Height);

                if (Area == area)
                {
                    if (tbdetail.Rows.Count != 0)
                    {

                        if (tbdetail.Columns.Contains(Field) == true)
                        {
                            var col = tbdetail.Columns[Field];
                            string context = "";
                            decimal val = 0;
                            foreach (DataRow row in tbdetail.Rows)
                            {
                                val += Helper.Conv.ToDecimal(row[Field].ToString());
                            }
                            if (Format == "")
                            {
                                context = val.ToString();
                            }
                            else if (Format == "大写金额")
                            {
                                context = Helper.Conv.DaXie2(val.ToString("0.00"));
                            }
                            else
                            {
                                if (col.DataType == typeof(decimal))
                                {
                                    context = val.ToString(Format);
                                }
                                else if (col.DataType == typeof(Int16))
                                {
                                    context = Helper.Conv.ToInt16(val).ToString(Format);
                                }
                                else if (col.DataType == typeof(Int32))
                                {
                                    context = Helper.Conv.ToInt32(val).ToString(Format);
                                }
                                else if (col.DataType == typeof(DateTime))
                                {
                                    context = Helper.Conv.ToDateTime(val).ToString(Format);
                                }
                                else
                                {
                                    context = val.ToString();
                                }
                            }

                            g.DrawString(context, font, new SolidBrush(color), rec, sf);
                            if (_BorderLeft == 1)
                            {
                                g.DrawLine(Pens.Black, left, top, left, top + Height);
                            }
                            if (_BorderRight == 1)
                            {
                                g.DrawLine(Pens.Black, left + Width, top, left + Width, top + Height);
                            }
                            if (_BorderTop == 1)
                            {
                                g.DrawLine(Pens.Black, left, top, left + Width, top);
                            }
                            if (_BorderBottom == 1)
                            {
                                g.DrawLine(Pens.Black, left, top + Height, left + Width, top + Height);
                            }
                        }

                    }

                }
            }
            foreach (Helper.ReadXml r2 in r.ReadList("PrintObject4"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                top += offset_y + cur_top;
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                if (Area == area)
                {
                    g.DrawLine(new Pen(color), left + Width / 2, top, left + Width / 2, top + Height);
                }

            }
            foreach (Helper.ReadXml r2 in r.ReadList("PrintObject5"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                top += offset_y + cur_top;
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                if (Area == area)
                {
                    g.DrawLine(new Pen(color), left, top + Height / 2, left + Width, top + Height / 2);
                }
            }
            foreach (Helper.ReadXml r2 in r.ReadList("PrintObject6"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                top += offset_y + cur_top;
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                var img = Helper.Conv.StringToImage(r2.Read("Image"));
                int Area = Convert.ToInt16(r2.Read("Area"));
                Rectangle rec = new Rectangle(left, top, Width, Height);
                if (Area == area)
                {
                    g.DrawImage(img, rec);
                }
            }
            foreach (Helper.ReadXml r2 in r.ReadList("PrintObject7"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));

                top += offset_y + cur_top;
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));

                int Align = Convert.ToInt16(r2.Read("Align"));
                var sf = Helper.Conv.AlignToStringFormat(Align);
                FontConverter fc = new FontConverter();
                var font = (Font)fc.ConvertFromString(r2.Read("Font"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                int _BorderLeft = Convert.ToInt16(r2.Read("BorderLeft"));
                int _BorderRight = Convert.ToInt16(r2.Read("BorderRight"));
                int _BorderTop = Convert.ToInt16(r2.Read("BorderTop"));
                int _BorderBottom = Convert.ToInt16(r2.Read("BorderBottom"));
                Rectangle rec = new Rectangle(left, top, Width, Height);

                if (Area == area)
                {

                    string context = "第N页,共M页".Replace("N", pageIndex.ToString())
                        .Replace("M", PageCount.ToString());
                    g.DrawString(context, font, new SolidBrush(color), rec, sf);
                    if (_BorderLeft == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left, top + Height);
                    }
                    if (_BorderRight == 1)
                    {
                        g.DrawLine(Pens.Black, left + Width, top, left + Width, top + Height);
                    }
                    if (_BorderTop == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left + Width, top);
                    }
                    if (_BorderBottom == 1)
                    {
                        g.DrawLine(Pens.Black, left, top + Height, left + Width, top + Height);
                    }
                }
            }
            foreach (Helper.ReadXml r2 in r.ReadList("PrintObject8"))
            {
                int left = Convert.ToInt16(r2.Read("Left"));
                int top = Convert.ToInt16(r2.Read("Top"));
                top += offset_y + cur_top;
                int Width = Convert.ToInt16(r2.Read("Width"));
                int Height = Convert.ToInt16(r2.Read("Height"));
                string format = r2.Read("Format");
                int Align = Convert.ToInt16(r2.Read("Align"));
                var sf = Helper.Conv.AlignToStringFormat(Align);
                FontConverter fc = new FontConverter();
                var font = (Font)fc.ConvertFromString(r2.Read("Font"));
                var color = Color.FromArgb(Convert.ToInt32(r2.Read("Color")));
                int Area = Convert.ToInt16(r2.Read("Area"));
                int _BorderLeft = Convert.ToInt16(r2.Read("BorderLeft"));
                int _BorderRight = Convert.ToInt16(r2.Read("BorderRight"));
                int _BorderTop = Convert.ToInt16(r2.Read("BorderTop"));
                int _BorderBottom = Convert.ToInt16(r2.Read("BorderBottom"));
                Rectangle rec = new Rectangle(left, top, Width, Height);

                if (Area == area)
                {
                    string context = "";
                    if (format == "")
                    {
                        context = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        context = System.DateTime.Now.ToString(format);
                    }
                    g.DrawString(context, font, new SolidBrush(color), rec, sf);
                    if (_BorderLeft == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left, top + Height);
                    }
                    if (_BorderRight == 1)
                    {
                        g.DrawLine(Pens.Black, left + Width, top, left + Width, top + Height);
                    }
                    if (_BorderTop == 1)
                    {
                        g.DrawLine(Pens.Black, left, top, left + Width, top);
                    }
                    if (_BorderBottom == 1)
                    {
                        g.DrawLine(Pens.Black, left, top + Height, left + Width, top + Height);
                    }
                }
            }
        }

        private void DrawGridHeader(Graphics g, int top)
        {

            int l = gridLeft;
            int t = top;

            for (int i = 0; i < tbstyle.Rows.Count; i++)
            {
                var row = tbstyle.Rows[i];
                if (row["display"].ToString() == "1")
                {
                    string colname = row["colname"].ToString();
                    string colbyname = row["colbyname"].ToString();
                    int colwidth = Convert.ToInt16(row["width"].ToString());
                    int align = Convert.ToInt16(row["align"].ToString());
                    var sf = Helper.Conv.AlignToStringFormat(2);
                    string format = row["format"].ToString();
                    Rectangle rec = new Rectangle(l, t, colwidth, rowHeight);

                    g.DrawString(colbyname, gridFont, Brushes.Black, rec, sf);
                    g.DrawRectangle(Pens.Black, rec);

                    l += colwidth;

                }
            }
        }

        private void DrawRow(Graphics g, DataRow dr, int top)
        {
            int l = gridLeft;
            int t = top;

            for (int i = 0; i < tbstyle.Rows.Count; i++)
            {
                var row = tbstyle.Rows[i];
                if (row["display"].ToString() == "1")
                {
                    string colname = row["colname"].ToString();
                    string colbyname = row["colbyname"].ToString();
                    int colwidth = Convert.ToInt16(row["width"].ToString());
                    int align = Convert.ToInt16(row["align"].ToString());
                    var sf = Helper.Conv.AlignToStringFormat(align);
                    string format = row["format"].ToString();
                    Rectangle rec = new Rectangle(l, t, colwidth, rowHeight);
                    string context = "";
                    if (tbdetail.Columns.Contains(colname) == true)
                    {
                        var col = tbdetail.Columns[colname];
                        if (format == "")
                        {
                            context = dr[colname].ToString();
                        }
                        else if (format == "大写金额")
                        {
                            context = Helper.Conv.DaXie2(dr[colname].ToString());
                        }
                        else
                        {
                            if (col.DataType == typeof(decimal))
                            {
                                context = Helper.Conv.ToDecimal(dr[colname]).ToString(format);
                            }
                            else if (col.DataType == typeof(Int16))
                            {
                                context = Helper.Conv.ToInt16(dr[colname]).ToString(format);
                            }
                            else if (col.DataType == typeof(Int32))
                            {
                                context = Helper.Conv.ToInt32(dr[colname]).ToString(format);
                            }
                            else if (col.DataType == typeof(DateTime))
                            {
                                context = Helper.Conv.ToDateTime(dr[colname]).ToString(format);
                            }
                            else
                            {
                                context = dr[colname].ToString();
                            }
                        }
                    }

                    g.DrawString(context, gridFont, Brushes.Black, rec, sf);
                    g.DrawRectangle(Pens.Black, rec);

                    l += colwidth;

                }
            }
        }

        private void DrawEmptyRow(Graphics g, int top)
        {
            int l = gridLeft;
            int t = top;

            for (int i = 0; i < tbstyle.Rows.Count; i++)
            {
                var row = tbstyle.Rows[i];
                if (row["display"].ToString() == "1")
                {
                    string colname = row["colname"].ToString();
                    string colbyname = row["colbyname"].ToString();
                    int colwidth = Convert.ToInt16(row["width"].ToString());
                    int align = Convert.ToInt16(row["align"].ToString());

                    string format = row["format"].ToString();
                    Rectangle rec = new Rectangle(l, t, colwidth, rowHeight);
                    g.DrawRectangle(Pens.Black, rec);

                    l += colwidth;

                }
            }
        }

        private void DrawSmallTotalRow(Graphics g, int top)
        {
            int l = gridLeft;
            int t = top;

            for (int i = 0; i < tbstyle.Rows.Count; i++)
            {
                var row = tbstyle.Rows[i];
                if (row["display"].ToString() == "1")
                {
                    string colname = row["colname"].ToString();
                    string colbyname = row["colbyname"].ToString();
                    int colwidth = Convert.ToInt16(row["width"].ToString());
                    int align = Convert.ToInt16(row["align"].ToString());
                    var sf = Helper.Conv.AlignToStringFormat(align);
                    string format = row["format"].ToString();
                    string smalltotal = row["smalltotal"].ToString();
                    if (colname == "行号")
                    {
                        Rectangle rec = new Rectangle(l, t, colwidth, rowHeight);
                        string context = "小计";

                        g.DrawString(context, gridFont, Brushes.Black, rec, sf);
                        g.DrawRectangle(Pens.Black, rec);

                    }
                    else if (smalltotal == "1")
                    {
                        Rectangle rec = new Rectangle(l, t, colwidth, rowHeight);
                        string context = "";
                        if (tbdetail.Columns.Contains(colname) == true)
                        {
                            decimal val = 0;
                            foreach (DataRow r in lstrow)
                            {
                                val += Helper.Conv.ToDecimal(r[colname].ToString());
                            }
                            var col = tbdetail.Columns[colname];
                            if (format == "")
                            {
                                context = val.ToString();
                            }
                            else if (format == "大写金额")
                            {
                                context = Helper.Conv.DaXie2(val.ToString("0.00"));
                            }
                            else
                            {
                                if (col.DataType == typeof(decimal))
                                {
                                    context = val.ToString(format);
                                }
                                else if (col.DataType == typeof(Int16))
                                {
                                    context = Helper.Conv.ToInt16(val).ToString(format);
                                }
                                else if (col.DataType == typeof(Int32))
                                {
                                    context = Helper.Conv.ToInt32(val).ToString(format);
                                }
                                else if (col.DataType == typeof(DateTime))
                                {
                                    context = Helper.Conv.ToDateTime(val).ToString(format);
                                }
                                else
                                {
                                    context = val.ToString();
                                }
                            }
                        }

                        g.DrawString(context, gridFont, Brushes.Black, rec, sf);
                        g.DrawRectangle(Pens.Black, rec);
                    }
                    else
                    {
                        Rectangle rec = new Rectangle(l, t, colwidth, rowHeight);
                        string context = "";

                        g.DrawString(context, gridFont, Brushes.Black, rec, sf);
                        g.DrawRectangle(Pens.Black, rec);
                    }


                    l += colwidth;

                }
            }
        }

        private System.Drawing.Size PageSize = new System.Drawing.Size(100, 100);
        private int PageCount = 1;
        private int PageIndex = 1;
        int row_count = 0;
        int row_index = 0;
        List<DataRow> lstrow = new List<DataRow>();
        int barHeight = 0;
        int pageWidth = 0;
        int pageHeight = 0;
        int ah = 0;
        int bh = 0;
        int ch = 0;
        int dh = 0;
        int eh = 0;
        int AppendEmptyRow = 0;
        int rowHeight = 0;
        int smallTotal = 0;
        Font gridFont = new Font("宋体", 10);
        int gridLeft = 0;
        private void pd_pagePrint(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Helper.ReadXml r = new Helper.ReadXml(xml);
            //


            int top = 0;
            Draw(g, 1, PageIndex, top);
            top += ah;
            if (PageIndex == 1)
            {
                Draw(g, 2, PageIndex, top);
                top += bh;
            }
            int flag = 0;
            if (row_count > 0)
            {
                DrawGridHeader(g, top);
                top += rowHeight;
                flag = 1;
            }
            while (row_count > 0)
            {
                if (pageHeight - top - rowHeight - smallTotal * rowHeight < eh)
                {
                    if (smallTotal == 1)
                    {
                        DrawSmallTotalRow(g, top);
                        top += smallTotal * rowHeight;
                        lstrow.Clear();
                    }
                    break;
                }
                row_count--;
                //
                DrawRow(g, tbdetail.Rows[row_index], top);
                lstrow.Add(tbdetail.Rows[row_index]);
                //
                row_index++;
                top += rowHeight;
            }
            if (row_count == 0)
            {
                if (pageHeight - top < dh + eh)
                {
                    if (AppendEmptyRow == 1)
                    {
                        while (flag == 1)
                        {
                            if (pageHeight - top - rowHeight - smallTotal * rowHeight < eh)
                            {
                                if (smallTotal == 1)
                                {
                                    DrawSmallTotalRow(g, top);
                                    top += smallTotal * rowHeight;
                                    lstrow.Clear();
                                }
                                break;
                            }
                            DrawEmptyRow(g, top);
                            top += rowHeight;
                        }
                    }
                    else
                    {
                        if (smallTotal == 1)
                        {
                            DrawSmallTotalRow(g, top);
                            top += smallTotal * rowHeight;
                            lstrow.Clear();
                        }
                    }


                    Draw(g, 5, PageIndex, top);
                    top += eh;
                    e.HasMorePages = true;
                }
                else
                {
                    if (AppendEmptyRow == 1)
                    {
                        while (flag == 1)
                        {
                            if (pageHeight - top - rowHeight - smallTotal * rowHeight < dh + eh)
                            {
                                if (smallTotal == 1)
                                {
                                    DrawSmallTotalRow(g, top);
                                    top += smallTotal * rowHeight;
                                    lstrow.Clear();
                                }
                                break;
                            }
                            DrawEmptyRow(g, top);
                            top += rowHeight;
                        }
                    }
                    else
                    {
                        if (smallTotal == 1)
                        {
                            DrawSmallTotalRow(g, top);
                            top += smallTotal * rowHeight;
                            lstrow.Clear();
                        }
                    }

                    Draw(g, 4, PageIndex, top);
                    top += dh;
                    Draw(g, 5, PageIndex, top);
                    top += eh;
                    e.HasMorePages = false;
                }
            }
            else
            {
                Draw(g, 5, PageIndex, top);
                top += eh;
                e.HasMorePages = true;
            }

            PageIndex++;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.printPreviewControl1.StartPage = this.toolStripComboBox1.SelectedIndex;
        }





    }
}
