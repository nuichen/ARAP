using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IPrintObject
    {
        /// <summary>
        /// 1普通文本;2字段;3表格;4竖线;5横线;6图片;7页码;8当前时间
        /// </summary>
        int objectType { get;   }
        void Show(System.Windows.Forms.Control par);
        bool Selected { get; set; }
        void SetSelectControl(IDesign select);
        bool FirstSelected { get; set; }
        string xml { get; set; }
        string propertyInfo { get; }
        IPrintObject Copy();
    }
}
