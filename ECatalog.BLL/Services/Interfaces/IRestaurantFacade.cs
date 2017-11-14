using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DTOs;

namespace ECatalog.BLL.Services.Interfaces
{
    public interface IRestaurantFacade
    {
        List<RestaurantTypeDto> GetAllRestaurantType(string language);
        bool AddRestaurantType(RestaurantTypeDto restaurantTypeDto, string language);
        void UpdateRestaurantType(RestaurantTypeDto restaurantTypeDto, string language);
        void AddRestaurant(RestaurantDTO restaurantDto,string language,string path);
        RestaurantDTO GetRestaurant(long restaurantId, string language);
        PagedResultsDto GetAllRestaurant(string language,int page,int pageSize);
        void ActivateRestaurant(long restaurantId);
        void DeActivateRestaurant(long restaurantId);
        void DeleteRestaurant(long restaurantId);
        void UpdateRestaurant(RestaurantDTO restaurantDto, string language, string path);
        void DeleteRestaurantType(long restaurantTypeId);
        RestaurantDTO CheckRestaurantReady(long restaurantAdminId);
        void PublishRestaurant(long restaurantAdminId);
    }
}
