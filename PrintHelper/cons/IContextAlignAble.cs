using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IContextAlignAble
    {
        /// <summary>
        /// 11_上左；12_上中;13_上右;21_中左;22_中中;23_中右;31_下左;32_下中;33_下右
        /// </summary>
        int Align { get; set; }
    }
}
