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
        private GebruikerEngine _gebruikerEngine;
        [TestInitialize]
        public void TestInitialize()
        {
            _gebruikerEngine = new GebruikerEngine();
        }
        [TestMethod()]
        public void InloggenTest1()
        {
            var resultaat = _gebruikerEngine.Inloggen("123456", "123456");
            Klant testklant = resultaat as Klant;
            int KlantIDResultaat = 30;

            Assert.AreEqual(KlantIDResultaat, testklant.KlantID);
        }
        [TestMethod()]
        public void InloggenTest2()
        {
            var resultaat = _gebruikerEngine.Inloggen("onzin", "onzin");
            Klant testklant = resultaat as Klant;
            string JuisteResultaat = " Account Gegevens bestaan niet.";

            Assert.AreEqual(JuisteResultaat, resultaat);
        }
        [TestMethod()]
        public void InloggenTest3()
        {
            var resultaat = _gebruikerEngine.Inloggen("", "");
            Klant testklant = resultaat as Klant;
            string JuisteResultaat = " Account Gegevens bestaan niet.";

            Assert.AreEqual(JuisteResultaat, resultaat);
        }

        [TestMethod()]
        public void WinkelwagenProductenTest()
        {
            int JuistResultaat = 1;

            Assert.AreEqual(JuistResultaat, _gebruikerEngine.WinkelwagenProducten(31).Count);
        }
    }
}