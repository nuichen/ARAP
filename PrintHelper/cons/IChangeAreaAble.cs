using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IChangeAreaAble
    {
        /// <summary>
        /// 1_页眉；2_页头;3_正文;4_页位;5_页角
        /// </summary>
        int Area { get; set; }
    }
}
