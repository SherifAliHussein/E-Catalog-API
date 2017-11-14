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
    public interface IMenuTranslationService:IService<MenuTranslation>
    {
        bool CheckMenuNameExistForRestaurant(string menuName, string language, long menuId, long restaurantId);
        PagedResultsDto GetAllMenusByRestaurantAdminId(string language, long restaurantAdminId, int page, int pageSize);
        bool CheckMenuByLanguage(long menuId, string language);
        PagedResultsDto GetActivatedMenusByRestaurantId(string language, long restaurantId, int page, int pageSize);
    }
}
