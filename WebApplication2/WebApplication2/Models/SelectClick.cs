using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ModelView
{
    public class SelectClick
    {
        DELogisticsEntities db = new DELogisticsEntities();

        public IQueryable getWinery()
        {
            var winery = db.Wineries.Select(w => new
            {
                w.WineryName,
                w.WineryID
            });

            return winery;
        }

        public IQueryable getProductName()
        {
            var product = db.Products.Select(p => new
            {
                p.ProductName,
                p.ProductID
            });

            return product;
        }

        public IQueryable getCategory()
        {
            var category = db.Categories.Select(c => new
            {
                c.CategoryID,
                c.CategoryName
            });

            return category;
        }

        public IQueryable getMilliliter()
        {
            var milliliter = db.Milliliters.Select(m => new
            {
                m.MilliliterID,
                m.capacity
            });

            return milliliter;
        }

        public IQueryable getShelf()
        {
            var shelf = db.Shelves.Select(s => new
            {
                s.ShelfID,
                s.ShelfPosition
            });

            return shelf;
        }
    }
}