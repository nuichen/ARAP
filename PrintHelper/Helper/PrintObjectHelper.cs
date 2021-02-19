using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PrintHelper.cons;
namespace PrintHelper.Helper
{
    class PrintObjectHelper
    {
        public static Type[] GetTypes(cons.IPrintObject[] objs)
        {
            List<Type> lst = new List<Type>();
         
          
            lst.Add(typeof(cons.IContextable));
            lst.Add(typeof(cons.IContextAlignAble));
            lst.Add(typeof(cons.IFontable));
            lst.Add(typeof(cons.IColorable));
            lst.Add(typeof(cons.IBorderable));
            lst.Add(typeof(cons.IFormatable));
            lst.Add(typeof(cons.IImageAble));
            lst.Add(typeof(cons.IFieldAble));
            lst.Add(typeof(cons.IChangeAreaAble));
            lst.Add(typeof(cons.ISizeable));
            lst.Add(typeof(cons.IDeleteable));
            lst.Add(typeof(cons.IGridable));
            lst.Add(typeof(cons.IPrintObject));
            Dictionary<Type, int> dic = new Dictionary<Type, int>();
            foreach (cons.IPrintObject obj in objs)
            {
                foreach (Type t in lst)
                {
                    if (obj.GetType().GetInterface(t.ToString()) != null)
                    {
                        int cnt;
                        if (dic.TryGetValue(t, out cnt) == false)
                        {
                            dic.Add(t, 1);
                        }
                        else
                        {
                            cnt++;
                            dic.Remove(t);
                            dic.Add(t, cnt);
                        }
                    }
                }
            }
            //
            List<Type> lst2 = new List<Type>();
            foreach (KeyValuePair<Type, int> kv in dic)
            {
                if (kv.Value == objs.Length)
                {
                    lst2.Add(kv.Key);
                }
            }
            return lst2.ToArray();
        }


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

        public static bool RectangleInRectangle(System.Drawing.Rectangle rec, System.Drawing.Rectangle rec2)
        {
            if (rec.IntersectsWith(rec2) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
          

        }

        /// <summary>
        /// 0默认;1左;2右;3上;4下;5左上;6右上;7左下;8右下
        /// </summary>
        /// <param name="p"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public static int MouseOverType(Point p, System.Windows.Forms.Control con)
        {
            Rectangle rec0 = new Rectangle(0, 0, con.Width, con.Height);
            Rectangle rec1 = new Rectangle(0, 0, 5, con.Height);
            Rectangle rec2 = new Rectangle(con.Width - 5, 0, 5, con.Height);
            Rectangle rec3 = new Rectangle(0, 0, con.Width, 5);
            Rectangle rec4 = new Rectangle(0, con.Height - 5, con.Width, 5);
            Rectangle rec5 = new Rectangle(0, 0, 5, 5);
            Rectangle rec6 = new Rectangle(con.Width - 5, 0, 5, 5);
            Rectangle rec7 = new Rectangle(0, con.Height - 5, 5, 5);
            Rectangle rec8 = new Rectangle(con.Width - 5, con.Height - 5, 5, 5);
            if (rec5.Contains(p)==true )
            {
                return 5;
            }
            else if (rec6.Contains(p)==true )
            {
                return 6;
            }
            else if (rec7.Contains(p) == true)
            {
                return 7;
            }
            else if (rec8.Contains(p) == true)
            {
                return 8;
            }
            else if (rec1.Contains(p) == true)
            {
                return 1;
            }
            else if (rec2.Contains(p) == true)
            {
                return 2;
            }
            else if (rec3.Contains(p) == true)
            {
                return 3;
            }
            else if (rec4.Contains(p) == true)
            {
                return 4;
            }
            else
            {
                return 0;
            }
           
        }

        public static System.Windows.Forms.Cursor GetCursor(int MouseOverType)
        {
            if (MouseOverType == 0)
            {
                return System.Windows.Forms.Cursors.Default;
            }
            else if (MouseOverType == 1)
            {
                return System.Windows.Forms.Cursors.SizeWE;
            }
            else if (MouseOverType == 2)
            {
                return System.Windows.Forms.Cursors.SizeWE;
            }
            else if (MouseOverType == 3)
            {
                return System.Windows.Forms.Cursors.SizeNS;
            }
            else if (MouseOverType == 4)
            {
                return System.Windows.Forms.Cursors.SizeNS;
            }
            else if (MouseOverType == 5)
            {
                return System.Windows.Forms.Cursors.SizeNWSE;
                
            }
            else if (MouseOverType == 6)
            {
                return System.Windows.Forms.Cursors.SizeNESW;
            }
            else if (MouseOverType == 7)
            {
                return System.Windows.Forms.Cursors.SizeNESW;
            }
            else if (MouseOverType == 8)
            {
                return System.Windows.Forms.Cursors.SizeNWSE;
            }
            else
            {
                return System.Windows.Forms.Cursors.Default;
            }
            
        }

 
        



        
        

    }
}
