using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunarWebShop.Models
{
    interface ILibraryManagement
    {
        void VerwijderProduct(Library library, Product product);
        void VoegProductToe(Library library, Product product);
    }
}
