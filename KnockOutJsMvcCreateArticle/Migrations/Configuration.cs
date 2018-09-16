namespace KnockOutJsMvcCreateArticle.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using DotNetOpenAuth.AspNet;
    using Microsoft.Web.WebPages.OAuth;
    using WebMatrix.WebData;
    using KnockOutJsMvcCreateArticle.Filters;
    using KnockOutJsMvcCreateArticle.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<KnockOutJsMvcCreateArticle.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KnockOutJsMvcCreateArticle.Models.UsersContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (!Roles.RoleExists("admin"))
                Roles.CreateRole("admin");
            if (!Roles.RoleExists("view"))
                Roles.CreateRole("view");
            // This is needed for WebSecurity classes
            WebSecurity.InitializeDatabaseConnection("ArticleDBContexConnection", "UserProfile", "UserId", "UserName", true);
            if (!WebSecurity.UserExists("admin_user"))
                WebSecurity.CreateUserAndAccount("admin_user", "admin123");
            if (!WebSecurity.UserExists("view_user"))
                WebSecurity.CreateUserAndAccount("view_user", "view123");

            // Assign roles to users
            if (!Roles.GetRolesForUser("admin_user").Contains("admin"))
                Roles.AddUsersToRoles(new[] { "admin_user" }, new[] { "admin" });
            if (!Roles.GetRolesForUser("view_user").Contains("view"))
                Roles.AddUsersToRoles(new[] { "view_user" }, new[] { "view" });


        }
    }
}
