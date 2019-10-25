using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CAProject.Models
{
    //register object is for the new sign up only
    public class Register
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare(
            "password", ErrorMessage = "Passwords must match")]
        [Display(Name = "Confirm Password")]
        public string confirm_password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter numbers only")]
        [Display(Name = "Postal")]
        public string postalcode { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter numbers only")]
        [Display(Name = "Phone")]
        public string phone { get; set; }
    }
}