using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using LunarWebShop.Models;
using LunarWebShop.Models.AccountManagement;
using Microsoft.Ajax.Utilities;

namespace LunarWebShop.Controllers
{
    public class KlantController : Controller
    {
        LunarProduct _database = new LunarProduct();
        //Registratie pagina
        [HttpGet]
        public ActionResult RegistratieKlant()
        {
            return View();
        }

        //Registratie invoeren
        [HttpPost]
        public ActionResult RegistratieKlant([Bind()]Klant user)
        {
            string message = "";
            bool Status = false;

            Account account = new Account();
            message = account.KlantToevoegen(user);

            if (message == "Succesvol")
            {
                message = " Account succesvol aangemaakt U kunt nu inloggen.";
                ViewBag.Message = message;
                ViewBag.Status = true;
                return View(user);
            }
            ViewBag.Status = false;
            ViewBag.Message = message;
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Gebruikersnaam"] != null)
            {
                return RedirectToAction("Index", "Home", new {Gebruikersnaam = Session["Gebruikersnaam"].ToString()});
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Inloggen login)
        {
            string message = "";
            bool Status = false;

            Account account = new Account();
            var gebruiker = account.Inloggen(login.Gebruikersnaam, login.Wachtwoord);

            if (gebruiker is Klant)
            {

                //Login Sessie aanmaken
                Session["Klant"] = gebruiker;
                return RedirectToAction("Index", "Home");
            }
            if (gebruiker is Administrator)
            {
                message = " Succesvol ingelogd.";
                ViewBag.Message = message;
                ViewBag.Status = true;

                //Login Sessie aanmaken
                Session["Administrator"] = gebruiker;
                return RedirectToAction("Index", "Home");
            }

            message = gebruiker.ToString();
            ViewBag.Status = false;
            ViewBag.Message = message;
            return View();
        }

        public ActionResult Uitloggen()
        {
            Session.Clear();
            return RedirectToAction("Login", "Klant");
        }

        public ActionResult MijnAccount()
        {
            return View();
        }

        //// GET: Winkelwagen
        //public ActionResult Winkelwagen()
        //{

        //    return View();

        //}

        //// GET: Producten/Details/5
        //public ActionResult Details(int id)
        //{
        //    Product Product = _database.Product.Find(id);
        //    Keycode keycode = _database.Keycode.Find(id);
        //    ViewModelProductKeycode ViewModelProductKeycode = new ViewModelProductKeycode();
        //    ViewModelProductKeycode.keycode = keycode;
        //    ViewModelProductKeycode.Product = Product;

        //    return View(ViewModelProductKeycode);
        //}

        //// GET: Producten/Create
        //public ActionResult Create(HttpPostedFileBase image)
        //{
        //    return View();
        //}

        //// POST: Producten/Create
        //[HttpPost]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Create([Bind(Exclude = "Id")] Product product)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        Keycode keycode = new Keycode();
        //        keycode.ProductID = product.ProductID;
        //        _database.Product.Add(product);
        //        _database.Keycode.Add(keycode);
        //        _database.SaveChanges();
        //        return RedirectToAction("Product");
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.ToString();
        //        return View();
        //    }
        //}

        //// GET: Producten/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var ProductToEdit = (from m in _database.Product

        //                         where m.ProductID == id

        //                         select m).First();
        //    return View(ProductToEdit);
        //}

        //// POST: Producten/Edit/5
        //[HttpPost]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Edit(Product ProductToEdit)
        //{
        //    var OriginalProduct = (from m in _database.Product
        //                           where m.ProductID == ProductToEdit.ProductID
        //                           select m).First();

        //    if (!ModelState.IsValid)
        //        return View(OriginalProduct);

        //    _database.Entry(OriginalProduct).CurrentValues.SetValues(ProductToEdit);
        //    _database.SaveChanges();

        //    return RedirectToAction("Product");
        //}

        //// GET: Producten/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    Product Product = _database.Product.Find(id);
        //    return View(Product);
        //}

        //// POST: Producten/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    Product product = _database.Product.Find(id);
        //    _database.Keycode.RemoveRange(_database.Keycode.Where(x => x.ProductID == id));
        //    _database.Product.Remove(product);
        //    _database.SaveChanges();
        //    return RedirectToAction("Product");
        //}

    }
}