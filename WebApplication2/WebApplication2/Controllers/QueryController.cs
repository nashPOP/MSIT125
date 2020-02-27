using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ModelViews;

namespace WebApplication2.Controllers
{
    public class QueryController : Controller
    {
        QueryFastory fas = new QueryFastory();
        DDLFastory ddl = new DDLFastory();

        #region Order Query Web

        // GET: OrderQuery
        /// <summary>
        /// Order Query (訂單查詢) 
        /// Qrder List (訂單資料表) 
        /// </summary>
        /// <name>林承緯</name>
        /// <date>2020/02/05</date>
        /// <returns>IEnumerable<FrogJump.Order></returns>
        public ActionResult OrderQuery()
        {
            QueryModelByView qv = new QueryModelByView()
            {
                Orderlist = fas.getAllOrder()
            };
            
            return View(qv);
        }

        [HttpPost]
        public ActionResult OrderQuery(QOrder qOrder)
        {
            QueryModelByView qv = new QueryModelByView()
            {
                Orderlist = fas.getOrderQuery(qOrder).ToList(),
            };
            return View(qv);
        }

        public ActionResult OrderEdit(int? id)
        {
            Frog_JumpEntities db = new Frog_JumpEntities();
            Order q = db.Order.FirstOrDefault(p => p.OrderID == id);
            var qd = db.Order_Details.Where(p=>p.OrderID==id).Select(p => p);

            QueryModelByView qv = new QueryModelByView()
            {
                Order = q,
                Order_details = qd.ToList()
            };

            if (q != null)
            {
                return View(qv);
            }
            return View();

            //return RedirectToAction("OrderQuery");
        }
        [HttpPost]
        public ActionResult OrderEdit(Order order)
        {
            fas.EditOrder(order);
            return RedirectToAction("OrderEdit", order.OrderID);
        }

        public ActionResult Order_DetailEdit(Order_Details item,int oid,int pid,int qty)
        {
            if(Session["OrderDetailList"]!=null)
            {
                List<Order_Details> details = Session["OrderDetailList"] as List<Order_Details>;
                Order_Details order_Details = new Order_Details() { OrderID = oid, ProductID = pid, Quantity = qty };
                details.Add(order_Details);

                Session["OrderDetailList"] = details;
                return Json("{'message':'0'}", JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Order_Details> details = new List<Order_Details>();
                Order_Details order_Details = new Order_Details() { OrderID = oid, ProductID = pid, Quantity = qty };
                details.Add(order_Details);

                Session["OrderDetailList"] = details;
                return Json("{'message':'0'}", JsonRequestBehavior.AllowGet);
            }
            //string message = fas.EditOrderDetail(oid,pid,qty);
            //if (!string.IsNullOrEmpty(message))
            //{
            //    return Json("{'message':" + message + "}", JsonRequestBehavior.AllowGet);
            //}
            //return Json("{'message':"+message+"}", JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderDelete()
        {
            return RedirectToAction("OrderQuery");
        }

        #endregion

        #region Instock Query Web

        public ActionResult InStockQuery()
        {
            var StockQuery = fas.getAllStockEnter();
            return View(StockQuery);
        }


        public ActionResult StockEnterQuery(int id, int pid, int wid, int mid, int sid, DateTime stockenterdate)
        {
            //StockEnter stock = new StockEnter() { StockEnterID = id, ProductID = pid, WineryID = wid, MilliliterID = mid,
            //    ShelfID = sid, StockEnterDate = stockenterdate.ToShortDateString() };
            //var query=fas.getStockEnter(stock).Select(p=>new
            //{
            //    p.StockEnterID,
            //    p.WineryID, p.Winery.WineryName,
            //    p.ProductID, p.Product.ProductName,
            //    p.MilliliterID, p.m.Milliliter1,
            //    p.ShelfID, p.Shelf.ShelfPosition,
            //    p.Quantity,
            //    p.Note,
            //    p.StockEnterDate
            //});

            return View();
        }

        public ActionResult StockEnterEdit(int id)
        {
            StockEnter StockEdit = fas.getStockEnterByid(id);
            return View(StockEdit);
        }

        [HttpPost]
        public ActionResult StockEnterEdit(StockEnter stock)
        {
            var StockEdit = fas.StockEnterEdit(stock);
            return View(StockEdit);
        }

        public ActionResult StockEnterDelete(int stid)
        {
            var StockDelete = fas.StockEnterDelete(stid);
            return View(StockDelete);
        }
        #endregion

        #region Inventory Query Web

        public ActionResult InventoryQuery()
        {
            IEnumerable<Inventory> Inventory = fas.getInventoryQuery();
            return View(Inventory);
        }

        [HttpPost]
        public ActionResult InventoryQuery(QInventory inventory)
        {
            var Inventory = fas.getInventoryQuery(inventory);
            return View(Inventory);
        }

        public ActionResult InventoryEdit(int id)
        {
            var InventoryEdit = fas.getInventoryByID(id);
            return View(InventoryEdit);
        }

        [HttpPost]
        public ActionResult InventoryEdit(Inventory inventory)
        {
            bool edit =fas.InventoryEdit(inventory);
            return RedirectToAction("InventoryQuery");
        }

        public ActionResult InventoryDelete(int inventoryid)
        {
            string message = fas.InventoryDelete(inventoryid);

            return Json("{'Message':" + message + "}", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DropDownList
        public ActionResult GetWinery()
        {
            var ddlWinery = ddl.getWinery();

            return Json(ddlWinery, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategory()
        {
            var ddlCategory = ddl.getCategory();

            return Json(ddlCategory, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProduct(int? id)
        {
            var ddlProduct = ddl.getProduct(id);

            return Json(ddlProduct, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMillilter()
        {
            var ddlMillilter = ddl.getMillilter();
            return Json(ddlMillilter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetShelf()
        {
            var ddlShelf = ddl.getShelf();
            return Json(ddlShelf, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dialog Data
        public ActionResult Dialog_Order(int id)
        {
            QueryFastory fas = new QueryFastory();
            IQueryable dialog_order = fas.getOrderdialog(id);

            return Json(dialog_order, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dialog_OrderDetail(int id, int? pid = null)
        {
            QueryFastory fas = new QueryFastory();
            IQueryable dialog_orderdetail = fas.getOrderDetail(id,pid);

            return Json(dialog_orderdetail, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
