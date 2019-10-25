using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAProject.Models
{
    public class OrderDetails
    {
        public string Customer_Id { get; set; }
        public string Order_Id { get; set; }
        public string Product_Id { get; set; }
        public string ActivationCode { get; set; }
        public double Price { get; set; }
    }
}