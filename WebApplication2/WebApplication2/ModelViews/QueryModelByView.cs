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

        public List<SelectListItem> Wineryddl { get; set; }
        public List<SelectListItem> Categoryddl { get; set; }
        public List<SelectListItem> Productddl { get; set; }
        public List<SelectListItem> Millilterddl { get; set; }
        public List<SelectListItem> Shellddl { get; set; }

    }
}