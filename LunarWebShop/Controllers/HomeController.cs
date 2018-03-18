using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LunarWebShop.Models;

namespace LunarWebShop.Controllers
{
    public class HomeController : Controller
    {
        LunarProduct _database = new LunarProduct();
        public ActionResult Index()
        {
            return View(_database.Product.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}