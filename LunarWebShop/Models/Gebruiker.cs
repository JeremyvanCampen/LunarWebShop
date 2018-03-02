using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    public abstract class Gebruiker
    {
        public string gebruikersnaam { get; set; }
        public string wachtwoord { get; set; }
        public string naam { get; set; }
        public string email { get; set; }
        public string adres { get; set; }
        public bool loginStatus { get; set; }

        public Gebruiker(string gebruikersnaam, string wachtwoord, string naam, string email, string adres)
        {
            this.gebruikersnaam = gebruikersnaam;
            this.wachtwoord = wachtwoord;
            this.naam = naam;
            this.email = email;
            this.adres = adres;
            this.loginStatus = false;
        }

        public void LoginCheck()
        {
            loginStatus = true;
        }
    }
}
