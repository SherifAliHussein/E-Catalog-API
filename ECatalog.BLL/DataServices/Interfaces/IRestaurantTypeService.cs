using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;

namespace ECatalog.BLL.DataService.Interfaces
{
    public interface IRestaurantTypeService:IService<RestaurantType>
    {
        long GetLastRecordId();
    }
}
