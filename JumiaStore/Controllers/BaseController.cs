using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
      /*  protected override void Initialize(HttpContext context)
        {
            var lang = context.Request.Headers["Accept-Language"];
            if (!string.IsNullOrEmpty(lang))
            {
                var culture = new CultureInfo(lang);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
            base.Initialize(context);
        }
   */ }
}
