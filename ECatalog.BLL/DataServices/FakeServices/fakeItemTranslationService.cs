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
    public class fakeItemTranslationService:Service<ItemTranslation>,IitemTranslationService
    {
        private fakeData dbFakeData;
        public fakeItemTranslationService()
        {
            dbFakeData = new fakeData();
        }

        public bool CheckItemNameExistForCategory(string itemName, string language, long itemId, long categoryId)
        {
            return dbFakeData._ItemTranslations.Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.ItemName.ToLower() == itemName.ToLower() &&
                          x.ItemId != itemId && x.Item.CategoryId == categoryId);
        }

        public PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData._ItemTranslations.Where(x => !x.Item.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item).Count(x => !x.IsDeleted);
            List<Item> items;
            if (pageSize > 0)
                items = dbFakeData._ItemTranslations.Where(x => !x.Item.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item)
                    .OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                items = dbFakeData._ItemTranslations.Where(x => !x.Item.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item)
                    .OrderBy(x => x.CategoryId).ToList();
            results.Data = Mapper.Map<List<Item>, List<ItemDTO>>(items, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Item item in src)
                        {
                            item.ItemTranslations = item.ItemTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }

        public List<ItemNamesDto> GetAllItemNamesByCategoryId(string language, long categoryId)
        {
            return Mapper.Map<List<Item>, List<ItemNamesDto>>(dbFakeData._ItemTranslations.Where(x => !x.Item.IsDeleted && x.Language.ToLower() == language.ToLower() &&
                            x.Item.CategoryId == categoryId).Select(x => x.Item).OrderBy(x => x.CategoryId).ToList());
        }

        public PagedResultsDto GetActivatedItemsByCategoryId(string language, long categoryId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData._ItemTranslations.Where(x => !x.Item.IsDeleted && x.Item.IsActive && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item).Count(x => !x.IsDeleted);
            List<Item> items;
            if (pageSize > 0)
                items = dbFakeData._ItemTranslations.Where(x => !x.Item.IsDeleted && x.Item.IsActive && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item)
                    .OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                items = dbFakeData._ItemTranslations.Where(x => !x.Item.IsDeleted && x.Item.IsActive && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item)
                    .OrderBy(x => x.CategoryId).ToList();
            results.Data = Mapper.Map<List<Item>, List<ItemDTO>>(items, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Item item in src)
                        {
                            item.ItemTranslations = item.ItemTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }

        public override void InsertRange(IEnumerable<ItemTranslation> entities)
        {
            dbFakeData._ItemTranslations.AddRange(entities);
        }
        
    }
}
