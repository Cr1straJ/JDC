﻿using System;

namespace JDC.BusinessLogic.Models
{
    public class SmtpClientSettings
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
    }
}
