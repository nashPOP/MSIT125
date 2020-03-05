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
        DDLFastory ddl = new DDLFastory();
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
                if (status == "Winery")
                {
                    var w = sC.warning((int)id, status);
                    return Json(w, JsonRequestBehavior.AllowGet);
                }
                else if (status == "Category")
                {
                    var c = sC.warning((int)id, status);
                    return Json(c, JsonRequestBehavior.AllowGet);
                }
                else if (status == "Product")
                {
                    var p = sC.warning((int)id, status);
                    return Json(p, JsonRequestBehavior.AllowGet);
                }
                else if (status == "Milliliter")
                {
                    var m = sC.warning((int)id, status);
                    return Json(m, JsonRequestBehavior.AllowGet);
                }
                else if (status == "Shelf")
                {
                    var s = sC.warning((int)id, status);
                    return Json(s, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("錯誤default", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("錯誤", JsonRequestBehavior.AllowGet);
            } 
        }

        public ActionResult Edit(string kk,string status)
        {
            string[] k = kk.Split(',');
            string s = sC.Edit(k,status);
            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategory()
        {
            var ddlCategory = ddl.getCategory();

            return Json(ddlCategory, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWinery()
        {
            var ddlWinery = ddl.getWinery();

            return Json(ddlWinery, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Insert(string kk, string status)
        {
            string[] k = kk.Split(',');
            string s = sC.Insert(k, status);
            return Json(s, JsonRequestBehavior.AllowGet);
        }
    }
}