using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading;

namespace AdminDashBoard.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("lang") != null)
            {
                string lang = filterContext.HttpContext.Session.GetString("lang");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }
        }
    }
}