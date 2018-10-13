using IdentityDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityDemo.Controllers
{
    public class ClaimsController : Controller
    {
        [Authorize]
        // GET: Claims
        public ActionResult Index()
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;

            if(ident == null)
            {
                return View("Error", new string[] { "No claims found" });
            }
            else
            {
                return View(ident.Claims);
            }
        }

        //[Authorize(Roles = "VadodaraStaff")]
        [ClaimAccess(Issuer = "RemoteClaims", ClaimType = ClaimTypes.PostalCode, Value = "390022")]
        public string OtherAction()
        {
            return "This is protected action";
        }
    }
}