using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Tests
{
    [TestClass()]
    public class GebruikerEngineTests
    {
      

        [TestMethod()]
        public void InloggenTest()
        {
            GebruikerEngine _gebruikerEngine = new GebruikerEngine();
            var resultaat = _gebruikerEngine.Inloggen("123456", "123456");
            Klant testklant = resultaat as Klant;   
            Klant klant = new Klant();
            klant.GebruikerID = 40;
            klant.Voornaam = "Jeremy";
            klant.Achternaam = "van Campen";
            klant.Gebruikersnaam = "123456";
            klant.Wachtwoord = "123456";
            klant.Email = "jeremyvancampen97@gmail.com";
            klant.KlantID = 30;
            klant.Saldo = Convert.ToDecimal(0.01);
            klant.Straat = "Marsstraat";
            klant.Huisnummer = 28;

            Assert.AreEqual(klant.KlantID, testklant.KlantID);

        }
    }
}