using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoard.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult Arabic()
        {

            HttpContext.Session.SetString("lang", "ar-EG");
            string referringUrl = Request.Headers["Referer"].ToString();
            return Redirect(referringUrl);
        }


        public IActionResult English()
        {
            HttpContext.Session.SetString("lang", "en-US");
            string referringUrl = Request.Headers["Referer"].ToString();
            return Redirect(referringUrl);
        }

    }
}
