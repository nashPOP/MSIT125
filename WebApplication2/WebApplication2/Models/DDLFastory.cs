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
        FrogJumpEntities fj = new FrogJumpEntities();

        public List<SelectListItem> getWinery()
        {
            var winery = fj.Winery.Select(p => p);

            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach(var item in winery)
            {
                SelectListItem s = new SelectListItem()
                {
                    Text = item.WineryName,
                    Value = item.WineryID.ToString()
                };
                listItems.Add(s);
            }
            return listItems;
        }

        public List<SelectListItem> getCategory()
        {
            var category = fj.Category.Select(p => p);

            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in category)
            {
                SelectListItem s = new SelectListItem()
                {
                    Text = item.CategoryName,
                    Value = item.CategoryID.ToString()
                };
                listItems.Add(s);
            }
            return listItems;
        }

        public List<SelectListItem> getProduct()
        {
            var products = fj.Product.Select(p => p);

            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (var item in products)
            {
                SelectListItem s = new SelectListItem()
                {
                    Text = item.ProductName,
                    Value = item.ProductID.ToString()
                };
                listItems.Add(s);
            }
            return listItems;
        }
    }
}
