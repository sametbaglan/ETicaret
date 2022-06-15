using ETicaret.BusinessLayer.Abstrack;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Layer.Models;
using UI.Layer.Models.PaymentEntegre;

namespace AppLegendSoft.UI.Controllers
{

    public class CategoriaController : Controller
    {
        public AllLayoutModel model = new AllLayoutModel();
        private ICategoriaService _categoriaService;
        private IProductService _productService;
        private IUsersService _usersService;
        public CategoriaController(IProductService productService, ICategoriaService categoriaService, IUsersService usersService)
        {
            _categoriaService = categoriaService;
            _productService = productService;
            _usersService = usersService;
        }
        public IActionResult CategoriaAdd()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            model.UserInfo = users;
            return View(new AllLayoutModel()
            {
                UserInfo = users,
            });
        }
        [HttpPost]
        public IActionResult CategoriaAdd(AllLayoutModel model)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            model.UserInfo = users;
            var entity = new Categoria()
            {
                CategoryName = model.categoria.CategoryName,
                CreatedDate=System.DateTime.Now,
                ModifitedDate=System.DateTime.Now,
                IsActive=true,
                IsDeleted=false
            };
            _categoriaService.Create(entity);
            return RedirectToAction("GetAll");
        }
        public IActionResult GetAll(PaymentResponseViewModel payment)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }

            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            model.UserInfo = users;
            if (payment.Result == true)
            {
                ViewBag.error = "Ürünü olan kategoriyi ürünleri çıkartmadan silemezsiniz";
                ViewBag.messagecolor = "alert alert-danger";
                ViewBag.State = "Silme işlemi.";
                return View(new AllLayoutModel()
                {
                    Categorias = _categoriaService.GetAll(),
                    UserInfo = users
                });
          
            }
            return View(new AllLayoutModel()
            {
                Categorias = _categoriaService.GetAll(),
                UserInfo = users
            });
        }
        public IActionResult Update(int categoryid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            model.UserInfo = users;
            var data = _categoriaService.GetById(categoryid);
            model.categoria = data;
            return View(model);

        }
        [HttpPost]
        public IActionResult Update(AllLayoutModel model)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            model.UserInfo = users;
            var entity = new Categoria()
            {
                CategoryName = model.categoria.CategoryName,
                ModifitedDate=System.DateTime.Now
            };
            _categoriaService.Update(entity);
            return RedirectToAction("GetAll");
        }
        [HttpPost]
        public IActionResult Delete(int categoryid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            model.UserInfo = users;
            var data = _categoriaService.GetById(categoryid);
            var result= _productService.GetByWithCategoriByProduct(categoryid);
            if (result.Count >0)
            {
                var response = new PaymentResponseViewModel
                {
                    Result = true
                };
                return RedirectToAction("GetAll",response);
            }
            data.IsDeleted = true;
            data.IsActive = false;
            _categoriaService.Update(data);
            return RedirectToAction("GetAll");
        }
        [HttpGet]
        public IActionResult GetByWithProductId(int categoriaid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return View(new AllLayoutModel()
                {
                    Products = _productService.GetByWithCategoriByProduct(categoriaid),
                    Categorias = _categoriaService.GetAll()
                });
            }
            else
            {
                var userid = TokenUserValueFunc.TokenGetValue(token);
                Users users = _usersService.GetById(userid);
                if (users.role != "A") { return Redirect("/Error/501"); }
                model.UserInfo = users;
                var result = _productService.GetByWithCategoriByProduct(categoriaid);
                if (result != null)
                {
                    return View(new AllLayoutModel()
                    {
                        Products = result,
                        UserInfo = users
                    });
                }
            }

            return View();
        }
    }
}
