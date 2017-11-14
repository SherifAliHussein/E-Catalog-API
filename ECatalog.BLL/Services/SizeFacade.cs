using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.BLL.Services.Interfaces;
using ECatalog.Common.CustomException;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace ECatalog.BLL.Services
{
    public class SizeFacade:BaseFacade,ISizeFacade
    {
        private ISizeService _sizeService;
        private IRestaurantService _restaurantService;
        private ISizeTranslationService _sizeTranslationService;
        public SizeFacade(ISizeService sizeService, ISizeTranslationService sizeTranslationService, IRestaurantService restaurantService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _sizeTranslationService = sizeTranslationService;
            _sizeService = sizeService;
            _restaurantService = restaurantService;

        }
        public SizeFacade(ISizeService sizeService, ISizeTranslationService sizeTranslationService, IRestaurantService restaurantService)
        {
            _sizeTranslationService = sizeTranslationService;
            _sizeService = sizeService;
            _restaurantService = restaurantService;

        }
        public PagedResultsDto GetAllSizes(string language,long userId, int page, int pageSize)
        {
            return _sizeTranslationService.GetAllSizes(language,userId, page, pageSize);
        }

        public void AddSize(SizeDto sizeDto,long restaurantAdminId, string language)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            ValidateSize(sizeDto, language, restaurantAdminId);
            var size = new Size();
            size.SizeTranslations.Add(new SizeTranslation
            {
                SizeName = sizeDto.SizeName,
                Language = language
            });
            size.RestaurantId = restaurant.RestaurantId;
            _sizeService.Insert(size);
            _sizeTranslationService.InsertRange(size.SizeTranslations);
            SaveChanges();
        }
        public void DeleteSize(long sizeId)
        {
            var size = _sizeService.Find(sizeId);
            if (size == null) throw new NotFoundException(ErrorCodes.SizeNotFound);
            size.IsDeleted = true;
            _sizeService.Update(size);
            SaveChanges();
        }
        public SizeDto GetSize(long sizeId, string language)
        {
            var size = _sizeService.Find(sizeId);
            if (size == null) throw new NotFoundException(ErrorCodes.SizeNotFound);
            if (size.IsDeleted) throw new NotFoundException(ErrorCodes.SizeDeleted);
            return Mapper.Map<Size, SizeDto>(size, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        src.SizeTranslations = src.SizeTranslations
                            .Where(x => x.Language.ToLower() == language.ToLower())
                            .ToList();
                    }
                );
            });
        }

        private void ValidateSize(SizeDto sizeDto,string language,long restaurantAdminId)
        {
            if (string.IsNullOrEmpty(sizeDto.SizeName)) throw new ValidationException(ErrorCodes.EmptySizeName);
            if (sizeDto.SizeName.Length > 100) throw new ValidationException(ErrorCodes.SizeNameExceedLength);
            if (sizeDto.SizeName.Length < 3) throw new ValidationException(ErrorCodes.SizeNameMinimumLength);
            if (_sizeTranslationService.CheckSizeNameExist(sizeDto.SizeName, language, sizeDto.SizeId, restaurantAdminId)) throw new ValidationException(ErrorCodes.SizeNameAlreadyExist);
        }

        public void UpdateSize(SizeDto sizeDto, long restaurantAdminId, string language)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            var size = _sizeService.Find(sizeDto.SizeId);
            if (size == null) throw new NotFoundException(ErrorCodes.SizeNotFound);
            ValidateSize(sizeDto, language, restaurantAdminId);
            var sizeTranslation = size.SizeTranslations.FirstOrDefault(x => x.Language.ToLower() == language.ToLower());
            if (sizeTranslation == null)
            {
                size.SizeTranslations.Add(new SizeTranslation
                {
                    Language = language,
                    SizeName = sizeDto.SizeName
                });
            }
            else
            {
                sizeTranslation.SizeName = sizeDto.SizeName;
            }
            size.RestaurantId = restaurant.RestaurantId;
            _sizeService.Update(size);
            SaveChanges();
        }
    }
}
