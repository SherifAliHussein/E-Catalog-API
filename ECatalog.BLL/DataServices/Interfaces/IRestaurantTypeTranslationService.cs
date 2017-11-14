using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;

namespace ECatalog.BLL.DataServices.Interfaces
{
    public interface IRestaurantTypeTranslationService: IService<RestaurantTypeTranslation>
    {
        bool CheckRepeatedType(string typeName, string language, long restaurantTypeId);
        List<RestaurantTypeTranslation> GeRestaurantTypeTranslation(string language);
    }
}
