using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace ECatalog.BLL.DataServices
{
    public class RestaurantAdminService:Service<RestaurantAdmin>,IRestaurantAdminService
    {
        public RestaurantAdminService(IRepositoryAsync<RestaurantAdmin> repository):base(repository)
        {
            
        }
        public bool CheckUserNameDuplicated(string userName,long restaurantId)
        {
            return _repository.Queryable().Any(u => u.UserName.ToLower() == userName.ToLower() && u.RestaurantId != restaurantId && !u.IsDeleted);
        }
    }
}
