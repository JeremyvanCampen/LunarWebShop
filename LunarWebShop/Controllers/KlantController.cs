using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LunarWebShop.Models;
using LunarWebShop.Models.AccountManagement;

namespace LunarWebShop.Controllers
{
    public class KlantController : Controller
    {
        //Registratie pagina
        [HttpGet]
        public ActionResult RegistratieKlant()
        {
            return View();
        }

        //Registratie invoeren
        [HttpPost]
        public ActionResult RegistratieKlant([Bind()]Klant user)
        {
            string message = "";
            bool Status = false;

            Account account = new Account();
            message = account.KlantToevoegen(user);

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
                return RedirectToAction("Index", "Home", new {Gebruikersnaam = Session["Gebruikersnaam"].ToString()});
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Inloggen login)
        {
            string message = "";
            bool Status = false;

            Account account = new Account();
            message = account.Inloggen(login.Gebruikersnaam, login.Wachtwoord);

            if (message == "Succesvol")
            {
                message = " Succesvol ingelogd.";
                ViewBag.Message = message;
                ViewBag.Status = true;

                //Login Sessie aanmaken
                Session["Klant"] = new Klant(){Gebruikersnaam = login.Gebruikersnaam};

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Status = false;
            //ViewBag.Message = message;
            return View();
        }

        public ActionResult Uitloggen()
        {
            Session.Clear();
            return RedirectToAction("Login", "Klant");
        }



    }
}