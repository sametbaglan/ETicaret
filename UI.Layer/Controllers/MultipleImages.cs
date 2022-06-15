using ETicaret.BusinessLayer.Abstrack;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using UI.Layer.Models;

namespace UI.Layer.Controllers
{
    public class MultipleImages : Controller
    {
        private IImagesService _ımagesService;
        private IUsersService _usersService;
        private IHostingEnvironment _hostingEnvironment;
        AllLayoutModel allLayout = new AllLayoutModel();
        public MultipleImages(IHostingEnvironment hostingEnvironment, IImagesService imagesService, IUsersService usersService)
        {
            _ımagesService = imagesService;
            _usersService = usersService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult ProductImages(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            allLayout.UserInfo = users;
            HttpContext.Session.SetString("productid", id.ToString());
            var ımages = _ımagesService.GetImagesWithProductid(id);
            if (ımages != null)
            {
                allLayout.ListImages = ımages;
            }
            else
            {
                allLayout.ListImages = ımages;
            }
            return View(allLayout);
        }
        [HttpPost]
        public IActionResult ImageAdd(IFormFile file)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            allLayout.UserInfo = users;
            var productid= HttpContext.Session.GetString("productid");
            string uniqfilename = Guid.NewGuid().ToString() + ".jpeg";
            Images img = new Images();
            if (file != null)
            {
                var Upload = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqfilename);
                file.CopyTo(new FileStream(Upload, FileMode.Create));
            }
            else
            {
                return Redirect("/MultipleImages/ProductImages/" + productid.ToString());
            }
            img.ProductID = Convert.ToInt32(productid.ToString());
            img.Image = uniqfilename;
            _ımagesService.Create(img);
            return Redirect("/MultipleImages/ProductImages/" + productid.ToString());
        }
        public IActionResult ImageUpdate(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            allLayout.UserInfo = users;
            var image = _ımagesService.GetById(id);
            allLayout.ımage = image;
            return View(allLayout);
        }
        [HttpPost]
        public IActionResult ImageUpdate(IFormFile file)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            allLayout.UserInfo = users;
            var imageid = HttpContext.Request.RouteValues["id"];         
            var image = _ımagesService.GetById(Convert.ToInt16(imageid.ToString()));
            var productid = HttpContext.Session.GetString("productid");
            string uniqfilename = Guid.NewGuid().ToString() + ".jpeg";
            if (file != null)
            {
                var Upload = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqfilename);
                file.CopyTo(new FileStream(Upload, FileMode.Create));

            }
            else
            {
                uniqfilename = image.Image;
            }
            image.Image = uniqfilename;
            _ımagesService.Update(image);
            return Redirect("/MultipleImages/ProductImages/" + productid.ToString());
        }
        [HttpPost]
        public IActionResult ImageDelete(int imageid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            allLayout.UserInfo = users;
            var productid = HttpContext.Session.GetString("productid");

            var image = _ımagesService.GetById(imageid);
            _ımagesService.Delete(image);
            return Redirect("/MultipleImages/ProductImages/" + productid.ToString());
        }
        public IActionResult CreateImages(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            allLayout.UserInfo = users;
            return View(allLayout);
        }
        [HttpPost]
        public IActionResult CreateImages(List<IFormFile> file)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            if (users.role != "A") { return Redirect("/Error/501"); }
            allLayout.UserInfo = users;
            var productid = HttpContext.Request.RouteValues["id"];
            if (productid == null)
            {
                productid= HttpContext.Session.GetString("productid");
            }
            string uniqfilename = "";
            if (file==null)
            {
                ViewBag.error = "Lütfen Resim Seçiniz";
                return View(allLayout);
            }
            else
            {
                foreach (var item in file)
                {
                    Images ımages = new Images();
                    uniqfilename = Guid.NewGuid().ToString() + ".jpeg";
                    var Upload = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqfilename);
                    item.CopyTo(new System.IO.FileStream(Upload, System.IO.FileMode.Create));
                    ımages.Image = uniqfilename;
                    ımages.ProductID = Convert.ToInt32(productid);
                    _ımagesService.Create(ımages);
                }
            }
            return Redirect("/MultipleImages/ProductImages/" + productid.ToString());
        }
    }
}
