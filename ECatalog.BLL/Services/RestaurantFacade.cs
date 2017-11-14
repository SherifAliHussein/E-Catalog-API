using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataService.Interfaces;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.BLL.Services.Interfaces;
using ECatalog.BLL.Services.ManageStorage;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using ECatalog.DAL.Entities.Model;
using Microsoft.Practices.ObjectBuilder2;
using Repository.Pattern.UnitOfWork;

namespace ECatalog.BLL.Services
{
    public class RestaurantFacade:BaseFacade,IRestaurantFacade
    {
        private IRestaurantTypeService _restaurantTypeService;
        private IRestaurantTypeTranslationService _restaurantTypeTranslationService;
        private IRestaurantService _restaurantService;
        private IRestaurantTranslationService _restaurantTranslationService;
        private IUserService _userService;
        private IRestaurantAdminService _restaurantAdminService;
        private IManageStorage _manageStorage;

        public RestaurantFacade(IRestaurantTypeService restaurantTypeService, IRestaurantTypeTranslationService restaurantTypeTranslationService
            , IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService, IUserService userService,IRestaurantAdminService restaurantAdminService
            ,IManageStorage manageStorage, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _restaurantTypeService = restaurantTypeService;
            _restaurantTypeTranslationService = restaurantTypeTranslationService;
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _userService = userService;
            _restaurantAdminService = restaurantAdminService;
            _manageStorage = manageStorage;
        }

        public RestaurantFacade(IRestaurantTypeService restaurantTypeService, IRestaurantTypeTranslationService restaurantTypeTranslationService,
            IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService, IUserService userService,IRestaurantAdminService restaurantAdminService, IManageStorage manageStorage)
        {
            _restaurantTypeService = restaurantTypeService;
            _restaurantTypeTranslationService = restaurantTypeTranslationService;
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _userService = userService;
            _restaurantAdminService = restaurantAdminService;
            _manageStorage = manageStorage;
        }

        public List<RestaurantTypeDto> GetAllRestaurantType(string language)
        {
            return Mapper.Map<List<RestaurantTypeDto>>(
                _restaurantTypeTranslationService.GeRestaurantTypeTranslation(language));
        }

        public bool AddRestaurantType(RestaurantTypeDto restaurantTypeDto, string language)
        {
            ValidateRestaurantType(restaurantTypeDto);
            RestaurantType restaurantType = new RestaurantType();
            restaurantType.RestaurantTypeId = _restaurantTypeService.GetLastRecordId() + 1;
            restaurantType.RestaurantTypeTranslations = new List<RestaurantTypeTranslation>();
            //foreach (var type in restaurantTypeDto.TypeName)
            //{
                if (_restaurantTypeTranslationService.CheckRepeatedType(restaurantTypeDto.TypeName, language,restaurantType.RestaurantTypeId))
                {
                    throw new ValidationException(ErrorCodes.RestaurantTypeAlreadyExist);
                }
                restaurantType.RestaurantTypeTranslations.Add(new RestaurantTypeTranslation
                {
                    Language = language,
                    TypeName = restaurantTypeDto.TypeName,
                    RestaurantTypeId = restaurantType.RestaurantTypeId

                });
            //}
            
            _restaurantTypeService.Insert(restaurantType);
            _restaurantTypeTranslationService.InsertRange(restaurantType.RestaurantTypeTranslations);
            SaveChanges();
            return true;
        }

        public void UpdateRestaurantType(RestaurantTypeDto restaurantTypeDto,string language)
        {
            ValidateRestaurantType(restaurantTypeDto);
            var restaurantType = _restaurantTypeService.Find(restaurantTypeDto.RestaurantTypeId);
            if (restaurantType == null) throw new NotFoundException(ErrorCodes.RestaurantTypeNotFound);
            //foreach (var type in restaurantTypeDto.TypeName)
            //{
            if (_restaurantTypeTranslationService.CheckRepeatedType(restaurantTypeDto.TypeName, language, restaurantType.RestaurantTypeId))
            {
                throw new ValidationException(ErrorCodes.RestaurantTypeAlreadyExist);
            }
            var restaurantTypeTranslation =
                    restaurantType.RestaurantTypeTranslations.FirstOrDefault(x => x.Language.ToLower() == language.ToLower());
                if (restaurantTypeTranslation != null) restaurantTypeTranslation.TypeName = restaurantTypeDto.TypeName;
                else restaurantType.RestaurantTypeTranslations.Add(new RestaurantTypeTranslation
                {
                    Language = language,
                    TypeName = restaurantTypeDto.TypeName,
                    RestaurantTypeId = restaurantType.RestaurantTypeId
                });
           // }
            _restaurantTypeService.Update(restaurantType);
            SaveChanges();
        }
        private void ValidateRestaurantType(RestaurantTypeDto restaurantTypeDto)
        {
            if (string.IsNullOrEmpty(restaurantTypeDto.TypeName)) throw new ValidationException(ErrorCodes.EmptyRestaurantType);
            if (restaurantTypeDto.TypeName.Length > 300)
                throw new ValidationException(ErrorCodes.RestaurantTypeExceedLength);
            
        }

        public void DeleteRestaurantType(long restaurantTypeId)
        {
            var restaurantType = _restaurantTypeService.Find(restaurantTypeId);
            if (restaurantType == null) throw new NotFoundException(ErrorCodes.RestaurantTypeNotFound);
            restaurantType.IsDeleted = true;
            foreach (var type in restaurantType.Restaurants)
            {
                type.IsDeleted = true;
            }
            _restaurantTypeService.Update(restaurantType);
            
            SaveChanges();
        }

        public void AddRestaurant(RestaurantDTO restaurantDto,string language,string path)
        {
            ValidateRestaurant(restaurantDto, language);

            var restaurantType = _restaurantTypeService.Find(restaurantDto.RestaurantTypeId);
            if (Strings.SupportedLanguages.Any(x => !restaurantType.RestaurantTypeTranslations.Select(m => m.Language.ToLower())
                    .Contains(x.ToLower())))
                throw new ValidationException(ErrorCodes.RestaurantTypeIsNotTranslated);

            Restaurant restaurant = new Restaurant
            {
                RestaurantTypeId = restaurantDto.RestaurantTypeId,
                IsActive = false,
                RestaurantTranslations = new List<RestaurantTranslation>()
               
            };
            restaurant.RestaurantTranslations.Add(new RestaurantTranslation
                {
                    Language = language,
                    RestaurantName = restaurantDto.RestaurantName,
                    RestaurantDescription = restaurantDto.RestaurantDescription
                });

            restaurant.RestaurantAdmin = new RestaurantAdmin
            {
                UserName = restaurantDto.RestaurantAdminUserName,
                Password = PasswordHelper.Encrypt(restaurantDto.RestaurantAdminPassword),
                Role = Enums.RoleType.RestaurantAdmin,
                IsDeleted = false
            };
            //_userService.Insert(restaurant.RestaurantAdmin);
            _restaurantAdminService.Insert(restaurant.RestaurantAdmin);
            _restaurantTranslationService.InsertRange(restaurant.RestaurantTranslations);
            _restaurantService.Insert(restaurant);
            SaveChanges();
            restaurant.RestaurantAdmin.RestaurantId = restaurant.RestaurantId;
            _restaurantAdminService.Update(restaurant.RestaurantAdmin);
            SaveChanges();
            _manageStorage.UploadImage(path+"\\" +restaurant.RestaurantId,restaurantDto.Image,restaurant.RestaurantId);
        }

        public RestaurantDTO GetRestaurant(long restaurantId, string language)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new NotFoundException(ErrorCodes.RestaurantDeleted);
            //var restaurantAdmin = _restaurantAdminService.Find(restaurant.RestaurantAdminId);
            //if(restaurantAdmin == null) throw new NotFoundException(ErrorCodes.RestaurantAdminNotFound);
            var restaurantdto = Mapper.Map<Restaurant, RestaurantDTO>(restaurant, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        src.RestaurantTranslations = src.RestaurantTranslations
                            .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        src.RestaurantType.RestaurantTypeTranslations = src.RestaurantType.RestaurantTypeTranslations
                            .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                    }
                );
            });
            //restaurantdto.RestaurantAdminPassword = restaurantAdmin.UserName;
            //restaurantdto.RestaurantAdminPassword = restaurantAdmin.Password;
            return restaurantdto;
        }
        private void ValidateRestaurant(RestaurantDTO restaurantDto,string language)
        {
            
            if (string.IsNullOrEmpty(restaurantDto.RestaurantName)) throw new ValidationException(ErrorCodes.EmptyRestaurantName);
            if (restaurantDto.RestaurantName.Length > 300) throw new ValidationException(ErrorCodes.RestaurantNameExceedLength);
            if (string.IsNullOrEmpty(restaurantDto.RestaurantDescription)) throw new ValidationException(ErrorCodes.EmptyRestaurantDescription);
            if (string.IsNullOrEmpty(restaurantDto.RestaurantAdminUserName))
                throw new ValidationException(ErrorCodes.EmptyRestaurantAdminUserName);
            if (string.IsNullOrEmpty(restaurantDto.RestaurantAdminPassword)) throw new ValidationException(ErrorCodes.EmptyRestaurantAdminPassword);
            if (restaurantDto.RestaurantAdminPassword.Length < 8 || restaurantDto.RestaurantAdminPassword.Length > 25) throw new ValidationException(ErrorCodes.RestaurantAdminPasswordLengthNotMatched);
            if(_restaurantTranslationService.CheckRestaurantNameExist(restaurantDto.RestaurantName, language,restaurantDto.RestaurantId)) throw new ValidationException(ErrorCodes.RestaurantNameAlreadyExist);
            if(_restaurantAdminService.CheckUserNameDuplicated(restaurantDto.RestaurantAdminUserName,restaurantDto.RestaurantId)) throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
            if (_userService.CheckUserNameDuplicated(restaurantDto.RestaurantAdminUserName)) throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
        }

        public PagedResultsDto GetAllRestaurant(string language,int page,int pageSize)
        {
                return _restaurantTranslationService.GetAllRestaurant(language, page, pageSize);
        }

        public void ActivateRestaurant(long restaurantId)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if(!restaurant.IsReady) throw  new ValidationException(ErrorCodes.RestaurantIsNotReady);
            if (restaurant.Menus.Count(m => m.IsActive) <= 0)
                throw new ValidationException(ErrorCodes.RestaurantMenuDoesNotActivated);
            //if(restaurant.RestaurantType.RestaurantTypeTranslations.Select(t=>t.Language))
            //if (Strings.SupportedLanguages.Any(x => !restaurant.RestaurantTranslations.Select(m => m.Language.ToLower())
            //    .Contains(x.ToLower())))
            //    throw new ValidationException(ErrorCodes.CategoryIsNotTranslated);
            restaurant.IsActive = true;
            _restaurantService.Update(restaurant);
            SaveChanges();
        }

        public void DeActivateRestaurant(long restaurantId)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            restaurant.IsActive = false;
            _restaurantService.Update(restaurant);
            SaveChanges();
        }
        public void DeleteRestaurant(long restaurantId)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            restaurant.IsDeleted = true;
            restaurant.RestaurantAdmin.IsDeleted = true;
            restaurant.RestaurantWaiters.ForEach(x => x.IsDeleted = true);
            _restaurantService.Update(restaurant);
            SaveChanges();
        }

        public void UpdateRestaurant(RestaurantDTO restaurantDto, string language, string path)
        {
            Restaurant restaurant = _restaurantService.Find(restaurantDto.RestaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            ValidateRestaurant(restaurantDto, language);
            var restaurantType = _restaurantTypeService.Find(restaurantDto.RestaurantTypeId);
            if (Strings.SupportedLanguages.Any(x => !restaurantType.RestaurantTypeTranslations.Select(m => m.Language.ToLower())
                .Contains(x.ToLower())))
                throw new ValidationException(ErrorCodes.RestaurantTypeIsNotTranslated);
            restaurant.RestaurantTypeId = restaurantDto.RestaurantTypeId;
            restaurant.RestaurantAdmin.UserName = restaurantDto.RestaurantAdminUserName;
            restaurant.RestaurantAdmin.Password = PasswordHelper.Encrypt(restaurantDto.RestaurantAdminPassword);

            var restaurantTranslation = restaurant.RestaurantTranslations.FirstOrDefault(x => x.Language.ToLower() == language.ToLower());
            if (restaurantTranslation == null)
            {
                restaurant.RestaurantTranslations.Add(new RestaurantTranslation
                {
                    Language = language,
                    RestaurantName = restaurantDto.RestaurantName,
                    RestaurantDescription = restaurantDto.RestaurantDescription
                });
            }
            else
            {
                restaurantTranslation.RestaurantName = restaurantDto.RestaurantName;
                restaurantTranslation.RestaurantDescription = restaurantDto.RestaurantDescription;
            }

            _restaurantService.Update(restaurant);
            SaveChanges();
            if (restaurantDto.IsLogoChange)
                _manageStorage.UploadImage(path + "\\" + restaurant.RestaurantId, restaurantDto.Image, restaurant.RestaurantId);
        }

        public RestaurantDTO CheckRestaurantReady(long restaurantAdminId)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            RestaurantDTO restaurantDto = new RestaurantDTO();
            restaurantDto.IsReady = restaurant.IsReady;
            return restaurantDto;
        }

        public void PublishRestaurant(long restaurantAdminId)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.Menus.Count(m => m.IsActive) <= 0)
                throw new ValidationException(ErrorCodes.RestaurantMenuDoesNotActivated);
            //if (Strings.SupportedLanguages.Any(x => !restaurant.RestaurantTranslations.Select(m => m.Language.ToLower())
            //    .Contains(x.ToLower())))
            //    throw new ValidationException(ErrorCodes.CategoryIsNotTranslated);
            restaurant.IsReady = true;
            _restaurantService.Update(restaurant);
            SaveChanges();
        }
    }
}
