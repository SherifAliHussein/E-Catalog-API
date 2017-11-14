using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.Common;
using ECatalog.DAL.Entities.Model;

namespace ECatalog.DAL.Entities
{
    public class fakeData
    {
        public List<RestaurantType> _RestaurantTypes = new List<RestaurantType>();
        public List<RestaurantTypeTranslation> _RestaurantTypeTranslations = new List<RestaurantTypeTranslation>();
        public List<Restaurant> _Restaurants = new List<Restaurant>();
        public List<RestaurantTranslation> _RestaurantTranslations = new List<RestaurantTranslation>();
        public List<User> _Users = new List<User>();
        public List<Menu> _Menus = new List<Menu>();
        public List<MenuTranslation> _MenuTranslations = new List<MenuTranslation>();
        public List<Category> _Categories = new List<Category>();
        public List<CategoryTranslation> _CategoryTranslations = new List<CategoryTranslation>();
        public List<Item> _Items = new List<Item>();
        public List<ItemTranslation> _ItemTranslations = new List<ItemTranslation>();
        public List<RestaurantAdmin> _RestaurantAdmins = new List<RestaurantAdmin>();
        public List<Size> _Sizes= new List<Size>();
        public List<SizeTranslation> _SizeTranslations = new List<SizeTranslation>();
        public List<SideItem> _SideItems = new List<SideItem>();
        public List<SideItemTranslation> _SideItemTranslations = new List<SideItemTranslation>();
        public List<ItemSideItem> _ItemSideItems = new List<ItemSideItem>();
        public List<ItemSize> _ItemSizes = new List<ItemSize>();
        public List<RestaurantWaiter> RestaurantWaiters  = new List<RestaurantWaiter>();

        public fakeData()
        {
            AddRestaurantType();
            AddRestaurantTypeTranslation();
            AddUser();
            AddRestaurant();
            AddRestaurantTranslations();
            AddMenus();
            AddMenuTranslations();
            AddCategories();
            AddCategoryTranslations();
            AddItems();
            AddItemTranslations();

            AddSizes();
            AddSizeTranslations();
            AddSideItems();
            AddSideItemTranslations();
        }

        private void AddRestaurantType()
        {
            _RestaurantTypes.Add(new RestaurantType {RestaurantTypeId = 1});
            _RestaurantTypes.Add(new RestaurantType {RestaurantTypeId = 2});
        }

        private void AddRestaurantTypeTranslation()
        {
            _RestaurantTypeTranslations.Add(new RestaurantTypeTranslation
            {
                RestaurantType = _RestaurantTypes.Find(x => x.RestaurantTypeId == 1),
                Language = Strings.DefaultLanguage,
                TypeName = "chinese",
                RestaurantTypeId = 1,
                RestaurantTypeTranslationId = 1
            });
            _RestaurantTypeTranslations.Add(new RestaurantTypeTranslation
            {
                RestaurantType = _RestaurantTypes.Find(x => x.RestaurantTypeId == 1),
                Language = "ar-eg",
                TypeName = "صيني",
                RestaurantTypeId = 1,
                RestaurantTypeTranslationId = 2
            });

            _RestaurantTypes.First(x => x.RestaurantTypeId == 1).RestaurantTypeTranslations =
                _RestaurantTypeTranslations.Where(x => x.RestaurantTypeId == 1).ToList();
        }

        private void AddUser()
        {
            _Users.Add(new User
            {
                UserId = 1,
                UserName = "SuperAdmin",
                Password = "wArilz/QIT55GuLgpRQlCHX0lir/WTXM8yc33MPiN3Bl26dnvS752gHPadYZoL20",
                IsDeleted = false,
                Role = Enums.RoleType.GlobalAdmin
            });
            _Users.Add(new User
            {
               UserId = 2,
               UserName = "adminstartor",
               Password = "wArilz/QIT55GuLgpRQlCHX0lir/WTXM8yc33MPiN3Bl26dnvS752gHPadYZoL20",
               IsDeleted = false,
               Role = Enums.RoleType.RestaurantAdmin
            });
            _RestaurantAdmins.Add(new RestaurantAdmin
            {
                UserId = 2,
                UserName = "adminstartor",
                Password = "wArilz/QIT55GuLgpRQlCHX0lir/WTXM8yc33MPiN3Bl26dnvS752gHPadYZoL20",
                IsDeleted = false,
                Role = Enums.RoleType.RestaurantAdmin,
                //RestaurantId = 1
            });
        }
        private void AddRestaurant()
        {
            _Restaurants.Add(new Restaurant
            {
                IsActive = false,
                RestaurantType = _RestaurantTypes.FirstOrDefault(x=>x.RestaurantTypeId == 1),
                RestaurantTypeId = 1,
                RestaurantAdmin = _RestaurantAdmins.FirstOrDefault(x=>x.UserId == 2),
                RestaurantAdminId = 2,
                RestaurantId = 1
            });
            _Restaurants.Add(new Restaurant
            {
                IsActive = false,
                RestaurantType = _RestaurantTypes.FirstOrDefault(x => x.RestaurantTypeId == 1),
                RestaurantTypeId = 1,
                RestaurantAdmin = _RestaurantAdmins.FirstOrDefault(x => x.UserId == 2),
                RestaurantAdminId = 2,
                RestaurantId = 2
            });
        }

        private void AddRestaurantTranslations()
        {
            _RestaurantTranslations.Add(new RestaurantTranslation
            {
                Restaurant =  _Restaurants.FirstOrDefault(x=>x.RestaurantId == 1),
                RestaurantId = 1,
                Language = "en-US",
                RestaurantName = "restaurant1",
                RestaurantDescription = "descri",
                RestaurantTranslationId = 1
            });
            _RestaurantTranslations.Add(new RestaurantTranslation
            {
                Restaurant = _Restaurants.FirstOrDefault(x => x.RestaurantId == 1),
                RestaurantId = 1,
                Language = "ar-EG",
                RestaurantName = "مطعم1",
                RestaurantDescription = "descri",
                RestaurantTranslationId = 2
            });

            _RestaurantTranslations.Add(new RestaurantTranslation
            {
                Restaurant = _Restaurants.FirstOrDefault(x => x.RestaurantId == 1),
                RestaurantId = 2,
                Language = "en-US",
                RestaurantName = "restaurant2",
                RestaurantDescription = "descri",
                RestaurantTranslationId = 3
            });
            _RestaurantTranslations.Add(new RestaurantTranslation
            {
                Restaurant = _Restaurants.FirstOrDefault(x => x.RestaurantId == 2),
                RestaurantId = 2,
                Language = "ar-EG",
                RestaurantName = "مطعم2",
                RestaurantDescription = "descri",
                RestaurantTranslationId = 4
            });
            _Restaurants.FirstOrDefault(x => x.RestaurantId == 1).RestaurantTranslations =
                _RestaurantTranslations.Where(x => x.RestaurantId == 1).ToList();
            _Restaurants.FirstOrDefault(x => x.RestaurantId == 2).RestaurantTranslations =
                _RestaurantTranslations.Where(x => x.RestaurantId == 2).ToList();
        }

        private void AddMenus()
        {
            _Menus.Add(new Menu
            {
                Restaurant = _Restaurants.FirstOrDefault(x=>x.RestaurantId == 1),
                RestaurantId = 1,
                IsActive = true,
                MenuId = 1,
                
            });
            _Menus.Add(new Menu
            {
                Restaurant = _Restaurants.FirstOrDefault(x => x.RestaurantId == 2),
                RestaurantId = 2,
                IsActive = false,
                MenuId = 2,

            });
            _Restaurants.FirstOrDefault(x => x.RestaurantId == 1).Menus =
                _Menus.Where(x => x.RestaurantId == 1).ToList();
            _Restaurants.FirstOrDefault(x => x.RestaurantId == 2).Menus =
                _Menus.Where(x => x.RestaurantId == 2).ToList();
        }

        private void AddMenuTranslations()
        {
            _MenuTranslations.Add(new MenuTranslation
            {
                Menu = _Menus.FirstOrDefault(x=>x.MenuId == 1),
                MenuId = 1,
                Language = "en-US",
                MenuName = "Menu1",
                MenuTranslationId = 1
            });
            _MenuTranslations.Add(new MenuTranslation
            {
                Menu = _Menus.FirstOrDefault(x => x.MenuId == 2),
                MenuId = 2,
                Language = "en-US",
                MenuName = "Menu2",
                MenuTranslationId = 2
            });

            _Menus.FirstOrDefault(x => x.MenuId == 1).MenuTranslations =
                _MenuTranslations.Where(x => x.MenuId == 1).ToList();
            _Menus.FirstOrDefault(x => x.MenuId == 2).MenuTranslations =
                _MenuTranslations.Where(x => x.MenuId == 2).ToList();
        }

        private void AddCategories()
        {
            _Categories.Add(new Category
            {
                CategoryId = 1,
                IsActive = false,
                MenuId = 1,
                Menu = _Menus.FirstOrDefault(x=>x.MenuId == 1)
            });
            _Categories.Add(new Category
            {
                CategoryId = 2,
                IsActive = true,
                MenuId = 2,
                Menu = _Menus.FirstOrDefault(x => x.MenuId == 2)
            });
            _Categories.Add(new Category
            {
                CategoryId = 4,
                IsActive = false,
                MenuId = 2,
                Menu = _Menus.FirstOrDefault(x => x.MenuId == 2)
            });

            _Menus.FirstOrDefault(x => x.MenuId == 1).Categories = _Categories.Where(x => x.MenuId == 1).ToList();
            _Menus.FirstOrDefault(x => x.MenuId == 2).Categories = _Categories.Where(x => x.MenuId== 2).ToList();
        }

        private void AddCategoryTranslations()
        {
            _CategoryTranslations.Add(new CategoryTranslation
            {
                CategoryId = 1,
                Category = _Categories.FirstOrDefault(x=>x.CategoryId == 1),
                CategoryName = "category1",
                Language = "en-US",
                CategoryTranslationId = 1
            });
            _CategoryTranslations.Add(new CategoryTranslation
            {
                CategoryId = 2,
                Category = _Categories.FirstOrDefault(x => x.CategoryId == 2),
                CategoryName = "category2",
                Language = "en-US",
                CategoryTranslationId = 2
            });
            _CategoryTranslations.Add(new CategoryTranslation
            {
                CategoryId = 4,
                Category = _Categories.FirstOrDefault(x => x.CategoryId == 4),
                CategoryName = "category4",
                Language = "en-US",
                CategoryTranslationId = 3
            });

            _Categories.FirstOrDefault(x => x.CategoryId == 1).CategoryTranslations =
                _CategoryTranslations.Where(x => x.CategoryId == 1).ToList();
            _Categories.FirstOrDefault(x => x.CategoryId == 2).CategoryTranslations =
                _CategoryTranslations.Where(x => x.CategoryId == 2).ToList();
            _Categories.FirstOrDefault(x => x.CategoryId == 4).CategoryTranslations =
                _CategoryTranslations.Where(x => x.CategoryId == 4).ToList();
        }

        private void AddItems()
        {
            _Items.Add(new Item
            {
                Category = _Categories.FirstOrDefault(x=>x.CategoryId == 1),
                CategoryId =  1,
                ItemId = 1,
                //Price = 1.5
            });
            _Items.Add(new Item
            {
                Category = _Categories.FirstOrDefault(x => x.CategoryId == 2),
                CategoryId = 2,
                ItemId = 2,
                //Price = 1.5
            });
            _Categories.FirstOrDefault(x => x.CategoryId == 1).Items = _Items.Where(x => x.CategoryId == 1).ToList();
            _Categories.FirstOrDefault(x => x.CategoryId == 2).Items = _Items.Where(x => x.CategoryId == 2).ToList();
        }

        private void AddItemTranslations()
        {
            _ItemTranslations.Add(new ItemTranslation
            {
                Item = _Items.FirstOrDefault(x=>x.ItemId == 1),
                ItemId = 1,
                Language = "en-US",
                ItemName = "item1",
                ItemDescription = "desc1",
                ItemTranslationId = 1
            });
            _ItemTranslations.Add(new ItemTranslation
            {
                Item = _Items.FirstOrDefault(x => x.ItemId == 2),
                ItemId = 2,
                Language = "en-US",
                ItemName = "item2",
                ItemDescription = "desc2",
                ItemTranslationId = 2
            });


            _Items.FirstOrDefault(x => x.ItemId == 1).ItemTranslations =
                _ItemTranslations.Where(x => x.ItemId == 1).ToList();
            _Items.FirstOrDefault(x => x.ItemId == 2).ItemTranslations =
                _ItemTranslations.Where(x => x.ItemId == 2).ToList();
        }

        private void AddSizes()
        {
            _Sizes.Add(new Size
            {
                IsDeleted = false,
                SizeId = 1,
                RestaurantId = 1,
                Restaurant = _Restaurants.FirstOrDefault(x => x.RestaurantId == 1)
            });
            _Sizes.Add(new Size
            {
                IsDeleted = false,
                SizeId = 2,
                RestaurantId = 1,
                Restaurant = _Restaurants.FirstOrDefault(x => x.RestaurantId == 1)
            });

        }

        private void AddSizeTranslations()
        {
            _SizeTranslations.Add(new SizeTranslation
            {
                Language = Strings.DefaultLanguage,
                Size = _Sizes.FirstOrDefault(x => x.SizeId == 1),
                SizeId = 1,
                SizeName = "regualr",
                SizeTranslationId = 1
            });
            _SizeTranslations.Add(new SizeTranslation
            {
                Language = Strings.DefaultLanguage,
                Size = _Sizes.FirstOrDefault(x => x.SizeId == 2),
                SizeId = 2,
                SizeName = "Large",
                SizeTranslationId = 2
            });
            _Sizes.FirstOrDefault(x => x.SizeId == 1).SizeTranslations = _SizeTranslations.Where(x => x.SizeId == 1).ToList();
            _Sizes.FirstOrDefault(x => x.SizeId == 2).SizeTranslations = _SizeTranslations.Where(x => x.SizeId == 2).ToList();

        }

        private void AddSideItems()
        {
            _SideItems.Add(new SideItem
            {
                IsDeleted = false,
                SideItemId = 1,
                Value = 1,
                RestaurantId = 1,
                Restaurant = _Restaurants.FirstOrDefault(x => x.RestaurantId == 1)
            });
            _SideItems.Add(new SideItem
            {
                IsDeleted = false,
                SideItemId = 2,
                Value = 2,
                RestaurantId = 1,
                Restaurant = _Restaurants.FirstOrDefault(x => x.RestaurantId == 1)
            });
        }

        private void AddSideItemTranslations()
        {
            _SideItemTranslations.Add(new SideItemTranslation
            {
                Language = Strings.DefaultLanguage,
                SideItem = _SideItems.FirstOrDefault(x => x.SideItemId == 1),
                SideItemId = 1,
                SideItemName = "Fries",
                SideItemTranslationId = 1
            });
            _SideItemTranslations.Add(new SideItemTranslation
            {
                Language = Strings.DefaultLanguage,
                SideItem = _SideItems.FirstOrDefault(x => x.SideItemId == 2),
                SideItemId = 2,
                SideItemName = "Pasta",
                SideItemTranslationId = 2
            });
            _SideItems.FirstOrDefault(x => x.SideItemId == 1).SideItemTranslations = _SideItemTranslations.Where(x => x.SideItemId == 1).ToList();
            _SideItems.FirstOrDefault(x => x.SideItemId == 2).SideItemTranslations = _SideItemTranslations.Where(x => x.SideItemId == 2).ToList();

        }
    }
}
