using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Model.StaticType
{
    public class CookbookStaticType
    {
        private CookbookStaticType()
        {
            Init();
        }

        public static readonly CookbookStaticType Examples = new CookbookStaticType();
        public readonly Dictionary<string, string> FormulaTypeDic = new Dictionary<string, string>();
        public readonly Dictionary<string, string> RecipeMenuTypeDic = new Dictionary<string, string>();
        public readonly Dictionary<string, string> UseTypeDic = new Dictionary<string, string>();
        void Init()
        {
            //1:主菜,2:混炒,3:时蔬
            FormulaTypeDic.Add("1", "主菜");
            FormulaTypeDic.Add("2", "混炒");
            FormulaTypeDic.Add("3", "时蔬");

            //1早餐,2午餐,3晚餐,4其他'
            RecipeMenuTypeDic.Add("1", "早餐");
            RecipeMenuTypeDic.Add("2", "午餐");
            RecipeMenuTypeDic.Add("3", "晚餐");
            RecipeMenuTypeDic.Add("4", "其他");

            //人群类型：1老师,2学生,3 BB餐,4其他
            UseTypeDic.Add("1", "老师餐");
            UseTypeDic.Add("2", "学生餐");
            UseTypeDic.Add("3", "BB餐");
            UseTypeDic.Add("4", "通用");
        }

        public string GetFormulaTypeFormat()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            int index = 0;
            foreach (var r in FormulaTypeDic)
            {
                sb.Append($"{r.Key}:{r.Value}");
                if (++index <= FormulaTypeDic.Count)
                {
                    sb.Append(",");
                }
            }

            sb.Append("}");
            return sb.ToString();
        }
        public string GetRecipeMenuTypeFormat()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            int index = 0;
            foreach (var r in RecipeMenuTypeDic)
            {
                sb.Append($"{r.Key}:{r.Value}");
                if (++index <= FormulaTypeDic.Count)
                {
                    sb.Append(",");
                }
            }

            sb.Append("}");
            return sb.ToString();
        }
        public string GetUseTypeFormat()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            int index = 0;
            foreach (var r in UseTypeDic)
            {
                sb.Append($"{r.Key}:{r.Value}");
                if (++index <= FormulaTypeDic.Count)
                {
                    sb.Append(",");
                }
            }

            sb.Append("}");
            return sb.ToString();
        }

        public Dictionary<string, string> GetFormulaTypeBindData()
        {
            Dictionary<string, string> dic = new Dictionary<string, string> { { "", "全部" } };
            foreach (var r in FormulaTypeDic)
            {
                dic.Add(r.Key, r.Value);
            }
            return dic;
        }

        public DataTable GetUserTypeTable()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("Key");
            tb.Columns.Add("Value");

            foreach (var key in UseTypeDic)
            {
                tb.Rows.Add(key.Key, key.Value);
            }

            return tb;
        }

    }
}