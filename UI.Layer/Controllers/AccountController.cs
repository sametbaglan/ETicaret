using ETicaret.BusinessLayer.Abstrack;
using ETicaret.BusinessLayer.Email_Entegrasyon;
using ETicaret.BusinessLayer.PasswordHashs;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models;
using ETicaret.UILayer.Models.IdentityModels;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using UI.Layer.Models;
using UI.Layer.Models.PaymentEntegre;

namespace AppLegendSoft.UI.Controllers
{
    public class AccountController : Controller
    {
        AllLayoutModel allLayoutModel = new AllLayoutModel();
        private readonly IUsersService _usersService;
        private readonly IProductService _productService;
        private readonly ICartDalService _cartDalService;
        private readonly ICategoriaService _categoryService;
        public AccountController(IUsersService usersService, ICategoriaService categoryService, IProductService productService, ICartDalService cartDalService)
        {
            _usersService = usersService;
            _productService = productService;
            _cartDalService = cartDalService;
            _categoryService = categoryService;
        }
        public IActionResult Register()
        {
            allLayoutModel.Categorias = _categoryService.GetAll();
            return View(allLayoutModel);
        }
        [HttpPost]
        public IActionResult Register(AllLayoutModel model)
        {
            if (!ModelState.IsValid)
            {
                var user = _usersService.GetByEmailUsers(model.RegisterModel.Email);
                if (user != null)
                {
                    model.Categorias = _categoryService.GetAll();
                    ViewBag.error = "Bu Mail adresi daha önce kayıtlıdır.Şifremi unuttum kısmına gidiniz veya yeni bir mail adresi ile kayıt yapınız";
                    ViewBag.messagecolor = "alert alert-danger";
                    ViewBag.State = "Üyelik Başarısız.";
                    return View(model);
                }
                var entity = new Users
                {
                    FirstName = model.RegisterModel.FirstName,
                    LastName = model.RegisterModel.LastName,
                    Email = model.RegisterModel.Email,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Today,
                    ModifitedDate = DateTime.Today,
                    DateOfBirth = model.RegisterModel.DateOfBirth,
                    Password = CommonMethods.ConvertToEncryp(model.RegisterModel.Password),
                    PhoneNumber = model.RegisterModel.PhoneNumber,
                    UserName = model.RegisterModel.UserName,
                    role = "B"
                };
                _usersService.Create(entity);
                var token = _usersService.Authenticate(model.RegisterModel.Email, model.RegisterModel.Password);
                var response = new PaymentResponseViewModel
                {
                    Result=true
                };
                var callback = Url.Action("ConfirmEmail", "Account", new
                {
                    token = token.Result
                });
                MailSend.SendMail(model.RegisterModel.Email, "Hesap Aktivasyon", $"Lütfen Hesabınızı Onaylamak İçin  <a href='https://giyimover.com{callback}'>Tıklayınız.</a>");

               return RedirectToAction("Login", response);
            }
            return View(model);
        }
        public IActionResult Login(PaymentResponseViewModel response) 
        {
            LoginModel model = new LoginModel();
            allLayoutModel.LoginModel = model;
            allLayoutModel.Categorias = _categoryService.GetAll();
            if (response.Result == true)
            {
                ViewBag.error = "Hesabınızı Aktive ediniz";
                ViewBag.messagecolor = "alert alert-success";
                ViewBag.State = "Üyelik Aktifliği.";
            }
            return View(allLayoutModel);
        }
        [HttpPost]
        public IActionResult Login(AllLayoutModel model)
        {
            var userdata = _usersService.GetByEmailUsers(model.LoginModel.Email);
            if (userdata != null)
            {
                if (userdata.IsConfirmEmail == false)
                {
                    ViewBag.message = "Lütfen Hesabınızı Onaylayınız";
                    return View(model);
                }
                var result = _usersService.Authenticate(model.LoginModel.Email, model.LoginModel.Password);
                if (result.IsCompleted == true)
                {
                    var token = result.Result;
                    allLayoutModel.Categorias = _categoryService.GetAll();
                    var userid = TokenUserValueFunc.TokenGetValue(token);
                    var userinfo = _usersService.GetById(userid);
                    allLayoutModel.UserInfo = userinfo;
                    HttpContext.Session.SetString("token", token);
                    return RedirectToAction("ProductList", "Product");
                }
                else
                {
                    var entity = new AllLayoutModel
                    {
                        UserInfo = null,
                        Categorias = _categoryService.GetAll(),
                    };
                    ViewBag.error = "Lütfen Email veya Şifrenizi Kontrol Ediniz";
                    ViewBag.messagecolor = "alert alert-danger";
                    ViewBag.State = "Hesap Kontrolü";
                    return View(entity);
                }
            }
            else
            {
                var entity = new AllLayoutModel
                {
                    UserInfo = null,
                    Categorias = _categoryService.GetAll(),
                };
                ViewBag.error = "Lütfen Email veya Şifrenizi Kontrol Ediniz";
                ViewBag.messagecolor = "alert alert-danger";
                ViewBag.State = "Hesap Kontrolü";
                return View(entity);
            }
     
        }
        public IActionResult Logout()
        {
            HttpContext.Session.GetString("token");
            HttpContext.Session.SetString("token", "");
            return RedirectToAction("Login");
        }
        public IActionResult ConfirmEmail(string token)
        {
            if (token == null)
            {
                TempData["confirm"] = "Geçerisz Token";
                return View();
            }
            else
            {
                var userid = TokenUserValueFunc.TokenGetValue(token);
                _cartDalService.InıtıliazerCart(Convert.ToInt32(userid));
                TempData["confirm"] = "Hesabınız Onaylandı";
                var user = _usersService.GetById(Convert.ToInt32(userid));
                user.IsConfirmEmail = true;
                _usersService.Update(user);

            }
            return View();
        }
        public IActionResult FargotPassword()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return View(new AllLayoutModel()
                {
                    Products = _productService.GetAll(),
                    Categorias = _categoryService.GetAll()
                });
            }
            else
            {
                return View(new AllLayoutModel()
                {
                    Products = _productService.GetAll(),
                    Categorias = _categoryService.GetAll()
                });
            }
        }
        [HttpPost]
        public IActionResult FargotPassword(AllLayoutModel model)
        {
            var userdata = _usersService.GetByEmailUsers(model.UserInfo.Email);
            if (userdata == null)
            {
                ViewBag.error = "Bu Email adresine ait üyelik bulunamamıştır.";
                allLayoutModel.Categorias = _categoryService.GetAll();
                return View(allLayoutModel);
            }
            var token = _usersService.Authenticate(userdata.Email, userdata.Password);
            var callback = Url.Action("ResetPassword", "Account", new
            {
                token = token,
            });
            MailSend.SendMail(userdata.Email, "Hesap Aktivasyon", $"Lütfen Hesabınızı Onaylamak İçin  <a href='https://localhost:44384{callback}'>Tıklayınız.</a>");
            return RedirectToAction("Login", "Account");
        }
        public IActionResult ResetPassword(string token)
        {
            if (token == null)
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var userdata = _usersService.GetById(Convert.ToInt32(userid));
            if (userdata == null)
            {
                return Redirect("/Error/501");
            }
            ResetPasswordModel loginModel = new ResetPasswordModel();
            loginModel.Email = userdata.Email;
            return View(loginModel);
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            var userdata = _usersService.GetByEmailUsers(model.Email);
            userdata.Password = model.Password;
            userdata.ModifitedDate = DateTime.Today;
            userdata.Email = model.Email;
            _usersService.Update(userdata);
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult UserDetail()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var userinfo = _usersService.GetById(Convert.ToInt32(userid));
            allLayoutModel.UserInfo = userinfo;
            allLayoutModel.Categorias = _categoryService.GetAll();
            return View(allLayoutModel);
        }
        [HttpPost]
        public IActionResult UserDetail(AllLayoutModel model, string password)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var userinfo = _usersService.GetById(Convert.ToInt32(userid));
            if (string.IsNullOrEmpty(password))
            {
                password = userinfo.Password;
            }
            else
            {
                password = CommonMethods.ConvertToEncryp(userinfo.Password);
            }
            userinfo.FirstName = model.UserInfo.FirstName;
            userinfo.LastName = model.UserInfo.LastName;
            userinfo.Email = model.UserInfo.Email;
            userinfo.DateOfBirth = model.UserInfo.DateOfBirth;
            userinfo.CreatedDate = model.UserInfo.CreatedDate;
            userinfo.ModifitedDate = DateTime.Today;
            userinfo.IsActive = model.UserInfo.IsActive;
            userinfo.IsDeleted = model.UserInfo.IsDeleted;
            userinfo.Password = password;
            userinfo.role = model.UserInfo.role;
            userinfo.PhoneNumber = model.UserInfo.PhoneNumber;
            userinfo.IsConfirmEmail = model.UserInfo.IsConfirmEmail;
            userinfo.UserName = model.UserInfo.UserName;
            _usersService.Update(userinfo);
            return RedirectToAction("UserDetail");
        }
    }
}