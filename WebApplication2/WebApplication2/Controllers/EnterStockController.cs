using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ModelView;

namespace WebApplication2.Controllers
{
    public class EnterStockController : Controller
    {
        // GET: EnterStock
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EnterStock()
        {
            return View();
        }

        public ActionResult Temporary()
        {
            //List<WebApplication2..EnterStock> 
            return View();
        }

        public ActionResult CreateBtn(int winery, int category, int item, int ml, int count, int area, string note)
        {
            List<StockEnter> list = new List<StockEnter>();
            if (Session["xxx"] != null)
            {
                list = Session["xxx"] as List<StockEnter>;
            }
            StockEnter stock = new StockEnter()
            {
                WineryID = winery,
                Note = note,
                MilliliterID = ml,
                ProductID = item,
                Quantity = count,
                CategoryID = category,
                ShelfID = area,
            };
            list.Add(stock);
            Session["xxx"] = list;

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DelSession(int Id)
        {
            if (Session["delid"] != null)
            {
                ((List<int>)Session["delid"]).Add(Id);
            }
            else
            {
                List<int> list = new List<int>();
                list.Add(Id);
                Session["delid"] = list;
            }
            return Json("{ 'message':'0'}", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectWinery ()
        {
            SelectClick data = new SelectClick();
            var winery = data.getWinery();
            return Json(winery, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectProduct()
        {
            SelectClick data = new SelectClick();
            var product = data.getProductName();
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectMilliliter()
        {
            SelectClick data = new SelectClick();
            var milliliter = data.getMilliliter();
            return Json(milliliter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectCategory()
        {
            SelectClick data = new SelectClick();
            var category = data.getCategory();
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectShelf()
        {
            SelectClick data = new SelectClick();
            var shelf = data.getShelf();
            return Json(shelf, JsonRequestBehavior.AllowGet);
        }
    }
}