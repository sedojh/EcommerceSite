using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAProject.Models;
using System.Data.SqlClient;

namespace CAProject.DB
{
    public class OrderData:Data
    {
        public static List<Order> GetAllOrdersByCustomer(string customer_id)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("Select * from Orders WHERE CustomerId = @text", conn);
                command.Parameters.AddWithValue("@text", customer_id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Order order = new Order();
                    {
                        order.Id = (string)reader["Id"];
                        order.Customer_Id = (string)reader["CustomerId"];
                        order.TotalPrice = (double)reader["TotalPrice"];
                        order.OrderDate = (string)reader["Orderdate"];
                        order.Coupon = (string)reader["Coupon"];
                        order.DiscountedPrice = (double)reader["DiscountedPrice"];
                    };
                    orders.Add(order);
                }
            }
            return orders;
        }

        public static double GetDiscount(string order_id)
        {
            double discount = 1.0;
            double totalprice, discountedprice;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("Select TotalPrice,DiscountedPrice from Orders WHERE Id = @text", conn);
                command.Parameters.AddWithValue("@text", order_id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    totalprice = (double)reader["TotalPrice"];
                    discountedprice = (double)reader["DiscountedPrice"];
                    discount = (discountedprice / totalprice);
                }
            }
            return discount;
        }

        //updating orders table in database
        public static string UpdateOrderDB(List<Product> cart,string cust_id,double discount,string couponcode)
        {
            double totalprice = 0;
            double discountedprice = 0;
            foreach (Product p in cart)
            {
                totalprice = totalprice + p.Price;
            }
            discountedprice = totalprice * discount;
            string orderID = Guid.NewGuid().ToString();
            DateTime dt = DateTime.Now;
            string date = dt.ToShortDateString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var command = new SqlCommand("INSERT INTO Orders VALUES(@orderid,@custid,'" + totalprice + "',@orderdate,@coupon,'" + discountedprice + "')", conn);
                command.Parameters.AddWithValue("@orderid", orderID);
                command.Parameters.AddWithValue("@custid", cust_id);
                command.Parameters.AddWithValue("@orderdate", date);
                command.Parameters.AddWithValue("@coupon", couponcode);
                command.ExecuteNonQuery();
            }
            return orderID;
        }
    }
}