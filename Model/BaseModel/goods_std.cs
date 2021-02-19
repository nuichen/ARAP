using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class goods_std
    {
        public string goods_id { get; set; }
        public string unit_no { get; set; }
        public string prices { get; set; }
        public string is_default { get; set; }
        public float qty { get; set; }
    }
}
