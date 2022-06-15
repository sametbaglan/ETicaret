using Business.Layer.Entegration.Trendyol.TrendyolKargo;
using Nancy.Json;
using System.Collections.Generic;
using System.Net;
using UI.Layer.Models.PasswordHashs;

namespace UI.Layer.Models.MarketPlaceModels.TrendyolEntegre.CargoMetot
{
    public class CargoList
    {
        static WebClient webClient = new WebClient();
        static JavaScriptSerializer js = new JavaScriptSerializer();
        public static List<TrendyolKargoList> CargoListt()
        {
            string apikey = "c769T3Zzh7CS3VPWapMF";
            string secret = "kGws2RuWIQEkd2wezo3O";
            webClient = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webClient.Encoding = System.Text.Encoding.UTF8;
            var authokey = EncodingAuth.EncodingAuthorize(apikey, secret);
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authokey);
            string data = webClient.DownloadString("https://api.trendyol.com/sapigw/shipment-providers");
            TrendyolKargoList root = js.Deserialize<TrendyolKargoList>(data);
            List<TrendyolKargoList> asd = new List<TrendyolKargoList>();
            return asd;
        }
    }
}
