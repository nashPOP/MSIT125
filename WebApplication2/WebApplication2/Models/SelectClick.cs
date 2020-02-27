using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;
namespace WebApplication2.ModelView
{
    public class SelectClick
    {
        Frog_JumpEntities db = new Frog_JumpEntities();

        public IQueryable getWinery()
        {
            var winery = db.Winery.Select(w => new
            {
                w.WineryName,
                w.WineryID
            });

            return winery;
        }

        public IQueryable getProductName()
        {
            var product = db.Product.Select(p => new
            {
                p.ProductName,
                p.ProductID
            });

            return product;
        }

        public IQueryable getCategory()
        {
            var category = db.Category.Select(c => new
            {
                c.CategoryID,
                c.CategoryName
            });

            return category;
        }

        public IQueryable getMilliliter()
        {
            var milliliter = db.Milliliter.Select(m => new
            {
                m.MilliliterID,
                m.capacity
            });

            return milliliter;
        }

        public IQueryable getShelf()
        {
            var shelf = db.Shelf.Select(s => new
            {
                s.ShelfID,
                s.ShelfPosition
            });

            return shelf;
        }
    }
}