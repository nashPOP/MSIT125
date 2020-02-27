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
        private readonly DELogisticsEntities db = new DELogisticsEntities();

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
    }
}