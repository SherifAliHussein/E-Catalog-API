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
    public interface ICategoryService:IService<Category>
    {
       // PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize);
    }
}
