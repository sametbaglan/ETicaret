using ETicaret.BusinessLayer.Abstrack;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UI.Layer.Models;

namespace ETicaret.UILayer.Controllers
{
    public class VaryantController : Controller
    {
        private IProductService _productService;
        private ICategoriaService _categoryService;
        private IItemBodySizeService _bodySizeService;
        private IItemColorService _ıtemColorService;
        private IUsersService _usersService;

        public VaryantController(IUsersService usersService, IProductService productService, ICategoriaService categoryService, IItemBodySizeService bodySizeService, IItemColorService ıtemColorService)
        {
            _ıtemColorService = ıtemColorService;
            _categoryService = categoryService;
            _productService = productService;
            _bodySizeService = bodySizeService;
            _usersService = usersService;
        }
        public IActionResult ColorAdd()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }

            return View(new AllLayoutModel()
            {
                UserInfo = users,
                Categorias = _categoryService.GetAll(),
            });
        }
        [HttpPost]
        public IActionResult ColorAdd(AllLayoutModel model)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var entity = new ItemColors
            {
                CreatedDate=DateTime.Now,
                ModifitedDate=DateTime.Now,
                IsActive=true,
                IsDeleted=false,
                productid = 0,
                Color = model.ıtemColor.Color,
                userid = userid
            };
            _ıtemColorService.Create(entity);

            return RedirectToAction("Attirbutes");
        }
        public IActionResult Attirbutes()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            return View(new AllLayoutModel()
            {
                UserInfo = users,
                Categorias = _categoryService.GetAll(),
                bodySizes = _bodySizeService.GetAll(),
                ıtemColors = _ıtemColorService.GetAll()
            });
        }
        public IActionResult ProductByWithListAttirbute(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            return View(new AllLayoutModel()
            {
                Categorias = _categoryService.GetAll(),
                UserInfo = users,
                ıtemColors = _ıtemColorService.GetItemColorsWithProductid(id),
                bodySizes = _bodySizeService.GetBodyWithProductid(id)
            });
        }
        public IActionResult BodySizeAdd()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var emptycoloritem = _ıtemColorService.GetEmptyItemColors();
            return View(new AllLayoutModel()
            {
                UserInfo = users,
                Categorias = _categoryService.GetAll(),
                ıtemColors = emptycoloritem
            });
        }
        [HttpPost]
        public IActionResult BodySizeAdd(AllLayoutModel model, List<string> emptycoloritem)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            if (emptycoloritem.Count > 0)
            {
                foreach (var item in emptycoloritem)
                {
                    var entity = new BodySizes()
                    {
                        BodyName = model.bodySize.BodyName,
                        ItemColorid = Convert.ToInt32(item),
                        BodyCount = model.bodySize.BodyCount,
                        Userid = userid,
                        productid = 0,
                        CreatedDate=DateTime.Now,
                        ModifitedDate=DateTime.Now,
                        IsActive=true,
                        IsDeleted=false
                    };
                    _bodySizeService.Create(entity);
                }
            }
            else
            {
                ViewBag.message = "Ürün için renk bulunmamaktadır. İlgili sayfaya giderek renk ekleyiniz";
            }
            return RedirectToAction("Attirbutes");
        }
        [HttpGet]
        public IActionResult AttirbuteUpdate(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var body = _bodySizeService.GetById(id);

            return View(new AllLayoutModel
            {
                Categorias = _categoryService.GetAll(),
                UserInfo = users,
                bodySize = body
            });
        }
        public IActionResult AttirbuteUpdate(AllLayoutModel model, int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var bodyid = HttpContext.Request.RouteValues["id"].ToString();
            var body = _bodySizeService.GetById(Convert.ToInt32(bodyid));
            body.BodyCount = model.bodySize.BodyCount;
            _bodySizeService.Update(body);
            return Redirect("/Varyant/ProductByWithListAttirbute/" + body.productid);
        }
        [HttpPost]
        public IActionResult AttirbuteActive(int bodyid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var body = _bodySizeService.GetById(bodyid);
            body.IsActive = false;
            body.IsDeleted = true;
            var product = _productService.GetById(body.productid);
            product.Stock=product.Stock - body.BodyCount;
            _productService.Update(product);
            _bodySizeService.Update(body);
            return Redirect("/Varyant/ProductByWithListAttirbute/" + body.productid);
        }
        [HttpGet]
        public IActionResult ToProductAttirbuteAdd(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var emptycoloritem = _ıtemColorService.GetColorEmptyAndProductWithProductid(id);
            return View(new AllLayoutModel
            {
                UserInfo = users,
                Categorias = _categoryService.GetAll(),
                ıtemColors = emptycoloritem
            });
        }
        [HttpPost]
        public IActionResult ToProductAttirbuteAdd(AllLayoutModel model, List<string> emptycoloritem)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var productid = HttpContext.Request.RouteValues["id"].ToString();
         
            if (emptycoloritem.Count > 0)
            {
                foreach (var item in emptycoloritem)
                {
                    var entity = new BodySizes()
                    {
                        BodyName = model.bodySize.BodyName,
                        ItemColorid = Convert.ToInt32(item),
                        BodyCount = model.bodySize.BodyCount,
                        Userid = userid,
                        productid = Convert.ToInt32(productid),
                        CreatedDate=DateTime.Now,
                        ModifitedDate=DateTime.Now,
                        IsActive=true,
                        IsDeleted=false
                    };
                    _bodySizeService.Create(entity);
                }
                var product = _productService.GetById(Convert.ToInt32(productid));
                product.Stock += model.bodySize.BodyCount;
                _productService.Update(product);
            }
            else
            {
                ViewBag.message = "Ürün için renk bulunmamaktadır. İlgili sayfaya giderek renk ekleyiniz";
            }
            return Redirect("/Varyant/ProductByWithListAttirbute/" + productid);
        }
    }
}
