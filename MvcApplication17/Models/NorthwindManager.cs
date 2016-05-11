using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Metadata.Providers;

namespace MvcApplication17.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
    }

    public class NorthwindManager
    {
        private string _connectionString;

        public NorthwindManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Product> GetProducts(decimal? minPrice)
        {
            if (minPrice == null)
            {
                minPrice = 0;
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT p.ProductName, p.QuantityPerUnit, p.UnitPrice, c.CategoryName " +
                                      "FROM Products p JOIN Categories c ON p.categoryId = c.categoryId " +
                                      "WHERE p.UnitPrice >= @minPrice";
                //"WHERE p.UnitPrice >= @minPrice and name like @name";
                command.Parameters.AddWithValue("@minPrice", minPrice);
                //command.Parameters.AddWithValue("@name", "%" + name + "%");
                List<Product> products = new List<Product>();
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Name = (string)reader["ProductName"];
                    product.QuantityPerUnit = (string)reader["QuantityPerUnit"];
                    product.UnitPrice = (decimal)reader["UnitPrice"];
                    product.CategoryName = (string)reader["CategoryName"];
                    products.Add(product);
                }

                return products;
            }
        }

        public IEnumerable<Product> Search(string searchQuery)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT p.ProductName, p.QuantityPerUnit, p.UnitPrice, c.CategoryName " +
                                      "FROM Products p JOIN Categories c ON p.categoryId = c.categoryId";
                if (searchQuery != null)
                {
                    command.CommandText += " WHERE p.ProductName LIKE @query";
                    command.Parameters.AddWithValue("@query", "%" + searchQuery + "%");
                }

                List<Product> products = new List<Product>();
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Name = (string)reader["ProductName"];
                    product.QuantityPerUnit = (string)reader["QuantityPerUnit"];
                    product.UnitPrice = (decimal)reader["UnitPrice"];
                    product.CategoryName = (string)reader["CategoryName"];
                    products.Add(product);
                }

                return products;
            }
        }

        public void AddRandom(string foo, string bar)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO RandomStuff (Foo, Bar) VALUES (@foo, @bar)";
                command.Parameters.AddWithValue("@foo", foo);
                command.Parameters.AddWithValue("@bar", bar);
                connection.Open();
                command.ExecuteNonQuery();
            }

        }
    }
}