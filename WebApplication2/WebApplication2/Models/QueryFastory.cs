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
            if (!string.IsNullOrEmpty(qOrder.DDL_Winery))
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

        public IQueryable<Order> getAllStockEnter()
        {
            var OrderTable = from n in db.Order
                             select n;
            return OrderTable;
        }

        public IQueryable<Order> getStockEnter(ModelViews.QOrder qOrder)
        {
            var table = getAllOrder().Select(p => p);

            if (!string.IsNullOrEmpty(qOrder.TB_OrderID))
            {
                table = table.Where(p => p.OrderID.ToString() == qOrder.TB_OrderID.Trim());
            }
            if (!string.IsNullOrEmpty(qOrder.DDL_Winery))
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
    }
}