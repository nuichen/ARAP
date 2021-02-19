using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace b2bclient
{
   public class AppSetting
    {
        private static string _svr;
        public static string svr
        {
            get
            {
                return _svr;
            }
            set
            {
                _svr = value;
            }
        }

        private static string _imgsvr;
        public static string imgsvr
        {
            get
            {
                return _imgsvr;
            }
            set
            {
                _imgsvr = value;
            }
        }

        private static string _wxsvr;
        public static string wxsvr
        {
            get
            {
                return _wxsvr;
            }
            set
            {
                _wxsvr = value;
            }
        }

        private static string _SoundForNewOrder;
        public static string SoundForNewOrder
        {
            get
            {
               
                return _SoundForNewOrder;
            }
            set
            {
                _SoundForNewOrder = value;
            }
        }

        private static int _ReplaySoundInterval;
        public static int ReplaySoundInterval
       {
           get
           {
               if (_ReplaySoundInterval == null || _ReplaySoundInterval == 0)
               {
                   _ReplaySoundInterval = Conv.ToInt(Helper.Par_aes.Read("app_set", "ReplaySoundInterval"));
               }
               if (_ReplaySoundInterval == 0)
               {
                   _ReplaySoundInterval = 10;
               }
               return _ReplaySoundInterval;
            }
            set
            {
                _ReplaySoundInterval = value;
            }
       }

    } 
}
