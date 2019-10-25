using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAProject.Models;
using CAProject.DB;
using CAProject.Filters;

namespace CAProject.Controllers
{
    [AuthorizationFilter]
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult ListProducts(string product_id, string searchStr)
        {
            ViewBag.searchStr = Session["search"];
            //check if user adds anything to cart. If no items added, enter this condition
            if(product_id == null || product_id == "")
            {
                Session["search"] = searchStr;
                ViewBag.searchStr = searchStr;
                return View();
            }
            //if user add to cart, enter this condition. Redirect to action method for adding to cart
            else
            {
                Session["productadd"] = product_id;
                return RedirectToAction("AddProductToCart", "Products");
            }
            
        }
        public ActionResult AddProductToCart()
        {
            string productadd = (string)Session["productadd"];
            List<Product> cart = (List<Product>)Session["cart"];
            Product product = new Product();
            //getting the product information
            product = ProductData.GetProduct(productadd);
            //add product to cart list
            cart.Add(product);
            //store cart into session["cart"] so information can be stored
            Session["cart"] = cart;
            ViewBag.cart = cart;
            //return back to Products page
            return RedirectToAction("ListProducts", "Products");
        }

        public ActionResult ViewCart(string quantity,string product_id,string coupon)
        {
            //get the list of products in session["cart"]
            List<Product> cart = (List<Product>)Session["cart"];

            //if there are no items in the cart
            if(cart.Count == 0)
            {
                return View();
            }

            //if there are items in the cart
            else
            {
                if (quantity == null && product_id == null)//this means that the user has not changed any quantity in the view cart page
                {
                    ViewBag.cart = cart;
                    IEnumerable<Product> testcart = cart.Distinct();
                    List<Product> distinctcart = new List<Product>();
                    foreach (var p in testcart)
                    {
                        distinctcart.Add(p);
                    }
                    ViewBag.distinctcart = distinctcart;
                    Coupon couponobj = CouponData.GetCoupon(coupon);
                    Session["discount"] = couponobj.Discount;
                    Session["couponcode"] = couponobj.Couponcode;
                    ViewBag.discount = couponobj.Discount;
                    return View();
                }

                else //user has changed the quantity of product in the view cart page
                {
                    int qtyint = Convert.ToInt32(quantity);
                    int qtyofproducts = 0;
                    //qtyint is the new quantity and qtyofproducts is the original quantity in the cart
                    for (int i = 0; i < cart.Count; i++)
                    {
                        if (product_id == cart[i].Id)
                        {
                            qtyofproducts++;
                        }
                    }
                    //for an increase of the number of product
                    if (qtyint > qtyofproducts)
                    {
                        int add = qtyint - qtyofproducts;
                        for (int j = 0; j < add; j++)
                        {
                            Product addproduct = new Product();
                            addproduct = ProductData.GetProduct(product_id);
                            cart.Add(addproduct);
                        }
                    }
                    //for a decrease in the number of product
                    else
                    {
                        int minus = qtyofproducts - qtyint;
                        for (int j = 0; j < minus; j++)
                        {
                            Product minusproduct = new Product();
                            minusproduct = ProductData.GetProduct(product_id);
                            cart.Remove(minusproduct);
                        }
                    }
                    //check if cart is empty. If empty redirect to same controller to display another screen
                    if (cart.Count == 0)
                    {
                        return RedirectToAction("ViewCart", "Products");
                    }
                    //if cart is not empty, return updated view
                    else
                    {
                        ViewBag.cart = cart;
                        Session["cart"] = cart;
                        IEnumerable<Product> testcart = cart.Distinct();
                        List<Product> distinctcart = new List<Product>();
                        //distinctcart is the cart with no duplicates
                        foreach (var p in testcart)
                        {
                            distinctcart.Add(p);
                        }
                        ViewBag.distinctcart = distinctcart;
                        Coupon couponobj = CouponData.GetCoupon(coupon);
                        Session["discount"] = couponobj.Discount;
                        Session["couponcode"] = couponobj.Couponcode;
                        ViewBag.discount = couponobj.Discount;
                        return View();
                    }
                    
                }
            }
        }

        public ActionResult Checkout()
        {
            List<Product> cart = new List<Product>();
            //retrieve list of products in cart
            cart = (List<Product>)Session["cart"];

            //if no products in cart
            if(cart.Count == 0)
            {
                return RedirectToAction("ViewCart", "Products");
            }

            //if there are products in cart
            else
            {
                string username = (string)Session["username"];//get the username
                double discount = (double)Session["discount"];//get the discount if coupon code is entered
                string couponcode = (string)Session["couponcode"];//get couponcode
                Customer customer = CustomerData.GetCustomerDetails(username);//get customer info from username
                string order_id = OrderData.UpdateOrderDB(cart, customer.Id, discount, couponcode);//update the orders table in sql and returning the generated order_id
                OrderDetailsData.UpdateOrderDetailsDB(cart, customer.Id, order_id);//update orderdetails table in sql
                OrderQuantityData.UpdateOrderQuantityDB(cart, customer.Id, order_id);//update orderquantity table in sql
                Session["cart"] = new List<Product>();//reset the cart so cart is empty
                return RedirectToAction("MyPurchases", "MyPurchases");
            }    
        }
    }
}