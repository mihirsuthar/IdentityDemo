namespace IdentityDemo.Migrations
{
    using IdentityDemo.Infrastructure;
    using IdentityDemo.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityDemo.Infrastructure.AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "IdentityDemo.Infrastructure.AppIdentityDbContext";
        }

        protected override void Seed(IdentityDemo.Infrastructure.AppIdentityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            AppUserManager userManager = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Admin";
            string userName = "Administrator";
            string password = "Administrator123";
            string email = "administrator@gmail.com";

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new AppRole(roleName));
            }

            AppUser user = userManager.FindByName(userName);
            if (user == null)
            {
                userManager.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userManager.FindByName(userName);
            }

            if (!userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, roleName);
            }

            foreach(AppUser dbuser in userManager.Users)
            {
                dbuser.City = "Baroda";
            }

            context.SaveChanges();
        }
    }
}
