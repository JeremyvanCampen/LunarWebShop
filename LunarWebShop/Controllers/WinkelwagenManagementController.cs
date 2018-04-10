using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using LunarWebShop.Models;
using LunarWebShop.Models.AccountManagement;

namespace LunarWebShop.Controllers
{
    public class WinkelwagenManagementController : Controller
    {
        LunarProduct _database = new LunarProduct();
        Account account = new Account();

        public ActionResult Add(int KlantID, int KeycodeID)
        {
            account.VoegToeAanWinkelwagen(KlantID, KeycodeID);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Myorder(int id)
        {
            List<Product> producten = account.WinkelwagenProducten(id);
            List<Product> productenmetkeycode = new List<Product>();
            ViewModelProductKeycodeList VMPK = new ViewModelProductKeycodeList();
            foreach (var item in producten)
            {
                List<Product> Product =
                    _database.Product.Where(product => product.ProductID == item.ProductID).ToList();
               VMPK.Product.Add(Product[0]);
               VMPK.TotaalBedrag = VMPK.TotaalBedrag + item.Prijs;
            }

            return View(VMPK);
        }
        public ActionResult Remove(int KeycodeID, int id)
        {
            account.VerwijderUitWinkelwagen(KeycodeID);
            return RedirectToAction("Myorder", new{id = id});
        }
    }
}