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
    public class fakeSizeTranslation:Service<SizeTranslation>,ISizeTranslationService
    {
        private fakeData dbFakeData;

        public fakeSizeTranslation()
        {
            dbFakeData = new fakeData();
        }

        public PagedResultsDto GetAllSizes(string language,long userId, int page, int pageSize)
        {PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData._SizeTranslations.Where(x => !x.Size.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Size.Restaurant.RestaurantAdminId == userId).Select(x => x.Size).Count(x => !x.IsDeleted);
            List<Size> menus;
            if (pageSize > 0)
                menus = dbFakeData._SizeTranslations.Where(x => !x.Size.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Size.Restaurant.RestaurantAdminId == userId).Select(x => x.Size)
                    .OrderBy(x => x.SizeId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                menus = dbFakeData._SizeTranslations.Where(x => !x.Size.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Size.Restaurant.RestaurantAdminId == userId).Select(x => x.Size)
                    .OrderBy(x => x.SizeId).ToList();
            results.Data = Mapper.Map<List<Size>, List<SizeDto>>(menus, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Size size in src)
                        {
                            size.SizeTranslations = size.SizeTranslations
                                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }

        public bool CheckSizeNameExist(string sizeName, string language, long sizeId,long restaurantAdminId)
        {
            return dbFakeData._SizeTranslations
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.SizeName.ToLower() == sizeName.ToLower() &&
                          !x.Size.IsDeleted && x.SizeId != sizeId && x.Size.Restaurant.RestaurantAdminId == restaurantAdminId);
        }

        public bool CheckSizeNameTranslated(string language, long sizeId)
        {
            return !dbFakeData._SizeTranslations.Any(x => x.Language.ToLower() == language.ToLower() && !x.Size.IsDeleted && x.SizeId == sizeId);
        }

        public override void InsertRange(IEnumerable<SizeTranslation> entities)
        {
            dbFakeData._SizeTranslations.AddRange(entities);
        }
    }
}
