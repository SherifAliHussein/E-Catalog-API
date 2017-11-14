using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.DAL.Entities.Model;
using ECatalog.DAL.Migrations;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities
{
    public class ECatalogContext:DataContext
    {
        public DbSet<User> Users { get; set; }
        //public DbSet<RestaurantAdmin> RestaurantAdmins { get; set; }
        //public DbSet<RestaurantWaiter> RestaurantWaiters { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations{ get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemTranslation> ItemTranslations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuTranslation> MenuTranslations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantTranslation> RestaurantTranslations { get; set; }
        public DbSet<RestaurantType> RestaurantTypes { get; set; }
        public DbSet<RestaurantTypeTranslation> RestaurantTypeTranslations { get; set; }

        public DbSet<RefreshToken> RefreshTokens{ get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeTranslation> SizeTranslations { get; set; }

        public DbSet<SideItem> SideItems { get; set; }
        public DbSet<SideItemTranslation> SideItemTranslations{ get; set; }
        public DbSet<ItemSideItem> ItemSideItems { get; set; }
        public DbSet<ItemSize> ItemSizes { get; set; }
        public ECatalogContext() : base("name=ECatalogDB")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ECatalogContext, Configuration>());
            Database.SetInitializer<ECatalogContext>(null);


        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ItemSideItem>()
                .HasRequired(c => c.SideItem)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ItemSize>()
                .HasRequired(c => c.Size)
                .WithMany()
                .WillCascadeOnDelete(false);


            //modelBuilder.Entity<RestaurantWaiter>()
            //    .HasRequired(c => c.Restaurant)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}
