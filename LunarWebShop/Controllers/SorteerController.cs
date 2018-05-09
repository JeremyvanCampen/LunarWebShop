using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logic;
using Models;

namespace LunarWebShop.Controllers
{
    public class SorteerController : Controller
    {
        ProductLogic Productlogic = new ProductLogic();
        public ActionResult IndexSearch()
        {
            return View();
        }
        public ActionResult IndexGenre(string genre)
        {
            List<Product> producten = new List<Product>();
            if (genre == "Action")
            {
                producten = Productlogic.ProductenGenre(Genre.Action);
            }
            if (genre == "Adventure")
            {
                producten = Productlogic.ProductenGenre(Genre.Adventure);
            }
            if (genre == "Fighting")
            {
                producten = Productlogic.ProductenGenre(Genre.Fighting);
            }
            if (genre == "Platform")
            {
                producten = Productlogic.ProductenGenre(Genre.Platform);
            }
            if (genre == "Puzzle")
            {
                producten = Productlogic.ProductenGenre(Genre.Puzzle);
            }
            if (genre == "Racing")
            {
                producten = Productlogic.ProductenGenre(Genre.Racing);
            }
            if (genre == "RolePlaying")
            {
                producten = Productlogic.ProductenGenre(Genre.RolePlaying);
            }
            if (genre == "Shooter")
            {
                producten = Productlogic.ProductenGenre(Genre.Shooter);
            }
            if (genre == "Simulation")
            {
                producten = Productlogic.ProductenGenre(Genre.Simulation);
            }
            if (genre == "Sports")
            {
                producten = Productlogic.ProductenGenre(Genre.Sports);
            }
            if (genre == "Strategy")
            {
                producten = Productlogic.ProductenGenre(Genre.Strategy);
            }
            if (genre == "Mmorpg")
            {
                producten = Productlogic.ProductenGenre(Genre.Mmorpg);
            }
            return View(producten);
        }
        public ActionResult IndexPrijsLaagNaarHoog()
        {
            return View(Productlogic.ProductenPrijsLaagHoog());
        }
        public ActionResult IndexPrijsHoogNaarLaag()
        {
            return View(Productlogic.ProductenPrijsHoogLaag());
        }
    }
}