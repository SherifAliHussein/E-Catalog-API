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
    public interface ICategoryTranslationService:IService<CategoryTranslation>
    {
        bool CheckCategoryNameExistForMenu(string categoryName, string language, long categoryId, long menuId);
        PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize);
        bool CheckCategoryByLanguage(long categoryId, string language);
        PagedResultsDto GetActivatedCategoriesByMenuId(string language, long menuId, int page, int pageSize);
    }
}
