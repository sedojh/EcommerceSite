using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.ComponentModel.DataAnnotations;
using CAProject.DB;
using CAProject.Models;

namespace CAProject.Filters
{
    public class AuthorizationFilter: ActionFilterAttribute, IAuthorizationFilter
    {
        //check if sessionid is issued. sessionid issued upon log in
        public void OnAuthorization(AuthorizationContext ac)
        {
            string sessionId = (string)HttpContext.Current.Session["sessionId"];
           
            string[] parts = null;

            if (sessionId != null)
                parts = sessionId.Split('-');

            if (sessionId == null ||
                parts == null ||
                parts.Length != 5 ||
                parts[0].Length != 8 ||
                parts[1].Length != 4 ||
                parts[2].Length != 4 ||
                parts[3].Length != 4 ||
                parts[4].Length != 12)
            {
                ac.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "Login"
                    }));
            }
        }
    }
 
}