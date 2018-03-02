using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    public class Administrator : Gebruiker, ILibraryManagement
    {
        public int administratorID { get; set; }
        public Administrator(int administratorID, string gebruikersnaam, string wachtwoord, string naam, string email, string adres) : base(gebruikersnaam, wachtwoord, naam, email, adres)
        {
            this.administratorID = administratorID;
            this.gebruikersnaam = gebruikersnaam;
            this.wachtwoord = wachtwoord;
            this.naam = naam;
            this.email = email;
            this.adres = adres;
        }
        public void VerwijderProduct(Library library, Product product)
        {
            library.producten.Remove(product);
        }

        public void VoegProductToe(Library library, Product product)
        {
            library.producten.Add(product);
        }
    }
}