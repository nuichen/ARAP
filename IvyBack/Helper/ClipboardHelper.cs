using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyBack.Helper
{
    /// <summary>
    /// 剪贴板操作
    /// </summary>
    public class ClipboardHelper
    {

        public static void SetData<T>(T obj)
        {
            byte[] array = obj.SerializableObject();
            System.Windows.Forms.Clipboard.SetDataObject(array);
        }

        public static T GetData<T>(T obj)
        {
            IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
            byte[] array = (byte[])data.GetData(typeof(byte[]));
            if (array != null && array.Length > 0)
            {
                obj = obj.DeSerializableObject(array);
            }
            return obj;
        }

    }
}
