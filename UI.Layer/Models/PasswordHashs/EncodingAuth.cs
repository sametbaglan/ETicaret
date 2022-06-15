using System;
using System.Text;

namespace UI.Layer.Models.PasswordHashs
{
    public class EncodingAuth
    {
        public static string EncodingAuthorize(string key,string password)
        {
            var plaintKey = Encoding.UTF8.GetBytes(key + ":" + password);
            return Convert.ToBase64String(plaintKey);
        }
    }
}
