﻿using System;
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
            /*
            ArrayList k = new ArrayList();
            DataTable dataTable = new DataTable();
            var e = (dbcontext.Order_Details.Where(n => n.Quantity > n.Product.Quantity).Select(p => new { p.Order_Detail_ID, p.ProductID })).ToList();
            dataTable.Columns.Add("Order_Detail_ID");
            dataTable.Columns.Add("ProductID");

            for (int i = 0; i < e.Count(); i++)
            {
                dataTable.Rows.Add(e[i].Order_Detail_ID, e[i].ProductID);
            }
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                //;
                var r = dbcontext.Order_Details.Where(h => h.Product == dataTable.Rows[i]["ProductID"]);

            }*/

            //Todo..Load
            /*
                        //宣告變數，用來儲存所有productID
                        ArrayList Ary_ProductID = new ArrayList();
                        var productID_list = (dbcontext.Product.Select(p => p.ProductID)).ToList();
                        foreach (var item in productID_list)
                        {
                            Ary_ProductID.Add(item);
                        }

                        #region for=> 把所有productID都循一遍
                        //宣告目前庫存變數
                        foreach (int pID in Ary_ProductID)
                        {
                            var stock_Qty = dbcontext.Product.Where(o => o.ProductID == pID).Select(p => p.Quantity);
                            // 宣告用來儲存的變數容器

                            //找出有對應productID的訂單
                            var orderList = dbcontext.Order_Details.Where(o => o.ProductID == pID).Select(p => p.Quantity);

                            //用for迴圈把對應productID的下單數量與庫存變數相減，把可出貨的放入變數容器，並減掉下單的數量
                            #region for迴圈=>把對應productID的下單數量與庫存變數相減，把可出貨的放入變數容器，並減掉下單的數量
                            foreach (var order in orderList)
                            {
                                //blablablabla
                                if(stock_Qty)
                                stock_Qty = (int)stock_Qty - order;
                            }
                            #endregion
                        }





                        #endregion
                        //把變數容器顯示出來
             */
            #endregion

            var q = dbcontext.Order_Details.Where(p => p.Quantity < p.Product.Quantity && p.Order.Status == "C").Select(p => p.Order);


            return View(q);
        }

        public ActionResult order_Detail(string orderID)
        {

            var order_details = dbcontext.Order_Details.Where(p => p.OrderID.ToString() == orderID).Select(p => new { p.OrderID, p.Product.ProductName, p.Quantity, p.Order_Detail_ID });

            return Json(order_details, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ajax_Pick()
        {


            // var orders = dbcontext.Order.OrderBy(p=>p.RequiredDate).Select(p => new { p.OrderID,  p.RequiredDate, p.Status });
            var orders = dbcontext.Order_Details.OrderBy(p => p.Order.RequiredDate).Where(p => p.Quantity < p.Product.Quantity).Select(p => p.Order);

            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ajax_NotAllowPick()
        {
            //var orders = dbcontext.Order_Details.Where(p => p.Quantity > p.Product.Quantity).Select(p => new { p.Order.OrderID, p.Order.RequiredDate, p.Order.Status });
            var orders = dbcontext.Order_Details.Where(p => p.Quantity > p.Product.Quantity).Select(p => new { p.Order.OrderID, p.Order.RequiredDate, p.Order.Status }).Distinct();
            return Json(orders, JsonRequestBehavior.AllowGet);
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
            using (var context = new Frog_JumpEntities())
            {
                var order_details = new Order_Details
                {
                    OrderID = OrderID,
                    Quantity = Quantity,
                    ProductID = ProductID
                };
                context.Order_Details.Add(order_details);
                context.SaveChanges();
            }
            return Content(OrderID.ToString());
        }
    }
}