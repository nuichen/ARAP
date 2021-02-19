using IvyTran.Helper;
using IvyTran.IBLL;
using IvyTran.IBLL.OnLine;

namespace IvyTran.BLL.OnLine
{
    public class Config : IConfig
    {
        string IConfig.kefu_tel
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "kefu_tel");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "kefu_tel", value);
            }
        }

        string IConfig.peisong_tel
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "peisong_tel");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "peisong_tel", value);
            }
        }

        string IConfig.peisong_amt
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "peisong_amt");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "peisong_amt", value);
            }
        }

        string IConfig.peisong_jl
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "peisong_jl");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "peisong_jl", value);
            }
        }

        string IConfig.wx_notice
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "wx_notice");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "wx_notice", value);
            }
        }

        string IConfig.take_fee
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "take_fee");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "take_fee", value);
            }
        }

        string IConfig.kefu_qrcode
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "kefu_qrcode");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "kefu_qrcode", value);
            }
        }

        string IConfig.print_count
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "print_count");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "print_count", value);
            }
        }

        string IConfig.start_time
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "start_time");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "start_time", value);
            }
        }

        string IConfig.end_time
        {
            get
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                return configDAL.SelectValue(Global.McId, "end_time");

            }
            set
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DAL.merchant_configDAL configDAL = new DAL.merchant_configDAL(db);
                configDAL.SaveValue(Global.McId, "end_time", value);
            }
        }
    }
}