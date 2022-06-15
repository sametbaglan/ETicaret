﻿using System.Collections.Generic;

namespace ETicaret.DataAccessLayer.Configurations
{
    public class CustomTokenOption
    {
        public List<string> Audience { get; set; }
        public string Issuer { get; set; }

        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }

        public string SecurityKey { get; set; }
    }
}