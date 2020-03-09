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
            try
            {
                if (Session["IdentityCode"] != null)
                {
                    if ((string)Session["IdentityCode"] == "A")
                    {
                        int.TryParse((string)Session["WineryID"],out int wineryid);
                        QueryModelByView qv = new QueryModelByView()
                        {
                            Orderlist = fas.getOrderByWineryID(wineryid)
                        };

                        return View(qv);
                    }
                    else if((string)Session["IdentityCode"] == "B")
                    {
                        QueryModelByView qv = new QueryModelByView()
                        {
                            Orderlist = fas.getAllOrder(),
                        };

                        return View(qv);
                    }
                    else
                    {
                        return RedirectToAction("LoginPage", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            catch(Exception ex)
            {
                string ErrorMessage = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult OrderQuery(QOrder qOrder)
        {
            try
            {
                if (Session["IdentityCode"] != null)
                {
                    if ((string)Session["IdentityCode"] == "A")
                    {
                        qOrder.DDL_Winery = (string)Session["WineryID"];
                        QueryModelByView qv = new QueryModelByView()
                        {
                            Orderlist = fas.getOrderQuery(qOrder).ToList(),
                        };
                        return View(qv);
                    }
                    else if ((string)Session["IdentityCode"] == "B")
                    {
                        QueryModelByView qv = new QueryModelByView()
                        {
                            Orderlist = fas.getOrderQuery(qOrder).ToList(),
                        };

                        return View(qv);
                    }
                    else
                    {
                        return RedirectToAction("LoginPage", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;
                return View();
            }
            
        }

        public ActionResult OrderEdit(int? id)
        {
            try
            {
                if (Session["IdentityCode"] != null)
                {
                    if ((string)Session["IdentityCode"] == "B")
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
                    else
                    {
                        return RedirectToAction("OrderQuery");
                    }
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            catch
            {
                return RedirectToAction("OrderQuery");
            }
        }

        [HttpPost]
        public ActionResult OrderEdit(Order order)
        {
            try
            {
                if (Session["IdentityCode"] != null)
                {
                    if ((string)Session["IdentityCode"] == "B")
                    {
                        fas.EditOrder(order);
                    }
                    else
                    {
                        return RedirectToAction("OrderQuery");
                    }
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            catch
            {
               
            }
            return RedirectToAction("OrderEdit", order.OrderID);
        }

        public ActionResult OrderDelete(int id)
        {
            try
            {
                if (Session["IdentityCode"] != null)
                {
                    if ((string)Session["IdentityCode"] == "B")
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
                    else
                    {
                        return RedirectToAction("OrderQuery");
                    }
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
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
                if (Session["IdentityCode"] != null)
                {
                    if ((string)Session["IdentityCode"] == "B")
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
                    else
                    {
                        return RedirectToAction("OrderQuery");
                    }
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
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
                if (Session["IdentityCode"] != null)
                {
                    if ((string)Session["IdentityCode"] == "B")
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
                    else
                    {
                        return RedirectToAction("OrderQuery");
                    }
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult OrderDetailDelete(string item)
        {
            try
            {
                if (Session["IdentityCode"] != null)
                {
                    if ((string)Session["IdentityCode"] == "B")
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
                            return Json("刪除成功", JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return RedirectToAction("OrderQuery");
                    }
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Instock Query Web

        public ActionResult InStockQuery()
        {

            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "A")
                {
                    int.TryParse((string)Session["WineryID"], out int wineryid);
                    var StockQuery = fas.getAllStockEnter(wineryid);
                    return View(StockQuery);
                }
                else if ((string)Session["IdentityCode"] == "B")
                {
                    var StockQuery = fas.getAllStockEnter();
                    return View(StockQuery);
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
            
        }

        [HttpPost]
        public ActionResult InStockQuery(QStockEnter qStockEnter)
        {
            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "A")
                {
                    qStockEnter.DDL_Winery = (string)Session["WineryID"];
                    var StockQuery = fas.getStockEnter(qStockEnter);
                    return View(StockQuery);
                }
                if ((string)Session["IdentityCode"] == "B")
                {
                    var StockQuery = fas.getStockEnter(qStockEnter);
                    return View(StockQuery);
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
        }

        public ActionResult StockEnterEdit(int? id)
        {
            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "B")
                {
                    if (id != null)
                    {
                        StockEnter StockEdit = fas.getStockEnterByid((int)id);
                        return View(StockEdit);
                    }
                    else
                    {
                        return RedirectToAction("InStockQuery");
                    }
                }
                else
                {
                    return RedirectToAction("InStockQuery");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
        }

        [HttpPost]
        public ActionResult StockEnterEdit(StockEnter stock)
        {
            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "B")
                {
                    bool StockEdit = fas.StockEnterEdit(stock);
                    if (StockEdit)
                    {
                        return RedirectToAction("InStockQuery");
                    }
                    else
                    {
                        return RedirectToAction("StockEnterEdit", stock.StockEnterID);
                    }
                }
                else
                {
                    return RedirectToAction("InStockQuery");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
        }

        public ActionResult StockEnterDelete(int? stid)
        {
            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "B")
                {
                    if (stid != null)
                    {
                        string StockDelete = fas.StockEnterDelete((int)stid);
                        return Json(StockDelete, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return RedirectToAction("InStockQuery");
                    }
                }
                else
                {
                    return RedirectToAction("InStockQuery");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
        }
        #endregion

        #region Inventory Query Web

        public ActionResult InventoryQuery()
        {
            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "A")
                {
                    int.TryParse((string)Session["WineryID"], out int wineryid);
                    IEnumerable<Inventory> Inventory = fas.getInventoryQuery(wineryid);
                    return View(Inventory);
                }
                else if ((string)Session["IdentityCode"] == "B")
                {
                    IEnumerable<Inventory> Inventory = fas.getInventoryQuery();
                    return View(Inventory);
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
            
        }

        [HttpPost]
        public ActionResult InventoryQuery(QInventory inventory)
        {
            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "A")
                {
                    int.TryParse((string)Session["WineryID"], out int wineryid);
                    var Inventory = fas.getInventoryQuery(inventory, wineryid).ToList();
                    return View(Inventory);
                }
                else if ((string)Session["IdentityCode"] == "B")
                {
                    var Inventory = fas.getInventoryQuery(inventory,null).ToList();
                    return View(Inventory);
                }
                else
                {
                    return RedirectToAction("LoginPage", "Login");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
        }

        public ActionResult InventoryEdit(int? id)
        {
            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "B")
                {
                    if (id != null)
                    {
                        var InventoryEdit = fas.getInventoryByID((int)id);
                        return View(InventoryEdit);
                    }
                    else
                    {
                        return RedirectToAction("InventoryQuery");
                    }
                }
                else
                {
                    return RedirectToAction("InventoryQuery");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
        }

        [HttpPost]
        public ActionResult InventoryEdit(Inventory inventory)
        {

            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "B")
                {
                    bool edit = fas.InventoryEdit(inventory);
                    if (edit)
                    {
                        return RedirectToAction("InventoryQuery");
                    }
                    else
                    {
                        return RedirectToAction("InventoryEdit", inventory.InventoryID);
                    }
                }
                else
                {
                    return RedirectToAction("InventoryQuery");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
        }

        public ActionResult InventoryDelete(int? inventoryid)
        {
            if (Session["IdentityCode"] != null)
            {
                if ((string)Session["IdentityCode"] == "B")
                {
                    if (inventoryid != null)
                    {
                        string message = fas.InventoryDelete((int)inventoryid);
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return RedirectToAction("InventoryQuery");
                    }
                }
                else
                {
                    return RedirectToAction("InventoryQuery");
                }
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
        }
        #endregion

        #region DropDownList
        public ActionResult GetWinery()
        {
            try
            {
                var ddlWinery = ddl.getWinery();
                return Json(ddlWinery, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCategory()
        {
            try
            {
                var ddlCategory = ddl.getCategory();
                return Json(ddlCategory, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProduct(int? id)
        {
            try
            {
                var ddlProduct = ddl.getProduct(id);
                return Json(ddlProduct, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetMillilter()
        {
            try
            {
                var ddlMillilter = ddl.getMillilter();
                return Json(ddlMillilter, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetShelf()
        {
            try
            {
                var ddlShelf = ddl.getShelf();
                return Json(ddlShelf, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
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
