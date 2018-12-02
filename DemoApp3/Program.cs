using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DemoApp3
{
    class Program
    {
        static void Main()
        {
            string connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // parameter placeholder for query string
            string queryString =
                "SELECT CustomerID, CompanyName, ContactName, Country from dbo.Customers "
                    + "WHERE Country = @custCountry "
                    + "ORDER BY CompanyName;";

            // parameter value
            string paramValue = "Mexico";

            // create/open connection using block to ensure resources will be closed/disposed
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // create command and parameters
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@custCountry", paramValue);

                // formatting for console
                string format = "{0,1}{1,50}{2,30}{3,15}";
                string[] header = new string[] { "Company ID: ", "Company Name: ", "Contact Name: ", "Country: " };

                // open connection in try/catch block
                // create and execute DataReader, writing result to console
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine(string.Format(format, header));
                    while (reader.Read())
                    {
                        Console.WriteLine(string.Format(format,
                        reader[0], reader[1], reader[2], reader[3]));
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}
