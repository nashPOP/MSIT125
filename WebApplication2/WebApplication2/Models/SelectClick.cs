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

        public string delete(int id, string status)
        {
            try
            {
                switch (status)
                {
                    case "Winery":
                        var wineryDelete = db.Winery.FirstOrDefault(p => p.WineryID == id);
                        if (wineryDelete != null)
                        {
                            db.Winery.Remove(wineryDelete);
                            db.SaveChanges();
                        }
                        break;
                    case "Category":
                        var categoryDelete = db.Category.FirstOrDefault(p => p.CategoryID == id);
                        if (categoryDelete != null)
                        {
                            db.Category.Remove(categoryDelete);
                            db.SaveChanges();
                        }
                        break;
                    case "Product":
                        var productDelete = db.Product.FirstOrDefault(p => p.ProductID == id);
                        if (productDelete != null)
                        {
                            db.Product.Remove(productDelete);
                            db.SaveChanges();
                        }
                        break;
                    case "Milliliter":
                        var milliliterDelete = db.Milliliter.FirstOrDefault(p => p.MilliliterID == id);
                        if (milliliterDelete != null)
                        {
                            db.Milliliter.Remove(milliliterDelete);
                            db.SaveChanges();
                        }
                        break;
                    case "Shelf":
                        var shelfDelete = db.Shelf.FirstOrDefault(p => p.ShelfID == id);
                        if (shelfDelete != null)
                        {
                            db.Shelf.Remove(shelfDelete);
                            db.SaveChanges();
                        }
                        break;
                }
                return "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}