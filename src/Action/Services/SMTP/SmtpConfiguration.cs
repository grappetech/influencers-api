namespace Action.Services.SMTP
{
    public class SmtpConfiguration
    {
        private static SmtpConfiguration _instance;

        public static void Configure(SmtpConfiguration configuration)
        {
            _instance = configuration;
        }

        public static void Configure(string host, int port, string username, string password, string sennder, bool isSSL)
        {
            _instance = new SmtpConfiguration
            {
                Host = host,
                Port = port,
                UserName = username,
                Password = password,
                Sender = sennder,
                IsSSL = isSSL
            };
        }

        public static SmtpConfiguration GetConfiguration()
        {
            return _instance;
        }
        
        public string Host { get; set; }
        public string Sender { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsSSL { get; set; }
    }
}