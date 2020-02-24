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
                Orderlist = fas.getAllOrder(),
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
            FrogJumpEntities db = new FrogJumpEntities();
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

        public ActionResult Order_DetailEdit(int oid,int pid,int qty)
        {
            string message = fas.EditOrderDetail(oid,pid,qty);
            if (!string.IsNullOrEmpty(message))
            {
                return Json("{'message':" + message + "}", JsonRequestBehavior.AllowGet);
            }
            return Json("{'message':"+message+"}", JsonRequestBehavior.AllowGet);
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

        //[HttpPost]
        //public ActionResult InStockQuery(FJInStock fjIS)
        //{
        //    FrogJumpEntities db = new FrogJumpEntities();
        //    var Query = Model.Select(p => p);
        //    if (!string.IsNullOrEmpty(fjIS.id))
        //    {
        //        Query.Where(ID => ID.id == fjIS.id);
        //    }

        //    return View(Query);
        //}
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
