using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.DAL.Entities;
using ECatalog.DAL.Entities.Model;

namespace ECatalog.BLL.DataServices.FakeServices
{
    public class fakeCategoryTranslationService:Service<CategoryTranslation>,ICategoryTranslationService
    {
        private fakeData dbFakeData;

        public fakeCategoryTranslationService()
        {
            dbFakeData = new fakeData();
        }
        public bool CheckCategoryNameExistForMenu(string categoryName, string language, long categoryId, long menuId)
        {

            return dbFakeData._CategoryTranslations
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.CategoryName.ToLower() == categoryName.ToLower() &&
                          x.CategoryId != categoryId && x.Category.MenuId == menuId);
        }

        public PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData._CategoryTranslations.Where(x => !x.Category.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category).Count(x => !x.IsDeleted);
            List<Category> categories;
            if (pageSize > 0)
                categories = dbFakeData._CategoryTranslations.Where(x => !x.Category.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                categories = dbFakeData._CategoryTranslations.Where(x => !x.Category.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).ToList();
            results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Category category in src)
                        {
                            category.CategoryTranslations = category.CategoryTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }

        public bool CheckCategoryByLanguage(long categoryId, string language)
        {
            return dbFakeData._CategoryTranslations.Any(x => x.CategoryId == categoryId && x.Language.ToLower() == language.ToLower() && !x.Category.IsDeleted);
        }

        public PagedResultsDto GetActivatedCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData._CategoryTranslations.Where(x => !x.Category.IsDeleted && x.Category.IsActive && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category).Count(x => !x.IsDeleted);
            List<Category> categories;
            if (pageSize > 0)
                categories = dbFakeData._CategoryTranslations.Where(x => !x.Category.IsDeleted && x.Category.IsActive && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                categories = dbFakeData._CategoryTranslations.Where(x => !x.Category.IsDeleted && x.Category.IsActive && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).ToList();
            results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Category category in src)
                        {
                            category.CategoryTranslations = category.CategoryTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }

        public override void InsertRange(IEnumerable<CategoryTranslation> entities)
        {
            dbFakeData._CategoryTranslations.AddRange(entities);
        }
    }
}
