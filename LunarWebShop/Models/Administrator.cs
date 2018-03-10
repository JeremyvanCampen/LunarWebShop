using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    public class Administrator : Gebruiker, ILibraryManagement
    {
        //    //public Administrator(string voornaam, string achternaam, string gebruikersnaam, string wachtwoord, string email, DateTime geboortedatum) : base(voornaam, achternaam, gebruikersnaam, wachtwoord, email, geboortedatum)
        //    //{
        //    //    this.Voornaam = voornaam;
        //    //    this.Achternaam = achternaam;
        //    //    this.Gebruikersnaam = gebruikersnaam;
        //    //    this.Wachtwoord = wachtwoord;
        //    //    this.Email = email;
        //    //    this.Geboortedatum = geboortedatum;
        //    //}
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