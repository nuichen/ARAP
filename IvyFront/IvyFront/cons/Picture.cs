using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront
{
   public  class Picture:System.Windows.Forms.PictureBox 
    {
       public Picture()
       {
           InitializeComponent();
           this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
       }

       private void InitializeComponent()
       {
           ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
           this.SuspendLayout();
           // 
           // Image
           // 
           //this.DoubleClick += new System.EventHandler(this.Image_DoubleClick);
           ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
           this.ResumeLayout(false);

       }

       private void Image_DoubleClick(object sender, EventArgs e)
       {
           var f = new System.Windows.Forms.OpenFileDialog();
           f.Filter = "*.jpg|*.jpg";
           if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
           {
               this.Image = System.Drawing.Image.FromFile(f.FileName);
           }
       }

       public static string ImageToString(System.Drawing.Image img)
       {
           System.IO.MemoryStream ms = new System.IO.MemoryStream();
           img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
           var bytes = ms.ToArray();
           StringBuilder sb = new StringBuilder();
           foreach (byte b in bytes)
           {
               sb.Append(Convert.ToString(b,2).PadLeft(8,'0'));
           }
           return sb.ToString();
       }

       public static System.Drawing.Image StringToImage(string str)
       {
           List<byte> lst = new List<byte>();
           StringBuilder sb=new StringBuilder();

           for (int i = 0; i < str.Length; i++)
           {
               sb.Append(str[i]);
               if (sb.Length==8)
               {
                   var b = Convert.ToByte(sb.ToString(), 2);
                   lst.Add(b);
                   sb.Clear();
               }

           }
           var bytes = lst.ToArray();

           System.IO.MemoryStream ms = new System.IO.MemoryStream();

           ms.Write (bytes, 0, bytes.Length);
 
           return System.Drawing.Image.FromStream(ms);
       }


    }
}
