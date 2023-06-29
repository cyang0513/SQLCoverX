﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace SQLCoverXTestConsole
{
   internal class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Test started...");
         var cover = new SQLCover.CodeCoverage("Data Source=.;Initial Catalog=TestDb;Integrated Security=True;TrustServerCertificate=True", "TestDb");
         cover.Start();

         using (var sqlCon = new SqlConnection("Data Source=.;Initial Catalog=TestDb;Integrated Security=True;TrustServerCertificate=True"))
         {
            sqlCon.Open();
            SqlCommand sql_cmnd = new SqlCommand("SelectAllCustomers", sqlCon);
            sql_cmnd.CommandType = CommandType.StoredProcedure;
            sql_cmnd.ExecuteNonQuery();
            sqlCon.Close();
         }

         using (var sqlCon = new SqlConnection("Data Source=.;Initial Catalog=TestDb;Integrated Security=True;TrustServerCertificate=True"))
         {
            sqlCon.Open();
            SqlCommand sql_cmnd = new SqlCommand("TestAdd", sqlCon);
            sql_cmnd.CommandType = CommandType.StoredProcedure;
            sql_cmnd.Parameters.AddWithValue("@v1", SqlDbType.Int).Value = 5;
            sql_cmnd.Parameters.AddWithValue("@v2", SqlDbType.Int).Value = 10;
            var returnVal = sql_cmnd.Parameters.Add("@v3", SqlDbType.Int);
            returnVal.Direction = ParameterDirection.ReturnValue;
            sql_cmnd.ExecuteNonQuery();

            Console.WriteLine($"Return {returnVal.Value}");

            sqlCon.Close();
         }

         var result = cover.Stop();

         var htmlFile = result.Html2();

         File.WriteAllText("coverage.html", htmlFile);

         Console.WriteLine("Test done...");

         Console.ReadKey();
      }
   }
}