using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DTOs;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;

namespace ECatalog.BLL.DataServices.Interfaces
{
    public interface IRestaurantWaiterService:IService<RestaurantWaiter>
    {
        bool CheckUserNameDuplicated(string userName, long restaurantId);
        PagedResultsDto GetAllRestaurantWaiters(long restaurantId, int page, int pageSize);
    }
}
