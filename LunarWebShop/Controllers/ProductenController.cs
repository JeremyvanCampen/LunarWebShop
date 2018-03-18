using System;
using System.Collections.Generic;
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
            return View(Product);
        }

        // GET: Producten/Create
        public ActionResult Create( HttpPostedFileBase image)
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
                // TODO: Add insert logic here
                _database.Product.Add(product);
                _database.SaveChanges();
                return RedirectToAction("Product");
            }
            catch
            {
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
            _database.Product.Remove(product);
            _database.SaveChanges();
            return RedirectToAction("Product");
        }
    }
}
