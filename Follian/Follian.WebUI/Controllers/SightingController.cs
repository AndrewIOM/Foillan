using Follian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Follian.WebUI.Controllers
{
	public class SightingController : Controller
	{
		private BiodiversityDbContext _Context;

		public SightingController()
		{
			_Context = new BiodiversityDbContext();
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