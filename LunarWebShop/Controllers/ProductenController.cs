using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LunarWebShop.Models;

namespace LunarWebShop.Controllers
{
    public class ProductenController : Controller
    {
       LunarProduct _database = new LunarProduct();
        // GET: Producten
        public ActionResult Product()
        {
            return View(_database.Product.ToList());
        }

        // GET: Producten/Details/5
        public ActionResult Details(int id)
        {
            Product Product = _database.Product.Find(id);
            Keycode keycode = _database.Keycode.Find(id);
            ViewModelProductKeycode ViewModelProductKeycode = new ViewModelProductKeycode();
            ViewModelProductKeycode.keycode = keycode;
            ViewModelProductKeycode.Product = Product;

            return View(ViewModelProductKeycode);
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
                //foto toevoegen
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
                Keycode keycode = new Keycode();
                keycode.ProductID = product.ProductID;
                _database.Product.Add(product);
                _database.Keycode.Add(keycode);
                _database.SaveChanges();

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
            var ProductToEdit = (from m in _database.Product

                where m.ProductID == id

                select m).First();
            return View(ProductToEdit);
        }

        // POST: Producten/Edit/5
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Product ProductToEdit)
        {
            var OriginalProduct = (from m in _database.Product
                where m.ProductID == ProductToEdit.ProductID
                select m).First();

            if (!ModelState.IsValid)
                return View(OriginalProduct);

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

            _database.Entry(OriginalProduct).CurrentValues.SetValues(ProductToEdit);
            _database.SaveChanges();

            return RedirectToAction("Product");
        }

        // GET: Producten/Delete/5
        public ActionResult Delete(int? id)
        {
            Product Product = _database.Product.Find(id);
            return View(Product);
        }

        // POST: Producten/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Product product = _database.Product.Find(id);
            _database.Keycode.RemoveRange(_database.Keycode.Where(x => x.ProductID == id));
            _database.Product.Remove(product);
            _database.SaveChanges();
            return RedirectToAction("Product");
        }
    }
}
