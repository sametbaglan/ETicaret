using ETicaret.BusinessLayer.Abstrack;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Layer.Models;

namespace UI.Layer.Controllers.UserContr
{
    public class ContractsController : Controller
    {
        private ICategoriaService _categoriaService;
        private IUsersService _usersService;
        public ContractsController(IUsersService usersService, ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
            _usersService = usersService;
        }
        AllLayoutModel allLayoutModel = new AllLayoutModel();
        public IActionResult SalesContract()
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
            };
        }
        public IActionResult AllContract()
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
            };
        }
        public IActionResult PrivacyAndSecurity()
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
            };
        }
        public IActionResult CancellationandRefund()
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
            };
        }
        public IActionResult PersonalDataPolicy()
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
            };
        }
    }
}
