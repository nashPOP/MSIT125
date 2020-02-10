using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class OrderQueryFastory
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
            var table = getAllOrder();
            table.Select(p => p);

            if (string.IsNullOrEmpty(qOrder.TB_OrderID.Trim()))
            {
                table.Where(p => p.OrderID.ToString() ==qOrder.TB_OrderID.Trim() );
            }
            if (string.IsNullOrEmpty(qOrder.DDL_Winery.Trim()))
            {
                table.Where(p => p.WineryID.ToString() == qOrder.DDL_Winery.Trim());
            }
            if (string.IsNullOrEmpty(qOrder.TB_CustomerName.Trim()))
            {
                table.Where(p => p.CustomerName.ToString() == qOrder.TB_CustomerName.Trim());
            }
            if (string.IsNullOrEmpty(qOrder.D_OrderDate.ToString()))
            {
                table.Where(p => p.OrderDate == qOrder.D_OrderDate);
            }
            
            return table;
        }
    }
}