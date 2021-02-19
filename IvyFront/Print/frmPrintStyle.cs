using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Xml;

namespace Print
{
    public partial class frmPrintStyle : Form
    {
        public frmPrintStyle()
        {
            InitializeComponent();
        }

        private string fields;
        public frmPrintStyle(string fields)
        {
            InitializeComponent();
            this.fields = fields;

            if (System.IO.File.Exists(Program.path + "\\print_style\\批发销售.xml") == true)
            {
                var xml = System.IO.File.ReadAllText(Program.path + "\\print_style\\批发销售.xml", System.Text.Encoding.GetEncoding("gb2312"));
                ReadFromXML(xml);
            }
        }

        private void PrintStyle_Save(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var sf = new System.Drawing.StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString("抬头区域", new Font("宋体", 9), new SolidBrush(Color.FromArgb(244, 244, 244)), new Rectangle(0, 0, panel1.Width, panel1.Height), sf);
            //
            var p = new Pen(Brushes.LightGray, 1);
            p.DashStyle = DashStyle.Custom;
            p.DashPattern = new Single[] { 1, 1 };
            e.Graphics.DrawLine(p, new Point(0, panel1.Height - 1), new Point(panel1.Width, panel1.Height - 1));
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            var sf = new System.Drawing.StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString("明细区域", new Font("宋体", 9), new SolidBrush(Color.FromArgb(244, 244, 244)), new Rectangle(0, 0, panel2.Width, panel2.Height), sf);
            //
            var p = new Pen(Brushes.LightGray, 1);
            p.DashStyle = DashStyle.Custom;
            p.DashPattern = new Single[] { 1, 1 };
            e.Graphics.DrawLine(p, new Point(0, panel2.Height - 1), new Point(panel2.Width, panel2.Height - 1));
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            var sf = new System.Drawing.StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString("结尾区域", new Font("宋体", 9), new SolidBrush(Color.FromArgb(244, 244, 244)), new Rectangle(0, 0, panel3.Width, panel3.Height), sf);
        }


        private LayoutHelper.LayoutHelper lay1;
        private LayoutHelper.LayoutHelper lay2;
        private LayoutHelper.LayoutHelper lay3;
        private void Form1_Load(object sender, EventArgs e)
        {
            LayoutHelper.LayoutHelper.Create(this);
            LayoutHelper.MouseControlHeight.Bind(this.panel1);
            LayoutHelper.MouseControlHeight.Bind(this.panel2);
            lay1 = LayoutHelper.LayoutHelper.Create(this.panel1);
            lay2 = LayoutHelper.LayoutHelper.Create(this.panel2);
            lay3 = LayoutHelper.LayoutHelper.Create(this.panel3);
        }

        DataTable dt = new DataTable();

        private Point p = new Point(0, 0);
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                cms.Items.Clear();
                cms.Tag = panel1;
                p = new Point(e.X, e.Y);
                foreach (string str in fields.Split(','))
                {
                    cms.Items.Add("#" + str);
                }
                cms.Items.Add("文本");
                cms.Items.Add("横线");
                cms.Items.Add("竖线");
                cms.Items.Add("图片");
                cms.Show((System.Windows.Forms.Control)sender, e.X, e.Y);
            }
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                cms.Items.Clear();
                cms.Tag = panel2;
                p = new Point(e.X, e.Y);
                foreach (string str in fields.Split(','))
                {
                    cms.Items.Add("#" + str);
                }
                cms.Items.Add("文本");
                cms.Items.Add("横线");
                cms.Items.Add("竖线");
                cms.Items.Add("图片");
                cms.Show((System.Windows.Forms.Control)sender, e.X, e.Y);
            }
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                cms.Items.Clear();
                cms.Tag = panel3;
                p = new Point(e.X, e.Y);
                foreach (string str in fields.Split(','))
                {
                    cms.Items.Add("#" + str);
                }
                cms.Items.Add("文本");
                cms.Items.Add("横线");
                cms.Items.Add("竖线");
                cms.Items.Add("图片");
                cms.Show((System.Windows.Forms.Control)sender, e.X, e.Y);
            }
        }


        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Save != null)
                //{
                //    Save.Invoke(this, new EventArgs());
                //}
                string xml = SaveToXML();
                System.IO.File.WriteAllText(Program.path + "\\print_style\\批发销售.xml", xml, System.Text.Encoding.GetEncoding("gb2312"));
                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存异常：" + ex.Message);
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string SaveToXML()
        {

            //
            var sb = new StringBuilderForXML();
            sb.Append("<xml>");
            //
            sb.Append("width", pnl.Width.ToString());
            sb.Append("height", pnl.Height.ToString());
            //
            sb.Append("<block1>");
            sb.Append("width", panel1.Width.ToString());
            sb.Append("height", panel1.Height.ToString());
            AppendItem(panel1, sb);
            sb.Append("</block1>");
            //
            sb.Append("<block2>");
            sb.Append("width", panel2.Width.ToString());
            sb.Append("height", panel2.Height.ToString());
            AppendItem(panel2, sb);
            sb.Append("</block2>");
            //
            sb.Append("<block3>");
            sb.Append("width", panel3.Width.ToString());
            sb.Append("height", panel3.Height.ToString());
            AppendItem(panel3, sb);
            sb.Append("</block3>");
            //
            sb.Append("</xml>");
            //
            return sb.ToString();
        }

        private void AppendItem(System.Windows.Forms.Control par, StringBuilderForXML sb)
        {
            foreach (System.Windows.Forms.Control con in par.Controls)
            {
                if (con.Visible == false)
                {
                    continue;
                }
                if (con.GetType() == typeof(FieldLabel))
                {
                    FieldLabel c = (FieldLabel)con;
                    sb.Append("<field>");
                    sb.Append("field", c.Field);
                    sb.Append("left", c.Left.ToString());
                    sb.Append("top", c.Top.ToString());
                    sb.Append("width", c.Width.ToString());
                    sb.Append("height", c.Height.ToString());
                    sb.Append("font_name", c.Font.FontFamily.Name);
                    sb.Append("font_size", c.Font.Size.ToString());
                    sb.Append("font_style", FontHelper.GetFontStyle(c.Font).ToString());
                    sb.Append("format", c.Format);
                    sb.Append("</field>");
                }
                if (con.GetType() == typeof(TextLabel))
                {
                    TextLabel c = (TextLabel)con;
                    sb.Append("<text>");
                    sb.Append("text", c.Text);
                    sb.Append("left", c.Left.ToString());
                    sb.Append("top", c.Top.ToString());
                    sb.Append("width", c.Width.ToString());
                    sb.Append("height", c.Height.ToString());
                    sb.Append("font_name", c.Font.FontFamily.Name);
                    sb.Append("font_size", c.Font.Size.ToString());
                    sb.Append("font_style", FontHelper.GetFontStyle(c.Font).ToString());
                    sb.Append("</text>");
                }
                if (con.GetType() == typeof(Line))
                {
                    Line c = (Line)con;
                    sb.Append("<line>");
                    sb.Append("left", c.Left.ToString());
                    sb.Append("top", c.Top.ToString());
                    sb.Append("width", c.Width.ToString());
                    sb.Append("height", c.Height.ToString());
                    sb.Append("</line>");
                }
                if (con.GetType() == typeof(LineII))
                {
                    LineII c = (LineII)con;
                    sb.Append("<lineII>");
                    sb.Append("left", c.Left.ToString());
                    sb.Append("top", c.Top.ToString());
                    sb.Append("width", c.Width.ToString());
                    sb.Append("height", c.Height.ToString());
                    sb.Append("</lineII>");
                }
                if (con.GetType() == typeof(Picture))
                {
                    Picture c = (Picture)con;
                    sb.Append("<picture>");
                    sb.Append("left", c.Left.ToString());
                    sb.Append("top", c.Top.ToString());
                    sb.Append("width", c.Width.ToString());
                    sb.Append("height", c.Height.ToString());
                    sb.Append("data", Picture.ImageToString(c.Image));
                    sb.Append("</picture>");
                }
            }
        }

        public void ReadFromXML(string xml)
        {
            try
            {
                ReadXml read = new ReadXml(xml);
                //
                this.pnl.Width = Convert.ToInt32(read.Read("width"));
                this.pnl.Height = Convert.ToInt32(read.Read("height"));
                //
                this.panel1.Width = Convert.ToInt32(read.Read("block1/width"));
                this.panel1.Height = Convert.ToInt32(read.Read("block1/height"));
                this.panel2.Width = Convert.ToInt32(read.Read("block2/width"));
                this.panel2.Height = Convert.ToInt32(read.Read("block2/height"));
                this.panel3.Width = Convert.ToInt32(read.Read("block3/width"));
                this.panel3.Height = Convert.ToInt32(read.Read("block3/height"));
                //
                ReadField(this.panel1, read, "block1/field");
                ReadField(this.panel2, read, "block2/field");
                ReadField(this.panel3, read, "block3/field");
                //
                ReadText(this.panel1, read, "block1/text");
                ReadText(this.panel2, read, "block2/text");
                ReadText(this.panel3, read, "block3/text");
                //
                ReadLine(this.panel1, read, "block1/line");
                ReadLine(this.panel2, read, "block2/line");
                ReadLine(this.panel3, read, "block3/line");
                //
                ReadLineII(this.panel1, read, "block1/lineII");
                ReadLineII(this.panel2, read, "block2/lineII");
                ReadLineII(this.panel3, read, "block3/lineII");
                //
                ReadPicture(this.panel1, read, "block1/picture");
                ReadPicture(this.panel2, read, "block2/picture");
                ReadPicture(this.panel3, read, "block3/picture");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void ReadField(System.Windows.Forms.Control par, ReadXml read, string path)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                var con = new FieldLabel(r.Read("field"));
                con.Left = Convert.ToInt32(r.Read("left"));
                con.Top = Convert.ToInt32(r.Read("top"));
                con.Width = Convert.ToInt32(r.Read("width"));
                con.Height = Convert.ToInt32(r.Read("height"));
                con.Font = new Font(r.Read("font_name"), Convert.ToSingle(r.Read("font_size")), (FontStyle)Convert.ToInt32(r.Read("font_style")));
                con.Field = r.Read("field");
                con.Format = r.Read("format");
                par.Controls.Add(con);
            }
        }

        private void ReadText(System.Windows.Forms.Control par, ReadXml read, string path)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                var con = new TextLabel();
                con.Left = Convert.ToInt32(r.Read("left"));
                con.Top = Convert.ToInt32(r.Read("top"));
                con.Width = Convert.ToInt32(r.Read("width"));
                con.Height = Convert.ToInt32(r.Read("height"));
                con.Font = new Font(r.Read("font_name"), Convert.ToSingle(r.Read("font_size")), (FontStyle)Convert.ToInt32(r.Read("font_style")));
                con.Text = r.Read("text");
                par.Controls.Add(con);
            }
        }

        private void ReadLine(System.Windows.Forms.Control par, ReadXml read, string path)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                var con = new Line();
                con.Left = Convert.ToInt32(r.Read("left"));
                con.Top = Convert.ToInt32(r.Read("top"));
                con.Width = Convert.ToInt32(r.Read("width"));
                con.Height = Convert.ToInt32(r.Read("height"));
                par.Controls.Add(con);
            }
        }

        private void ReadLineII(System.Windows.Forms.Control par, ReadXml read, string path)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                var con = new LineII();
                con.Left = Convert.ToInt32(r.Read("left"));
                con.Top = Convert.ToInt32(r.Read("top"));
                con.Width = Convert.ToInt32(r.Read("width"));
                con.Height = Convert.ToInt32(r.Read("height"));
                par.Controls.Add(con);
            }
        }

        private void ReadPicture(System.Windows.Forms.Control par, ReadXml read, string path)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                var con = new Picture();
                con.Left = Convert.ToInt32(r.Read("left"));
                con.Top = Convert.ToInt32(r.Read("top"));
                con.Width = Convert.ToInt32(r.Read("width"));
                con.Height = Convert.ToInt32(r.Read("height"));

                con.Image = Picture.StringToImage(r.Read("data"));
                par.Controls.Add(con);
            }
        }

        public event EventHandler Save;


        private void cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            cms.Visible = false;
            System.Windows.Forms.Control par = (System.Windows.Forms.Control)cms.Tag;
            if (e.ClickedItem.Text.StartsWith("#") == true)//字段
            {
                var lbl = new FieldLabel(e.ClickedItem.Text.Substring(1));
                lbl.AutoSize = false;
                lbl.Location = p;
                par.Controls.Add(lbl);
            }
            else if (e.ClickedItem.Text == "文本")
            {
                var frm = new frmInputbox("文本");
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var lbl = new TextLabel();
                    lbl.AutoSize = false;
                    lbl.Text = frm.Context();
                    lbl.Location = p;
                    par.Controls.Add(lbl);
                }

            }
            else if (e.ClickedItem.Text == "横线")
            {
                var line = new Line();
                line.Location = p;
                par.Controls.Add(line);
            }
            else if (e.ClickedItem.Text == "竖线")
            {
                var line = new LineII();
                line.Location = p;
                par.Controls.Add(line);
            }
            else if (e.ClickedItem.Text == "图片")
            {
                var f = new System.Windows.Forms.OpenFileDialog();
                f.Filter = "*.jpg|*.jpg";
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var pic = new Picture();
                    pic.Image = Image.FromFile(f.FileName);
                    pic.Location = p;
                    par.Controls.Add(pic);
                }
            }

        }

        private void 纸宽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmInputbox((this.pnl.Width / getAnCMInterval()).ToString());
            frm.label1.Text = "纸宽(cm)";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Single width = 0;
                Single.TryParse(frm.Context(), out width);
                if (width != 0)
                {
                    this.pnl.Width = (int)(width * getAnCMInterval());
                }
            }
        }

        private Single getAnCMInterval()
        {
            return System.Drawing.Printing.PrinterUnitConvert.Convert(100,
            System.Drawing.Printing.PrinterUnit.TenthsOfAMillimeter, System.Drawing.Printing.PrinterUnit.Display);
        }

        private void 制定打印机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetPrinter frmSetPrinter = new FrmSetPrinter();
            frmSetPrinter.ShowDialog();
        }





    }
}
