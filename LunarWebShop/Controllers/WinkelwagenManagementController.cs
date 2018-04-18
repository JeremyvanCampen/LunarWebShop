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
        Account account = new Account();

        public ActionResult Add(int KlantID, int KeycodeID)
        {
            account.VoegToeAanWinkelwagen(KlantID, KeycodeID);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Myorder(int id)
        {
            bool status = false;
            string message = "";

            List<Product> producten = account.WinkelwagenProducten(id);
            ViewModelProductKeycodeList VMPK = new ViewModelProductKeycodeList();
            foreach (var item in producten)
            {
                VMPK.Product.Add(item);
                VMPK.TotaalBedrag = VMPK.TotaalBedrag + item.Prijs;
            }

            if (TempData["Message"] != null)
            {
                if (TempData["Message"].ToString().Contains("Onvoldoende Saldo"))
                {
                    ViewBag.Message = TempData["Message"];
                    ViewBag.Status = false;
                    return View(VMPK);
                }
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