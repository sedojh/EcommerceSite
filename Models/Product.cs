using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAProject.Models
{
    public class Product: IEquatable<Product>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public bool Equals(Product other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            int hashProductId = Id.GetHashCode();
            return hashProductId;
        }
    }
}