namespace IvyTran.body
{
    public class wxapp
    {
        public wxapp()
        {

        }

        public wxapp(string context)
        {
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(context);
            appid = read.Read("appid");
            secret = read.Read("secret");
            token = read.Read("token");
            ticket = read.Read("ticket");
            create_time = read.Read("create_time");
        }

        public string appid { get; set; }
        public string secret { get; set; }
        public string token { get; set; }
        public string ticket { get; set; }
        public string create_time { get; set; }
    }
}