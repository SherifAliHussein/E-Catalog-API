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
    public interface ISideItemTranslationService:IService<SideItemTranslation>
    {
        PagedResultsDto GetAllSideItems(string language, long userId, int page, int pageSize);
        bool CheckSideItemNameExist(string sideItemName, string language, long sizeId, long restaurantAdminId);
        bool CheckSideItemNameTranslated(string language, long sideItemId);
    }
}
