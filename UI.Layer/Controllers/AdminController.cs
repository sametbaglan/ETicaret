using Business.Layer.Abstrack;
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using UI.Layer.Models;
namespace AppLegendSoft.UI.Controllers
{
    public class AdminController : Controller
    {
        AllLayoutModel allLayout = new AllLayoutModel();
        private IProductService _productService;
        private ICategoriaService _categoryService;
        private IItemBodySizeService _bodySizeService;
        private IItemColorService _ıtemColorService;
        private IUsersService _usersService;
        private IHostingEnvironment _hostingEnvironment;
        private IShipmenItemsService _ShipmenItemsService;
        private IImagesService _ımagesService;
        private IShipmenService _shipmenService;
        public AdminController(IShipmenService shipmenService, IShipmenItemsService ShipmenItemsService, IImagesService ımagesService, IUsersService usersService, IProductService productService, IHostingEnvironment hostingEnvironment, ICategoriaService categoryService, IItemBodySizeService bodySizeService, IItemColorService ıtemColorService)
        {
            _ıtemColorService = ıtemColorService;
            _categoryService = categoryService;
            _productService = productService;
            _bodySizeService = bodySizeService;
            _hostingEnvironment = hostingEnvironment;
            _usersService = usersService;
            _ımagesService = ımagesService;
            _ShipmenItemsService = ShipmenItemsService;
            _shipmenService = shipmenService;
        }
        public IActionResult List()
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
                Products = _productService.GetAll(),
                Categorias = _categoryService.GetAll(),
                UserInfo = users
            });
        }
        [HttpPost]
        public IActionResult Delete(int productid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var deger = _productService.GetById(productid);
            deger.IsActive = false;
            deger.IsDeleted = true;
            var image = _ımagesService.GetImageWithProductId(productid);
            if (image != null) _ımagesService.Delete(image);
            var itemcolor = _ıtemColorService.GetItemColorsWithProductid(productid);
            var bodyitem = _bodySizeService.GetBodyWithProductid(productid);
            foreach (var bdy in bodyitem)
            {
                bdy.productid = 0;
                _bodySizeService.Update(bdy);
            }
            foreach (var color in itemcolor)
            {
                color.productid = 0;
                _ıtemColorService.Update(color);
            }
            _productService.Update(deger);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var deger = _productService.GetById(id);
            var categorylist = _categoryService.GetAll();
            List<SelectListItem> list = new List<SelectListItem>();
            if (categorylist != null)
            {
                foreach (var category in categorylist)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = category.CategoryName,
                        Value = category.CategoriaID.ToString(),
                        Selected = true
                    });
                }
                ViewBag.selectlist = list;
            }
            return View(new AllLayoutModel()
            {
                UserInfo = users,
                Product = deger,
                Categorias = _categoryService.GetAll()
            });
        }
        [HttpPost]
        public IActionResult Update(AllLayoutModel model, int id, IFormFile file)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var deger = _productService.GetById(id);
            string uniqfilename = Guid.NewGuid().ToString() + ".jpeg";
            if (file != null)
            {
                var Upload = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqfilename);
                file.CopyTo(new FileStream(Upload, FileMode.Create));
            }
            else
            {
                uniqfilename = deger.ImageUrl;
            }
            var categorylist = _categoryService.GetAll();
            List<SelectListItem> list = new List<SelectListItem>();
            if (categorylist != null)
            {
                foreach (var category in categorylist)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = category.CategoryName,
                        Value = category.CategoriaID.ToString(),
                        Selected = true
                    });
                }
                ViewBag.selectlist = list;
            }
            var entity = new Product
            {
                Stock = deger.Stock,
                Barcode = model.Product.Barcode,
                CategoryID = model.categoria.CategoriaID,
                CreatedDate = deger.CreatedDate,
                ModifitedDate = DateTime.Now,
                Descriptions = model.Product.Descriptions,
                IsActive = true,
                IsDeleted = false,
                ProductName = model.Product.ProductName,
                ReducedPrice = model.Product.ReducedPrice,
                Price = model.Product.Price,
                ProductID = deger.ProductID,
                Category = deger.Category,
                Images = deger.Images,
                ImageUrl = uniqfilename
            };
            _productService.Update(entity);
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Create()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var categorylist = _categoryService.GetAll();
            List<SelectListItem> list = new List<SelectListItem>();
            if (categorylist != null)
            {
                foreach (var category in categorylist)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = category.CategoryName,
                        Value = category.CategoriaID.ToString(),
                        Selected = true
                    });
                }
                ViewBag.selectlist = list;
            }
            allLayout.UserInfo = users;
            return View(new AllLayoutModel()
            {
                UserInfo = users,
                ıtemColors = _ıtemColorService.GetAll(),
                bodySizes = _bodySizeService.GetAll(),
            });
        }
        [HttpPost]
        public IActionResult Create(AllLayoutModel models, IFormFile file, List<string> emptycoloritem)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            if (models.categoria.CategoriaID == null || emptycoloritem.Count == 0)
            {
                var categorylist = _categoryService.GetAll();
                List<SelectListItem> list = new List<SelectListItem>();
                if (categorylist != null)
                {
                    foreach (var category in categorylist)
                    {
                        list.Add(new SelectListItem()
                        {
                            Text = category.CategoryName,
                            Value = category.CategoriaID.ToString(),
                            Selected = true
                        });
                    }
                    ViewBag.selectlist = list;
                }
                ViewBag.error = "Kategori veya Varyantları Seçiniz";

                return View(new AllLayoutModel() { bodySizes = _bodySizeService.GetAll(), ıtemColors = _ıtemColorService.GetAll(), Categorias = _categoryService.GetAll() });
            }
            string uniqfilename = Guid.NewGuid().ToString() + ".jpeg";
            if (file != null)
            {
                var Upload = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqfilename);
                file.CopyTo(new FileStream(Upload, FileMode.Create));
            }
            else
            {
                uniqfilename = Path.Combine(_hostingEnvironment.WebRootPath, "images", "logo.png");
            }
            if (models.Product.ReducedPrice == null)
            {
                models.Product.ReducedPrice = 0;
            }
            var entity = new Product
            {
                ProductName = models.Product.ProductName,
                Stock = 0,
                Barcode = models.Product.Barcode,
                CategoryID = models.categoria.CategoriaID,
                CreatedDate = DateTime.Now,
                ModifitedDate = DateTime.Now,
                Descriptions = models.Product.Descriptions,
                ImageUrl = uniqfilename,
                IsActive = true,
                IsDeleted = false,
                Price = models.Product.Price,
                ReducedPrice = models.Product.ReducedPrice,
            };
            _productService.Create(entity);
            var lastproduct = _productService.GetByFindProductName(entity.ProductName);
            int stokcount = 0;
            foreach (var item in emptycoloritem)
            {
                var bodycount = _bodySizeService.GetById(Convert.ToInt32(item));
                bodycount.productid = lastproduct.ProductID;
                _bodySizeService.Update(bodycount);
                stokcount += bodycount.BodyCount;
                var color = _ıtemColorService.GetById(bodycount.ItemColorid);
                color.productid = lastproduct.ProductID;
                _ıtemColorService.Update(color);
            }
            lastproduct.Stock = stokcount;
            _productService.Update(lastproduct);
            return RedirectToAction("List");
        }
        public IActionResult CompletedPackets()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var shipmen = _ShipmenItemsService.GetNewOrders(3);
            return View(new AllLayoutModel
            {
                ShipmenList=shipmen,
                Categorias=_categoryService.GetAll(),
                UserInfo=users
            });
        }
        [HttpGet]
        public IActionResult MyNewOrders()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            return View(new AllLayoutModel
            {
                ShipmenList = _ShipmenItemsService.GetNewOrders(1),
                Categorias = _categoryService.GetAll(),
                UserInfo = users
            });
        }
        [HttpPost]
        public IActionResult MyNewOrders(int orderid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var order = _ShipmenItemsService.GetById(orderid);
            order.State = 2;
            _ShipmenItemsService.Update(order);
            return RedirectToAction("MyNewOrders");
        }
        [HttpGet]
        public IActionResult PacketList()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            return View(new AllLayoutModel
            {
                ShipmenList = _ShipmenItemsService.GetNewOrders(2),
                Categorias = _categoryService.GetAll(),
                UserInfo = users
            });
        }
        [HttpPost]
        public IActionResult PacketList(int orderid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var shipmen = _ShipmenItemsService.GetById(orderid);
            shipmen.State = 3;
            _ShipmenItemsService.Update(shipmen);
         return   RedirectToAction("PacketList");
        }
    }
}
