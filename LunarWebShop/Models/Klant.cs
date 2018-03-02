using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    public class Klant : Gebruiker
    {
        public int klantID { get; private set; }
        public decimal saldo { get; private set; }
        //Producten in bezit
        public List<Product> ProductenGekocht { get; private set; }
        //Producten in winkelwagen
        public Winkelwagen winkelwagen { get; private set; }

        public Klant(int klantID, string gebruikersnaam, string wachtwoord, string naam, string email, string adres) : base(gebruikersnaam, wachtwoord, naam, email, adres)
        {
            this.klantID = klantID;
            this.gebruikersnaam = gebruikersnaam;
            this.wachtwoord = wachtwoord;
            this.naam = naam;
            this.email = email;
            this.adres = adres;
            ProductenGekocht = new List<Product>();
            winkelwagen = new Winkelwagen(BetaalMethode.IDeal);
        }

        public void SaldoVerhogen(decimal verhoogbedrag)
        {
            saldo = saldo + verhoogbedrag;
        }

        public void SaldoVerlagen(decimal verlaagbedrag)
        {
            saldo = saldo - verlaagbedrag;
        }

        public void ProductenAfrekenen()
        {
            if (saldo >= winkelwagen.prijs)
            {
                saldo = saldo - winkelwagen.prijs;
                foreach (Product p in winkelwagen.producten)
                {
                    ProductenGekocht.Add(p);
                }
                winkelwagen.producten.Clear();
            }
        }

    }
}
