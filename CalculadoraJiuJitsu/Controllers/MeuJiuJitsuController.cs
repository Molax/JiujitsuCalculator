using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraJiuJitsu.Controllers
{
    public class MeuJiuJitsuController : Controller
    {
        // GET: MeuJiuJitsu
        public ActionResult Index()
        {
            ViewBag.UrlFb = GetFacebookLoginUrl();

            return View();
        }

        public string GetFacebookLoginUrl()
        {
            dynamic parameters = new ExpandoObject();
            parameters.client_id = "1363597323667956";
            parameters.redirect_uri = "http://localhost:10738/Calculadora/Index";
            parameters.response_type = "code";
            parameters.display = "page";

            var extendedPermissions = "public_profile,publish_actions";
            parameters.scope = extendedPermissions;

            var _fb = new FacebookClient();
            var url = _fb.GetLoginUrl(parameters);

            return url.ToString();
        }
    }
}