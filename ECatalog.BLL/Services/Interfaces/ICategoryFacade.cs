using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DTOs;

namespace ECatalog.BLL.Services.Interfaces
{
    public interface ICategoryFacade
    {
        void AddCategory(CategoryDTO categoryDto, string language, string path);
        CategoryDTO GetCategory(long categoryId, string language);
        PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize);
        void ActivateCategory(long categoryId);
        void DeActivateCategory(long categoryId);
        void DeleteCategory(long categoryId);
        void UpdateCategory(CategoryDTO categoryDto, string language, string path);
        PagedResultsDto GetActivatedCategoriesByMenuId(string language, long menuId, int page, int pageSize);
    }
}
