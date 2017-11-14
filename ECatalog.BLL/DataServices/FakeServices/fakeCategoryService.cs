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
using Service.Pattern;

namespace ECatalog.BLL.DataServices.FakeServices
{
    public class fakeCategoryService:Service<Category>,ICategoryService
    {
        private fakeData dbFakeData;
        public fakeCategoryService()
        {
            dbFakeData = new fakeData();
        }

        public override void Insert(Category entity)
        {
            dbFakeData._Categories.Add(entity);
        }

        public override void Update(Category entity)
        {
            var category = dbFakeData._Categories.FirstOrDefault(x => x.CategoryId == entity.CategoryId);
            category = entity;
        }

        //public PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        //{
        //    var query = dbFakeData._Categories.Where(x => x.MenuId == menuId);
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

        public override Category Find(params object[] keyValues)
        {
            return dbFakeData._Categories.FirstOrDefault(x => x.CategoryId == (long)keyValues[0]);
        }
    }
}
