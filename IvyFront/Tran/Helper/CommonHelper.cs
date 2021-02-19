using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tran
{
    class CommonHelper
    {
        /// <summary>
        /// 是否包含某些键
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keys"></param>
        public static bool ExistsKeys(Dictionary<string, object> keyValue, params string[] keys)
        {
            for (var i = 0; i < keys.Length; i++)
            {
                if (!keyValue.ContainsKey(keys[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
