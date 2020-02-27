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
        DELogisticsEntities dbcontext = new DELogisticsEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult pick()
        {
           
            // var q = from p in dbcontext.Order_Details.Where(p => p.Order.OrderID == p.OrderID).Where(p => p.Quantity < p.Product.StockEnter.Quantity);
            var q = dbcontext.Order.Select(p => p);

            return View(q);
        }

        public ActionResult order_Detail(string orderID)
        {
            
            var order_details = dbcontext.Order_Details.Where(p => p.OrderID.ToString() == orderID).Select(p => new { p.OrderID, p.Product.ProductName, p.Quantity , p.Order_Detail_ID});

            return Json(order_details, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Ajax_Pick()
        {
           

            var orders = dbcontext.Order.Select(p => new { p.OrderID, p.RequiredDate , p.Status});
            
            return Json(orders,JsonRequestBehavior.AllowGet); 
        }
        public ActionResult order_Detail_Fix(int order_detail_id)
        {          

            var orderdetail = dbcontext.Order_Details.Where(p => p.Order_Detail_ID == order_detail_id).Select(p => new {
                p.OrderID,
                p.Product.ProductName,
                p.Order_Detail_ID,
                p.Quantity
            });
            
            return Json(orderdetail, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult order_Detail_Update(int Orderdetail_ID , int Quantity)
        {
            var q = dbcontext.Order_Details.Where(p => p.Order_Detail_ID == Orderdetail_ID).FirstOrDefault();
            q.Quantity = Quantity;
            dbcontext.SaveChanges();    
            return Content(String.Format("Order_detail_ID = {0}, 數量 = {1}", Orderdetail_ID, Quantity));
        }
    }
}