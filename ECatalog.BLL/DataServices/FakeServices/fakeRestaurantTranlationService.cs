using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.DAL.Entities;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;

namespace ECatalog.BLL.DataServices.FakeServices
{
    public class fakeRestaurantTranlationService:Service<RestaurantTranslation>,IRestaurantTranslationService
    {
        private fakeData dbFakeData;

        public fakeRestaurantTranlationService()
        {
            dbFakeData = new fakeData();
        }
        public bool CheckRestaurantNameExist(string restaurantName, string language, long restaurantId)
        {
            return dbFakeData._RestaurantTranslations.Any(
                x => x.RestaurantName.ToLower() == restaurantName.ToLower() &&
                     x.Language.ToLower() == language.ToLower()&&
                     !x.Restaurant.IsDeleted && x.RestaurantId != restaurantId);
        }

        public RestaurantTranslation GetRestaurantTranslation(string language, long restaurantId)
        {
            return dbFakeData._RestaurantTranslations.FirstOrDefault(
                x => x.Language.ToLower() == language.ToLower() && x.RestaurantId == restaurantId);
        }

        public override void InsertRange(IEnumerable<RestaurantTranslation> entities)
        {
            dbFakeData._RestaurantTranslations.AddRange(entities);
        }
        public PagedResultsDto GetAllRestaurant(string language, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData._RestaurantTranslations.Count(x => !x.Restaurant.IsDeleted);
            List<Restaurant> restaurants;
            if (pageSize > 0)
                restaurants = dbFakeData._RestaurantTranslations.Where(x => !x.Restaurant.IsDeleted && x.Language.ToLower() == language.ToLower()).Select(x => x.Restaurant)
                    .OrderBy(x => x.RestaurantId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                restaurants = dbFakeData._RestaurantTranslations.Where(x => !x.Restaurant.IsDeleted && x.Language.ToLower() == language.ToLower()).Select(x => x.Restaurant)
                    .OrderBy(x => x.RestaurantId).ToList();
            results.Data = Mapper.Map<List<Restaurant>, List<RestaurantDTO>>(restaurants, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Restaurant restaurant in src)
                        {
                            restaurant.RestaurantTranslations = restaurant.RestaurantTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                            restaurant.RestaurantType.RestaurantTypeTranslations = restaurant.RestaurantType.RestaurantTypeTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }

        public bool CheckRestaurantByLanguage(long restaurantAdminId, string language)
        {
            return dbFakeData._RestaurantTranslations.Any(x => x.Restaurant.RestaurantAdminId == restaurantAdminId && x.Language.ToLower() == language.ToLower() && !x.Restaurant.IsDeleted);
        }
    }
}
