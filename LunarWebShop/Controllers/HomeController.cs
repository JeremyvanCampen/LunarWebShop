using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logic;
using Models;

namespace LunarWebShop.Controllers
{
    public class HomeController : Controller
    {
       
        private ProductLogic ProductLogic = new ProductLogic();
        public ActionResult Index()
        {
            return View(ProductLogic.AlleProducten());
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