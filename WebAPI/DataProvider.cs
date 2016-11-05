using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public class DataProvider
    {
        public static SqlConnection ConnectionData()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SERVER;Integrated Security=True";
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            return cn;
        }
    }
}