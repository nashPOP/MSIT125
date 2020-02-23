using EntityModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class MembersFactory
    {
        public Members getById(int fAccount)
        {
            List<Members> list = queryBySql("SELECT * FROM Account WHERE Account=" + fAccount.ToString());
            if (list.Count == 0)
                return null;
            return list[0];
        }
        public Members getByPassWord(string PassWord)
        {
            List<Members> list = queryBySql("SELECT * FROM Account WHERE PassWord='" + PassWord + "'");
            if (list.Count == 0)
                return null;
            return list[0];
        }
        public List<Members> getAll()
        {
            return queryBySql("SELECT * FROM Account");
        }


        public List<Members> queryBySql(string sql)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=DELogisticsEntities1;Integrated Security=True";
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();

            List<Members> list = new List<Members>();
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                Members x = new Members();
                x.fAccount = r["fAccount"].ToString();
                x.fPassWord = r["fPassWord"].ToString();
                x.fIdentityCode = r["fIdentityCode"].ToString();
               
                list.Add(x);
            }
            return list;
        }

       
      

        internal void delete(string fAccount)
        {
            executeSql("DELETE FROM Account WHERE Account=" + fAccount.ToString());
        }
        /// <summary>
        /// 刪除客戶資料
        /// </summary>
        /// <param name="p">被刪除客戶PK</param>
        /// <date>2020-1-15</date>
        /// <ver>1.0</ver>
        /// <author>Marco (marco@gmail.com)</author>
        /// 
        public void create(Members p)
        {
            string sql = "INSERT INTO Account (";
            sql += " Account,";
            sql += " PassWord,";
            sql += " IdentityCode,";
            sql += " WineryID";
            sql += ") VALUES (";
            sql += " '" + p.fAccount + "',";
            sql += " '" + p.fPassWord + "',";
            sql += " '" + p.fIdentityCode + "',";
            sql += " '" + p.fWineryID + "')";

            executeSql(sql);

        }

        private static void executeSql(string sql)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDmeo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}