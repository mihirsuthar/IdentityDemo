using IdentityDemo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IdentityDemo.Infrastructure
{
    public class AppIdentityDbContext: IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(): base("IdentityDb")
        {}

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }


    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        private void PerformInitialSetup(AppIdentityDbContext context)
        {
            AppUserManager userManager = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Admins";
            string userName = "Administrator";
            string password = "Administrator123";
            string email = "administrator@gmail.com";
                        
            if (roleManager.RoleExists(roleName))
            {
                roleManager.Create(new AppRole(roleName));
            }

            AppUser user = userManager.FindByName(userName);
            if(user == null)
            {
                userManager.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userManager.FindByName(userName);
            }

            if(!userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, roleName);
            }

        }
    }
}