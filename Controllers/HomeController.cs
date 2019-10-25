using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using CAProject.DB;
using CAProject.Models;
using CAProject.Filters;

namespace CAProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Contact()
        {
            return View();
        }

        [AuthorizationFilter]
        public ActionResult MyInfo()
        {
            string username = (string)Session["username"];
            if(username == null || username == "")
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                Customer customer = CustomerData.GetCustomerDetails(username);
                ViewBag.customer = customer;
                return View();
            }

        }

        public ActionResult Login(string username, string password)
        {
            //To check if username and password is empty when submitted
            Dictionary<string, string> errDict = new Dictionary<string, string>();
            if (username == "" || password == "" || username == null || password == null)
            {
                if (Request.RequestType == "POST")
                {
                    if (Request["username"].Length == 0)
                        errDict.Add("username", "Username is required.");
                    if (Request["password"].Length == 0)
                        errDict.Add("password", "Password is required");
                }

                ViewData["errDict"] = errDict;
                return View();
            }
            //if username and password is entered
            if (username != null && password != null)
            {
                Customer customer = CustomerData.GetCustomerDetails(username);
                if (customer.Id == "FALSE")
                {
                    ViewBag.wrong = 0;
                    return View();
                }

                else
                {
                    //retrieving hashed password from database
                    byte[] hashbytes = Convert.FromBase64String(customer.Password);
                    byte[] salt = new byte[16];
                    Array.Copy(hashbytes, 0, salt, 0, 16);
                    var special = new Rfc2898DeriveBytes(password, salt, 10000);
                    byte[] hash = special.GetBytes(20);
                    for (int i = 0; i < 20; i++)
                    {
                        //if password enter does not match unhashed password
                        if (hashbytes[i + 16] != hash[i])
                        {
                            ViewBag.wrong = 0;
                            return View();
                        }
                    }
                    //if password matched, user will be issued Session object and directed to Products page
                    Session["username"] = username;
                    Session["sessionId"] = Guid.NewGuid().ToString();
                    Session["discount"] = 1.0;
                    List<Product> cart = new List<Product>();
                    Session["cart"] = cart;
                    return RedirectToAction("ListProducts", "Products");
                }
            }
            return View();
        }

        public ActionResult Forgot(string email)
        {
            if (email == null || email == "")
            {
                return View();
            }
            else
            {
                //checking if email is found in database
                if(CustomerData.CheckCustomerEmail(email) == true)
                {
                    Session["sessionid"] = Guid.NewGuid().ToString();
                    return RedirectToAction("Reset", "Home");
                }
                //return invalid email if not found
                else
                {
                    ViewBag.invalid = email;
                    return View();
                }
            }
        }

        [AuthorizationFilter]
        public ActionResult Reset()
        {
            Session.Clear();
            return View();
        }

        [AuthorizationFilter]
        public ActionResult Logout()
        {
            //clearing of session data when log out
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

    }
}