using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Model;
using IvyFront.Helper;

namespace IvyFront.Forms
{
    public partial class CGMainForm : Form
    {
        List<bi_t_item_cls> clsList = new List<bi_t_item_cls>();
        List<bi_t_item_info> goodsList = new List<bi_t_item_info>();
        /// <summary>
        /// 所有商品
        /// </summary>
        List<Model.bi_t_item_info> goodsListAll = new List<bi_t_item_info>();
        Dictionary<string, List<Model.bi_t_item_info>> clsDic = new Dictionary<string, List<Model.bi_t_item_info>>();
        Dictionary<string, Model.bi_t_item_info> goodsDic = new Dictionary<string, Model.bi_t_item_info>();
        List<Model.t_order_detail> lines = new List<Model.t_order_detail>();
        private Model.bi_t_item_info currGoods = null;

        private Label lblUp = new Label();
        private Label lblDown = new Label();
        private Label lblLeft = new Label();
        private Label lblRight = new Label();

        private int cls_pageIndex = 0;
        private int cls_selectIndex = 0;
        Point cls_clickPoint = new Point(-100, -100);

        int pageIndex = 0;
        int selectIndex = -1;
        Point clickPoint = new Point(-100, -100);
        List<CellInfo> pageInfo = new List<CellInfo>();
        private string supcus_no = "";
        private decimal Weight = 0.00M;
        //销售流水
        private int sale_page_no = 1;
        private int sale_page_size = 8;
        private decimal total_weight = 0;//连续称重

        public CGMainForm()
        {
            InitializeComponent();
            verify_menu();
            this.Text = "常春藤" + Program.software_name;
            if (Program.is_continue_weight == "1")
            {
                pic_continue_weight.Image = Properties.Resources.weight;
            }
            else
            {
                pic_continue_weight.Image = Properties.Resources.noweight;
            }
            //
            cons.AddClickAct.Add3(lblWeight);
            panel1.Controls.Add(lblWeight);
            lblcussup.Text = "";
            //
            lblUp.Text = "<";
            lblUp.Font = new Font("SimSun", 26);
            lblUp.BackColor = Color.FromArgb(49, 106, 196);
            lblUp.ForeColor = Color.White;
            lblUp.TextAlign = ContentAlignment.MiddleCenter;
            panel1.Controls.Add(lblUp);
            lblUp.Click += this.lblUp_Click;
            cons.AddClickAct.Add2(lblUp);

            lblDown.Text = ">";
            lblDown.Font = new Font("SimSun", 26);
            lblDown.BackColor = Color.FromArgb(49, 106, 196);
            lblDown.ForeColor = Color.White;
            lblDown.TextAlign = ContentAlignment.MiddleCenter;
            panel1.Controls.Add(lblDown);
            lblDown.Click += this.lblDown_Click;
            cons.AddClickAct.Add2(lblDown);
            //
            lblLeft.Text = "<";
            lblLeft.Font = new Font("SimSun", 20);
            lblLeft.BackColor = Color.FromArgb(89, 96, 138);
            lblLeft.ForeColor = Color.White;
            lblLeft.TextAlign = ContentAlignment.MiddleCenter;
            panel4.Controls.Add(lblLeft);
            lblLeft.Click += this.lblLeft_Click;
            cons.AddClickAct.Add2(lblLeft);

            lblRight.Text = ">";
            lblRight.Font = new Font("SimSun", 20);
            lblRight.BackColor = Color.FromArgb(89, 96, 138);
            lblRight.ForeColor = Color.White;
            lblRight.TextAlign = ContentAlignment.MiddleCenter;
            panel4.Controls.Add(lblRight);
            lblRight.Click += this.lblRight_Click;
            cons.AddClickAct.Add2(lblRight);
            //
            lbldate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            lbloper.Text = Program.oper_id + "/" + Program.oper_name;
            lbl_branch_no.Text = Program.branch_no;

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public void WeightMsg(decimal val)
        {
            //Weight = val;

            Weight = decimal.Round(val, 3, MidpointRounding.AwayFromZero); //四舍五入
            lblWeight.Refresh();
        }
        
        private void ShowCls(List<Model.bi_t_item_cls> lst, Control par, ref int pageIndex, Graphics g)
        {
            int w = 80;
            int h = 60;
            int w_offset = 0;
            int h_offset = 0;
            int min_offset = 9999;
            for (int i = 0; i < 20; i++)
            {
                int val = w + i;
                int mod = par.Width % val;
                if (min_offset > mod)
                {
                    min_offset = mod;
                    w_offset = i;
                }
            }

            min_offset = 9999;
            for (int i = 0; i < 20; i++)
            {
                int val = h + i;
                int mod = par.Height % val;
                if (min_offset > mod)
                {
                    min_offset = mod;
                    h_offset = i;
                }
            }

            w += w_offset - 1;
            h += h_offset - 1;
            int colNum = par.Width / w;
            int rowNum = par.Height / h;
            int pageSize = rowNum * colNum;
            pageSize -= 2;
            //
            int index = pageIndex * pageSize;
            if (index > lst.Count)
            {
                pageIndex--;
                index = pageIndex * pageSize;
            }

            SolidBrush bru = new SolidBrush(Color.FromArgb(89, 96, 138));
            SolidBrush bru2 = new SolidBrush(Color.FromArgb(89, 96, 138));
            Pen p = new Pen(Color.White);
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        Rectangle rec = new Rectangle(j * w, i * h, w, h);
                        lblLeft.Location = new Point(rec.Left + 1, rec.Top + 1);
                        lblLeft.Size = new Size(rec.Width - 1, rec.Height - 1);

                        g.DrawRectangle(p, rec);
                    }
                    else if (i == 0 && j == colNum - 1)
                    {
                        Rectangle rec = new Rectangle(j * w, i * h, w, h);
                        lblRight.Location = new Point(rec.Left + 1, rec.Top + 1);
                        lblRight.Size = new Size(rec.Width - 1, rec.Height - 1);

                        g.DrawRectangle(p, rec);
                    }
                    else if (index >= lst.Count)
                    {
                        Rectangle rec = new Rectangle(j * w, i * h, w, h);
                        g.FillRectangle(bru2, rec);
                        g.DrawRectangle(p, rec);
                        clickPoint = new Point(-100, -100);
                        index++;
                    }
                    else
                    {
                        Rectangle rec = new Rectangle(j * w, i * h, w, h);
                        Color ForeColor = Color.White;
                        if (rec.Contains(cls_clickPoint) == true)
                        {
                            cls_selectIndex = index;
                            g.FillRectangle(Brushes.White, rec);
                            cls_clickPoint = new Point(-100, -100);
                            ForeColor = Color.Black;
                        }
                        else if (cls_selectIndex == index)
                        {
                            g.FillRectangle(Brushes.White, rec);
                            ForeColor = Color.Black;
                        }
                        else
                        {
                            g.FillRectangle(bru, rec);
                            ForeColor = Color.White;
                        }

                        g.DrawRectangle(p, rec);
                        var item = lst[index];
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        string item_clsname = "";
                        if (item.item_clsname == null)
                        {
                            item_clsname = "";
                        }
                        else
                        {
                            int flag = 0;
                            for (int k = 1; k <= item.item_clsname.Length; k++)
                            {
                                item_clsname = item.item_clsname.Substring(0, k);
                                System.Drawing.SizeF s1 = g.MeasureString(item_clsname, new Font("SimSun", 12));
                                if (s1.Width > rec.Width)
                                {
                                    item_clsname = item.item_clsname.Substring(0, k - 1);
                                    flag = 1;
                                    break;
                                }
                            }

                            if (flag == 0)
                            {
                                item_clsname = item.item_clsname;
                            }
                        }

                        g.DrawString(item_clsname, new Font("SimSun", 12), new SolidBrush(ForeColor), rec, sf);
                        index++;
                    }
                }
            }
        }

        private void ShowGoods(List<Model.bi_t_item_info> lst, Control par, ref int pageIndex, Graphics g)
        {
            int w = 100;
            int h = 70;
            int w_offset = 0;
            int h_offset = 0;
            int min_offset = 9999;
            for (int i = 0; i < 20; i++)
            {
                int val = w + i;
                int mod = par.Width % val;
                if (min_offset > mod)
                {
                    min_offset = mod;
                    w_offset = i;
                }
            }

            min_offset = 9999;
            for (int i = 0; i < 20; i++)
            {
                int val = h + i;
                int mod = par.Height % val;
                if (min_offset > mod)
                {
                    min_offset = mod;
                    h_offset = i;
                }
            }

            w += w_offset - 1;
            h += h_offset - 1;
            int colNum = par.Width / w;
            int rowNum = par.Height / h;
            int pageSize = rowNum * colNum;
            pageSize -= colNum;
            //
            int index = pageIndex * pageSize;
            if (index > lst.Count - 1)
            {
                pageIndex--;
                index = pageIndex * pageSize;
            }

            SolidBrush bru = new SolidBrush(Color.WhiteSmoke);
            Pen p = new Pen(Color.Gray);
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    
                    if (i == 0 && j == 0)
                    {
                        Rectangle rec = new Rectangle(j * w, i * h, w, h);
                        lblUp.Location = new Point(rec.Left + 1, rec.Top + 1);
                        lblUp.Size = new Size(rec.Width - 1, rec.Height - 1);
                        g.DrawRectangle(p, rec);
                    }
                    else if (i == 0 && j == colNum - 1)
                    {
                        Rectangle rec = new Rectangle(j * w, i * h, w, h);
                        lblDown.Location = new Point(rec.Left + 1, rec.Top + 1);
                        lblDown.Size = new Size(rec.Width - 1, rec.Height - 1);
                        g.DrawRectangle(p, rec);
                    }
                    else if (i == 0)
                    {
                    }
                    else if (index > lst.Count - 1)
                    {
                        Rectangle rec = new Rectangle(j * w, i * h, w, h);
                        g.FillRectangle(bru, rec);
                        g.DrawRectangle(p, rec);
                        clickPoint = new Point(-100, -100);
                        index++;
                    }
                    else
                    {
                        Rectangle rec = new Rectangle(j * w, i * h, w, h);
                        Color ForeColor = Color.Black;
                        if (rec.Contains(clickPoint) == true && index >= 0)
                        {
                            selectIndex = index;
                            currGoods = lst[selectIndex];
                            g.FillRectangle(Brushes.Blue, rec);
                            clickPoint = new Point(-100, -100);
                            ForeColor = Color.White;
                            ShowSelectedGoods();
                        }
                        else if (selectIndex == index)
                        {
                            g.FillRectangle(Brushes.Blue, rec);
                            ForeColor = Color.White;
                        }
                        else
                        {
                            g.FillRectangle(bru, rec);
                            ForeColor = Color.Black;
                        }

                        g.DrawRectangle(p, rec);
                        if (lst.Count > index && index >= 0)
                        {
                            var item = lst[index];
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;
                            Rectangle rec1 = new Rectangle(rec.Left, rec.Top, rec.Width, 40);
                            //g.DrawString(item.item_subno, new Font("SimSun", 12), new SolidBrush(ForeColor), rec1, sf);
                            Rectangle rec2 = new Rectangle(rec.Left, rec.Top + 20, rec.Width, 30);
                            string item_name = "";
                            string item_size = "";
                            if (item != null && item.item_name != null && item.item_name.Length > 0)
                            {
                                item_size = item.item_size;
                                int flag = 0;
                                for (int k = 1; k <= item.item_name.Length; k++)
                                {
                                    item_name = item.item_name.Substring(0, k);
                                    var s1 = g.MeasureString(item_name, new Font("SimSun", 12));
                                    if (s1.Width > rec.Width)
                                    {
                                        item_name = item.item_name.Substring(0, k - 1);
                                        flag = 1;
                                        break;
                                    }
                                }

                                if (flag == 0)
                                {
                                    item_name = item.item_name;
                                }
                            }

                            g.DrawString(item_name, new Font("SimSun", 12), new SolidBrush(ForeColor), rec2, sf);
                            //Rectangle rec3 = new Rectangle(rec.Left, rec.Top + 40, rec.Width, 30);
                            //g.DrawString(item_size, new Font("SimSun", 12), new SolidBrush(ForeColor), rec3, sf);
                            index++;
                        }
                    }
                }

                if (i == 0)
                {
                    Rectangle rec = new Rectangle(1 * w, i * h, (colNum - 2) * w, h);
                    lblWeight.Location = new Point(rec.Left + 1, rec.Top + 1);

                    lblWeight.Size = new Size(rec.Width - 1, rec.Height - 1);

                    g.DrawRectangle(p, rec);
                }
            }
        }

        private void ShowSelectedGoods() 
        {
            if (currGoods != null)
            {
                IBLL.ISaleData bll = new BLL.SaleData();
                if (supcus_no != "")
                {
                    lbl_price.Text = bll.GetSupItemPrice(supcus_no, currGoods.item_no).ToString("F2"); //供应商进价
                }
                else
                {
                    lbl_price.Text = currGoods.price.ToString("F2");//进价
                }
                
                lblgoods.Text = currGoods.item_name;

                //自动取重模式：称重商品将自动取重
                if (Program.weight_model == "2" && currGoods.item_flag == "1")
                {
                    inputQty("取重");
                }
            }
        }

        private void ShowBottom()
        {
            string cmds = "退出收银,归零,去皮,新开采购,输入货商,挂账/预付,现结,手输数量,取重,找商品";

            string[] arr = cmds.Split(',');
            int w = this.Panel3.Width / arr.Length;
            int h = Panel3.Height;
            int left = 0;
            Panel3.Controls.Clear();
            for (int i = 0; i < arr.Length; i++)
            {
                string str = arr[i];
                Label lbl = new Label();
                if (str == "取重" || str == "现结")
                {
                    //lbl.BackColor = Color.Blue;
                    lbl.BackColor = Color.FromArgb(13, 40, 70);
                }
                else 
                {
                    lbl.BackColor = Color.FromArgb(18, 67, 125);
                }
                lbl.Text = str;
                lbl.ForeColor = Color.White;
                lbl.Font = new Font("SimSun", 12.25F);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                if (i == arr.Length - 1)
                {
                    lbl.Width = this.Panel3.Width - left;
                    lbl.Height = h;
                }
                else
                {
                    lbl.Width = w;
                    lbl.Height = h;
                }

                lbl.Left = left;
                left += lbl.Width - 1;
                Panel3.Controls.Add(lbl);
                cons.AddClickAct.Add(lbl);
                lbl.Click += lbl_Click;
                lbl.Paint += lbl_Paint;
            }
        }

        private void newOrder(string text)
        {
            if (lines.Count != 0)
            {
                var frm = new YesNoForm("新增前是否保存本单据？");
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.Yes)
                {
                    CashPayOrder();
                }
            }
            supcus_no = "";
            lblcussup.Text = "";
            lines.Clear();
            this.ShowLine();
            selectIndex = -1;
            currGoods = null;
            panel1.Refresh();

            IBLL.ICGRK bll2 = new BLL.CGRK();
            lblsheet_no.Text = bll2.GetNewOrderCode();
        }

        private void insertCGRK(string pay_way, decimal pay_amt)
        {
            var gd = new Model.bi_t_item_info();

            decimal amt = 0; //单据总金额

            IBLL.ICGRK bll = new BLL.CGRK();
            var lines_cgrk = new List<Model.ic_t_inout_store_detail>();
            int index = 0;
            foreach (var item in this.lines)
            {
                if (goodsDic.TryGetValue(item.item_no, out gd) == false)
                {
                    new MsgForm("商品读取错误").ShowDialog();
                    return;
                }
                ++index;
                var line_cgrk = new Model.ic_t_inout_store_detail();
                line_cgrk.sheet_no = lblsheet_no.Text.Trim();
                line_cgrk.item_no = item.item_no;
                line_cgrk.item_subno = item.item_subno;
                line_cgrk.item_name = item.item_name;
                line_cgrk.unit_no = gd.unit_no;
                line_cgrk.in_qty = item.qty;
                line_cgrk.orgi_price = item.source_price;
                line_cgrk.valid_price = item.price;
                line_cgrk.cost_price = item.price;
                line_cgrk.valid_date = DateTime.Now;
                line_cgrk.barcode = gd.barcode;
                line_cgrk.sheet_sort = index;
                line_cgrk.other3 = item.is_give;
                amt += line_cgrk.valid_price * line_cgrk.in_qty;
                lines_cgrk.Add(line_cgrk);
            }

            var ord = new Model.ic_t_inout_store_master();
            ord.sheet_no = lblsheet_no.Text.Trim();
            ord.trans_no = "A";
            ord.branch_no = Program.branch_no;
            ord.supcust_no = supcus_no;
            ord.total_amount = amt;
            ord.inout_amount = pay_amt;
            ord.approve_flag = "0";
            ord.oper_date = DateTime.Now;
            ord.oper_id = Program.oper_id;
            ord.pay_way = pay_way;
            //新建
            bll.InsertOrder(ord, lines_cgrk);
        }

        //挂账/预付单据
        private void CreditPayOrder()
        {
            if (this.lines.Count <= 0)
            {
                new MsgForm("无商品明细").ShowDialog();
                return;
            }
            IBLL.ISaleData bll = new BLL.SaleData();
            if (supcus_no == "")
            {
                new MsgForm("未选择供应商").ShowDialog();
                return;
            }
            if (new YesNoForm("确认挂账/预付？").ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    insertCGRK("", 0);
                    bll.SubmitOrder(lblsheet_no.Text, 1, Program.oper_id);
                    new MsgForm("单据提交成功！").ShowDialog();
                    clear_data();
                    newOrder("新开采购");
                }
                catch (Exception ex)
                {
                    new MsgForm("操作异常:" + ex.GetMessage()).ShowDialog();
                    return;
                }
            }
        }

        //现结单据
        private void CashPayOrder()
        {
            if (this.lines.Count <= 0)
            {
                new MsgForm("无商品明细").ShowDialog();
                return;
            }
            IBLL.ISaleData bll = new BLL.SaleData();

            if (supcus_no == "")
            {
                new MsgForm("未选择供应商").ShowDialog();
                return;
            }
            frmPay frm2 = new frmPay("1");
            //应付
            decimal yf = lines.Sum(d => d.qty * d.price);
            //实付
            decimal sf = 0;
            string pay_way2 = "";
            var res2 = frm2.Cash(yf, out sf, out pay_way2);
            if (res2)
            {
                try
                {
                    var item = new Model.ot_pay_flow();
                    item.sheet_no = lblsheet_no.Text;
                    item.cus_no = supcus_no;
                    item.oper_id = Program.oper_id;
                    item.oper_date = DateTime.Now;
                    item.pay_way = pay_way2;
                    item.sale_amount = yf;
                    item.pay_amount = sf;
                    item.old_amount = yf;
                    item.jh = Program.jh;
                    item.ml = 0;
                    int flow_id = 0;
                    bll.InsertPayFlow(item, out flow_id);

                    insertCGRK(pay_way2, sf);
                    bll.SubmitOrder(lblsheet_no.Text, 1, Program.oper_id);
                    new MsgForm("单据提交成功！").ShowDialog();
                    clear_data();
                    newOrder("新开采购");
                }
                catch (Exception ex)
                {
                    new MsgForm("操作异常:" + ex.GetMessage()).ShowDialog();
                    return;
                }
            }
        }

        private void clear_data() 
        {
            lines.Clear();
            dataGridView1.Rows.Clear();
            total_weight = 0;
            supcus_no = "";
            lblcussup.Text = "";
            lblsheet_no.Text = "";
            lbl_sum_amt.Text = "0.00";
            lbl_sum_qty.Text = "0.00";
            lbl_total_amt.Text = "0.00";
            lbl_goods_sum.Text = "0.00";
        }

        //取重、输入数量
        private void inputQty(string text)
        {
            if (supcus_no == "")
            {
                new MsgForm("未选择供应商").ShowDialog();
                return;
            }
            if (currGoods != null)
            {
                if (Program.is_continue_weight == "1" && total_weight == Weight)
                {
                    new MsgForm("请先放商品，再取重").ShowDialog();
                    return;
                }
                IBLL.ISaleData bll = new BLL.SaleData();
                IBLL.IGoods goodsBLL = new BLL.Goods();
                Model.bi_t_item_info gd = currGoods;
                Model.t_order_detail item = new Model.t_order_detail();
                item.item_no = gd.item_no;
                switch (text)
                {
                    case "取重":
                        if (Program.is_continue_weight == "1" && total_weight != 0)
                        {
                            item.qty = Weight - total_weight;
                        }
                        else
                        {
                            item.qty = Weight;
                        }
                        break;
                    case "手输数量":
                        if (Program.can_input_qty == "1" && currGoods.item_flag == "1")
                        {
                            new MsgForm("称重商品不可手输数量").ShowDialog();
                            return;
                        }
                        InputNumerForm frm = new InputNumerForm("数量",3);
                        decimal qty = 0;
                        if (frm.Input(out qty) == true)
                        {
                            if (qty <= 0)
                            {
                                new MsgForm("数量不正确").ShowDialog();
                                return;
                            }
                            item.qty = qty;
                        }
                        else
                        {
                            return;
                        }

                        break;
                }
                total_weight = Weight;

                item.price = bll.GetSupItemPrice(supcus_no, item.item_no);//gd.price;
                item.item_subno = gd.item_subno;
                item.item_name = gd.item_name;
                item.unit_no = gd.unit_no;
                item.amt = item.qty * item.price;
                item.sheet_no = lblsheet_no.Text;
                item.oper_id = Program.oper_id;
                item.oper_date = DateTime.Now;
                item.jh = Program.jh;
                item.cost_price = goodsBLL.GetCost(gd.item_no, Program.branch_no);
                item.branch_no = Program.branch_no;
                item.cus_no = supcus_no;
                item.sup_no = supcus_no;
                item.is_give = "0";
                item.source_price = item.price;
                item.discount = 1;

                int flow_id = 0;
                bll.Insert(item, out flow_id);
                item.flow_id = flow_id;
                this.lines.Add(item);
                this.ShowLine();
            }
        }

        private void lblUp_Click(object sender, EventArgs e)
        {
            if (pageIndex > 0)
            {
                pageIndex--;
                this.panel1.Refresh();
            }
        }

        private void lblDown_Click(object sender, EventArgs e)
        {
            pageIndex++;
            this.panel1.Refresh();
        }

        private void lblLeft_Click(object sender, EventArgs e)
        {
            if (cls_pageIndex > 0)
            {
                cls_pageIndex--;
                this.panel4.Refresh();
            }
        }

        private void lblRight_Click(object sender, EventArgs e)
        {
            cls_pageIndex++;
            this.panel4.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (goodsList != null)
            {
                this.ShowGoods(this.goodsList, panel1, ref pageIndex, e.Graphics);
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            hideKeyboard();
            clickPoint = new Point(e.X, e.Y);
            this.panel1.Refresh();
            this.panel1.Refresh();
        }

        private void ShowLine()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.dataGridView1.Rows.Clear();
                var lst = lines.OrderByDescending(d => d.flow_id).Skip((sale_page_no - 1) * sale_page_size).Take(sale_page_size).OrderBy(d => d.flow_id).ToList();
                foreach (Model.t_order_detail item in lst)
                {
                    var index = this.dataGridView1.Rows.Add();
                    var row = this.dataGridView1.Rows[index];
                    row.Tag = item.flow_id;
                    row.Cells[0].Value = item.flow_id.ToString();
                    row.Cells[1].Value = item.item_name;
                    row.Cells[2].Value = item.unit_no;
                    row.Cells[3].Value = item.qty.ToString("F3");
                    row.Cells[4].Value = item.price.ToString("F2");
                    row.Cells[5].Value = (item.qty * item.price).ToString("F2");
                    if (item.is_give == "1")
                    {
                        row.Cells[5].Style.Font = new Font("SimSun", 10.5F, FontStyle.Strikeout);
                    }
                }
                decimal sum_qty = lines.Sum(d => d.qty);
                decimal sum_amt = lines.Sum(d => d.qty * d.price);
                decimal give_amt = lines.Where(d => d.is_give == "1").Sum(d => d.qty * d.source_price);
                lbl_sum_qty.Text = sum_qty.ToString("F3");
                lbl_sum_amt.Text = give_amt.ToString("F2");
                lbl_total_amt.Text = sum_amt.ToString("F2");
            }
            catch (Exception ex)
            {
                var frm = new MsgForm(ex.GetMessage());
                frm.ShowDialog();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        //隐藏找商品小键盘
        private void hideKeyboard() 
        {
            ChooseGoods2.HideKeyboard();
        }

        //找商品小键盘
        private void KeywordChange(string keyword)
        {
            pageIndex = 0;
            List<Model.bi_t_item_info> lst2 = new List<bi_t_item_info>();
            if (keyword != "")
            {
                foreach (Model.bi_t_item_info item in goodsListAll)
                {
                    if (item.item_subno.Contains(keyword) == true || item.item_subname.Contains(keyword) == true || item.barcode.Contains(keyword) == true)
                    {
                        lst2.Add(item);
                    }
                }
            }
            else
            {
                var cls = clsList[cls_selectIndex];
                clsDic.TryGetValue(cls.item_clsno, out lst2);
            }
            this.goodsList = lst2;
            this.panel1.Refresh();
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            Control con = (Control) sender;
            try
            {
                switch (con.Text)
                {
                    case "新开采购":
                        newOrder(con.Text);
                        break;
                    case "输入客户":
                        hideKeyboard();
                        ChooseCus frm = new ChooseCus();
                        var item = frm.Select();
                        if (item != null)
                        {
                            lblcussup.Text = item.supcust_no + "/" + item.sup_name;
                            supcus_no = item.supcust_no;
                        }

                        panel2.Refresh();
                        break;
                    case "输入货商":
                    {
                        hideKeyboard();
                        ChooseSup frm2 = new ChooseSup();
                        var item2 = frm2.Select();
                        if (item2 != null)
                        {
                            lblcussup.Text = item2.supcust_no + "/" + item2.sup_name;
                            supcus_no = item2.supcust_no;
                        }
                    }
                        break;
                    case "找商品":
                    {
                        ChooseGoods2.ShowKeyboard(KeywordChange);
                    }
                        break;
                    case "取重":
                    case "手输数量":
                        inputQty(con.Text);
                        break;
                    case "现结":
                        CashPayOrder();
                        break;
                    case "挂账/预付":
                        CreditPayOrder();
                        break;
                    case "归零":
                        hideKeyboard();
                        Program.ReadWeight.set0();
                        break;
                    case "去皮":
                        hideKeyboard();
                        Program.ReadWeight.qupi();
                        break;
                    case "退出收银":
                        hideKeyboard();
                        if (lines.Count != 0)
                        {
                            if (new YesNoForm("退出前是否保存本单据?").ShowDialog() == DialogResult.Yes)
                            {
                                CashPayOrder();
                            }
                            this.Close();
                        }
                        else 
                        {
                            if (new YesNoForm("确定退出收银?").ShowDialog() == DialogResult.Yes)
                            {
                                this.Close();
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                new MsgForm(ex.GetMessage()).ShowDialog();
            }
        }

        private void lbl_Paint(object sender, PaintEventArgs e)
        {
            Control con = (Control) sender;
            Pen p = new Pen(Color.FromArgb(1, 46, 69));
            e.Graphics.DrawRectangle(p, 0, 0, con.Width - 1, con.Height - 1);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            if (this.clsList != null)
            {
                this.ShowCls(this.clsList, this.panel4, ref cls_pageIndex, e.Graphics);
            }
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            hideKeyboard();
            cls_clickPoint = new Point(e.X, e.Y);
            this.panel4.Refresh();
            this.panel4.Refresh();
            if (clsList.Count > 0 && clsList.Count > cls_selectIndex)
            {
                var cls = clsList[cls_selectIndex];
                clsDic.TryGetValue(cls.item_clsno, out this.goodsList);
                this.pageIndex = 0;
                this.selectIndex = -1;
                this.panel1.Refresh();
            }
        }

        private void lblWeight_Paint(object sender, PaintEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            System.Drawing.SizeF s1 = e.Graphics.MeasureString(Weight.ToString("F3"), new Font("SimHei", 42, FontStyle.Bold));
            System.Drawing.SizeF s2 = e.Graphics.MeasureString("kg", new Font("SimHei", 18));

            e.Graphics.DrawString(Weight.ToString("F3"), new Font("SimHei", 42, FontStyle.Bold), Brushes.Red,
                new Rectangle((lblWeight.Width - (int) s1.Width - (int) s2.Width) / 2, 0, 300, lblWeight.Height - 1),sf);
            e.Graphics.DrawString("kg", new Font("SimHei", 18), Brushes.White,
                new Rectangle((lblWeight.Width - (int) s1.Width - (int) s2.Width) / 2 + (int) s1.Width, 0, 300,lblWeight.Height - 1), sf);
            //
            if (currGoods != null) 
            {
                lbl_goods_sum.Text = (Weight * Conv.ToDecimal(lbl_price.Text)).ToString("F2");
            }
        }

        private void lblWeight_Click(object sender, EventArgs e)
        {
            try
            {
                inputQty("取重");
            }
            catch (Exception ex)
            {
                new MsgForm(ex.GetMessage()).ShowDialog();
            }
        }

        private void Panel3_SizeChanged(object sender, EventArgs e)
        {
            this.ShowBottom();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            pageInfo = null;
            clsList = null;
            goodsList = null;
            goodsListAll = null;
            clsDic = null;
            goodsDic = null;
            lines = null;
            currGoods = null;
            Program.ReadWeight.WeightMsg -= WeightMsg;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                if (Program.ReadWeight.have_ini() == false)
                {
                    Program.ReadWeight.Ini();
                }

                Program.ReadWeight.WeightMsg += new IBLL.WeightMsgHandler(WeightMsg);
            }
            catch (Exception ex)
            {
                new MsgForm(ex.GetMessage()).ShowDialog();
            }
            finally 
            {
                if (frmTop != null) frmTop.Close();
            }
        }

        Form frmTop = new Form();
        private void MainForm_Load(object sender, EventArgs e)
        {
            frmTop.FormBorderStyle = FormBorderStyle.None;
            frmTop.WindowState = FormWindowState.Maximized;
            frmTop.BackColor = Color.FromArgb(30, 110, 165);
            Label lbl = new Label();
            lbl.AutoSize = false;
            lbl.ForeColor = Color.White;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("SimSun", 20);
            lbl.Text = "正在初始数据……";
            lbl.Dock = DockStyle.Fill;
            frmTop.Controls.Add(lbl);
            frmTop.Show();
            Application.DoEvents();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                IBLL.IGoods goodsBLL = new BLL.Goods();
                clsList = goodsBLL.GetClsList();
                goodsListAll = goodsBLL.GetList("", "");

                foreach (Model.bi_t_item_info item in goodsListAll)
                {
                    if (String.IsNullOrWhiteSpace(item.item_no)) continue;
                    goodsDic.Add(item.item_no, item);
                }

                foreach (Model.bi_t_item_cls cls in clsList)
                {
                    List<Model.bi_t_item_info> lst = new List<Model.bi_t_item_info>();
                    foreach (Model.bi_t_item_info gd in goodsListAll)
                    {
                        if (gd.item_clsno.Length>=2 && gd.item_clsno.Substring(0, 2) == cls.item_clsno)
                        {
                            lst.Add(gd);
                        }
                    }

                    clsDic.Add(cls.item_clsno, lst);
                }

                if (clsList.Count != 0)
                {
                    clsDic.TryGetValue(clsList[0].item_clsno, out this.goodsList);
                }

                newOrder("新开采购");
                ShowBottom();
            }
            catch (Exception ex)
            {
                new MsgForm(ex.GetMessage()).ShowDialog();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (sale_page_no > 1)
            {
                --sale_page_no;
                this.ShowLine();
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (lines.Count > sale_page_no * sale_page_size)
            {
                ++sale_page_no;
                this.ShowLine();
            }
        }

        private void btn_give_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            if (row != null && row.Tag != null && row.Tag.ToString() != "")
            {
                if (Program.oper_type != "1000")
                {
                    //new MsgForm("当前用户不属于[系统管理员]组").ShowDialog();
                    return;
                }
                int flow_id = Conv.ToInt(row.Tag);
                var line = this.lines.FirstOrDefault(d => d.flow_id == flow_id);

                IBLL.ISaleData bll = new BLL.SaleData();
                bll.GiveSaleFlow(line.sheet_no,flow_id,Program.oper_id);
                line.is_give = "1";
                line.price = 0;
                line.amt = 0;
                this.ShowLine();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            if (row != null && row.Tag != null && row.Tag.ToString() != "") 
            {
                int flow_id = Conv.ToInt(row.Tag);
                var line = this.lines.FirstOrDefault(d => d.flow_id == flow_id);
                IBLL.ISaleData bll = new BLL.SaleData();
                bll.DeleteSaleFlow(line.sheet_no, flow_id, Program.oper_id);
                this.lines.Remove(line);
                this.ShowLine();
            }
        }

        private void btn_change_price_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            if (row != null && row.Tag != null && row.Tag.ToString() != "")
            {
                if (Program.oper_type != "1000")
                {
                    //new MsgForm("当前用户不属于[系统管理员]组").ShowDialog();
                    return;
                }

                var frm = new InputNumerForm("单价",2);
                decimal price = 0;
                if (frm.Input(out price) == true)
                {
                    if (price <= 0)
                    {
                        new MsgForm("单价不正确").ShowDialog();
                        return;
                    }
                    else
                    {
                        int flow_id = Conv.ToInt(row.Tag);
                        var line = this.lines.FirstOrDefault(d => d.flow_id == flow_id);
                        IBLL.ISaleData bll = new BLL.SaleData();
                        bll.UpdateSaleFlowPrice(line.sheet_no, flow_id, price, Program.oper_id);
                        line.price = price;
                        line.amt = line.qty * line.price;
                        this.ShowLine();
                    }
                }
            }
        }

        private void btn_change_qty_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            if (row != null && row.Tag != null && row.Tag.ToString() != "")
            {
                if (Program.oper_type != "1000")
                {
                    //new MsgForm("当前用户不属于[系统管理员]组").ShowDialog();
                    return;
                }

                InputNumerForm frm = new InputNumerForm("数量",3);
                decimal qty = 0;
                if (frm.Input(out qty) == true)
                {
                    if (qty <= 0)
                    {
                        new MsgForm("数量不正确").ShowDialog();
                        return;
                    }
                    else
                    {
                        int flow_id = Conv.ToInt(row.Tag);
                        var line = this.lines.FirstOrDefault(d => d.flow_id == flow_id);
                        IBLL.ISaleData bll = new BLL.SaleData();
                        bll.UpdateSaleFlowQty(line.sheet_no, flow_id, qty, Program.oper_id);
                        line.qty = qty;
                        line.amt = line.qty * line.price;
                        this.ShowLine();
                    }
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            hideKeyboard();
        }

        private void verify_menu()
        {
            if (Program.oper_type != "1000")
            {
                btn_change_price.Enabled = false;
                btn_change_qty.Enabled = false;
                btn_give.Enabled = false;
            }
            else
            {
                btn_change_price.Enabled = true;
                btn_change_qty.Enabled = true;
                btn_give.Enabled = true;
            }
        }

        private void pic_continue_weight_Click(object sender, EventArgs e)
        {
            if (Program.is_continue_weight == "1")
            {
                Program.is_continue_weight = "0";
                pic_continue_weight.Image = Properties.Resources.noweight;
            }
            else
            {
                Program.is_continue_weight = "1";
                pic_continue_weight.Image = Properties.Resources.weight;
            }
            IBLL.ISysBLL bll = new BLL.SysBLL();
            var lst = new List<Model.bt_par_setting>();
            lst.Add(new Model.bt_par_setting { par_id = "is_continue_weight", par_val = Program.is_continue_weight });
            bll.UpdateParSetting(lst);
        }

    }
}