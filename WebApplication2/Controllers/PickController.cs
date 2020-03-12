using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PickController : Controller
    {
        Frog_JumpEntities dbcontext = new Frog_JumpEntities();

        //Load
        public ActionResult pick()
        {
            #region 庫存判斷            

            //DateTime RequiredDate_Send = new DateTime();
            List<Order> q = new List<Order>();
            List<int> OrderID_Send = new List<int>();
            List<DateTime> RequiredDate_Send = new List<DateTime>();
            List<string> status_Send = new List<string>();
            // List<DateTime> RequiredDate_Send = new List<DateTime>();
            var list_Order_DetailID_More = dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity && n.Order.Status != "Z").Select(n => n.Order.OrderID).Distinct().ToArray();
            var list_ProductID = dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity && n.Order.Status != "Z").Select(n => n.Order.OrderID).ToArray();
            var list_ProductName = dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity && n.Order.Status != "Z").Select(n => n.Order.OrderID).ToArray();

            var list_all_Order_ID = dbcontext.Order_Details.Where(p => p.Order.Status != "Z").Select(p => p.OrderID).Distinct().ToArray();
            int[] temp = new int[list_all_Order_ID.Length - list_Order_DetailID_More.Length];
            foreach (var numberToRemove in list_Order_DetailID_More)
            {
                list_all_Order_ID = list_all_Order_ID.Where(val => val != numberToRemove).ToArray();
            }

            #endregion

            for (int i = 0; i < list_all_Order_ID.Length; i++)
            {
                int Sel = list_all_Order_ID[i];
                var OrderID_Send_tmp = dbcontext.Order.Where(p => p.OrderID == Sel).OrderBy(p => p.RequiredDate).Select(p => p.OrderID).ToList();
                var RequiredDate_Send_tmp = (dbcontext.Order.Where(p => p.OrderID == Sel).OrderBy(p=>p.RequiredDate).Select(p => p.RequiredDate).ToList());
                var status_Send_tmp = (dbcontext.Order.Where(p => p.OrderID == Sel).OrderBy(p => p.RequiredDate).Select(p => p.Status).ToList());

                OrderID_Send.Add(OrderID_Send_tmp[0]);
                RequiredDate_Send.Add(RequiredDate_Send_tmp[0]);
                status_Send.Add(status_Send_tmp[0]);
            }

            for (int i = 0; i < OrderID_Send.Count(); i++)
            {
                q.Add(new Order { OrderID = OrderID_Send[i], RequiredDate = Convert.ToDateTime(RequiredDate_Send[i]), Status = Convert.ToString(status_Send[i]) });
            }


            return View(q.OrderBy(p=>p.RequiredDate));
        }

        public ActionResult order_Detail(string orderID)
        {

            var order_details = dbcontext.Order_Details.Where(p => p.OrderID.ToString() == orderID).Select(p => new { p.OrderID,p.ProductID, p.Product.ProductName, p.Quantity, p.Order_Detail_ID, p.Product.Shelf.ShelfPosition });

            return Json(order_details, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ajax_Pick()
        {
            // var orders = dbcontext.Order.OrderBy(p=>p.RequiredDate).Select(p => new { p.OrderID,  p.RequiredDate, p.Status });
            //var q = dbcontext.Order_Details.Where(p => p.Quantity < p.Product.Quantity && p.Order.Status == "C").Select(p => new { p.Order.OrderID, p.Order.RequiredDate, p.Order.Status });



            #region 庫存判斷            


            //DateTime RequiredDate_Send = new DateTime();
            List<Order> q = new List<Order>();
            List<int> OrderID_Send = new List<int>();
            List<DateTime> RequiredDate_Send = new List<DateTime>();
            List<string> status_Send = new List<string>();
            // List<DateTime> RequiredDate_Send = new List<DateTime>();
            var list_Order_DetailID_More = dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity && n.Order.Status != "Z").Select(n => n.Order.OrderID).Distinct().ToArray();
            var list_ProductID = dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity && n.Order.Status != "Z").Select(n => n.Order.OrderID).ToArray();
            var list_ProductName = dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity && n.Order.Status != "Z").Select(n => n.Order.OrderID).ToArray();

            var list_all_Order_ID = dbcontext.Order_Details.Where(p => p.Order.Status != "Z").Select(p => p.OrderID).Distinct().ToArray();
            int[] temp = new int[list_all_Order_ID.Length - list_Order_DetailID_More.Length];
            foreach (var numberToRemove in list_Order_DetailID_More)
            {
                list_all_Order_ID = list_all_Order_ID.Where(val => val != numberToRemove).ToArray();
            }

            #endregion

            for (int i = 0; i < list_all_Order_ID.Length; i++)
            {
                int Sel = list_all_Order_ID[i];
                var OrderID_Send_tmp = dbcontext.Order.Where(p => p.OrderID == Sel).Select(p => p.OrderID).ToList();
                var RequiredDate_Send_tmp = (dbcontext.Order.Where(p => p.OrderID == Sel).Select(p => p.RequiredDate).ToList());
                var status_Send_tmp = (dbcontext.Order.Where(p => p.OrderID == Sel).Select(p => p.Status).ToList());

                OrderID_Send.Add(OrderID_Send_tmp[0]);
                RequiredDate_Send.Add(RequiredDate_Send_tmp[0]);
                status_Send.Add(status_Send_tmp[0]);
            }

            for (int i = 0; i < OrderID_Send.Count(); i++)
            {
                q.Add(new Order { OrderID = OrderID_Send[i], RequiredDate = Convert.ToDateTime(RequiredDate_Send[i]), Status = Convert.ToString(status_Send[i]) });
            }

            return Json(q, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ajax_NotAllowPick()
        {
            //var orders = dbcontext.Order_Details.Where(p => p.Quantity > p.Product.Quantity).Select(p => new { p.Order.OrderID, p.Order.RequiredDate, p.Order.Status });
            //var orders = dbcontext.Order_Details.Where(p => p.Quantity > p.Product.Quantity).Select(p => new { p.Order.OrderID, p.Order.RequiredDate, p.Order.Status }).Distinct();
            #region 庫存判斷            


            //DateTime RequiredDate_Send = new DateTime();
            List<Order> q = new List<Order>();
            List<int> OrderID_Send = new List<int>();
            List<DateTime> RequiredDate_Send = new List<DateTime>();
            List<string> status_Send = new List<string>();
            // List<DateTime> RequiredDate_Send = new List<DateTime>();
            var list_OrderDetail_OrderID_More = dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity && n.Order.Status != "Z").Select(n => n.Order.OrderID).Distinct().ToArray();
            var list_ProductID = dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity && n.Order.Status != "Z").Select(n => n.Order.OrderID).ToArray();
            var list_ProductName = dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity && n.Order.Status != "Z").Select(n => n.Order.OrderID).ToArray();


            for (int i = 0; i < list_OrderDetail_OrderID_More.Length; i++)
            {
                int Sel = list_OrderDetail_OrderID_More[i];
                var OrderID_Send_tmp = dbcontext.Order.Where(p => p.OrderID == Sel).Select(p => p.OrderID).ToList();
                var RequiredDate_Send_tmp = (dbcontext.Order.Where(p => p.OrderID == Sel).Select(p => p.RequiredDate).ToList());
                var status_Send_tmp = (dbcontext.Order.Where(p => p.OrderID == Sel).Select(p => p.Status).ToList());

                OrderID_Send.Add(OrderID_Send_tmp[0]);
                RequiredDate_Send.Add(RequiredDate_Send_tmp[0]);
                status_Send.Add(status_Send_tmp[0]);
            }
            #endregion
            for (int i = 0; i < OrderID_Send.Count(); i++)
            {
                q.Add(new Order { OrderID = OrderID_Send[i], RequiredDate = Convert.ToDateTime(RequiredDate_Send[i]), Status = Convert.ToString(status_Send[i]) });
            }


            return Json(q, JsonRequestBehavior.AllowGet);
        }



        public ActionResult order_Detail_Fix(int order_detail_id)
        {

            var orderdetail = dbcontext.Order_Details.Where(p => p.Order_Detail_ID == order_detail_id).Select(p => new
            {
                p.OrderID,
                p.Product.ProductName,
                p.Order_Detail_ID,
                p.Quantity
            });

            return Json(orderdetail, JsonRequestBehavior.AllowGet);
        }


        public ActionResult order_Detail_Update(int Orderdetail_ID, int Quantity)
        {
            var q = dbcontext.Order_Details.Where(p => p.Order_Detail_ID == Orderdetail_ID).FirstOrDefault();
            q.Quantity = Quantity;
            dbcontext.SaveChanges();
            return Content(String.Format("Order_detail_ID = {0}, 數量 = {1}", Orderdetail_ID, Quantity));
        }

        public ActionResult order_Detail_option_Add()
        {
            var product_List = dbcontext.Product.Select(p => new { p.ProductID, p.ProductName });
            return Json(product_List, JsonRequestBehavior.AllowGet);
        }

        public ActionResult order_Detail_Insert(int OrderID, int Quantity, int ProductID)
        {
            
            
                var order_details = new Order_Details
                {
                    OrderID = OrderID,
                    Quantity = Quantity,
                    ProductID = ProductID
                };
                dbcontext.Order_Details.Add(order_details);
                dbcontext.SaveChanges();
            
            return Content(OrderID.ToString());
        }

        public ActionResult pick_Update(int orderID, DateTime RequiredDate)
        {
            var Ship = new Ship
            {
                OrderID = orderID,
                RequiredDate = RequiredDate,
                ShipDate = DateTime.Now
            };
            dbcontext.Ship.Add(Ship);
            var Order_Status = dbcontext.Order.Where(p => p.OrderID == orderID).FirstOrDefault();
            Order_Status.Status = "Z";
            dbcontext.SaveChanges();
            
            

            return View();
        }
    }
}