using System;
using System.Text;

namespace ETicaret.BusinessLayer.PasswordHashs
{
    public class CommonMethods
    {
        public static string Key = "adef@@kfxbv@";
        public static string ConvertToEncryp(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public static string ConvertDecrypt(string passwordBytes)
        {
            if (string.IsNullOrEmpty(passwordBytes)) return "";
            var base64String = Convert.FromBase64String(passwordBytes);
            var result = Encoding.UTF8.GetString(base64String);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}
