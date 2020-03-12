using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApplication2.Models
{
    public class dbSimulator
    {
        private readonly Frog_JumpEntities db = new Frog_JumpEntities();
        public List<string> GetWinerys()
        {
            var res = (from data in db.Winery
                       select data.WineryName).ToList();
            return res;
        }

        public List<string> GetCategoerys()
        {
            var res = (from data in db.Category
                       select data.CategoryName).ToList();
            return res;
        }

        public List<ProductsData> GetProductsDatas()
        {
            List<ProductsData> res = (from product in db.Product
                                      join winery in db.Winery on product.WineryID equals winery.WineryID
                                      join category in db.Category on product.CategoryID equals category.CategoryID
                                      select new ProductsData()
                                      {
                                          Winery = winery.WineryName,
                                          Category = category.CategoryName,
                                          ProductId = product.ProductID.ToString(),
                                          ProductName = product.ProductName
                                      }).ToList();
            return res;
        }

        public void AddToDb(OrderInputPostModel[] input)
        {
            foreach (OrderInputPostModel item in input)
            {
                int productid = (from p in db.Product
                                 where item.ProductName == p.ProductName
                                 select p.ProductID).FirstOrDefault();
                int wineryid = (from w in db.Winery
                                where item.Winery == w.WineryName
                                select w.WineryID).FirstOrDefault();
                //先插入ORDER取得ID
                Order o = new Order()
                {
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Parse(item.ShippedDate),
                    CustomerName = item.CustomerName,
                    WineryID = wineryid,
                    Note = item.Note,
                    CustomerAddress = item.Address,
                    CustomerPhone = item.Telphone,
                    Status = "P"
                };
                db.Order.Add(o);
                db.SaveChanges();

                int oid = o.OrderID;

            Order_Details od = new Order_Details()
                {
                    OrderID = oid,
                    ProductID = productid,
                    Quantity = Int32.Parse(item.Quantity)
                };

                db.Order_Details.Add(od);
                db.SaveChanges();

            }
            
        }
    }

    public class ProductsData
    {
        public string Winery { get; set; }
        public string Category { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }

    public class OrderInputPostModel
    {
        public string Winery { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string CustomerName { get; set; }
        public string Telphone { get; set; }
        public string Address { get; set; }
        public string ShippedDate { get; set; }
        public string Note { get; set; }
    }
}