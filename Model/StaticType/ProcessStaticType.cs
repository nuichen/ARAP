using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Model.StaticType
{
    public class ProcessStaticType
    {
        private ProcessStaticType()
        {
            Init();
        }
        public static readonly ProcessStaticType Examples = new ProcessStaticType();

        public readonly Dictionary<string, string> ItemPropertyDic = new Dictionary<string, string>();
        public readonly Dictionary<string, string> ProcessTypeDic = new Dictionary<string, string>();
        public readonly Dictionary<string, string> OperTypeDic = new Dictionary<string, string>();

        private void Init()
        {
            //0:外购,1:自制
            ItemPropertyDic.Add("0", "外购");
            ItemPropertyDic.Add("1", "自制");

            //加工类型：1：多进A出，2：A进多出，3：多进多出，0：无
            ProcessTypeDic.Add("0", "无");
            ProcessTypeDic.Add("1", "多进A出");
            ProcessTypeDic.Add("2", "A进多出");
            ProcessTypeDic.Add("3", "多进多出");

            //0:外购,1:自制
            OperTypeDic.Add("OM", "领料出库");
            OperTypeDic.Add("RM", "退料入库");
            OperTypeDic.Add("PE", "成品入库");
        }

        private string Format(Dictionary<string, string> dic)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            int index = 0;
            foreach (var r in dic)
            {
                sb.Append($"{r.Key}:{r.Value}");
                if (++index <= dic.Count)
                {
                    sb.Append(",");
                }
            }

            sb.Append("}");
            return sb.ToString();
        }

        private BindingSource BindingData(Dictionary<string, string> obj)
        {
            Dictionary<string, string> dic = new Dictionary<string, string> { { "", "全部" } };
            foreach (var r in obj)
            {
                dic.Add(r.Key, r.Value);
            }
            return new BindingSource(dic, "");
        }

        public string ItemPropertyFormat => Format(ItemPropertyDic);
        public string ProcessTypeFormat => Format(ProcessTypeDic);
        public string OperTypeFormat => Format(OperTypeDic);

        public BindingSource OperTypeSource => BindingData(OperTypeDic);

    }
}