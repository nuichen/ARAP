using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace IvyFront
{
    public class printXSCK
    {
        private DataTable dtPrint;

        public printXSCK(DataTable dt)
        {
            dtPrint = dt;
        }

        public void print()
        {
            try
            {
                PrintDocument print = new PrintDocument();
                int height = GetHight();

                print.PrintPage += print_printpage;
                print.DefaultPageSettings.PaperSize = new PaperSize("printer", 1000, height);
                //  print.DefaultPageSettings.PaperSize = new PaperSize("printer", 1000, 2000);

                print.PrinterSettings.PrinterName = Appsetting.print_name;
                PrintController printController = new StandardPrintController();
                print.PrintController = printController;
                print.Print();
            }
            catch (Exception ex)
            {
                throw new Exception("请检查打印机设置。");
            }
        }

        private int GetHight()
        {
            int height = 0;

            var xml = System.IO.File.ReadAllText(Program.path + "\\print_style\\批发销售.xml",
                System.Text.Encoding.GetEncoding("gb2312"));
            ReadXml read = new ReadXml(xml);

            int block1Height = Convert.ToInt32(read.Read("block1/height"));
            int block2Height = Convert.ToInt32(read.Read("block2/height"));
            int block3Height = Convert.ToInt32(read.Read("block3/height"));

            height += block1Height;

            foreach (DataRow row in dtPrint.Rows)
            {
                height += block2Height;
            }

            height += block3Height;

            if (height < 550) height = 550;

            if (height % 550 != 0)
            {
                height += 550 - height % 550;
            }

            return height;
        }

        private void print_printpage(object sender, PrintPageEventArgs e)
        {
            int top = 0;
            var xml = System.IO.File.ReadAllText(Program.path + "\\print_style\\批发销售.xml", System.Text.Encoding.GetEncoding("gb2312"));
            ReadXml read = new ReadXml(xml);
            //
            int block1Height = Convert.ToInt32(read.Read("block1/height"));
            int block2Height = Convert.ToInt32(read.Read("block2/height"));
            int block3Height = Convert.ToInt32(read.Read("block3/height"));
            //
            DrawField(e.Graphics, read, top, "block1/field", dtPrint.Rows[0]);
            DrawText(e.Graphics, read, top, "block1/text");
            DrawLine(e.Graphics, read, top, "block1/line");
            DrawLineII(e.Graphics, read, top, "block1/lineII");
            DrawPicture(e.Graphics, read, top, "block1/picture");
            top += block1Height;
            //
            foreach (DataRow row in dtPrint.Rows)
            {
                DrawField(e.Graphics, read, top, "block2/field", row);
                DrawText(e.Graphics, read, top, "block2/text");
                DrawLine(e.Graphics, read, top, "block2/line");
                DrawLineII(e.Graphics, read, top, "block2/lineII");
                DrawPicture(e.Graphics, read, top, "block2/picture");
                top += block2Height;
            }

            //
            DrawField(e.Graphics, read, top, "block3/field", dtPrint.Rows[0]);
            DrawText(e.Graphics, read, top, "block3/text");
            DrawLine(e.Graphics, read, top, "block3/line");
            DrawLineII(e.Graphics, read, top, "block3/lineII");
            DrawPicture(e.Graphics, read, top, "block3/picture");
        }

        private void DrawField(Graphics g, ReadXml read, int add_top, string path, DataRow row)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                int left = Convert.ToInt32(r.Read("left"));
                int top = add_top + Convert.ToInt32(r.Read("top"));
                int width = Convert.ToInt32(r.Read("width"));
                int height = Convert.ToInt32(r.Read("height"));
                var f = new Font(r.Read("font_name"), Convert.ToSingle(r.Read("font_size")),
                    (FontStyle) Convert.ToInt32(r.Read("font_style")));
                if (row.Table.Columns.Contains(r.Read("field")) == true)
                {
                    object obj = row[r.Read("field")];
                    string value = "";
                    if (obj == DBNull.Value)
                    {
                        value = "";
                    }
                    else if (r.Read("format") == "")
                    {
                        value = obj.ToString();
                    }
                    else
                    {
                        try
                        {
                            switch (row.Table.Columns[r.Read("field")].DataType.FullName)
                            {
                                case "System.String":
                                    value = obj.ToString();
                                    break;
                                case "System.Int16":
                                    value = Convert.ToInt32(obj).ToString(r.Read("format"));
                                    break;
                                case "System.Int32":
                                    value = Convert.ToInt32(obj).ToString(r.Read("format"));
                                    break;
                                case "System.Int64":
                                    value = Convert.ToInt64(obj).ToString(r.Read("format"));
                                    break;
                                case "System.Decimal":
                                    value = Convert.ToDecimal(obj).ToString(r.Read("format"));
                                    break;
                                case "System.Double":
                                    value = Convert.ToDouble(obj).ToString(r.Read("format"));
                                    break;
                                case "System.Float":
                                    value = Convert.ToSingle(obj).ToString(r.Read("format"));
                                    break;
                                case "System.DateTime":
                                    value = Convert.ToDateTime(obj).ToString(r.Read("format"));
                                    break;
                                default:
                                    value = obj.ToString();
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            value = obj.ToString();
                        }
                    }

                    g.DrawString(value, f, Brushes.Black, new Rectangle(left, top, width, height));
                }
            }
        }

        private void DrawText(Graphics g, ReadXml read, int add_top, string path)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                int left = Convert.ToInt32(r.Read("left"));
                int top = add_top + Convert.ToInt32(r.Read("top"));
                int width = Convert.ToInt32(r.Read("width"));
                int height = Convert.ToInt32(r.Read("height"));
                var f = new Font(r.Read("font_name"), Convert.ToSingle(r.Read("font_size")),
                    (FontStyle) Convert.ToInt32(r.Read("font_style")));
                string text = r.Read("text");
                g.DrawString(text, f, Brushes.Black, new Rectangle(left, top, width, height));
            }
        }

        private void DrawLine(Graphics g, ReadXml read, int add_top, string path)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                int left = Convert.ToInt32(r.Read("left"));
                int top = add_top + Convert.ToInt32(r.Read("top"));
                int width = Convert.ToInt32(r.Read("width"));
                int height = Convert.ToInt32(r.Read("height"));
                g.DrawLine(Pens.Black, new Point(left, top), new Point(left + width, top));
            }
        }

        private void DrawLineII(Graphics g, ReadXml read, int add_top, string path)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                int left = Convert.ToInt32(r.Read("left"));
                int top = add_top + Convert.ToInt32(r.Read("top"));
                int width = Convert.ToInt32(r.Read("width"));
                int height = Convert.ToInt32(r.Read("height"));
                g.DrawLine(Pens.Black, new Point(left, top), new Point(left, top + height));
            }
        }

        private void DrawPicture(Graphics g, ReadXml read, int add_top, string path)
        {
            foreach (ReadXml r in read.ReadList(path))
            {
                int left = Convert.ToInt32(r.Read("left"));
                int top = add_top + Convert.ToInt32(r.Read("top"));
                int width = Convert.ToInt32(r.Read("width"));
                int height = Convert.ToInt32(r.Read("height"));
                Image img = Picture.StringToImage(r.Read("data"));
                g.DrawImage(img, new Rectangle(left, top, width, height));
            }
        }
    }
}