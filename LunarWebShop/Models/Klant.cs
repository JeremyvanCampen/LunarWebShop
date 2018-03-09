using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    public class Klant

    {
        //public decimal Saldo { get; private set; }
        ////Producten in bezit
        //public List<Product> ProductenGekocht { get; private set; }
        ////Producten in winkelwagen
        //public Winkelwagen Winkelwagen { get; private set; }

        //public Klant(string voornaam, string achternaam, string gebruikersnaam, string wachtwoord, string email, DateTime geboortedatum, decimal saldo) : base(voornaam, achternaam, gebruikersnaam, wachtwoord, email, geboortedatum)
        //{
        //    this.Voornaam = voornaam;
        //    this.Achternaam = achternaam;
        //    this.Gebruikersnaam = gebruikersnaam;
        //    this.Wachtwoord = wachtwoord;
        //    this.Email = email;
        //    this.Geboortedatum = geboortedatum;
        //    //this.Saldo = saldo;
        //    //ProductenGekocht = new List<Product>();
        //    //Winkelwagen = new Winkelwagen(BetaalMethode.IDeal);
        //}

        //public void SaldoVerhogen(decimal verhoogbedrag)
        //    {
        //        Saldo = Saldo + verhoogbedrag;
        //    }

        //    public void SaldoVerlagen(decimal verlaagbedrag)
        //    {
        //        Saldo = Saldo - verlaagbedrag;
        //    }

        //    public void ProductenAfrekenen()
        //    {
        //        if (Saldo >= Winkelwagen.prijs)
        //        {
        //            Saldo = Saldo - Winkelwagen.prijs;
        //            foreach (Product p in Winkelwagen.producten)
        //            {
        //                ProductenGekocht.Add(p);
        //            }
        //            Winkelwagen.producten.Clear();
        //        }
        //    }

    }
}