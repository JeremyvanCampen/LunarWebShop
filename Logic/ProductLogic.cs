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

        public void CreateProduct(Product product, int hoeveelheid)
        {
            _productEngine.CreateProduct(product, hoeveelheid);
        }

        public void ProductAanpassen(Product product)
        {
            _productEngine.ProductAanpassen(product);
        }

        public object ProductOphalen(int id)
        {
            return _productEngine.ProductOphalen(id);
        }

        public List<Keycode> KeycodeOphalen(int productid)
        {
            return _productEngine.KeycodeOphalen(productid);
        }

        public string ProductVerkopen(int KlantID, int KeycodeID)
        {
            return _productEngine.ProductVerkopen(KlantID, KeycodeID);
        }

        public List<Product> AlleProductenvanGebruiker(int id)
        {
            return _productEngine.AlleProductenvanGebruiker(id);
        }

        public void Voorraadbijvullen(int productid, int hoeveelheid)
        {
            _productEngine.Voorraadbijvullen(productid,hoeveelheid);
        }

        public List<Product> ProductenGenre (Genre Genre)
        {
            return _productEngine.ProductenGenre(Genre);
        }
        public List<Product> ProductenPrijsHoogLaag()
        {
            return _productEngine.ProductenPrijsHoogLaag();
        }
        public List<Product> ProductenPrijsLaagHoog()
        {
            return _productEngine.ProductenPrijsLaagHoog();
        }
    }
}
