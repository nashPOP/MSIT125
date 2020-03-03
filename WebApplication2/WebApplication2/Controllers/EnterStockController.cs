using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ModelView;

namespace WebApplication2.Controllers
{
    public class EnterStockController : Controller
    {
        DDLFastory db = new DDLFastory();
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

        public ActionResult CreateBtn(int winery, int category, int product, int milliliter, int count, int shelf, string note)
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
                MilliliterID = milliliter,
                ProductID = product,
                Quantity = count,
                CategoryID = category,
                ShelfID = shelf,
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
            var winery = db.getWinery();
            return Json(winery, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectProduct(int category)
        {
            var product = db.getProduct(category);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectMilliliter()
        {
            var milliliter = db.getMillilter();
            return Json(milliliter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectCategory()
        {
            var category = db.getCategory();
            return Json(category, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectShelf()
        {
            var shelf = db.getShelf();
            return Json(shelf, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertStock()
        {
            List<StockEnter> x = Session["xxx"] as List<StockEnter>;
            if (Session["delid"] != null)
            {
                List<int> i = Session["delid"] as List<int>;
                i.Sort();
                i.Reverse();

                foreach (int d in i)
                {
                    x.RemoveAt(d - 1);
                }
            }
            EnterStockFactory enterStockFactory = new EnterStockFactory();
            string message = enterStockFactory.Insert(x);
            if(message!="0")
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session.Remove("xxx");
                Session.Remove("delid");
                return Json("0",JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}