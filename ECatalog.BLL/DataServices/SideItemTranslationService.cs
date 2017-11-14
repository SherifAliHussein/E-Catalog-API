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
    public class SideItemTranslationService:Service<SideItemTranslation>,ISideItemTranslationService
    {
        public SideItemTranslationService(IRepositoryAsync<SideItemTranslation> repository):base(repository)
        {
            
        }
        public PagedResultsDto GetAllSideItems(string language, long userId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.SideItem.IsDeleted && x.Language.ToLower() == language.ToLower() && x.SideItem.Restaurant.RestaurantAdminId == userId).Select(x => x.SideItem).Count(x => !x.IsDeleted);
            List<SideItem> sideItems;
            if (pageSize > 0)
                sideItems = _repository.Query(x => !x.SideItem.IsDeleted && x.Language.ToLower() == language.ToLower() && x.SideItem.Restaurant.RestaurantAdminId == userId).Select(x => x.SideItem)
                    .OrderBy(x => x.SideItemId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                sideItems = _repository.Query(x => !x.SideItem.IsDeleted && x.Language.ToLower() == language.ToLower() && x.SideItem.Restaurant.RestaurantAdminId == userId).Select(x => x.SideItem)
                    .OrderBy(x => x.SideItemId).ToList();
            results.Data = Mapper.Map<List<SideItem>, List<SideItemDTO>>(sideItems, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (SideItem sideItem in src)
                        {
                            sideItem.SideItemTranslations = sideItem.SideItemTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }

        public bool CheckSideItemNameExist(string sideItemName, string language, long sizeId, long restaurantAdminId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.SideItemName.ToLower() == sideItemName.ToLower() &&
                          !x.SideItem.IsDeleted && x.SideItemId != sizeId && x.SideItem.Restaurant.RestaurantAdminId == restaurantAdminId);
        }

        public bool CheckSideItemNameTranslated(string language, long sideItemId)
        {
            return !Queryable().Any(x => x.Language.ToLower() == language.ToLower() && !x.SideItem.IsDeleted && x.SideItemId == sideItemId);
        }
    }
}
