using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAProject.Models;
using System.Data.SqlClient;

namespace CAProject.DB
{
    public class CouponData:Data
    {
        public static Coupon GetCoupon(string couponcode)
        {
            Coupon coupon = new Coupon();
            if (couponcode == null || couponcode == "")//if couponcode is not entered
            {
                coupon.Couponcode = "Nil";
                coupon.Discount = 1.0;
                return coupon;
            }

            //if couponcode is entered
            else
            {
                coupon.Couponcode = "Nil";
                coupon.Discount = 1.0;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var command = new SqlCommand("Select * from Coupon WHERE Coupon = @text", conn);
                    command.Parameters.AddWithValue("@text", couponcode);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())//if coupon code is found,coupon will carry the discount value.
                    {
                        coupon.Couponcode = (string)reader["Coupon"];
                        coupon.Discount = (double)reader["Discount"];
                    }
                }
                return coupon;
            }
        }


    }
}