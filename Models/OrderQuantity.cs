using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAProject.Models
{
    public class OrderQuantity
    {
        public string Order_Id { get; set; }
        public string Product_Id { get; set; }
        public int Quantity { get; set; }
        public string Customer_Id { get; set; }
    }
}