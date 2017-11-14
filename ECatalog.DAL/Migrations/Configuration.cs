using ECatalog.Common;
using ECatalog.DAL.Entities.Model;

namespace ECatalog.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ECatalog.DAL.Entities.ECatalogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ECatalog.DAL.Entities.ECatalogContext context)
        {
            //context.Users.Add(new User
            //{
            //    IsDeleted = false,
            //    Password = "wArilz/QIT55GuLgpRQlCHX0lir/WTXM8yc33MPiN3Bl26dnvS752gHPadYZoL20",
            //    UserName = "Sherif",
            //    Role = Enums.RoleType.GlobalAdmin
            //});

            //context.Users.Add(new RestaurantWaiter()
            //{
            //    IsDeleted = false,
            //    Password = "wArilz/QIT55GuLgpRQlCHX0lir/WTXM8yc33MPiN3Bl26dnvS752gHPadYZoL20",
            //    UserName = "devWaiter",
            //    Role = Enums.RoleType.Waiter
            //});
        }
    }
}
