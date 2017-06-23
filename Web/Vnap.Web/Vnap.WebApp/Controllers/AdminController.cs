using System.Web.Mvc;

namespace Vnap.WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Admin Page";

            return View();
        }
    }
}
