using Business.Layer.Entegration.Trendyol.GetShipmenPackages;
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy.Json;
using System.Collections.Generic;
using System.Net;
using UI.Layer.Models;
using UI.Layer.Models.MarketPlaceModels.TrendyolEntegre.BrandMetot;
using UI.Layer.Models.MarketPlaceModels.TrendyolEntegre.CategoryMetot;
using UI.Layer.Models.MarketPlaceModels.TrendyolEntegre.ShipmensMetot;
using UI.Layer.Models.PasswordHashs;

namespace UI.Layer.Controllers.MarketPlaceCs
{

    public class TrendyolController : Controller
    {
        AllLayoutModel allLayoutModel = new AllLayoutModel();
        JavaScriptSerializer js = new JavaScriptSerializer();
        WebClient webClient = new WebClient();
        private ICategoriaService _categoriaService;
        private IUsersService _usersService;
        public TrendyolController(ICategoriaService categoriaService, IUsersService usersService)
        {
            _categoriaService = categoriaService;
            _usersService = usersService;
        }
        public IActionResult ShipmenList()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }

            string apikey = "c769T3Zzh7CS3VPWapMF";
            string secret = "kGws2RuWIQEkd2wezo3O";
            webClient = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webClient.Encoding = System.Text.Encoding.UTF8;
            var authokey = EncodingAuth.EncodingAuthorize(apikey, secret);
            webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authokey);
            string data = webClient.DownloadString("https://api.trendyol.com/sapigw/suppliers/513272/orders");
            ShipmenRoot root = js.Deserialize<ShipmenRoot>(data);
            List<ShipmenContent> listshipmen = root.content;
            allLayoutModel.TrendyolShipmenList = listshipmen;
            allLayoutModel.Categorias = _categoriaService.GetAll();
            allLayoutModel.UserInfo = users;
            var neworderlist= NewOrderListGet.NewOrderList();
            allLayoutModel.NewOrderListGet = neworderlist;
            return View(allLayoutModel);
        }
        public IActionResult ProductSend()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            allLayoutModel.UserInfo = users;
            List<SelectListItem> selectLists = new List<SelectListItem>();
            var brands = BrandGet.BrandList();
            foreach(var brand in brands)
            {
                selectLists.Add(new SelectListItem
                {
                    Text = brand.name,
                    Value = brand.id.ToString()
                });
            }
            ViewBag.brandFilters = selectLists;
            allLayoutModel.TrendyolCategoryList= SubCategoryList.CategoriaList();
            return View(allLayoutModel);
        }
    }
}
