using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication2.ModelView;

namespace WebApplication2.Models
{
    public class EnterStockFactory
    {
        private readonly Frog_JumpEntities db = new Frog_JumpEntities();

        public CEnterStock getByWinery (int WineryID)
        {          
            List<CEnterStock> ListWinery = queryBySql("select * from DELogistics");
            if (ListWinery.Count == 0)
                return null;
            return ListWinery[0];
        }

        private List<CEnterStock> queryBySql(string sql)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=.;Initial Catalog=DELogistics;Integrated Security=True";
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            conn.Close();
            List<CEnterStock> list = new List<CEnterStock>();

            foreach(DataRow r in ds.Tables[0].Rows)
            {
                CEnterStock x = new CEnterStock();
                x.Winery = r["WineryID"].ToString();
                list.Add(x);
            }
            return list;
        }

        public string Insert(List<StockEnter> x)
        {
            try
            {
                DateTime datetime = DateTime.Now;
                foreach (var d in x)
                {
                    d.StockEnterDate = datetime;
                    db.StockEnter.Add(d);
                }
                string message = InsertInventory(x);
                if (message == "0")
                {
                    db.SaveChanges();
                    return "0";
                }
                else
                {
                    return message;
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string InsertInventory(List<StockEnter> x)
        {
            try
            {
                foreach (var i in x)
                {
                    var product = db.Product.FirstOrDefault
                        (
                        p => p.ProductID == i.ProductID
                        );
                    if (product != null)
                    {
                        product.Quantity += i.Quantity;
                        db.SaveChanges();
                    }
                    //else
                    //{
                    //    db.Product.Add(new Product()
                    //    {
                    //        ProductID = i.ProductID,
                    //        CategoryID = i.CategoryID,
                    //        Quantity = i.Quantity,
                    //        ShelfID = i.ShelfID
                    //    });
                    //    db.SaveChanges();
                    //}
                }
                return "0";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}