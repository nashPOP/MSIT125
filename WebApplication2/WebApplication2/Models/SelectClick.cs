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

        public IEnumerable<Winery> getWinery()
        {
            var winery = db.Winery.ToList();
            return winery;
        }

        public IEnumerable<Product> getProductName()
        {
            var product = db.Product.ToList();
            return product;
        }

        public IEnumerable<Category> getCategory()
        {
            var category = db.Category.ToList();
            return category;
        }

        public IEnumerable<Milliliter> getMilliliter()
        {
            var milliliter = db.Milliliter.ToList();
            return milliliter;
        }

        public IEnumerable<Shelf> getShelf()
        {
            var shelf = db.Shelf.ToList();
            return shelf;
        }
    }
}