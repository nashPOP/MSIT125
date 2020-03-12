using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
namespace WebApplication2.ModelViews
{
    public class QueryModelByView
    {
        public QOrder QOrder { get; set; }
        public FJWinery FJWinery { get; set; }

        public IEnumerable<Order> Orderlist { get; set; }
        public IEnumerable<StockEnter> StockEnterlist { get; set; }
        public IEnumerable<Inventory> Inventorylist { get; set; }

        public Order Order { get; set; }
        public List<Order_Details> Order_details { get; set; }

    }
}