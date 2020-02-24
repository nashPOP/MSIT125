using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class QueryFastory
    {
        FrogJumpEntities db = new FrogJumpEntities();

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
                table = table.Where(p => p.OrderID.ToString() ==qOrder.TB_OrderID.Trim() );
            }
            if (!string.IsNullOrEmpty(qOrder.DDL_Winery) && qOrder.DDL_Winery !="0")
            {
                table = table.Where(p => p.WineryID.ToString() == qOrder.DDL_Winery.Trim());
            }
            if (!string.IsNullOrEmpty(qOrder.TB_CustomerName))
            {
                table = table.Where(p => p.CustomerName.ToString() == qOrder.TB_CustomerName.Trim());
            }
            if (!string.IsNullOrEmpty(qOrder.D_OrderDate.ToString()))
            {
                table = table.Where(p => p.OrderDate == qOrder.D_OrderDate);
            }
            if (!string.IsNullOrEmpty(qOrder.D_OrderDate.ToString()))
            {
                table = table.Where(p => p.RequiredDate == qOrder.D_RequiredDate);
            }
            if (!string.IsNullOrEmpty(qOrder.D_OrderDate.ToString()))
            {
                table = table.Where(p => p.ShippedDate == qOrder.D_ShippedDate);
            }

            return table;
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
                    Edit.ShippedDate = order.ShippedDate;
                    Edit.Note = order.Note;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public string EditOrderDetail(int oid,int pid,int qty)
        {
            try
            {
                Order_Details order_Details = db.Order_Details.FirstOrDefault(p => p.OrderID == oid && p.ProductID == pid);
                if (order_Details != null)
                {
                    order_Details.ProductID = pid;
                    order_Details.Quantity = qty;

                    db.SaveChanges();
                    return "";
                }
                return "儲存錯誤";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IQueryable<StockEnter> getAllStockEnter()
        {
            var StockEnterTable = db.StockEnter.Select(p => p);
            return StockEnterTable;
        }

        public IQueryable<Order> getStockEnter(ModelViews.QOrder qOrder)
        {
            var table = getAllOrder().Select(p => p);

            //if (!string.IsNullOrEmpty(qOrder.TB_OrderID))
            //{
            //    table = table.Where(p => p.OrderID.ToString() == qOrder.TB_OrderID.Trim());
            //}
            //if (!string.IsNullOrEmpty(qOrder.DDL_Winery))
            //{
            //    table = table.Where(p => p.WineryID.ToString() == qOrder.DDL_Winery.Trim());
            //}
            //if (!string.IsNullOrEmpty(qOrder.TB_CustomerName))
            //{
            //    table = table.Where(p => p.CustomerName.ToString() == qOrder.TB_CustomerName.Trim());
            //}
            //if (!string.IsNullOrEmpty(qOrder.D_OrderDate.ToString()))
            //{
            //    table = table.Where(p => p.OrderDate == qOrder.D_OrderDate);
            //}
            //if (!string.IsNullOrEmpty(qOrder.D_OrderDate.ToString()))
            //{
            //    table = table.Where(p => p.RequiredDate == qOrder.D_RequiredDate);
            //}
            //if (!string.IsNullOrEmpty(qOrder.D_OrderDate.ToString()))
            //{
            //    table = table.Where(p => p.ShippedDate == qOrder.D_ShippedDate);
            //}

            return table;
        }

        

        public IEnumerable<Inventory> getInventoryQuery()
        {
            var table = from n in db.Inventory
                        select n;
            return table.ToList();
        }
        
        public IEnumerable<Inventory> getInventoryQuery(ModelViews.QInventory qinv)
        {
            var table = db.Inventory.Select(p => p);
            if (!string.IsNullOrEmpty(qinv.TB_ProductID))
            {
                table = table.Where(p => p.ProductID.ToString() == qinv.TB_ProductID.Trim());
            }
            if (!string.IsNullOrEmpty(qinv.DDL_Product))
            {
                table = table.Where(p => p.Product.ProductName == qinv.DDL_Product);
            }
            if (!string.IsNullOrEmpty(qinv.DDL_Millilter.ToString()))
            {
                table = table.Where(p => p.MilliliterID == qinv.DDL_Millilter);
            }
            if (!string.IsNullOrEmpty(qinv.DDL_Shelf.ToString()))
            {
                table = table.Where(p => p.ShelfID == qinv.DDL_Shelf);
            }

            return table;
        }

        public Inventory getInventoryByID(int id)
        {

            Inventory inventory = db.Inventory.FirstOrDefault(p => p.InventoryID == id);

            return inventory;
        }

        public bool InventoryEdit(Inventory inventory)
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

        public string InventoryDelete(int inventoryid)
        {
            try
            {
                Inventory inv = db.Inventory.FirstOrDefault(p => p.InventoryID == inventoryid);
                db.Inventory.Remove(inv);
                db.SaveChanges();
                return "刪除成功";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public IQueryable getOrderdialog(int id)
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

        public IQueryable getOrderDetail(int? OrderID ,int? ProductID)
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

    }
}