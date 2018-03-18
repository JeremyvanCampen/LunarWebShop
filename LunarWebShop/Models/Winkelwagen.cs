using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    //    public class Winkelwagen
    //    {
    //        public int winkelWagenID { get; private set; }
    //        public List<Product> producten { get; private set; }
    //        public decimal prijs { get; private set; }
    //        public BetaalMethode betaalMethode { get; private set; }

    //        public Winkelwagen(BetaalMethode betaalMethode)
    //        {
    //            this.betaalMethode = betaalMethode;
    //            producten = new List<Product>();
    //        }

    //        public void addProduct(Product product)
    //        {
    //            producten.Add(product);
    //            prijs = prijs + product.prijs;
    //        }
    //        public void RemoveProduct(Product product)
    //        {
    //            producten.Remove(product);
    //            prijs = prijs - product.prijs;
    //        }

    //        public override string ToString()
    //        {
    //            string WinkelwagenDetails = "De Producten in uw winkelwagen zijn op dit moment : ";
    //            foreach (var p in producten)
    //            {
    //                WinkelwagenDetails = WinkelwagenDetails + p.productNaam + " ";
    //            }
    //            WinkelwagenDetails = WinkelwagenDetails + "en dat komt neer op een totaal bedrag van : " + Convert.ToString(prijs);
    //            return WinkelwagenDetails;
    //        }
    //    }
}
