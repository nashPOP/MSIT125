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

        public IQueryable warning(int id, string status)
        {
            try
            {
                switch (status)
                {
                    case "Winery":
                        var wineryWarning = db.Winery.Where(p => p.WineryID == id).Select(p=>new
                        {
                            p.WineryID,
                            p.WineryName,
                            p.WineryPhone,
                            p.WineryAddress,
                            p.WineryEmail
                        });
                        return wineryWarning;

                    case "Category":
                        var categoryWarning = db.Category.Where(p => p.CategoryID == id).Select(p=>new
                        {
                            p.CategoryID,
                            p.CategoryName
                        });
                        return categoryWarning;

                    case "Product":
                        var productWarning = db.Product.Where(p => p.ProductID == id).Select(p=>new
                        {
                            p.ProductID,
                            p.ProductName,
                            p.Quantity,
                            p.WineryID,
                            p.CategoryID,
                            p.ShelfID
                        });
                        return productWarning;

                    case "Milliliter":
                        var milliliterWarning = db.Milliliter.Where(p => p.MilliliterID == id).Select(p=>new
                        {
                            p.MilliliterID,
                            p.capacity
                        });
                        return milliliterWarning;

                    case "Shelf":
                        var shelfWarning = db.Shelf.Where(p => p.ShelfID == id).Select(p=>new
                        {
                            p.ShelfID,
                            p.ShelfPosition
                        });
                        return shelfWarning;

                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public string Insert(string[] k, string status)
        {
            try
            {
                switch (status)
                {
                    case "Winery":
                        Winery w = new Winery();
                        w.WineryName = k[1];
                        w.WineryPhone = k[2];
                        w.WineryAddress = k[3];
                        w.WineryEmail = k[4];
                        db.Winery.Add(w);
                        db.SaveChanges();
                        return "新增成功";

                    case "Category":
                        Category c = new Category();
                        c.CategoryName = k[1];
                        db.Category.Add(c);
                        db.SaveChanges();
                        return "新增成功";

                    case "Product":
                        Product p = new Product();
                        p.ProductName = k[1];
                        //p.Quantity = int.Parse(k[2]);
                        p.WineryID = int.Parse(k[3]);
                        p.CategoryID = int.Parse(k[4]);
                        db.Product.Add(p);
                        db.SaveChanges();
                        return "新增成功";

                    case "Milliliter":
                        Milliliter m = new Milliliter();
                        m.capacity = int.Parse(k[1]);
                        db.Milliliter.Add(m);
                        db.SaveChanges();
                        return "新增成功";

                    case "Shelf":
                        Shelf s = new Shelf();
                        s.ShelfPosition = k[1];
                        db.Shelf.Add(s);
                        db.SaveChanges();
                        return "新增成功";

                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public string Edit(string[] k, string status)
        {
            try
            {
                switch (status)
                {
                    case "Winery":
                        int.TryParse(k[0], out int wid);
                        var w = db.Winery.FirstOrDefault(p => p.WineryID == wid);
                        if (w != null) 
                        {
                            w.WineryName = k[1];
                            w.WineryPhone = k[2];
                            w.WineryAddress = k[3];
                            w.WineryEmail = k[4];
                            db.SaveChanges();
                            return "編輯成功";
                        }
                        return "沒有資料";

                    case "Category":
                        int.TryParse(k[0], out int cid);
                        var c = db.Category.FirstOrDefault(p => p.CategoryID == cid);
                        if(c !=null)
                        {
                            c.CategoryName = k[1];
                            db.SaveChanges();
                            return "編輯成功";
                        }
                        return "沒有資料";

                    case "Product":
                        int.TryParse(k[0], out int pid);
                        var pr = db.Product.FirstOrDefault(p => p.ProductID == pid);
                        if (pr != null)
                        {
                            int.TryParse(k[3], out int wwid);
                            int.TryParse(k[4], out int ccid);
                            pr.ProductName = k[1];
                            //p.Quantity = k[2];
                            pr.WineryID = wwid;
                            pr.CategoryID = ccid;
                            db.SaveChanges();
                            return "編輯成功";
                        }
                        return "沒有資料";

                    case "Milliliter":
                        int.TryParse(k[0], out int mid);
                        var m = db.Milliliter.FirstOrDefault(p => p.MilliliterID == mid);
                        if (m != null)
                        {
                            int.TryParse(k[1], out int mmid);
                            m.capacity = mmid;
                            db.SaveChanges();
                            return "編輯成功";
                        }
                        return "沒有資料";

                    case "Shelf":
                        int.TryParse(k[0], out int sid);
                        var s = db.Shelf.FirstOrDefault(p => p.ShelfID == sid);
                        if (s != null)
                        {
                            s.ShelfPosition = k[1];
                            db.SaveChanges();
                            return "編輯成功";
                        }
                        return "沒有資料";

                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}