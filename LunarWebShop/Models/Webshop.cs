using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    public class Webshop
    {
        public string Webshopnaam { get; private set; }
        public List<Gebruiker> gebruikers { get; private set; }
        public Library library { get; private set; }

        public Webshop(string naam)
        {
            Webshopnaam = naam;
            gebruikers = new List<Gebruiker>();
            library = new Library();
        }

        public void VoegGebruikerToe(Gebruiker gebruiker)
        {
            gebruikers.Add(gebruiker);
        }
    }
}
