using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DTOs;

namespace ECatalog.BLL.Services.Interfaces
{
    public interface ISideItemFacade
    {
        PagedResultsDto GetAllSideItems(string language,long userId, int page, int pageSize);
        void AddSideItem(SideItemDTO sideItemDto,long restaurantAdminId, string language);
        void DeleteSideItem(long sideItemId);
        SideItemDTO GetSideItem(long sideItemId, string language);
        void UpdateSideItem(SideItemDTO sideItemDto, long restaurantAdminId, string language);
    }
}
