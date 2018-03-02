using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    public class Product
    {
        public int productID { get; private set; }
        public string productNaam { get; private set; }
        public decimal prijs { get; private set; }
        public int productKey { get; private set; }
        public Genre genre { get; private set; }
        public Uitgever uitgever { get; private set; }

        public Product(int productID, string productNaam, decimal prijs, int productKey, Genre genre, Uitgever uitgever)
        {
            this.productID = productID;
            this.productNaam = productNaam;
            this.prijs = prijs;
            this.uitgever = uitgever;
            this.productKey = productKey;
            this.genre = genre;
        }
    }
}
