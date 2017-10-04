using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vnap.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Download()
        {
            var file = "com.troylee.vnap.apk";
            var dir = Path.Combine(Server.MapPath("~/App_Data"), file);
            return File(dir, "application/vnd.android.package-archive", file);
        }
    }
}
