using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

namespace AdminDashBoard.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Session.GetString("lang") != null)
            {
                string lang = HttpContext.Session.GetString("lang");
                CultureInfo.CurrentCulture = new CultureInfo(lang);
                CultureInfo.CurrentUICulture = new CultureInfo(lang);
            }
            /*if (filterContext.HttpContext.Session.GetString("lang") != null)
            {
                string lang = filterContext.HttpContext.Session.GetString("lang");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                Console.WriteLine("Current culture: " + lang);
            }*/
        }
    }
}