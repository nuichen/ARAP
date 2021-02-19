using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront
{
    class Appsetting
    {
        private static string _is_mobile_pay;
        public static string is_mobile_pay
        {
            get
            {
                if (_is_mobile_pay == null)
                {
                    _is_mobile_pay = Par_aes.Read("app_set", "is_mobile_pay");
                }
                return _is_mobile_pay;
            }
        }

        private static string _svr;
        public static string svr
        {
            get
            {
                if (_svr == null)
                {
                    _svr = Par_aes.Read("app_set", "svr");
                }
                return _svr;
            }
        }

        private static string _mer_key;
        public static string mer_key
        {
            get
            {
                if (_mer_key == null)
                {
                    _mer_key = Par_aes.Read("app_set", "mer_key");
                }
                return _mer_key;
            }
        }

        private static string _is_print;
        public static string is_print
        {
            get
            {
                if (_is_print == null)
                {
                    _is_print = Par_aes.Read("app_set", "is_print");
                }
                return _is_print;
            }

            set 
            {
                Par_aes.Write("app_set", "is_print", value);
                _is_print = value;
            }
        }

        private static string _print_name;
        public static string print_name
        {
            get
            {
                if (_print_name == null)
                {
                    _print_name = Par_aes.Read("app_set", "print_name");
                }
                return _print_name;
            }
        }


        private static string _ws_svr;
        public static string ws_svr
        {
            get
            {
                if (_ws_svr == null)
                {
                    _ws_svr = Par_aes.Read("app_set", "ws_svr");
                }
                return _ws_svr;
            }

            set
            {
                Par_aes.Write("app_set", "ws_svr", value);
                _ws_svr = value;
            }
        }

        private static string _is_bind_ip;
        public static string is_bind_ip
        {
            get
            {
                if (_is_bind_ip == null)
                {
                    _is_bind_ip = Par_aes.Read("app_set", "is_bind_ip");
                }
                return _is_bind_ip;
            }

            set
            {
                Par_aes.Write("app_set", "is_bind_ip", value);
                _is_bind_ip = value;
            }
        }

    }
}