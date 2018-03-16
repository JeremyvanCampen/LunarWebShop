using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    public class Klant:Gebruiker

    {
        public int KlantID { get; set; }

        public decimal Saldo { get; set; }

        [Display(Name = "Straatnaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Straatnaam moet ingevoerd worden!")]
        public string Straat { get; set; }

        [Display(Name = "Huisnummer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Huisnummer met ingevoerd worden")]
        public int Huisnummer { get; set; }

        public void CreateSession()
        {
            
        }

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