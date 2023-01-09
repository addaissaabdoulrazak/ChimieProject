using System;
using System.Data.SqlClient;

namespace ChimieProject.Models
{
    public class DBConnection
    {
     
         
            static string DBConnectionString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = Chimie; Integrated Security = True; Connect Timeout = 30;TrustServerCertificate=true";
            //static string DBConnectionString= @"Server=(localdb)\MSSQLLocalDB;Trusted_Connection=True;Connection Timeout=30;Database=ArticleDB;";

            public static SqlConnection GetConnection()
            {
                return new SqlConnection(DBConnectionString);
            }
    }
}
