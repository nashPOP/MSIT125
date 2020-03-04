﻿using System;
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
            try
            {
                QueryModelByView qv = new QueryModelByView()
                {
                    Orderlist = fas.getAllOrder(),
                };

                return View(qv);
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult OrderQuery(QOrder qOrder)
        {
            try
            {
                QueryModelByView qv = new QueryModelByView()
                {
                    Orderlist = fas.getOrderQuery(qOrder).ToList(),
                };
                return View(qv);
            }
            catch (Exception ex)
            {
                return View();
            }
            
        }

        public ActionResult OrderEdit(int? id)
        {
            try
            {
                if (id != null)
                {
                    Order q = fas.getOrderByID((int)id);
                    var qd = fas.getOrder_DetailsByID((int)id).ToList();

                    QueryModelByView model = new QueryModelByView()
                    {
                        Order = q,
                        Order_details = qd
                    };
                    return View(model);
                }
                else
                {
                    return RedirectToAction("OrderQuery");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("OrderQuery");
            }
        }

        [HttpPost]
        public ActionResult OrderEdit(Order order)
        {
            try
            {
                fas.EditOrder(order);
            }
            catch(Exception ex)
            {

            }
            return RedirectToAction("OrderEdit", order.OrderID);
        }

        public ActionResult OrderDelete(int id)
        {
            try
            {
                string message = fas.OrdersDelete(id);
                if (message != "0")
                {
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("刪除成功", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Order_DetailInsert(int? oid, int? pid, int? qty)
        {
            try
            {
                Order_Details od = new Order_Details()
                {
                    OrderID = (int)oid,
                    ProductID = (int)pid,
                    Quantity = (int)qty
                };

                string message = fas.Order_DetailInsert(od);
                if (message != "0")
                {
                    return Json("新增成功", JsonRequestBehavior.AllowGet);
                }
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Order_DetailEdit(string item, int? oid, int? pid, int? qty)
        {
            try
            {
                string[] od1 = item.Split(',');
                int.TryParse(od1[1], out int pdid);
                int.TryParse(od1[2], out int q);
                Order_Details od = new Order_Details()
                {
                    OrderID = (int)oid,
                    ProductID = pdid,
                    Quantity = q
                };
                Order_Details New_od = new Order_Details()
                {
                    OrderID = (int)oid,
                    ProductID = (int)pid,
                    Quantity = (int)qty
                };
                string message = fas.EditOrderDetail(od, New_od);
                if (message == "0")
                {
                    return Json("編輯成功", JsonRequestBehavior.AllowGet);
                }
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult OrderDetailDelete(string item)
        {
            string[] OD_item = item.Split(',');
            int.TryParse(OD_item[0], out int orderid);
            int.TryParse(OD_item[1], out int productid);
            int.TryParse(OD_item[2], out int qty);
            Order_Details od = new Order_Details() { OrderID = orderid, ProductID = productid, Quantity = qty };

            string message = fas.OrderDetailDelete(od);
            if (message != "0")
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("OrderEdit", new { id = orderid });
            }
        }

        #endregion

        #region Instock Query Web

        public ActionResult InStockQuery()
        {
            var StockQuery = fas.getAllStockEnter();
            return View(StockQuery);
        }

        [HttpPost]
        public ActionResult InStockQuery(QStockEnter qStockEnter)
        {
            var StockQuery = fas.getStockEnter(qStockEnter);
            return View(StockQuery);
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
            string StockDelete = fas.StockEnterDelete(stid);
            return Json(StockDelete,JsonRequestBehavior.AllowGet);
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
            var Inventory = fas.getInventoryQuery(inventory).ToList();
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
            bool edit = fas.InventoryEdit(inventory);
            return RedirectToAction("InventoryQuery");
        }

        public ActionResult InventoryDelete(int inventoryid)
        {
            string message = fas.InventoryDelete(inventoryid);

            return Json(message, JsonRequestBehavior.AllowGet);
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
            IQueryable dialog_orderdetail = fas.getOrderDetail(id, pid);

            return Json(dialog_orderdetail, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
