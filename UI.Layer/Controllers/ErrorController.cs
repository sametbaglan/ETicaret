using Business.Layer;
using Microsoft.AspNetCore.Mvc;

namespace UI.Layer.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult GeneralError(int statusCode)
        {
            if (statusCode == 404)
            {
                ViewBag.Title = "Not Found";
                ViewBag.ErrorCode = "404";
                ViewBag.ErrorMessage = Messages.PageNotFound;
                return View();
            }
            else if (statusCode == 401)
            {
                ViewBag.Title = "Unauthorized Access";
                ViewBag.ErrorCode = "401";
                ViewBag.ErrorMessage = Messages.Unauthorized;
                return View();
            }
            else if (statusCode == 501)
            {
                ViewBag.Title = "Servicess Error";
                ViewBag.ErrorCode = "501";
                ViewBag.ErrorMessage = Messages.ServicessError;
            }

            return View();
        }
    }
}
