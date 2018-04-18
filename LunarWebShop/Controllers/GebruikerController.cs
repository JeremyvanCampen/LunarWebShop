﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls.Expressions;
using LunarWebShop.Models;
using LunarWebShop.Models.AccountManagement;
using Microsoft.Ajax.Utilities;

namespace LunarWebShop.Controllers
{
    public class KlantController : Controller
    {
        private Account account = new Account();

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
            var gebruiker = account.Inloggen(login.Gebruikersnaam, login.Wachtwoord);

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
            return View(account.KlantgegevensZonderSaldo(id, gebruikersnaam));
        }
        [HttpPost]
        public ActionResult SaldoUploaden([Bind()] Klant user)
        {
            account.SaldoToevoegen(user.Saldo, user.KlantID);
            var gebruiker = account.KlantgegevensVolledig(user.KlantID, user.Gebruikersnaam);
            Session["Klant"] = gebruiker;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ProductVerkopen(int productid, int id, string gebruikersnaam)
        {
            string message = "";
            message = account.ProductVerkopen(id, productid);

            if (message == "Onvoldoende Saldo")
            {
                TempData["Message"] = message;
                return RedirectToAction("Myorder", "WinkelwagenManagement", new { id = id });
            }

            var gebruiker = account.KlantgegevensVolledig(id, gebruikersnaam);
            Session["Klant"] = gebruiker;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GameBibliotheek(int id)
        {

            return View(account.AlleProductenvanGebruiker(id));
        }
    }
}