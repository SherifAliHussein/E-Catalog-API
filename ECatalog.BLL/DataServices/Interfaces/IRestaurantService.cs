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
    public interface IRestaurantService:IService<Restaurant>
    {
        PagedResultsDto GetAllRestaurant(string language, int page, int pageSize);
        Restaurant GetRestaurantByAdminId(long adminId);
    }
}
