using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.BLL.Services.Interfaces;
using ECatalog.BLL.Services.ManageStorage;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace ECatalog.BLL.Services
{
    public class CategoryFacade:BaseFacade,ICategoryFacade
    {
        private ICategoryService _categoryService;
        private ICategoryTranslationService _categoryTranslationService;
        private IMenuService _menuService;
        private IMenuTranslationService _menuTranslationService;
        private IManageStorage _manageStorage;
        private IRestaurantService _restaurantService;

        public CategoryFacade(ICategoryService categoryService, ICategoryTranslationService categoryTranslationService, IMenuService menuService, IManageStorage manageStorage, IMenuTranslationService menuTranslationService
            ,IRestaurantService restaurantService, IUnitOfWorkAsync unitOfWork):base(unitOfWork)
        {
            _categoryService = categoryService;
            _categoryTranslationService = categoryTranslationService;
            _menuService = menuService;
            _manageStorage = manageStorage;
            _menuTranslationService = menuTranslationService;
            _restaurantService = restaurantService;
        }

        public CategoryFacade(ICategoryService categoryService, ICategoryTranslationService categoryTranslationService, IMenuService menuService, IManageStorage manageStorage, IMenuTranslationService menuTranslationService
        ,IRestaurantService restaurantService)
        {
            _categoryService = categoryService;
            _categoryTranslationService = categoryTranslationService;
            _menuService = menuService;
            _manageStorage = manageStorage;
            _menuTranslationService = menuTranslationService;
            _restaurantService = restaurantService;
        }

        public void AddCategory(CategoryDTO categoryDto, string language, string path)
        {
            ValidateCategory(categoryDto, language);
            var menu = _menuService.Find(categoryDto.MenuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (menu.IsDeleted) throw new ValidationException(ErrorCodes.MenuDeleted);
            var category = Mapper.Map<Category>(categoryDto);
            category.CategoryTranslations.Add(new CategoryTranslation
            {
                CategoryName = categoryDto.CategoryName,
                Language = language
            });
            
            _categoryTranslationService.InsertRange(category.CategoryTranslations);
            _categoryService.Insert(category);
            SaveChanges();
            _manageStorage.UploadImage(path + "\\" + menu.RestaurantId+"\\"+menu.MenuId+"\\", categoryDto.Image, category.CategoryId);
        }

        public CategoryDTO GetCategory(long categoryId, string language)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            if (category.IsDeleted) throw new NotFoundException(ErrorCodes.CategoryDeleted);
            return Mapper.Map<Category, CategoryDTO>(category, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        src.CategoryTranslations = src.CategoryTranslations
                            .Where(x => x.Language.ToLower() == language.ToLower())
                            .ToList();
                    }
                );
            });
        }
        private void ValidateCategory(CategoryDTO categoryDto,string language)
        {
            if (string.IsNullOrEmpty(categoryDto.CategoryName))
                throw new ValidationException(ErrorCodes.EmptyCategoryName);
            if (categoryDto.CategoryName.Length > 300)
                throw new ValidationException(ErrorCodes.CategoryNameExceedLength);
            if (_categoryTranslationService.CheckCategoryNameExistForMenu(categoryDto.CategoryName, language, categoryDto.CategoryId, categoryDto.MenuId)) throw new ValidationException(ErrorCodes.CategoryNameAlreadyExist);
        }

        public PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (menu.IsDeleted) throw new ValidationException(ErrorCodes.MenuDeleted);
            var results = _categoryTranslationService.GetAllCategoriesByMenuId(language, menuId, page, pageSize);
            results.IsParentTranslated = language == Strings.DefaultLanguage || _menuTranslationService.CheckMenuByLanguage(menuId, language);
            return results;
        }

        public PagedResultsDto GetActivatedCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (menu.IsDeleted) throw new ValidationException(ErrorCodes.MenuDeleted);
            var results = _categoryTranslationService.GetActivatedCategoriesByMenuId(language, menuId, page, pageSize);
            //results.IsParentTranslated = language == Strings.DefaultLanguage || _menuTranslationService.CheckMenuByLanguage(menuId, language);
            return results;
        }

        public void ActivateCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (category.Items.Count(m => m.IsActive) <= 0)
                throw new ValidationException(ErrorCodes.CategoryItemsDoesNotActivated);
            if (Strings.SupportedLanguages.Any(x => !category.CategoryTranslations.Select(m => m.Language.ToLower())
                .Contains(x.ToLower())))
                throw new ValidationException(ErrorCodes.CategoryIsNotTranslated);
            category.IsActive = true;
            _categoryService.Update(category);
            SaveChanges();
        }

        public void DeActivateCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            category.IsActive = false;
            _categoryService.Update(category);
            var menuHasCategoryActivated = category.Menu.Categories.Any(x => x.IsActive);
            if (!menuHasCategoryActivated)
            {
                category.Menu.IsActive = false;
                _menuService.Update(category.Menu);
            }
            SaveChanges();
        }
        public void DeleteCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            category.IsDeleted = true;
            category.IsActive = false;
            _categoryService.Update(category);
            var menuHasCategoryActivated = category.Menu.Categories.Any(x => x.IsActive);
            if (!menuHasCategoryActivated)
            {
                category.Menu.IsActive = false;
                _menuService.Update(category.Menu);
                CheckRestaurantHasMenuActivated(category);
            }
            SaveChanges();
        }
        private void CheckRestaurantHasMenuActivated(Category category)
        {
            var restaurantHasMenuActivated = category.Menu.Restaurant.Menus.Any(x => x.IsActive);
            if (!restaurantHasMenuActivated)
            {
                category.Menu.Restaurant.IsActive = false;
                _restaurantService.Update(category.Menu.Restaurant);
            }
        }

        public void UpdateCategory(CategoryDTO categoryDto, string language, string path)
        {
            ValidateCategory(categoryDto, language);
            var category = _categoryService.Find(categoryDto.CategoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);

            var categoryTranslation =
                category.CategoryTranslations.FirstOrDefault(x => x.Language.ToLower() == language.ToLower());
            if (categoryTranslation == null)
            {
                category.CategoryTranslations.Add(new CategoryTranslation
                {
                    Language = language,
                    CategoryName = categoryDto.CategoryName
                });
            }
            else
            {
                categoryTranslation.CategoryName = categoryDto.CategoryName;
            }

            _categoryService.Update(category);
            SaveChanges();
            if (categoryDto.IsImageChange)
                _manageStorage.UploadImage(path + "\\" + category.Menu.RestaurantId + "\\" + category.MenuId + "\\", categoryDto.Image, categoryDto.CategoryId);
        }
    }
}
