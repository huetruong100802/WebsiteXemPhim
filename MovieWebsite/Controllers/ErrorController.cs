using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Models;
using System.Diagnostics;

namespace MovieWebsite.Controllers
{
    public class ErrorController : BaseController
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    ViewBag.Path = statusCodeResult?.OriginalPath;
                    ViewBag.QS = statusCodeResult?.OriginalQueryString;
                    break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionDetail= HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionDetail != null)
            {
                ViewBag.ExceptionPath = exceptionDetail.Path;
                ViewBag.ErrorMessage = exceptionDetail.Error.Message;
                ViewBag.StackTrace = exceptionDetail.Error.StackTrace;
            }
            else
            {
                ViewBag.ErrorMessage = "Unknown error";

            }
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return View("Error");
        }
    }
}
