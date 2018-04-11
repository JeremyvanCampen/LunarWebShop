﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LunarWebShop.Models;
using LunarWebShop.Models.AccountManagement;

namespace LunarWebShop.Controllers
{
    public class ProductenController : Controller
    {
        Account account = new Account();
        // GET: Producten
        public ActionResult Product()
        {
            return View(account.AlleProducten());
        }

        // GET: Producten/Details/5
        public ActionResult Details(int id)
        {
            var product = account.ProductOphalen(id);
            var keycode = account.KeycodeOphalen(id);
            ViewModelProductKeycode ViewModelProductKeycode = new ViewModelProductKeycode();
            ViewModelProductKeycode.keycode = keycode as Keycode;
            ViewModelProductKeycode.Product = product as Product;

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
                account.CreateProduct(product);

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
            var ProductToEdit = account.ProductOphalen(id);
            return View(ProductToEdit);
        }

        // POST: Producten/Edit/5
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Product ProductToEdit)
        {
            var OriginalProduct = account.ProductOphalen(ProductToEdit.ProductID);

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

            account.ProductAanpassen(ProductToEdit);

            return RedirectToAction("Product");
        }

        // GET: Producten/Delete/5
        public ActionResult Delete(int? id)
        {
            return View(account.ProductOphalen(Convert.ToInt32(id)));
        }

        // POST: Producten/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            account.DeleteProduct(id);
            return RedirectToAction("Product");
        }
    }
}
