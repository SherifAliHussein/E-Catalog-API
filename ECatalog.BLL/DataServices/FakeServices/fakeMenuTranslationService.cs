using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.DAL.Entities;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;
using AutoMapper;

namespace ECatalog.BLL.DataServices.FakeServices
{
    public class fakeMenuTranslationService:Service<MenuTranslation>,IMenuTranslationService
    {
        private fakeData dbFakeData;

        public fakeMenuTranslationService()
        {
            dbFakeData = new fakeData();
        }

        public bool CheckMenuNameExistForRestaurant(string menuName, string language, long menuId, long restaurantId)
        {

            return dbFakeData._MenuTranslations
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.MenuName.ToLower() == menuName.ToLower()&&
                          x.MenuId != menuId && x.Menu.RestaurantId == restaurantId);
        }

        public PagedResultsDto GetAllMenusByRestaurantAdminId(string language, long restaurantAdminId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData._MenuTranslations.Where(x => !x.Menu.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Menu.Restaurant.RestaurantAdminId == restaurantAdminId).Select(x => x.Menu).Count(x => !x.IsDeleted);
            List<Menu> menus;
            if (pageSize > 0)
                menus = dbFakeData._MenuTranslations.Where(x => !x.Menu.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Menu.Restaurant.RestaurantAdminId == restaurantAdminId).Select(x => x.Menu)
                    .OrderBy(x => x.MenuId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                menus = dbFakeData._MenuTranslations.Where(x => !x.Menu.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Menu.Restaurant.RestaurantAdminId == restaurantAdminId).Select(x => x.Menu)
                    .OrderBy(x => x.RestaurantId).ToList();
            results.Data = Mapper.Map<List<Menu>, List<MenuDTO>>(menus, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Menu menu in src)
                        {
                            menu.MenuTranslations = menu.MenuTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }

        public bool CheckMenuByLanguage(long menuId, string language)
        {
            return dbFakeData._MenuTranslations.Any(x => x.MenuId == menuId && x.Language.ToLower() == language.ToLower());
        }

        public PagedResultsDto GetActivatedMenusByRestaurantId(string language, long restaurantId, int page, int pageSize)
        {

            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData._MenuTranslations.Where(x => !x.Menu.IsDeleted && x.Menu.IsActive && x.Language.ToLower() == language.ToLower() && x.Menu.RestaurantId == restaurantId).Select(x => x.Menu).Count(x => !x.IsDeleted);
            List<Menu> menus;
            if (pageSize > 0)
                menus = dbFakeData._MenuTranslations.Where(x => !x.Menu.IsDeleted && x.Menu.IsActive && x.Language.ToLower() == language.ToLower() && x.Menu.RestaurantId == restaurantId).Select(x => x.Menu)
                    .OrderBy(x => x.MenuId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                menus = dbFakeData._MenuTranslations.Where(x => !x.Menu.IsDeleted && x.Menu.IsActive && x.Language.ToLower() == language.ToLower() && x.Menu.RestaurantId == restaurantId).Select(x => x.Menu)
                    .OrderBy(x => x.RestaurantId).ToList();
            results.Data = Mapper.Map<List<Menu>, List<MenuDTO>>(menus, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Menu menu in src)
                        {
                            menu.MenuTranslations = menu.MenuTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }

        public override void InsertRange(IEnumerable<MenuTranslation> entities)
        {
            dbFakeData._MenuTranslations.AddRange(entities);
        }
    }
}
