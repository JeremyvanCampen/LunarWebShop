using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls.Expressions;
using Logic;
using Models;

namespace LunarWebShop.Controllers
{
    public class KlantController : Controller
    {
        private GebruikerLogic GebruikerLogic = new GebruikerLogic();
        private ProductLogic ProductLogic = new ProductLogic();

        //Registratie pagina
        [HttpGet]
        public ActionResult RegistratieKlant()
        {
            return View();
        }

        //Registratie invoeren
        [HttpPost]
        public ActionResult RegistratieKlant([Bind()] Klant user)
        {
            string message = "";

            message = GebruikerLogic.KlantToevoegen(user);

            if (message == "Succesvol")
            {
                message = " Account succesvol aangemaakt U kunt nu inloggen.";
                ViewBag.Message = message;
                ViewBag.Status = true;
                return View(user);
            }

            ViewBag.Status = false;
            ViewBag.Message = message;
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Gebruikersnaam"] != null)
            {
                return RedirectToAction("Index", "Home", new { Gebruikersnaam = Session["Gebruikersnaam"].ToString() });
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Inloggen login)
        {
            string message = "";
            var gebruiker = GebruikerLogic.Inloggen(login.Gebruikersnaam, login.Wachtwoord);
            if (ModelState.IsValid)
            {
                if (gebruiker is Klant)
                {

                    //Login Sessie aanmaken
                    Session["Klant"] = gebruiker;
                    return RedirectToAction("Index", "Home");
                }

                if (gebruiker is Administrator)
                {

                    message = " Succesvol ingelogd.";
                    ViewBag.Message = message;
                    ViewBag.Status = true;

                    //Login Sessie aanmaken
                    Session["Administrator"] = gebruiker;
                    return RedirectToAction("Index", "Home");
                }
            }
            message = gebruiker.ToString();
            ViewBag.Status = false;
            ViewBag.Message = message;
            return View();
        }

        public ActionResult Uitloggen()
        {
            Session.Clear();
            return RedirectToAction("Login", "Klant");
        }

        public ActionResult MijnAccount()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SaldoUploaden(int id, string gebruikersnaam)
        {
            return View(GebruikerLogic.KlantgegevensZonderSaldo(id, gebruikersnaam));
        }
        [HttpPost]
        public ActionResult SaldoUploaden([Bind()] Klant user)
        {
            GebruikerLogic.SaldoToevoegen(user.Saldo, user.KlantID);
            var gebruiker = GebruikerLogic.KlantgegevensVolledig(user.KlantID, user.Gebruikersnaam);
            Session["Klant"] = gebruiker;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ProductVerkopen(int id, string gebruikersnaam)
        {
            string message = "";
            foreach (Product p in GebruikerLogic.WinkelwagenProducten(id))
            {
                if (ProductLogic.ProductVerkopen(id, p.Keycode.First().KeycodeID) == "Onvoldoende Saldo")
                {
                    message = "Onvoldoende Saldo";
                    break;
                }
            }

            if (message == "Onvoldoende Saldo")
            {
                TempData["Message"] = message;
                return RedirectToAction("Myorder", "WinkelwagenManagement", new { id = id });
            }

            var gebruiker = GebruikerLogic.KlantgegevensVolledig(id, gebruikersnaam);
            Session["Klant"] = gebruiker;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GameBibliotheek(int id)
        {
            return View(ProductLogic.AlleProductenvanGebruiker(id));
        }
    }
}