using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PrintHelper.cons
{
    public partial class PrintObject6 : UserControl,IPrintObject ,IImageAble ,IDeleteable ,ISizeable ,IChangeAreaAble 
    {
        /// <summary>
        /// 图片框
        /// </summary>
        public PrintObject6()
        {
            InitializeComponent();
            //
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.AllPaintingInWmPaint, true);
            //
            this.BackColor = Color.White;
        }

        IPrintObject IPrintObject.Copy()
        {
            IPrintObject ins = this;
            IPrintObject item = new PrintObject6();
            string xml = "<xml>" + ins.xml + "</xml>";
            Helper.ReadXml r = new Helper.ReadXml(xml);
            xml = "<xml>" + r.Read("PrintObject6") + "</xml>";
            item.xml = xml;
            return item;
        }

        int IPrintObject.objectType
        {
            get { return 6; }
        }

        void IPrintObject.Show(Control par)
        {
            par.Controls.Add(this);
            this.BringToFront();
        }

        private bool _Selected = false;
        bool IPrintObject.Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                if (_Selected == false)
                {
                    select.Remove(this);
                    _FirstSelected = false;
                }
                else
                {
                    IPrintObject ins = this;
                    ins.FirstSelected = true;
                    select.Add(this);
                    select.FirstSelectObject = this;
                }
                this.Refresh();
            }
        }

        private IDesign select;
        void IPrintObject.SetSelectControl(IDesign select)
        {
            this.select = select;
            Helper.PrintObjectMouse.Bind(this, select);
        }

        private bool _FirstSelected = false;
        bool IPrintObject.FirstSelected
        {
            get
            {
                return _FirstSelected;
            }
            set
            {
                _FirstSelected = value ;
                this.Refresh();
            }
        }

       
        string IPrintObject.xml
        {
            get
            {
                Helper.StringBuilderForXML sb = new Helper.StringBuilderForXML();
                sb.Append("Left", this.Left.ToString());
                sb.Append("Top", this.Top.ToString());
                sb.Append("Width", this.Width.ToString());
                sb.Append("Height", this.Height.ToString());
                cons.IImageAble imageable = this;
                sb.Append("Image", Helper.Conv.ImageToString(imageable.Image));
                sb.Append("Area", this._Area.ToString());
                string str = sb.ToString();
                sb.Clear();
                sb.Append("PrintObject6", str);
                return sb.ToString();
            }
            set
            {

                Helper.ReadXml r = new Helper.ReadXml(value);
                int left, top, width, height;
                int.TryParse(r.Read("Left"), out left);
                int.TryParse(r.Read("Top"), out top);
                int.TryParse(r.Read("Width"), out width);
                int.TryParse(r.Read("Height"), out height);
                this.Left = left;
                this.Top = top;
                this.Width = width;
                this.Height = height;
                cons.IImageAble imageable = this;
              
                imageable.Image = Helper.Conv.StringToImage(r.Read("Image"));
                IChangeAreaAble areaable = this;
                areaable.Area = Convert.ToInt16(r.Read("Area"));
                //
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_image != null)
            {
                e.Graphics.DrawImage(_image, 0, 0, this.Width, this.Height);
            }
            if (_Selected == false)
            {
                Pen p = new Pen(Color.Gray);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            }
            else
            {
                if (_FirstSelected == true)
                {
                    e.Graphics.DrawRectangle(new Pen(Color.Blue, 2), 1, 1, this.Width - 2, this.Height - 2);
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.Blue, 0, 0, this.Width - 1, this.Height - 1);
                }
            }
        }

        private Image _image;
        Image IImageAble.Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                this.Refresh();
            }
        }

    

        private void PrintObject6_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }


        void IDeleteable.Delete(Control par)
        {
            IPrintObject ins = this;
            ins.Selected = false;
            par.Controls.Remove(this);
        }


        string IPrintObject.propertyInfo
        {
            get { return "类型：图片"; }
        }

        private int _Area = 1;
        int IChangeAreaAble.Area
        {
            get
            {
                return _Area;
            }
            set
            {
                _Area = value;
            }
        }
    }
}
