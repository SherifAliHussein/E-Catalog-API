using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace ECatalog.BLL.DataServices
{
    public class RestaurantService:Service<Restaurant>,IRestaurantService
    {
        public RestaurantService(IRepositoryAsync<Restaurant> repository) : base(repository)
        {
            _repository = repository;
        }

        public PagedResultsDto GetAllRestaurant(string language, int page, int pageSize)
        {

            //var query = Query(x => x.Language == "").Select(x => x.Restaurant);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Queryable().Count(x=>!x.IsDeleted);
            results.Data = Mapper.Map<List<Restaurant>, List<RestaurantDTO>>(_repository.Query(x => !x.IsDeleted).Select().OrderBy(x => x.RestaurantId).Skip((page - 1) * pageSize)
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
            return _repository.Query(x => x.RestaurantAdminId == adminId).Select().FirstOrDefault();
        }
    }
}
