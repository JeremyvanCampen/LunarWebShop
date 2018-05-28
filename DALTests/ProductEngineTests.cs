using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Tests
{
    [TestClass()]
    public class ProductEngineTests
    {
        private ProductEngine _productEngine;
        [TestInitialize]
        public void TestInitialize()
        {
            _productEngine = new ProductEngine();
        }

        [TestMethod()]
        public void AlleProductenTest()
        {
            Assert.AreEqual(10, _productEngine.AlleProducten().Count);
        }

        [TestMethod()]
        public void ProductOphalenTest()
        {
            var TestProduct = _productEngine.ProductOphalen(42);
            Product TestProduct2 = TestProduct as Product;
            Assert.AreEqual("Red Dead Redemption 2", TestProduct2.Naam);
        }

        [TestMethod()]
        public void AlleProductenvanGebruikerTest()
        {

            Assert.AreEqual(11, _productEngine.AlleProductenvanGebruiker(30).Count);
        }

        [TestMethod()]
        public void ProductenGenreTest1()
        {
            Assert.AreEqual(Genre.Mmorpg, _productEngine.ProductenGenre(Genre.Mmorpg).First().Genre);
        }

        [TestMethod()]
        public void ProductenGenreTest2()
        {
            Assert.AreNotEqual(Genre.Mmorpg, _productEngine.ProductenGenre(Genre.Action).First().Genre);
        }


    }
}