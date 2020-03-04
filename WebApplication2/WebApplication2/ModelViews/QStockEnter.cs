using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ModelViews
{
    public class QStockEnter
    {
        public string TB_StockEnterID { get; set; }
        public string TB_ProductID { get; set; }
        public string DDL_Product { get; set; }
        public string DDL_Winery { get; set; }
        public string DDL_Millilter { get; set; }
        public string DDL_Shelf { get; set; }
        public string D_StockEnterDate { get; set; }
    }
}