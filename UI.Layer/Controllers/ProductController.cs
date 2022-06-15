using Business.Layer.Abstrack;
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using ETicaret.UILayer.Models.OrdersModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.Layer.Models;
using UI.Layer.Models.MarketPlaceModels.TrendyolEntegre.BrandMetot;
using UI.Layer.Models.MarketPlaceModels.TrendyolEntegre.CargoMetot;
using UI.Layer.Models.MarketPlaceModels.TrendyolEntegre.CategoryMetot;

namespace UI.Layer.Controllers
{
    public class ProductController : Controller
    {

        private IProductService _productService;
        private readonly IItemBodySizeService _ıtemBodySizeService;
        private readonly IItemColorService _ıtemColorService;
        private ICategoriaService _categoriaService;
        private IUsersService _usersService;
        private IImagesService _ımagesService;
        private IShipmenItemsService _shipmenItems;
        public ProductController(IShipmenItemsService shipmenItems, IImagesService ımagesService, IItemBodySizeService ıtemBodySizeService, IItemColorService ıtemColorService, IUsersService usersService, IProductService productService, ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
            _productService = productService;
            _usersService = usersService;
            _ıtemBodySizeService = ıtemBodySizeService;
            _ıtemColorService = ıtemColorService;
            _ımagesService = ımagesService;
            _shipmenItems = shipmenItems;
        }
        AllLayoutModel layoutModel = new AllLayoutModel();
        public IActionResult ProductList()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return View(new AllLayoutModel()
                {
                    UserInfo = null,
                    Products = _productService.GetAll(),
                    Categorias = _categoriaService.GetAll()
                });
            }
            else
            {
                var userid = TokenUserValueFunc.TokenGetValue(token);
                var user = _usersService.GetById(userid);
                if (user.role == "A")
                {
                    var model = new AllLayoutModel()
                    {
                        Products = _productService.GetAll(),
                        UserInfo = user,
                    };
                    return RedirectToAction("List", "Admin");
                }
                else
                {
                    return View(new AllLayoutModel()
                    {
                        Products = _productService.GetAll(),
                        Categorias = _categoriaService.GetAll(),
                        UserInfo = user
                    });
                }
            }
        }
        public IActionResult GetCategoryByProductList(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                var data = _productService.GetByWithCategoriByProduct(id);
                if (data == null)
                {
                    ViewBag.nullerror = "Bu Kategoriye Ait Ürün Bulunmamaktadır.";
                    return View();
                }
                else
                {
                    return View(new AllLayoutModel()
                    {
                        UserInfo = null,
                        Products = data,
                        categoryname = _categoriaService.GetById(id).CategoryName,
                        Categorias = _categoriaService.GetAll(),
                    });
                }

            }
            else
            {
                var userid = TokenUserValueFunc.TokenGetValue(token);
                Users users = _usersService.GetById(userid);
                var data = _productService.GetByWithCategoriByProduct(id);
                if (data == null)
                {
                    ViewBag.nullerror = "Bu Kategoriye Ait Ürün Bulunmamaktadır.";
                    return View();
                }
                else
                {
                    return View(new AllLayoutModel()
                    {
                        UserInfo = users,
                        Products = data,
                        categoryname = _categoriaService.GetById(id).CategoryName,
                        Categorias = _categoriaService.GetAll(),
                    });
                }
            }
        }
        [HttpGet]
        public IActionResult ProductDetail(int id)
        {
            Users useR = null;
            var token = HttpContext.Session.GetString("token");
            if (!string.IsNullOrEmpty(token))
            {
                var userid = TokenUserValueFunc.TokenGetValue(token);
                useR= _usersService.GetById(userid);
            }
            Product product = _productService.GetByProductDetail(id);
            if (product == null)
            {
                return Redirect("/Error/404");
            }
            else
            {
                string message = HttpContext.Session.GetString("message");
                ViewBag.Message = message;
                return View(new AllLayoutModel()
                {
                    UserInfo = useR,
                    Categorias = _categoriaService.GetAll(),
                    Product = product,
                    bodySizes = _ıtemBodySizeService.GetBodyWithProductid(product.ProductID),
                    ıtemColors = _ıtemColorService.GetItemColorsWithProductid(product.ProductID),
                    ListImages = _ımagesService.GetImagesWithProductid(id)
                });
            }
        }
    }
}
