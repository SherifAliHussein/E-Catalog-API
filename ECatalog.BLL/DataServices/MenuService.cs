using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace ECatalog.BLL.DataServices
{
    public class MenuService:Service<Menu>,IMenuService
    {
        public MenuService(IRepositoryAsync<Menu> repository) : base(repository)
        {
            
        }
        //public PagedResultsDto GetAllMenusByRestaurantId(string language, long restaurantId, int page, int pageSize)
        //{
        //    var query = Queryable().Where(x=>x.Restaurant.RestaurantAdminId== restaurantId);
        //    PagedResultsDto results = new PagedResultsDto();
        //    results.TotalCount = query.Select(x=>x).Count();
        //    results.Data = Mapper.Map<List<Menu>, List<MenuDTO>>(query.OrderBy(x=>x.MenuId).Skip((page - 1) * pageSize)
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

    }
}
