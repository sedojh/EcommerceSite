using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAProject.Models;
using CAProject.DB;

namespace CAProject.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult New(string name, string username, string password, string email, string address, string postalcode, string phone)
        {
            if (name == null || name == "" ||
               username == null || username == "" ||
               password == null || password == "" ||
               email == null || email == "" ||
               address == null || address == "")//checking if user has submitted the required fields for registration
            {
                //get sessionid. sessionid will be issued if user has logged in
                string sessionid = (string)Session["sessionId"];
                if (sessionid == null || sessionid == "")//if user has not logged in
                {
                    return View();
                }
                //if user has already log in, he should not see the sign up page.Redirect to products page
                else
                {
                    return RedirectToAction("ListProducts", "Products");
                }
            }

            //for new user registration
            else
            {
                //check the username if it is taken
                bool check = CustomerData.CheckUsername(username);

                //if username has not been used
                if (check)
                {
                    Customer customer = new Customer();
                    customer.Id = Guid.NewGuid().ToString();
                    customer.Name = name;
                    customer.Username = username;
                    customer.Password = PasswordHash.HashPassword(password);
                    customer.Email = email;
                    customer.Address = address;
                    customer.Postalcode = Convert.ToInt32(postalcode);
                    customer.Phone = Convert.ToInt32(phone);
                    CustomerData.UpdateCustomer(customer);
                    return RedirectToAction("Login", "Home");
                }

                //if username is taken
                else
                {
                    ViewBag.userfalse = "Username already exists";
                    return View();
                }
            }
        }
    }
}