using System.Web.Mvc;
using Foillan.Models;
using Foillan.Models.DataAccessLayer;
using Foillan.Models.DataAccessLayer.Concrete;

namespace Foillan.WebUI.Controllers
{
	public class SightingController : Controller
	{
		private FoillanContext _Context;

		public SightingController()
		{
			_Context = new FoillanContext();
		}

		[HttpGet]
		public ActionResult Sighting()
		{
			var Model = new Sighting();
			return View("Sighting", Model);
		}

		[HttpPost]
		public ActionResult Sighting(Sighting NewSighting) {

			if (!ModelState.IsValid)
			{
				return View("Sighting");
			}

			return View("Success");
		}
	}
}