using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ModelView;
using WebApplication2.ModelViews;

namespace WebApplication2.Controllers
{
    public class SelectorCreateController : Controller
    {
        SelectClick sC = new SelectClick();
        // GET: SelectorCreate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectorCreate(int table = 0)
        {
            
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

        public ActionResult Delete(int id, string status)
        {
            string result = sC.delete(id, status);
            if (result == "0")
            {
                return Json("刪除成功", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Warning(int? id, string status)
        {
            if (id != null)
            {
                switch (status)
                {
                    case "Winery":
                        var w = sC.warning((int)id, status);
                        return Json((Winery)w, JsonRequestBehavior.AllowGet);
                    case "Category":
                        var c = sC.warning((int)id, status);
                        return Json((Category)c, JsonRequestBehavior.AllowGet);
                    case "Product":
                        var p = sC.warning((int)id, status);
                        return Json((Product)p, JsonRequestBehavior.AllowGet);
                    case "Milliliter":
                        var m = sC.warning((int)id, status);
                        return Json((Milliliter)m, JsonRequestBehavior.AllowGet);
                    case "Shelf":
                        var s = sC.warning((int)id, status);
                        return Json((Shelf)s, JsonRequestBehavior.AllowGet);
                    default:
                        return Json("錯誤", JsonRequestBehavior.AllowGet);
                }

            }
            return Json("錯誤", JsonRequestBehavior.AllowGet);
        }

    }
}