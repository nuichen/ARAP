using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IvyBack;
using IvyBack.Helper;
using IvyBack.MainForm;
using IvyBack.SysForm.market;
using Model;
using Model.StaticType;

namespace UnitTest_IvyBack
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string con = "user id=ivytalktm;password=346440713;initial catalog=ivytalkag_11; data source=123.57.33.28,4001";
            IDB db = new DBByAutoClose(con);

            DataTable tb = db.ExecuteToTable("SELECT top 10 * FROM dbo.bi_t_item_info where 1=0", null);

            IvyTran.Helper.WebHelper webHelper = new IvyTran.Helper.WebHelper();
            webHelper.WriteTableStruct(tb);
            string json = webHelper.NmJson();
            Console.WriteLine(json);

            IvyBack.Helper.JsonRequest r = new IvyBack.Helper.JsonRequest();
            r.SetJson(json);

            DataTable dt = r.GetDataTable();

            Console.WriteLine("完成");
        }

        [TestMethod]
        public void TestMethod02()
        {
            string str = "123,456,456,78,45,415212,";
            var arr = str.Split(',');
            foreach (var s in arr)
            {
                Console.WriteLine(s);
            }
        }

        [TestMethod]
        public void TestMethod03()
        {
            string str = "客户销售:asd";

            Console.WriteLine(str.Substring(0, str.IndexOf(':') + 1));
        }

        [TestMethod]
        public void METHOD()
        {
            Form frm = new Form();
            frm.Width = 500;
            frm.Height = 600;

            frmLoad.LoadWait(frm,
                Task.Factory.StartNew(new Action(() =>
                {
                    Thread.Sleep(50000);
                })), "正在生成");
        }

        [TestMethod]
        [STAThread]
        public void Method2()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmTest frm = new frmTest();
            frm.ShowDialog();
        }

        [TestMethod]
        public void Method3()
        {
            var dic = ProcessStaticType.Examples.ItemPropertyDic.FirstOrDefault(d => d.Value.Equals("外购"));
            if (default(KeyValuePair<string, string>).Equals(dic))
            {
                Console.WriteLine("空的");
            }
            else
            {
                Console.WriteLine("");
            }
        }

        [TestMethod]
        public void Method4()
        {
            ExcelHelper excel = new ExcelHelper();
            string fileName = @"C:\Users\77359\Desktop\菜谱档案导入模板.xlsx";

            DataTable tb = excel.ExportDataTableAsString(fileName);
            Console.WriteLine(tb);
        }

        [TestMethod]
        public void Method5()
        {
            string useTypeString = "老师餐/学生餐/通用";
            bi_t_formula_main formula = new bi_t_formula_main();
            foreach (var key in CookbookStaticType.Examples.UseTypeDic)
            {
                if (useTypeString.Contains(key.Value))
                {
                    if (!string.IsNullOrWhiteSpace(formula.formula_pro))
                    {
                        formula.formula_pro += ",";
                    }
                    formula.formula_pro += key.Key;
                }
            }

            Console.WriteLine(formula.formula_pro);
        }


        [TestMethod]
        public void Method06()
        {
            DataTable tbs = new DataTable();
            tbs.Columns.Add("Col");
            tbs.Rows.Add("1");

            System.Windows.Forms.Clipboard.SetDataObject(tbs, true, 3, 100);

            IDataObject data = Clipboard.GetDataObject();
            DataTable tb = data.GetData(typeof(DataTable)) as DataTable;

            if (tb == null)
            {
                Console.WriteLine("复制失败");
            }
            else
            {
                Console.WriteLine(tb.Rows[0][0].ToString());
            }

        }

        [TestMethod]
        public void Method07()
        {
            Console.WriteLine("0".ToBool());
            Console.WriteLine("1".ToBool());
        }

    }
}
