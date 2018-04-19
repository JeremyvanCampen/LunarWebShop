using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace Logic
{
    public class ProductLogic 
    {
        private ProductEngine _productEngine = new ProductEngine();
        public List<Product> AlleProducten()
        {
            return _productEngine.AlleProducten();
        }
        public void DeleteProduct(int id)
        {
            _productEngine.DeleteProduct(id);
        }

        public void CreateProduct(Product product)
        {
            _productEngine.CreateProduct(product);
        }

        public void ProductAanpassen(Product product)
        {
            _productEngine.ProductAanpassen(product);
        }

        public object ProductOphalen(int id)
        {
            return _productEngine.ProductOphalen(id);
        }

        public object KeycodeOphalen(int productid)
        {
            return _productEngine.KeycodeOphalen(productid);
        }

        public string ProductVerkopen(int KlantID, int ProductID)
        {
            return _productEngine.ProductVerkopen(KlantID, ProductID);
        }

        public List<Product> AlleProductenvanGebruiker(int id)
        {
            return _productEngine.AlleProductenvanGebruiker(id);
        }

    }
}
