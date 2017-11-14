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
    public class fakeItemService:Service<Item>,IitemService
    {
        private fakeData dbFakeData;
        public fakeItemService()
        {
            dbFakeData = new fakeData();
        }

        public override void Insert(Item entity)
        {
            dbFakeData._Items.Add(entity);
        }

        public override Item Find(params object[] keyValues)
        {
            return dbFakeData._Items.FirstOrDefault(x => x.ItemId == (long)keyValues[0]);
        }

        public override void Update(Item entity)
        {
            var item = dbFakeData._Items.FirstOrDefault(x => x.ItemId == entity.ItemId);
            item = entity;
        }

        public PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize)
        {
            var query = dbFakeData._Items.Where(x => x.CategoryId == categoryId);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();
            results.Data = Mapper.Map<List<Item>, List<ItemDTO>>(query.OrderBy(x => x.ItemId).Skip((page - 1) * pageSize)
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
