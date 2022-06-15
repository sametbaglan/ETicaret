
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Layer.Models;

namespace ETicaret.UILayer.Controllers
{

    public class ItemColorController : Controller
    {
        private IItemColorService _ıtemColorService;
        private IUsersService _usersService;    
        public ItemColorController(IItemColorService ıtemColorService, IUsersService usersService)
        {
            _ıtemColorService = ıtemColorService;
            _usersService = usersService;
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
            return View(new IItemColorListModel() { 
            
            ItemColors=_ıtemColorService.GetAll()
            });
        }
        public IActionResult AddColor()
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
        public IActionResult AddColor(ColorCreateModel model)
        {
            var entity = new ItemColors()
            {
              
            };
            _ıtemColorService.Create(entity);
          
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete(int colorid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            var body = _ıtemColorService.GetById(colorid);
            body.IsActive = false;
            body.IsDeleted = true;
            _ıtemColorService.Update(body);
            return RedirectToAction("Attirbutes", "Varyant");
        }
    }
}
