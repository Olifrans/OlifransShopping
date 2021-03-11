using OlifransShopping.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OlifransShopping.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string search, int? page ) 
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 12, page));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description c.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}