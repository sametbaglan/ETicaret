using ETicaret.BusinessLayer.Abstrack;
using ETicaret.BusinessLayer.Email_Entegrasyon;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Layer.Models;
using UI.Layer.Models.UserModel;

namespace UI.Layer.Controllers.UserContr
{
    public class UsersController : Controller
    {
        private ICategoriaService _categoriaService;
        private IUsersService _usersService;
        public UsersController(IUsersService usersService, ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
            _usersService = usersService;
        }
        AllLayoutModel allLayoutModel = new AllLayoutModel();
        public IActionResult Contact()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return View(new AllLayoutModel()
                {
                    Categorias = _categoriaService.GetAll()
                });
            }
            else
            {
                var userid = TokenUserValueFunc.TokenGetValue(token);
                var user = _usersService.GetById(userid);
                return View(new AllLayoutModel()
                {
                    UserInfo = user,
                    Categorias = _categoriaService.GetAll()
                });
            }
        }
        public IActionResult InfoSendEmail()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return View(new AllLayoutModel()
                {
                    Categorias = _categoriaService.GetAll()
                });
            }
            else
            {
                var userid = TokenUserValueFunc.TokenGetValue(token);
                var user = _usersService.GetById(userid);
                return View(new AllLayoutModel()
                {
                    UserInfo = user,
                    Categorias = _categoriaService.GetAll()
                });
            }
        }
        [HttpPost]
        public IActionResult InfoSendEmail(AllLayoutModel model, bool c1)
        {
            if (c1 == false)
            {
                ViewBag.error = "Lütfen Kvkk Metnini Onaylayınız";
                return RedirectToAction("InfoSendEmail");
            }
            MailSend.SendMail("samt51.m@icloud.com", model.ınfoSendEmail.Subject, model.ınfoSendEmail.NameSurname + "</br> " + model.ınfoSendEmail.Phone + "</br> " + model.ınfoSendEmail.Message);

            allLayoutModel.Categorias = _categoriaService.GetAll();
            ViewBag.error = "Mesajınız İletilmiştir En kısa sürede iletişeme geçilecektir.";
            return RedirectToAction("InfoSendEmail");
        }
    }
}



