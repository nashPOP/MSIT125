using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ModelView;
using WebApplication2.ModelViews;

namespace WebApplication2.Controllers
{
    public class SelectorCreateController : Controller
    {
        // GET: SelectorCreate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectorCreate()
        {
            SelectClick sC = new SelectClick();
            SelectTable sT = new SelectTable();
            sT.wineryTable = sC.getWinery();
            sT.productTable = sC.getProductName();
            sT.categoryTable = sC.getCategory();
            sT.milliliterTabel = sC.getMilliliter();
            sT.shelfTable = sC.getShelf();
            return View(sT);
        }
    }
}