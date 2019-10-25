using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAProject.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string Customer_Id { get; set; }
        public double TotalPrice { get; set; }
        public string OrderDate { get; set; }
        public string Coupon { get; set; }
        public double DiscountedPrice {get;set;}

    }
}