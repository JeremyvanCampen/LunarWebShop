using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logic;
using Models;

namespace LunarWebShop.Controllers
{
    public class ProductenController : Controller
    {
        private ProductLogic ProductLogic = new ProductLogic();
        // GET: Producten
        public ActionResult Product()
        {
            return View(ProductLogic.AlleProducten());
        }

        // GET: Producten/Details/5
        public ActionResult Details(int id)
        {
            var product = ProductLogic.ProductOphalen(id);
            var keycode = ProductLogic.KeycodeOphalen(id);
            ViewModelProductKeycode ViewModelProductKeycode = new ViewModelProductKeycode();
            ViewModelProductKeycode.Product = product as Product;
            foreach (var item in keycode)
            {
                ViewModelProductKeycode.Product.Keycode.Add(item);
            }

            foreach (var item in ViewModelProductKeycode.Product.Keycode)
            {
                ViewModelProductKeycode.Product.Hoeveelheid = ViewModelProductKeycode.Product.Hoeveelheid + 1;
            }

            return View(ViewModelProductKeycode);
        }
        [HttpGet]
        //post : Asyncdetails
        public ActionResult AsyncDetails(int id)
        {
            var product = ProductLogic.ProductOphalen(id);
            var keycode = ProductLogic.KeycodeOphalen(id);
            ViewModelProductKeycode ViewModelProductKeycode = new ViewModelProductKeycode();
            ViewModelProductKeycode.Product = product as Product;
            foreach (var item in keycode)
            {
                ViewModelProductKeycode.Product.Keycode.Add(item);
            }

            foreach (var item in ViewModelProductKeycode.Product.Keycode)
            {
                ViewModelProductKeycode.Product.Hoeveelheid = ViewModelProductKeycode.Product.Hoeveelheid + 1;
            }

            return PartialView("Details", ViewModelProductKeycode);
        }
        [HttpGet]
        // GET: Producten/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producten/Create
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] Product product)
        {
            try
            {
                //foto opslaan in de map Images en de URL daarvan opslaan in de database zodat deze kan worden uitgelezen
                string filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extention = Path.GetExtension(product.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
                product.Foto = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                product.ImageFile.SaveAs(filename);

                string filename2 = Path.GetFileNameWithoutExtension(product.ImageFile2.FileName);
                string extention2 = Path.GetExtension(product.ImageFile2.FileName);
                filename2 = filename2 + DateTime.Now.ToString("yymmssfff") + extention2;
                product.AchtergrondFoto = "~/Images/" + filename2;
                filename2 = Path.Combine(Server.MapPath("~/Images/"), filename2);
                product.ImageFile2.SaveAs(filename2);

                //Keycode en product toevoegen aan database
                ProductLogic.CreateProduct(product, product.Hoeveelheid);

                return RedirectToAction("Product");
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
                return View();
            }
        }

        // GET: Producten/Edit/5
        public ActionResult Edit(int id)
        {
            var ProductToEdit = ProductLogic.ProductOphalen(id);
            return View(ProductToEdit);
        }

        // POST: Producten/Edit/5
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Product ProductToEdit)
        {
            var OriginalProduct = ProductLogic.ProductOphalen(ProductToEdit.ProductID);

            if (!ModelState.IsValid)
                return View(OriginalProduct);

            //foto opslaan in de map Images en de URL daarvan opslaan in de database zodat deze kan worden uitgelezen
            string filename = Path.GetFileNameWithoutExtension(ProductToEdit.ImageFile.FileName);
            string extention = Path.GetExtension(ProductToEdit.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extention;
            ProductToEdit.Foto = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            ProductToEdit.ImageFile.SaveAs(filename);


            string filename2 = Path.GetFileNameWithoutExtension(ProductToEdit.ImageFile2.FileName);
            string extention2 = Path.GetExtension(ProductToEdit.ImageFile2.FileName);
            filename2 = filename2 + DateTime.Now.ToString("yymmssfff") + extention2;
            ProductToEdit.AchtergrondFoto = "~/Images/" + filename2;
            filename2 = Path.Combine(Server.MapPath("~/Images/"), filename2);
            ProductToEdit.ImageFile2.SaveAs(filename2);

            ProductLogic.ProductAanpassen(ProductToEdit);

            return RedirectToAction("Product");
        }

        // GET: Producten/Delete/5
        public ActionResult Delete(int? id)
        {
            return View(ProductLogic.ProductOphalen(Convert.ToInt32(id)));
        }

        // POST: Producten/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ProductLogic.DeleteProduct(id);
            return RedirectToAction("Product");
        }
        [HttpGet]
        public ActionResult VoorraadBijvullen(int productid)
        {
            return View(ProductLogic.ProductOphalen(productid));
        }
        [HttpPost]
        public ActionResult VoorraadBijvullen([Bind()] Product product)
        {
            ProductLogic.Voorraadbijvullen(product.ProductID, product.Hoeveelheid);
            return RedirectToAction("Product", "Producten");
        }

    }
}
