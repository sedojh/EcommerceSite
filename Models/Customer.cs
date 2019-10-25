using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAProject.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Postalcode { get; set; }
        public int Phone { get; set; }

        //other attributes
    }
}