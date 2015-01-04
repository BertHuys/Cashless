namespace nmct.ba.cashlessproject.ITWeb.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using nmct.ba.cashlessproject.ITWeb.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<nmct.ba.cashlessproject.ITWeb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(nmct.ba.cashlessproject.ITWeb.Models.ApplicationDbContext context)
        {
            string roleAdmin = "Administrator";
            IdentityResult roleResult;

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!RoleManager.RoleExists(roleAdmin))
            {
                roleResult = RoleManager.Create(new IdentityRole(roleAdmin));
            }

            if (!context.Users.Any(u => u.Email.Equals("bert@berthuys.be")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Email = "bert@berthuys.be",
                    UserName = "bert@berthuys.be"
                };
                manager.Create(user, "8Zvv6d5lj?");
                manager.AddToRole(user.Id, roleAdmin);
            }
        }
    }
}