using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.ModelViews
{
    public class QueryModelByView
    {
        public QOrder QOrder { get; set; }
        public FJWinery FJWinery { get; set; }

        public IEnumerable<WebApplication2.Order> Orderlist { get; set; }
        public IEnumerable<WebApplication2.StockEnter> StockEnterlist { get; set; }
        public IEnumerable<WebApplication2.Inventory> Inventorylist { get; set; }

        public WebApplication2.Order Order { get; set; }
        public List<WebApplication2.Order_Details> Order_details { get; set; }

    }
}