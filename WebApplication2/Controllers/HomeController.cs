using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult pick()
        {
            DELogisticsEntities dbcontext = new DELogisticsEntities();
            // var q = from p in dbcontext.Order_Details.Where(p => p.Order.OrderID == p.OrderID).Where(p => p.Quantity < p.Product.StockEnter.Quantity);
            var q = dbcontext.Order.Select(p => p);

            return View(q);
        }

        public ActionResult order_Detail(string orderID)
        {
            DELogisticsEntities dbcontext = new DELogisticsEntities();
            var order_details = dbcontext.Order_Details.Where(p => p.OrderID.ToString() == orderID).Select(p => new { p.OrderID, p.Product.ProductName, p.Quantity , p.Order_Detail_ID});

            return Json(order_details, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Ajax_Pick()
        {
            DELogisticsEntities dbcontext = new DELogisticsEntities();

            var orders = dbcontext.Order.Select(p => new { p.OrderID, p.RequiredDate , p.Status});
            
            return Json(orders,JsonRequestBehavior.AllowGet); 
        }
    }
}