using System.Web.Mvc;

namespace Foillan.WebService.Controllers.Mvc
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
