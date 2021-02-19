using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tran.Helper
{
    class RecHelper
    {

        public static System.Drawing.Rectangle CreateRectangle(System.Drawing.Point p, System.Drawing.Point p2)
        {
            int width = Math.Abs(p.X - p2.X);
            int height = Math.Abs(p.Y - p2.Y);
            int x, y;
            if (p.X > p2.X)
            {
                x = p2.X;
            }
            else
            {
                x = p.X;
            }
            if (p.Y > p2.Y)
            {
                y = p2.Y;
            }
            else
            {
                y = p.Y;
            }
            var rec = new System.Drawing.Rectangle(x, y, width, height);
            return rec;
        }

        public static int OverPixel(Rectangle rec1, Rectangle rec2)
        {
            int left1 = rec1.Left;
            int top1 = rec1.Top;
            int right1 = rec1.Right;
            int bottom1 = rec1.Bottom;
            int left2 = rec2.Left;
            int top2 = rec2.Top;
            int right2 = rec2.Right;
            int bottom2 = rec2.Bottom;
            int overlapX = (right1 - left1) + (right2 - left2) - (max(right1, right2) - min(left1, left2));
            int overlapY = (bottom1 - top1) + (bottom2 - top2) - (max(bottom1, bottom2) - min(top1, top2));
            if (overlapX < 0)
            {
                return 0;
            }
            if (overlapY < 0)
            {
                return 0;
            }
            else
            {
                return overlapX * overlapY;
            }


        }

        public static double inter(Rectangle rec1, Rectangle rec2)
        {
            Point p = new Point(rec1.Left + rec1.Width / 2, rec1.Top + rec1.Height / 2);
            Point p2 = new Point(rec2.Left + rec2.Width / 2, rec2.Top + rec2.Height / 2);
            double value = Math.Sqrt(Math.Abs(p.X - p2.X) * Math.Abs(p.X - p2.X) + Math.Abs(p.Y - p2.Y) * Math.Abs(p.Y - p2.Y));
            return value;
        }

        private static int max(int val1, int val2)
        {
            if (val1 > val2)
            {
                return val1;
            }
            else
            {
                return val2;
            }
        }

        private static int min(int val1, int val2)
        {
            if (val1 > val2)
            {
                return val2;
            }
            else
            {
                return val1;
            }
        }

    }
}
