using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using LunarWebShop.Models;
using LunarWebShop.Models.AccountManagement;


namespace LunarWebShop.Controllers
{
    public class KlantController : Controller
    {
        //Registratie pagina
        [HttpGet]
        public ActionResult Registratie()
        {
            return View();
        }

        //Registratie invoeren
        [HttpPost]
        public ActionResult Registratie([Bind()]Gebruiker user)
        {
            string message = "";
            bool Status = false;
            Account account = new Account();

                if (account.CreateKlant(user))
                {
                    message = " Account succesvol aangemaakt U kunt nu inloggen";
                    ViewBag.Message = message;
                    ViewBag.Status = true;
                    return View(user);
                }
                message = " Email of gebruikersnaam bestaat al";
                ViewBag.Status = false;
                ViewBag.Message = message;
                return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Inloggen login)
        {
            string message = "";
            Account account = new Account();
            if (account.Inloggen(login.Gebruikersnaam, login.Wachtwoord) == "Fout")
            {
                message = "Account gegevens bestaan niet.";
                ViewBag.Message = message;
                return View();

            }
            return RedirectToAction("Index", "Home");
        }

    }
}