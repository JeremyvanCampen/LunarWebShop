using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    public class Library : ILibraryManagement
    {
        public int libraryID { get; private set; }
        public List<Product> producten { get; set; }
        public Library()
        {
            producten = new List<Product>();
        }
        public void VerwijderProduct(Library library, Product product)
        {
            producten.Add(product);
        }

        public void VoegProductToe(Library library, Product product)
        {
            producten.Remove(product);
        }
    }
}
