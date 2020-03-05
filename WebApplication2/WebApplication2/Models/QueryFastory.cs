using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.ModelViews;

namespace WebApplication2.Models
{
    public class QueryFastory
    {
        Frog_JumpEntities db = new Frog_JumpEntities();

        #region Order
        public IQueryable<Order> getAllOrder()
        {
            var OrderTable = from n in db.Order
                             select n;
            return OrderTable;
        }

        public IQueryable<Order> getOrderQuery(ModelViews.QOrder qOrder)
        {
            var table = getAllOrder().Select(p => p);

            if (!string.IsNullOrEmpty(qOrder.TB_OrderID))
            {
                table = table.Where(p => p.OrderID.ToString() == qOrder.TB_OrderID.Trim());
            }
            if (!string.IsNullOrEmpty(qOrder.DDL_Winery) && qOrder.DDL_Winery != "0")
            {
                table = table.Where(p => p.WineryID.ToString() == qOrder.DDL_Winery.Trim());
            }
            if (!string.IsNullOrEmpty(qOrder.TB_CustomerName))
            {
                table = table.Where(p => p.CustomerName.ToString() == qOrder.TB_CustomerName.Trim());
            }
            if (qOrder.D_OrderDate!=null)
            {
                DateTime lasttime = qOrder.D_OrderDate.Value.AddDays(1);
                table = table.Where(p => p.OrderDate >= qOrder.D_OrderDate && p.OrderDate < lasttime);
            }
            if (qOrder.D_RequiredDate != null)
            {
                DateTime lasttime = qOrder.D_RequiredDate.Value.AddDays(1);
                table = table.Where(p => p.RequiredDate >= qOrder.D_RequiredDate && p.RequiredDate < lasttime);
            }
            if (qOrder.D_ShippedDate != null)
            {
                DateTime lasttime = qOrder.D_ShippedDate.Value.AddDays(1);
                table = table.Where(p => p.ShippedDate >= qOrder.D_ShippedDate && p.ShippedDate < lasttime);
            }

            return table;
        }

        public Order getOrderByID(int id)
        {
            Order order = db.Order.FirstOrDefault(p => p.OrderID == id);
            return order;
        }

        public bool EditOrder(Order order)
        {
            try
            {
                Order Edit = db.Order.FirstOrDefault(p => p.OrderID == order.OrderID);
                if (Edit != null)
                {
                    Edit.WineryID = order.WineryID;
                    Edit.CustomerName = order.CustomerName;
                    Edit.OrderDate = order.OrderDate;
                    Edit.RequiredDate = order.RequiredDate;
                    Edit.ShippedDate = (DateTime)order.ShippedDate;
                    Edit.Note = order.Note;
                    Edit.Status = "";
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string Order_DetailInsert(Order_Details od)
        {
            try
            {
                db.Order_Details.Add(od);
                db.SaveChanges();

                return "0";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string OrdersDelete(int id)
        {
            try
            {
                var order = db.Order.FirstOrDefault(p => p.OrderID == id);
                db.Order.Remove(order);
                db.SaveChanges();

                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region Order_Detail
        public IEnumerable<Order_Details> getOrder_DetailsByID(int id)
        {
            var order_details = db.Order_Details.Where(p => p.OrderID == id);
            return order_details;
        }

        public string EditOrderDetail(Order_Details old_od, Order_Details new_od)
        {
            try
            {
                Order_Details order_Details = db.Order_Details.
                    FirstOrDefault(p =>
                    p.OrderID == old_od.OrderID &&
                    p.ProductID == old_od.ProductID &&
                    p.Quantity == old_od.Quantity);

                if (order_Details != null)
                {
                    db.Order_Details.Remove(order_Details);
                    db.Order_Details.Add(new Order_Details()
                    {
                        OrderID = new_od.OrderID,
                        ProductID = new_od.ProductID,
                        Quantity = new_od.Quantity
                    });

                    db.SaveChanges();
                    return "0";
                }
                return "儲存錯誤";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string OrderDetailDelete(Order_Details od)
        {
            try
            {
                var order_detail = db.Order_Details.FirstOrDefault(p =>
                  p.OrderID == od.OrderID &&
                  p.ProductID == od.ProductID &&
                  p.Quantity == od.Quantity);
                db.Order_Details.Remove(order_detail);
                db.SaveChanges();
                return "0";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region StockEnter

        public IQueryable<StockEnter> getAllStockEnter()
        {
            var StockEnterTable = db.StockEnter.Select(p => p);

            return StockEnterTable;
        }

        public string StockEnterEdit(StockEnter stock)
        {
            try
            {
                var stockenter = db.StockEnter.FirstOrDefault(p => p.StockEnterID == stock.StockEnterID);
                stockenter = new StockEnter()
                {
                    WineryID = stock.WineryID,
                    ProductID = stock.ProductID,
                    MilliliterID = stock.MilliliterID,
                    ShelfID = stock.ShelfID,
                    Quantity = stock.Quantity,
                    Note = stock.Note,
                    StockEnterDate = stock.StockEnterDate
                };
                db.SaveChanges();
                return "編輯成功";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
        }

        public string StockEnterDelete(int stid)
        {
            try
            {
                var stockenter = db.StockEnter.FirstOrDefault(p => p.StockEnterID == stid);
                db.StockEnter.Remove(stockenter);
                db.SaveChanges();
                return "刪除成功";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public StockEnter getStockEnterByid(int id)
        {
            var table = db.StockEnter.FirstOrDefault(p => p.StockEnterID == id);
            return table;
        }

        public IEnumerable<StockEnter> getStockEnter(QStockEnter qStockEnter)
        {
            try
            {
                var table = getAllStockEnter().Select(p => p);

                if (!string.IsNullOrEmpty(qStockEnter.TB_StockEnterID))
                {
                    table = table.Where(p => p.StockEnterID.ToString() == qStockEnter.TB_StockEnterID);
                }
                if (!string.IsNullOrEmpty(qStockEnter.TB_ProductID))
                {
                    table = table.Where(p => p.ProductID.ToString() == qStockEnter.TB_ProductID);
                }
                if (!string.IsNullOrEmpty(qStockEnter.DDL_Winery) && qStockEnter.DDL_Winery != "0")
                {
                    table = table.Where(p => p.WineryID.ToString() == qStockEnter.DDL_Winery);
                }
                if (!string.IsNullOrEmpty(qStockEnter.DDL_Product) && qStockEnter.DDL_Product != "0")
                {
                    table = table.Where(p => p.ProductID.ToString() == qStockEnter.DDL_Product);
                }
                if (!string.IsNullOrEmpty(qStockEnter.DDL_Millilter) && qStockEnter.DDL_Millilter != "0")
                {
                    table = table.Where(p => p.MilliliterID.ToString() == qStockEnter.DDL_Millilter);
                }
                if (!string.IsNullOrEmpty(qStockEnter.DDL_Shelf) && qStockEnter.DDL_Shelf != "0")
                {
                    table = table.Where(p => p.ShelfID.ToString() == qStockEnter.DDL_Shelf);
                }
                if (!string.IsNullOrEmpty(qStockEnter.D_StockEnterDate))
                {
                    table = table.Where(p => p.StockEnterDate.StartsWith(qStockEnter.D_StockEnterDate));
                }

                return table;
            }
            catch (Exception ex)
            {
                return getAllStockEnter().Select(p => p);
            }
        }

        #endregion

        #region Inventory
        public IEnumerable<Inventory> getInventoryQuery()
        {
            var table = from n in db.Inventory
                        select n;
            return table.ToList();
        }

        public IQueryable<Inventory> getInventoryQuery(ModelViews.QInventory qinv)
        {
            try
            {
                var table = db.Inventory.Select(p => p);
                if (!string.IsNullOrEmpty(qinv.TB_ProductID))
                {
                    table = table.Where(p => p.ProductID.ToString() == qinv.TB_ProductID.Trim());
                }
                if (!string.IsNullOrEmpty(qinv.DDL_Product.ToString()) && qinv.DDL_Product != 0)
                {
                    table = table.Where(p => p.ProductID == qinv.DDL_Product);
                }
                if (!string.IsNullOrEmpty(qinv.DDL_Millilter.ToString()) && qinv.DDL_Millilter != 0)
                {
                    table = table.Where(p => p.MilliliterID == qinv.DDL_Millilter);
                }
                if (!string.IsNullOrEmpty(qinv.DDL_Shelf.ToString()) && qinv.DDL_Shelf != 0)
                {
                    table = table.Where(p => p.ShelfID == qinv.DDL_Shelf);
                }
                return table;
            }
            catch (Exception ex)
            {
                return db.Inventory.Select(p => p);
            }

        }

        public Inventory getInventoryByID(int id)
        {

            Inventory inventory = db.Inventory.FirstOrDefault(p => p.InventoryID == id);

            return inventory;
        }

        public bool InventoryEdit(Inventory inventory)
        {
            try
            {
                Inventory inv = db.Inventory.FirstOrDefault(p => p.InventoryID == inventory.InventoryID);
                if (inv != null)
                {
                    inv.ProductID = inventory.ProductID;
                    inv.MilliliterID = inventory.MilliliterID;
                    inv.ShelfID = inventory.ShelfID;
                    inv.Quantity = inventory.Quantity;
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public string InventoryDelete(int inventoryid)
        {
            try
            {
                Inventory inv = db.Inventory.FirstOrDefault(p => p.InventoryID == inventoryid);
                db.Inventory.Remove(inv);
                db.SaveChanges();
                return "刪除成功";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IQueryable getOrderdialog(int id)
        {
            try
            {
                var order = from n in db.Order
                            where n.OrderID == id
                            select new
                            {
                                n.OrderID,
                                n.Winery.WineryName,
                                n.CustomerName,
                                n.OrderDate,
                                n.RequiredDate,
                                n.ShippedDate
                            };
                return order;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        public IQueryable getOrderDetail(int? OrderID, int? ProductID)
        {
            try
            {
                IQueryable orderdetail;

                if (ProductID != null)
                {
                    orderdetail = from n in db.Order_Details
                                  where n.OrderID == OrderID && n.ProductID == ProductID
                                  select new
                                  {
                                      n.OrderID,
                                      n.ProductID,
                                      n.Product.ProductName,
                                      n.Quantity
                                  };
                }
                else
                {
                    orderdetail = from n in db.Order_Details
                                  where n.OrderID == OrderID
                                  select new
                                  {
                                      n.OrderID,
                                      n.ProductID,
                                      n.Product.ProductName,
                                      n.Quantity
                                  };
                }

                return orderdetail;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}