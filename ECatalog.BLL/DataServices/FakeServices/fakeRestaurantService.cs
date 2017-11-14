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
    public class fakeRestaurantService:Service<Restaurant>,IRestaurantService
    {
        private fakeData dbFakeData;

        public fakeRestaurantService()
        {
            dbFakeData = new fakeData();
        }

        public override void Insert(Restaurant entity)
        {
            entity.RestaurantId = 2;
            dbFakeData._Restaurants.Add(entity);
        }

        public PagedResultsDto GetAllRestaurant(string language, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData._Restaurants.Count(x=>!x.IsDeleted);
            results.Data = Mapper.Map<List<Restaurant>, List<RestaurantDTO>>(dbFakeData._Restaurants.Where(x => !x.IsDeleted).OrderBy(x => x.RestaurantId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList(), opt =>
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

        public Restaurant GetRestaurantByAdminId(long adminId)
        {
            return dbFakeData._Restaurants.FirstOrDefault(x => x.RestaurantAdminId == adminId);
        }

        public override Restaurant Find(params object[] keyValues)
        {
            return dbFakeData._Restaurants.FirstOrDefault(x => x.RestaurantId == (long)keyValues[0]);
        }

        public override void Update(Restaurant entity)
        {
            var restaurant = dbFakeData._Restaurants.FirstOrDefault(x => x.RestaurantId == entity.RestaurantId);
            restaurant = entity;
        }
    }
}
