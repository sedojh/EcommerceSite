using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAProject.Models;
using System.Data.SqlClient;

namespace CAProject.DB
{
    public class OrderDetailsData:Data
    {
        public static List<OrderDetails> GetAllOrderDetails()
        {
            List<OrderDetails> orderdetails = new List<OrderDetails>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT * from OrderDetails";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderDetails orderdetail = new OrderDetails();
                    {
                        orderdetail.Customer_Id = (string)reader["CustomerId"];
                        orderdetail.Product_Id = (string)reader["ProductId"];
                        orderdetail.Order_Id = (string)reader["OrderId"];
                        orderdetail.ActivationCode = (string)reader["Activationcode"];
                        orderdetail.Price = (double)reader["Price"];
                        //add more attributes here
                    };
                    orderdetails.Add(orderdetail);
                }
            }
            return orderdetails;
        }

        public static List<OrderDetails> GetOrderDetailsByCustomer(string customer_id)
        {
            List<OrderDetails> orderdetails = new List<OrderDetails>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("Select * from OrderDetails WHERE CustomerId = @text", conn);
                command.Parameters.AddWithValue("@text", customer_id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    OrderDetails orderdetail = new OrderDetails();
                    {
                        orderdetail.Customer_Id = (string)reader["CustomerId"];
                        orderdetail.Product_Id = (string)reader["ProductId"];
                        orderdetail.Order_Id = (string)reader["OrderId"];
                        orderdetail.ActivationCode = (string)reader["Activationcode"];
                        orderdetail.Price = (double)reader["Price"];
                    };
                    orderdetails.Add(orderdetail);
                }
            }
            return orderdetails;
        }

        //updating order details table in database
        public static void UpdateOrderDetailsDB(List<Product> cart, string cust_id, string order_id)
        {
            foreach(Product p in cart)
            {
                string activate = Guid.NewGuid().ToString();
                string product_id = p.Id;
                double price = p.Price;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new SqlCommand("INSERT INTO OrderDetails VALUES(@custid,@productid,@orderid,@act,'"+price+"')", conn);
                    command.Parameters.AddWithValue("@custid", cust_id);
                    command.Parameters.AddWithValue("@productid", product_id);
                    command.Parameters.AddWithValue("@orderid", order_id);
                    command.Parameters.AddWithValue("@act", activate);
                    command.ExecuteNonQuery();
                }
            }

        }




    }
}