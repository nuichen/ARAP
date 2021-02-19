using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace IvyFront.BLL
{
    class ReadWeight_Common : IBLL.IReadWeight
    {
        System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
        private SerialPort serialPort = new SerialPort();
        private string start_str = "";
        private string end_str = "";
        private bool _have_ini = false;
        private string port = "";
        string Thousands = "0";

        bool IBLL.IReadWeight.Ini()
        {
            _have_ini = true;
            IBLL.IPar par = new BLL.PACKCHEN();
            string val = "";
            port = par.Read("Communications", "Port");
            this.start_str = par.Read("Communications", "CharStart");
            this.end_str = par.Read("Communications", "CharEnd");
            this.Thousands = par.Read("Communications", "Thousands");

            int br;
            int.TryParse(par.Read("Communications", "Settings").Split(',')[0], out br);
            serialPort.PortName = port;
            serialPort.BaudRate = br;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.DataReceived += serialPort_DataReceived;
            serialPort.Open();
            tmr.Interval = 20;
            tmr.Tick += tmr_tick;
            tmr.Enabled = true;
            return true;
        }

        private void tmr_tick(object sender, EventArgs e)
        {
            if (deleg != null)
            {
                deleg.Invoke(weight);
            }
        }

        private decimal weight = 0;
        private string context = "";

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string str = "";
                System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
                txt.Text = serialPort.ReadExisting();
                foreach (string line in txt.Lines)
                {
                    str += line;
                }

                if (str == null)
                {
                    str = "";
                }

                context += str;


                int index = context.IndexOf(start_str, StringComparison.OrdinalIgnoreCase);
                if (index != -1)
                {
                    int index2 = context.IndexOf(end_str, index + start_str.Length, StringComparison.OrdinalIgnoreCase);
                    if (index2 != -1)
                    {
                        int len = index2 - index - start_str.Length;
                        if (len <= 0)
                        {
                            string res = "";
                            res = StringReplace(res);
                            decimal val = 0;
                            decimal.TryParse(res, out val);
                            weight = val;
                            if (Thousands == "1")
                            {
                                weight = weight / 1000;
                            }

                            context = "";
                        }
                        else
                        {
                            string res = context.Substring(index + start_str.Length, len).Trim();
                            res = StringReplace(res);
                            decimal val = 0;
                            decimal.TryParse(res, out val);
                            weight = val;
                            if (Thousands == "1")
                            {
                                weight = weight / 10000;
                            }

                            context = "";
                        }
                    }
                }
                else if (context.Length > 10000)
                {
                    context = "";
                }
            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        /// 字符串替换
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string StringReplace(string key)
        {
            String lastChar = key.Substring(key.Length - 1, 1);
            if (!Char.IsDigit(lastChar[0]))
            {
                key = key.Substring(0, key.Length - 1) + "0";
            }

            return key;
        }

        private IBLL.WeightMsgHandler deleg;

        event IBLL.WeightMsgHandler IBLL.IReadWeight.WeightMsg
        {
            add { deleg += value; }
            remove { deleg -= value; }
        }

        bool IBLL.IReadWeight.have_bala()
        {
            IBLL.IPar par = new BLL.PACKCHEN();
            string val = par.Read("Communications", "balance");
            if (val == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        bool IBLL.IReadWeight.have_ini()
        {
            return _have_ini;
        }


        void IBLL.IReadWeight.Dis()
        {
            try
            {
                serialPort.DataReceived -= serialPort_DataReceived;
                serialPort.Close();
            }
            catch (Exception ex)
            {
            }
        }


        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        bool IBLL.IReadWeight.qupi()
        {
            Beep(2766, 100);

            serialPort.Write(Convert.ToChar(60).ToString());
            serialPort.Write(Convert.ToChar(84).ToString());
            serialPort.Write(Convert.ToChar(75).ToString());
            serialPort.Write(Convert.ToChar(62).ToString());
            serialPort.Write(Convert.ToChar(9).ToString());
            return true;
        }

        bool IBLL.IReadWeight.set0()
        {
            Beep(2766, 100);
            serialPort.Write(Convert.ToChar(60).ToString());
            serialPort.Write(Convert.ToChar(90).ToString());
            serialPort.Write(Convert.ToChar(75).ToString());
            serialPort.Write(Convert.ToChar(62).ToString());
            serialPort.Write(Convert.ToChar(9).ToString());
            return true;
        }
    }
}