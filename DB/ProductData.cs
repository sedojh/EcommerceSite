using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAProject.Models;
using System.Data.SqlClient;

namespace CAProject.DB
{
    public class ProductData:Data
    {
        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT * from Products";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();
                    {
                        product.Id = (string)reader["Id"];
                        product.Name = (string)reader["Productname"];
                        product.Description = (string)reader["Description"];
                        product.Price = (double)reader["Price"];
                        //add more attributes here
                    };
                    products.Add(product);
                }
            }
            return products;
        }

        //getting product with product id
        public static Product GetProduct(string id)
        { 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("Select * from Products WHERE Id = @text", conn);
                command.Parameters.AddWithValue("@text", id);
                SqlDataReader reader = command.ExecuteReader();

                Product product = new Product();
                while (reader.Read())
                {
                    product.Id = (string)reader["Id"];
                    product.Name = (string)reader["Productname"];
                    product.Description = (string)reader["Description"];
                    product.Price = (double)reader["Price"];
                    //add more attributes here
                }
                return product;
            }
        }
    }
}
