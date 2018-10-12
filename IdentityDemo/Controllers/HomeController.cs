using IdentityDemo.Infrastructure;
using IdentityDemo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityDemo.Controllers
{
    public class HomeController : Controller
    {
        private AppUserManager UserManager{
            get{
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppUser CurrentUser{
            get{
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }

        [Authorize]
        // GET: Home
        public ActionResult Index()
        {
            return View(GetData("Index"));            
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }
        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("User", HttpContext.User.Identity.Name);
            dict.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Auth Type", HttpContext.User.Identity.AuthenticationType);
            dict.Add("In Users Role", HttpContext.User.IsInRole("Users"));
            return dict;
        }
    }
}