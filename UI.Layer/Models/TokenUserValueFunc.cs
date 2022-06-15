using System;
using System.IdentityModel.Tokens.Jwt;

namespace UI.Layer.Models
{
    public class TokenUserValueFunc
    {
        public static int TokenGetValue(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token);
            object userid = null;
            foreach (var item in decodedValue.Payload)
            {
                if (item.Key == "userId")
                {
                    userid = item.Value;
                }
            }
            return Convert.ToInt32(userid);
        }
    }
}
