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
    public interface IitemTranslationService:IService<ItemTranslation>
    {
        bool CheckItemNameExistForCategory(string itemName, string language, long itemId, long categoryId);
        PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize);
        List<ItemNamesDto> GetAllItemNamesByCategoryId(string language, long categoryId);
        PagedResultsDto GetActivatedItemsByCategoryId(string language, long categoryId, int page, int pageSize);
    }
}
