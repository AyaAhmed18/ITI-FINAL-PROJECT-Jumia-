using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AdminDashBoard.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult SetLanguage(string lang)
        {
            // Store selected language in session
            HttpContext.Session.SetString("lang", lang);

            // Redirect back to the previous page
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Arabic()
        {
            HttpContext.Session.SetString("lang", "ar-EG");

            // Set the current culture for the current thread
            CultureInfo.CurrentCulture = new CultureInfo("ar-EG");
            CultureInfo.CurrentUICulture = new CultureInfo("ar-EG");

            string referringUrl = Request.Headers["Referer"].ToString();
            return Redirect(referringUrl);
        
       /* HttpContext.Session.SetString("lang", "Ar-EG");
            string referringUrl = Request.Headers["Referer"].ToString();
            return Redirect(referringUrl);*/
        }

       
        public IActionResult English()
        {
            HttpContext.Session.SetString("lang", "en-US");

            // Set the current culture for the current thread
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            CultureInfo.CurrentUICulture = new CultureInfo("en-US");

            string referringUrl = Request.Headers["Referer"].ToString();
            return Redirect(referringUrl);
           /* HttpContext.Session.SetString("lang", "EN-US");
            string referringUrl = Request.Headers["Referer"].ToString();
            return Redirect(referringUrl);*/
        }

    }
}
