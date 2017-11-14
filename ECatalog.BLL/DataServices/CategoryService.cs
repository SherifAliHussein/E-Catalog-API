using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace ECatalog.BLL.DataServices
{
    public class CategoryService:Service<Category>,ICategoryService
    {
        public CategoryService(IRepositoryAsync<Category> repository):base(repository)
        {
            
        }
        //public PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        //{
        //    var query = Queryable().Where(x => x.MenuId == menuId);
        //    PagedResultsDto results = new PagedResultsDto();
        //    results.TotalCount = query.Select(x => x).Count();
        //    results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(query.OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
        //        .Take(pageSize).ToList(), opt =>
        //    {
        //        opt.BeforeMap((src, dest) =>
        //            {
        //                foreach (Category category in src)
        //                {
        //                    category.CategoryTranslations = category.CategoryTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();
        //                }

        //            }
        //        );
        //    });
        //    return results;
        //}
    }
}
