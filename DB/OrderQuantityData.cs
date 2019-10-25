using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAProject.Models;
using System.Data.SqlClient;

namespace CAProject.DB
{
    public class OrderQuantityData:Data
    {
        public static List<OrderQuantity> GetAllOrderQuantity()
        {
            List<OrderQuantity> orderquantities = new List<OrderQuantity>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT * from OrderQuantity";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderQuantity orderquantity = new OrderQuantity();
                    {
                        orderquantity.Order_Id = (string)reader["OrderId"];
                        orderquantity.Product_Id = (string)reader["ProductId"];
                        orderquantity.Customer_Id = (string)reader["CustomerId"];
                        orderquantity.Quantity = (int)reader["Quantity"];
                        //add more attributes here
                    };
                    orderquantities.Add(orderquantity);
                }
            }
            return orderquantities;
        }

        public static List<OrderQuantity> GetOrdersHistory(string customer_id)
        {
            {
                List<OrderQuantity> orderquantities = new List<OrderQuantity>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new SqlCommand("Select * from OrderQuantity WHERE CustomerId = @text", conn);
                    command.Parameters.AddWithValue("@text", customer_id);
                    SqlDataReader reader = command.ExecuteReader();  

                    while (reader.Read())
                    {
                        OrderQuantity orderquantity = new OrderQuantity();
                        {
                            orderquantity.Order_Id = (string)reader["OrderId"];
                            orderquantity.Product_Id = (string)reader["ProductId"];
                            orderquantity.Customer_Id = (string)reader["CustomerId"];
                            orderquantity.Quantity = (int)reader["Quantity"];
                        };
                        orderquantities.Add(orderquantity);
                    }
                }
                return orderquantities;
            }
        }
        //update orderquantity table in database
        public static void UpdateOrderQuantityDB(List<Product> cart, string cust_id, string order_id)
        {
            IEnumerable<Product> testcart = cart.Distinct();
            List<Product> distinctcart = new List<Product>();
            foreach (var p in testcart)
            {
                distinctcart.Add(p);
            }

            List<int> quantity = new List<int>();
            var iter = from product in cart
                       group product by product.Id;
            foreach(var p in iter)
            {
                quantity.Add(p.Count());
            }

            for(int i = 0; i < distinctcart.Count; i++)
            {
                string product_id = distinctcart[i].Id;
                int qty = quantity[i];
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new SqlCommand("INSERT INTO OrderQuantity VALUES(@orderid,@productid,@custid,'"+qty+"')", conn);
                    command.Parameters.AddWithValue("@orderid", order_id);
                    command.Parameters.AddWithValue("@productid", product_id);
                    command.Parameters.AddWithValue("@custid", cust_id);
                    command.ExecuteNonQuery();
                }

            }

        }
    }
}