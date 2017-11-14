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
     public class fakeMenuService:Service<Menu>,IMenuService
    {
        private fakeData dbFakeData;

        public fakeMenuService()
        {
            dbFakeData = new fakeData();
        }

        public override void Insert(Menu entity)
        {
            dbFakeData._Menus.Add(entity);
        }

        //public PagedResultsDto GetAllMenusByRestaurantId(string language, long restaurantId, int page, int pageSize)
        //{
        //    var query =dbFakeData._Menus.Where(x => x.RestaurantId == restaurantId);
        //    PagedResultsDto results = new PagedResultsDto();
        //    results.TotalCount = query.Select(x => x).Count();
        //    results.Data = Mapper.Map<List<Menu>, List<MenuDTO>>(query.OrderBy(x => x.MenuId).Skip((page - 1) * pageSize)
        //        .Take(pageSize).ToList(), opt =>
        //    {
        //        opt.BeforeMap((src, dest) =>
        //            {
        //                foreach (Menu menu in src)
        //                {
        //                    menu.MenuTranslations = menu.MenuTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();
        //                }

        //            }
        //        );
        //    });
        //    return results;
        //}

        public override Menu Find(params object[] keyValues)
        {
            return dbFakeData._Menus.FirstOrDefault(x => x.MenuId == (long)keyValues[0]);
        }

        public override void Update(Menu entity)
        {
            var menu = dbFakeData._Menus.FirstOrDefault(x => x.MenuId == entity.MenuId);
            menu = entity;
        }
    }
}
