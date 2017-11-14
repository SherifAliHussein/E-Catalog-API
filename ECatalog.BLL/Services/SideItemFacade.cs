using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.Common.CustomException;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace ECatalog.BLL.Services.Interfaces
{
    public class SideItemFacade:BaseFacade,ISideItemFacade
    {
        private ISideItemService _sideItemService;
        private ISideItemTranslationService _sideItemTranslationService;
        private IRestaurantService _restaurantService;

        public SideItemFacade(ISideItemService sideItem, ISideItemTranslationService sideItemTranslationService, IRestaurantService restaurantService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _sideItemTranslationService = sideItemTranslationService;
            _sideItemService = sideItem;
            _restaurantService = restaurantService;

        }
        public SideItemFacade(ISideItemService sizeService, ISideItemTranslationService sideItemTranslationService, IRestaurantService restaurantService)
        {
            _sideItemTranslationService = sideItemTranslationService;
            _sideItemService = sizeService;
            _restaurantService = restaurantService;

        }
        public PagedResultsDto GetAllSideItems(string language, long userId, int page, int pageSize)
        {
            return _sideItemTranslationService.GetAllSideItems(language,userId, page, pageSize);
        }

        public void AddSideItem(SideItemDTO sideItemDto,long restaurantAdminId, string language)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            ValidateSideItem(sideItemDto, language, restaurantAdminId);
            var sideItem = new SideItem();
            sideItem.SideItemTranslations.Add(new SideItemTranslation
            {
                SideItemName = sideItemDto.SideItemName,
                Language = language
            });
            sideItem.Value = sideItemDto.Value;
            sideItem.RestaurantId = restaurant.RestaurantId;
            _sideItemService.Insert(sideItem);
            _sideItemTranslationService.InsertRange(sideItem.SideItemTranslations);
            SaveChanges();
        }
        public void DeleteSideItem(long sideItemId)
        {
            var sideItem = _sideItemService.Find(sideItemId);
            if (sideItem == null) throw new NotFoundException(ErrorCodes.SideItemNotFound);
            sideItem.IsDeleted = true;
            _sideItemService.Update(sideItem);
            SaveChanges();
        }
        public SideItemDTO GetSideItem(long sideItemId, string language)
        {
            var sideItem = _sideItemService.Find(sideItemId);
            if (sideItem == null) throw new NotFoundException(ErrorCodes.SideItemNotFound);
            if (sideItem.IsDeleted) throw new NotFoundException(ErrorCodes.SideItemDeleted);
            return Mapper.Map<SideItem, SideItemDTO>(sideItem, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        src.SideItemTranslations = src.SideItemTranslations
                            .Where(x => x.Language.ToLower() == language.ToLower())
                            .ToList();
                    }
                );
            });
        }

        private void ValidateSideItem(SideItemDTO sideItemDto, string language, long restaurantAdminId)
        {
            if (string.IsNullOrEmpty(sideItemDto.SideItemName)) throw new ValidationException(ErrorCodes.EmptySideItemName);
            if (sideItemDto.SideItemName.Length > 100) throw new ValidationException(ErrorCodes.SideItemNameExceedLength);
            if (sideItemDto.SideItemName.Length < 3) throw new ValidationException(ErrorCodes.SideItemNameMinimumLength);
            if (sideItemDto.Value <= 0) throw new ValidationException(ErrorCodes.InvalidSideItemValue);
            if (_sideItemTranslationService.CheckSideItemNameExist(sideItemDto.SideItemName, language, sideItemDto.SideItemId, restaurantAdminId)) throw new ValidationException(ErrorCodes.SideItemNameAlreadyExist);
        }

        public void UpdateSideItem(SideItemDTO sideItemDto,long restaurantAdminId, string language)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            var sideItem = _sideItemService.Find(sideItemDto.SideItemId);
            if (sideItem == null) throw new NotFoundException(ErrorCodes.SizeNotFound);
            ValidateSideItem(sideItemDto, language, restaurantAdminId);
            var sizeTranslation = sideItem.SideItemTranslations.FirstOrDefault(x => x.Language.ToLower() == language.ToLower());
            sideItem.Value = sideItemDto.Value;
            if (sizeTranslation == null)
            {
                sideItem.SideItemTranslations.Add(new SideItemTranslation
                {
                    Language = language,
                    SideItemName = sideItemDto.SideItemName
                });
            }
            else
            {
                sizeTranslation.SideItemName = sideItemDto.SideItemName;
            }
            sideItem.RestaurantId = restaurant.RestaurantId;
            _sideItemService.Update(sideItem);
            SaveChanges();
        }
    }
}
