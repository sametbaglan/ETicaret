using System;

namespace UI.Layer.Models.UserModel
{
    public class DiscountCalculation
    {
        public static int percentileCalculation(decimal price,decimal? reducedprice)
        {
            int? bolum1 = 0;
            if (reducedprice > 0)
            {
                bolum1 =Convert.ToInt32(100 - (reducedprice * 100 / price));
                return (int)bolum1;
            }
            else
            {
                return (int)bolum1;
            }
        }
    }
}
