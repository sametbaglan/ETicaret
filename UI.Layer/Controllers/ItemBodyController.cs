
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Layer.Models;

namespace AppLegendSoft.UI.Controllers
{

    public class ItemBodyController : Controller
    {
        private IItemBodySizeService _ıtemBodySizeService;
        private IUsersService _usersService;
        public ItemBodyController(IItemBodySizeService ıtemBodySizeService, IUsersService usersService)
        {
            _ıtemBodySizeService = ıtemBodySizeService;
            _usersService = usersService;
        }
        public IActionResult BodyList()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            return View(new BodyListModel() { 
            
            bodySizes=_ıtemBodySizeService.GetAll()
            });
        }
        public IActionResult BodyCreate()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            return View();
        }
        [HttpPost]
        public IActionResult BodyCreate(BodySizeCreateModel model)
        {
            var entity = new BodySizes()
            {
               
            };
            _ıtemBodySizeService.Create(entity);
            return RedirectToAction("BodyList");
        }
        [HttpPost]
        public IActionResult Delete(int bodyid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var body = _ıtemBodySizeService.GetById(bodyid);
            body.IsActive = false;
            body.IsDeleted = true;
            _ıtemBodySizeService.Update(body);
            return RedirectToAction("Attirbutes", "Varyant");
        }
    }
}
