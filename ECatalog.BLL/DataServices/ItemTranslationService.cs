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
    public class ItemTranslationService:Service<ItemTranslation>,IitemTranslationService
    {
        public ItemTranslationService(IRepositoryAsync<ItemTranslation> repository):base(repository)
        {
            
        }
        public bool CheckItemNameExistForCategory(string itemName, string language, long itemId, long categoryId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.ItemName.ToLower() == itemName.ToLower() &&
                          x.ItemId != itemId && x.Item.CategoryId == categoryId && !x.Item.IsDeleted);
        }

        public PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Item.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item).Count(x => !x.IsDeleted);
            List<Item> items;
            if (pageSize > 0)
                items = _repository.Query(x => !x.Item.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item)
                    .OrderBy(x => x.ItemId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                items = _repository.Query(x => !x.Item.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item)
                    .OrderBy(x => x.ItemId).ToList();
            results.Data = Mapper.Map<List<Item>, List<ItemDTO>>(items, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Item item in src)
                        {
                            item.ItemTranslations = item.ItemTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                            //item.ItemSizes = item.ItemSizes
                            //    .Where(x => x.Size.SizeTranslations.FirstOrDefault(
                            //                    s => s.Language.ToLower() == language.ToLower()) != null).ToList();
                            //dest[0].
                        }

                    }
                );

                //opt.AfterMap((src, dest) =>
                //{
                //    dest.ForEach(dto => dto.Sizes = src.Select(x=>x.));
                //    for (int i = 0; i < dest.Count; i++)
                //    {
                //        dest[i].Sizes = src[i].ItemSizes.Where(x=>x.SizeId == dest[i].s)
                //    }
                //});
            });
            return results;
        }
        public PagedResultsDto GetActivatedItemsByCategoryId(string language, long categoryId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Item.IsDeleted && x.Item.IsActive && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item).Count(x => !x.IsDeleted);
            List<Item> items;
            if (pageSize > 0)
                items = _repository.Query(x => !x.Item.IsDeleted && x.Item.IsActive && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item)
                    .OrderBy(x => x.ItemId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                items = _repository.Query(x => !x.Item.IsDeleted && x.Item.IsActive && x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId).Select(x => x.Item)
                    .OrderBy(x => x.ItemId).ToList();
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
            return Mapper.Map<List<Item>, List<ItemNamesDto>>(
                _repository.Query(x => !x.Item.IsDeleted && x.Language.ToLower() == language.ToLower() &&
                                       x.Item.CategoryId == categoryId).Select(x => x.Item).OrderBy(x => x.CategoryId)
                    .ToList(),
                opt =>
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
        }
    }
}
