using Business.Layer.Entegration.Trendyol.GetShipmenPackages;
using Nancy.Json;
using System.Collections.Generic;
using System.Net;
using UI.Layer.Models.PasswordHashs;

namespace UI.Layer.Models.MarketPlaceModels.TrendyolEntegre.ShipmensMetot
{
    public class NewOrderListGet
    {
       static WebClient webClient = new WebClient();
        static JavaScriptSerializer js = new JavaScriptSerializer();
        public static List<ShipmenContent> NewOrderList()
        {
            string apikey = "c769T3Zzh7CS3VPWapMF";
            string secret = "kGws2RuWIQEkd2wezo3O";
            webClient = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webClient.Encoding = System.Text.Encoding.UTF8;
            var authokey = EncodingAuth.EncodingAuthorize(apikey, secret);
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authokey);
            string data = webClient.DownloadString("https://api.trendyol.com/sapigw/suppliers/513272/orders?status=Created");
            ShipmenRoot root = js.Deserialize<ShipmenRoot>(data);
            List<ShipmenContent> listshipmen = root.content;
            return listshipmen;
        }
    }
}
