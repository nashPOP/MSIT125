using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ModelViews;

namespace WebApplication2.Models
{
    public class DDLFastory
    {
        Frog_JumpEntities fj = new Frog_JumpEntities();

        /// <summary>
        /// DropDownList Data By Winery
        /// 酒莊下拉式選單
        /// </summary>
        /// <returns>{Int:WineryID,String:WineryName}</returns>
        public IQueryable getWinery()
        {
            var winery = from n in fj.Winery
                         select new
                         {
                             n.WineryID,
                             n.WineryName
                         };

            return winery;
        }

        /// <summary>
        /// DropDownList Data By Category
        /// 酒莊下拉式選單
        /// </summary>
        /// <returns>{Int:CategoryID,String:CategoryName}</returns>
        public IQueryable getCategory()
        {
            var category = fj.Category.Select(p => new {
                p.CategoryID,
                p.CategoryName
            });

            return category;
        }

        /// <summary>
        /// DropDownList Data By Product
        /// 酒莊下拉式選單
        /// 由CategoryID(產品分類)判斷
        /// </summary>
        /// <returns>{Int:ProductID,String:ProductName}</returns>
        public IQueryable getProduct(int? id)
        {
            var products = fj.Product.Select(p => p);

            if (id != null && id != 0) 
            {
                var q = from n in products
                        where n.CategoryID == id
                        select new { n.ProductID, n.ProductName };
                return q;
            }
            else
            {
                var q=from n in products
                      select new { n.ProductID, n.ProductName };
                return q;
            }

        }

        public IQueryable getMillilter()
        {
            var millilter = fj.Milliliter.Select(p => new { p.MilliliterID, MilliliterName = p.capacity });
            return millilter;
        }

        public IQueryable getShelf()
        {
            var shelf = fj.Shelf.Select(p => new { p.ShelfID, p.ShelfPosition });
            return shelf;
        }
    }
}
