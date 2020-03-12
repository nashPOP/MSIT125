using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ModelViews
{
    public class QOrder
    {
        public string TB_OrderID { get; set; }
        public string TB_CustomerName { get; set; }
        public string DDL_Winery { get; set; }
        public string DDL_Category { get; set; }
        public string DDL_Product { get; set; }
        public string D_CustomerAddress { get; set; }
        public string TB_CustomerPhone { get; set; }
        public DateTime? D_OrderDate { get; set; }
        public DateTime? D_RequiredDate { get; set; }
        public DateTime? D_ShippedDate { get; set; }
    }
}