﻿namespace ConsoleApp.Core
{
    public class SmtpConfig
    {
        public string Server { get; set; }
        public string From { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
