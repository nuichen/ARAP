using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Print
{
    class FontHelper
    {
        public static int GetFontStyle(System.Drawing.Font f)
        {
            int style = 0;
            if (f.Bold == true)
            {
                style += (int)System.Drawing.FontStyle.Bold;
            }
            if (f.Italic == true)
            {
                style += (int)System.Drawing.FontStyle.Italic;
            }
            if (f.Underline == true)
            {
                style += (int)System.Drawing.FontStyle.Underline;
            }
            if (f.Strikeout == true)
            {
                style += (int)System.Drawing.FontStyle.Strikeout;
            }
            return style;
        }
    }
}
