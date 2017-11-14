using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.BLL.Services.Interfaces;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace ECatalog.BLL.Services
{
    public class MenuFacade:BaseFacade,IMenuFacade
    {
        private IMenuService _menuService;
        private IMenuTranslationService _menuTranslationService;
        private IRestaurantService _restaurantService;
        private IRestaurantTranslationService _restaurantTranslationService;
        private IRestaurantWaiterService _restaurantWaiterService;

        public MenuFacade(IMenuService menuService,IMenuTranslationService menuTranslationService, IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService
            ,IRestaurantWaiterService restaurantWaiterService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _menuService = menuService;
            _menuTranslationService = menuTranslationService;
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _restaurantWaiterService = restaurantWaiterService;
        }

        public MenuFacade(IMenuService menuService, IMenuTranslationService menuTranslationService, IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService
            , IRestaurantWaiterService restaurantWaiterService)
        {
            _menuService = menuService;
            _menuTranslationService = menuTranslationService;
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _restaurantWaiterService = restaurantWaiterService;
        }

        public void AddMenu(MenuDTO menuDto,long restaurantAdminId,string language)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            ValidateMenu(menuDto, language,restaurant.RestaurantId);
            var menu = new Menu();
            menu.MenuTranslations.Add(new MenuTranslation
            {
                MenuName = menuDto.MenuName,
                Language = language
            });
            menu.RestaurantId = restaurant.RestaurantId;
            _menuService.Insert(menu);
            _menuTranslationService.InsertRange(menu.MenuTranslations);
            SaveChanges();
        }

        public MenuDTO GetMenu(long menuId, string language)
        {
            var menu = _menuService.Find(menuId);
            if(menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if(menu.IsDeleted) throw new NotFoundException(ErrorCodes.MenuDeleted);
            return Mapper.Map<Menu, MenuDTO>(menu, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        src.MenuTranslations = src.MenuTranslations
                            .Where(x => x.Language.ToLower() == language.ToLower())
                            .ToList();
                    }
                ); 
            });
        }
        private void ValidateMenu(MenuDTO menuDto, string language,long restaurantId)
        {
            if (string.IsNullOrEmpty(menuDto.MenuName))
                throw new ValidationException(ErrorCodes.EmptyMenuName);
            if (menuDto.MenuName.Length > 300)

                throw new ValidationException(ErrorCodes.MenuNameExceedLength);
            if ( _menuTranslationService.CheckMenuNameExistForRestaurant(menuDto.MenuName, language, menuDto.MenuId, restaurantId)) throw new ValidationException(ErrorCodes.MenuNameAlreadyExist);
        }

        public PagedResultsDto GetAllMenusByRestaurantId(string language,long restaurantAdminId, int page, int pageSize)
        {
            var result = _menuTranslationService.GetAllMenusByRestaurantAdminId(language, restaurantAdminId, page, pageSize);
            result.IsParentTranslated = language == Strings.DefaultLanguage || _restaurantTranslationService.CheckRestaurantByLanguage(restaurantAdminId, language);
            return result;
        }

        public PagedResultsDto GetActivatedMenusByRestaurantId(string language, long userId, int page, int pageSize)
        {
            var waiter = _restaurantWaiterService.Find(userId);
            if(waiter == null) throw new NotFoundException(ErrorCodes.RestaurantAdminNotFound);
            var result = _menuTranslationService.GetActivatedMenusByRestaurantId(language, waiter.RestaurantId, page, pageSize);
            //result.IsParentTranslated = language == Strings.DefaultLanguage || _restaurantTranslationService.CheckRestaurantByLanguage(restaurantAdminId, language);
            return result;
        }
        public void ActivateMenu(long menuId)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (menu.Categories.Count(m => m.IsActive) <= 0)
                throw new ValidationException(ErrorCodes.MenuCategoriesDoesNotActivated);
            if (Strings.SupportedLanguages.Any(x => !menu.MenuTranslations.Select(m => m.Language.ToLower())
                .Contains(x.ToLower())))
                throw new ValidationException(ErrorCodes.MenuIsNotTranslated);
            menu.IsActive = true;
            _menuService.Update(menu);
            SaveChanges();
        }

        public void DeActivateMenu(long menuId)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            menu.IsActive = false;
            _menuService.Update(menu);
            var restaurantHasMenuActivated = menu.Restaurant.Menus.Any(x => x.IsActive);
            if (!restaurantHasMenuActivated)
            {
                menu.Restaurant.IsActive = false;
                menu.Restaurant.IsReady = false;
                _restaurantService.Update(menu.Restaurant);
            }
            SaveChanges();
        }
        public void DeleteMenu(long menuId)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            menu.IsDeleted = true;
            menu.IsActive = false;
            _menuService.Update(menu);
            var restaurantHasMenuActivated = menu.Restaurant.Menus.Any(x => x.IsActive);
            if (!restaurantHasMenuActivated)
            {
                menu.Restaurant.IsActive = false;
                menu.Restaurant.IsReady = false;
                _restaurantService.Update(menu.Restaurant);
            }
            SaveChanges();
        }

        public void UpdateMenu(MenuDTO menuDto, long restaurantAdminId, string language)
        {
            var menu = _menuService.Find(menuDto.MenuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            ValidateMenu(menuDto, language,menu.RestaurantId);
            var menuTranslation = menu.MenuTranslations.FirstOrDefault(x => x.Language.ToLower() == language.ToLower());
            if (menuTranslation == null)
            {
                menu.MenuTranslations.Add(new MenuTranslation
                {
                    Language = language,
                    MenuName = menuDto.MenuName
                });
            }
            else
            {
                menuTranslation.MenuName = menuDto.MenuName;
            }


            _menuService.Update(menu);
            SaveChanges();
        }
    }
}
