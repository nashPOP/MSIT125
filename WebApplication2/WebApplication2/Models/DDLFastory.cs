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
            try
            {
                var winery = from n in fj.Winery
                             select new
                             {
                                 n.WineryID,
                                 n.WineryName
                             };

                return winery;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// DropDownList Data By Category
        /// 酒莊下拉式選單
        /// </summary>
        /// <returns>{Int:CategoryID,String:CategoryName}</returns>
        public IQueryable getCategory()
        {
            try
            {
                var category = fj.Category.Select(p => new
                {
                    p.CategoryID,
                    p.CategoryName
                });

                return category;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// DropDownList Data By Product
        /// 酒莊下拉式選單
        /// 由CategoryID(產品分類)判斷
        /// </summary>
        /// <returns>{Int:ProductID,String:ProductName}</returns>
        public IQueryable getProduct(int? id)
        {
            try
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
                    var q = from n in products
                            select new { n.ProductID, n.ProductName };
                    return q;
                }
            }
            catch
            {
                return null;
            }
        }

        public IQueryable getMillilter()
        {
            try
            {
                var millilter = fj.Milliliter.Select(p => new { p.MilliliterID, MilliliterName = p.capacity });
                return millilter;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable getShelf()
        {
            try
            {
                var shelf = fj.Shelf.Select(p => new { p.ShelfID, p.ShelfPosition });
                return shelf;
            }
            catch
            {
                return null;
            }
        }
    }
}
