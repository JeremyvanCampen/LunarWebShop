using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using Logic;
using Models;

namespace LunarWebShop.Controllers
{
    public class WinkelwagenManagementController : Controller
    {
        private GebruikerLogic GebruikerLogic = new GebruikerLogic();

        public ActionResult Add(int KlantID, int ProductID)
        {
            GebruikerLogic.VoegToeAanWinkelwagen(KlantID, ProductID);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Myorder(int id)
        {

            List<Product> producten = GebruikerLogic.WinkelwagenProducten(id);
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
        public ActionResult Remove(int KeycodeID,int productid, int id)
        {
            GebruikerLogic.VerwijderUitWinkelwagen(KeycodeID, productid, id);
            return RedirectToAction("Myorder", new{id = id});
        }

      
    }


}