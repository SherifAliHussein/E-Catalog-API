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
    public class ItemService:Service<Item>,IitemService
    {
        public ItemService(IRepositoryAsync<Item> repository):base(repository)
        {
            
        }
        public PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize)
        {
            var query = Queryable().Where(x => x.CategoryId == categoryId);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();
            results.Data = Mapper.Map<List<Item>, List<ItemTranslation>>(query.OrderBy(x => x.ItemId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList(), opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Item menu in src)
                        {
                            menu.ItemTranslations = menu.ItemTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }
    }
}
