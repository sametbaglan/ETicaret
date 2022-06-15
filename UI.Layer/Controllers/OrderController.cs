using Business.Layer.Abstrack;
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UI.Layer.Models;
using UI.Layer.Models.OrdersModels;

namespace UI.Layer.Controllers
{
    public class OrderController : Controller
    {
        private IShipmenService _shipmenService;
        private readonly IUsersService _usersService;
        private readonly IShipmenItemsService _shipmenItems;
        private readonly ICategoriaService _categoriaService;
        private readonly IProductService _productService;
        AllLayoutModel allLayout = new AllLayoutModel();
        public OrderController(IShipmenService shipmenService, IShipmenItemsService shipmenItems, IProductService productService, IUsersService usersService, ICategoriaService categoriaService)
        {
            _shipmenService = shipmenService;
            _shipmenItems = shipmenItems;
            _usersService = usersService;
            _categoriaService = categoriaService;
            _productService = productService;
        }
        public IActionResult MyOrders()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var user = _usersService.GetById(userid);
            allLayout.UserInfo = user;
            var shipmen = _shipmenService.GetOrdersWithUserid(userid, "Complated");
            allLayout.Shipmens = shipmen;
            allLayout.Categorias = _categoriaService.GetAll();
            return View(allLayout);
        }
    }
}
