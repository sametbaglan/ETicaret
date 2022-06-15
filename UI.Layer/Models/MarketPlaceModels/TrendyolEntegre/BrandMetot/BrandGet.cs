using Business.Layer.Entegration.Trendyol.TrendyolBrand;
using Nancy.Json;
using System.Collections.Generic;
using System.Net;
using UI.Layer.Models.PasswordHashs;

namespace UI.Layer.Models.MarketPlaceModels.TrendyolEntegre.BrandMetot
{
    public class BrandGet
    {
        static WebClient webClient = new WebClient();
        static JavaScriptSerializer js = new JavaScriptSerializer();
        public static List<Brand> BrandList()
        {
            try
            {
                string apikey = "c769T3Zzh7CS3VPWapMF";
                string secret = "kGws2RuWIQEkd2wezo3O";
                webClient = new WebClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                webClient.Encoding = System.Text.Encoding.UTF8;
                var authokey = EncodingAuth.EncodingAuthorize(apikey, secret);
                webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authokey);
                string data = webClient.DownloadString("https://api.trendyol.com/sapigw/brands");
                BrandRoot root = js.Deserialize<BrandRoot>(data);
                List<Brand> asd = root.brands;
                return asd;
            }
            catch (System.Exception)
            {

                return null;
            }
          
        }
    }
}
