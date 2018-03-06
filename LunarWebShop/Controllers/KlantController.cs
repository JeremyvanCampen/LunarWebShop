using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LunarWebShop.Models;
using LunarWebShop.Models.Extended;

namespace LunarWebShop.Controllers
{
    public class KlantController : Controller
    {
        // Registratie Action
        [HttpGet]
        public ActionResult Registratie()

        {
            return View();
        }


        //Na Registratie
        [HttpPost]
        public ActionResult Registratie([Bind()]Klant klant)
        {
            using (LunarEntities1 dc = new LunarEntities1())
            {
                dc.Klants.Add(klant);
                dc.SaveChanges();
            }
                return View(klant);
        }

        //Inloggen
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }
        //Na Inloggen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KlantInloggen login, string ReturnUrl = "")
        {
            string message = "";
            using (LunarEntities1 dc = new LunarEntities1())
            {
                var g = dc.Klants.Where(a => a.Gebruikersnaam == login.Gebruikersnaam).FirstOrDefault();
                if (g != null)
                {
                    var p = dc.Klants.Where(a => a.Wachtwoord == login.Wachtwoord).FirstOrDefault();
                    if (p != null)
                    {
                        //525600 min = 1 jaar
                        int timeout = login.OnthoudtMijnInloggegevens ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.Gebruikersnaam,login.OnthoudtMijnInloggegevens, timeout);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Account gegevens bestaan niet.";
                    }
                }
                else
                {
                    message = "Account gegevens bestaan niet.";
                }
            }
            ViewBag.Message = message;
                return View();
        }

        //Uitloggen
        [Authorize]
        [HttpPost]
        public ActionResult Uitloggen()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Klant");
        }
    }
}