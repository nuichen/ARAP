using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Management;

namespace b2bclient.sys
{
  public class Security
    {

      public static string ReadMacId()
      {
          Security se = new Security();
          string str = se.getCPU() + se.getHD();
          str = se.toMD5(str);
          return str;
      }

      private string getCPU()
      {
          ManagementClass obj = new ManagementClass("Win32_processor");
          ManagementObjectCollection moc = obj.GetInstances();
          string str = "";
          foreach (ManagementObject mo in moc)
          {
              str = mo.Properties["processorId"].Value.ToString();
              break;
          }
          return str;
      }

      private string getHD()
      {
          ManagementClass obj = new ManagementClass("Win32_DiskDrive");
          ManagementObjectCollection moc = obj.GetInstances();
          string str = "";
          foreach (ManagementObject mo in moc)
          {
              str = mo.Properties["Model"].Value.ToString();
              break;
          }
          return str;
      }

      public bool CheckMacId()
      {
        //  string macId = this.ReadMacId();
          //macId为参数，调后端判断数据库里是否存在，存在返回true
          return true;
      }

      private string toMD5(string value)
      {
          if (value == null || value == "")
          {
              return "";
          }
          byte[] data = System.Text.Encoding.Default.GetBytes(value);
          var md = new MD5CryptoServiceProvider();
          var data2 = md.ComputeHash(data);
          var v = BitConverter.ToString(data2);
          v = v.Replace("-", "");
          v = v.ToLower();
          return v;
      }

    }
}
