using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DTOs;

namespace ECatalog.BLL.Services.Interfaces
{
    public interface IUserFacade
    {
        UserDto ValidateUser(string email, string password);
        UserDto GetUser(long UserId);
        void AddRestaurantWaiter(RestaurantWaiterDTO restaurantWaiterDto,long restaurantAdminId);
        PagedResultsDto GetAllRestaurantWaiters(long restaurantAdminId, int page, int pageSize);
        void UpdateRestaurantWaiter(RestaurantWaiterDTO restaurantWaiterDto);
        void DeleteRestaurantWaiter(long restaurantWaiterId);
        RestaurantWaiterDTO GetRestaurantWaiter(long waiterId);
    }
}
