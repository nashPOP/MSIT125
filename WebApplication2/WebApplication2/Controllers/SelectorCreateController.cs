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

        public ActionResult SelectorCreate(int table = 0)
        {
            SelectClick sC = new SelectClick();
            SelectTable sT = new SelectTable();
            sT.wineryTable = sC.getWinery();
            sT.productTable = sC.getProductName();
            sT.categoryTable = sC.getCategory();
            sT.milliliterTabel = sC.getMilliliter();
            sT.shelfTable = sC.getShelf();

            if (table == 1)
            {
                sT.status = "Winery";
            }
            else if (table == 2)
            {
                sT.status = "Category";
            }
            else if (table == 3)
            {
                sT.status = "Product";
            }
            else if (table == 4)
            {
                sT.status = "Milliliter";
            }
            else if (table == 5) 
            {
                sT.status = "Shelf";
            }
            else
            {
                sT.status = null;
            }

            return View(sT);
        }

        public ActionResult change(int value)
        {
            return Json(Url.Action("SelectorCreate",new { table= value }));
        }
    }
}