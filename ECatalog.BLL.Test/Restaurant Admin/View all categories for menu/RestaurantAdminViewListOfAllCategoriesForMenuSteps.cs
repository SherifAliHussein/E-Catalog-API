using System;
using System.Collections.Generic;
using System.Linq;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.View_all_categories_for_menu
{
    [Binding]
    public class RestaurantAdminViewListOfAllCategoriesForMenuSteps:BaseStep
    {
        private UserDto _userDto;
        private List<CategoryDTO> _categoryTranslationDtos;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _categoryTranslationDtos = new List<CategoryDTO>();
        }
        [Given(@"I am logged in as a restaurant admin to view list of all categories for menu")]
        public void GivenIAmLoggedInAsARestaurantAdminToViewListOfAllCategoriesForMenu()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }

        [When(@"I list all categories")]
        public void WhenIListAllCategories()
        {
            _categoryTranslationDtos = (List<CategoryDTO>) _CategoryFacade.GetAllCategoriesByMenuId(Strings.DefaultLanguage, 1, 1, 10).Data;
        }

        [Then(@"the list of categories will display with the name, description, status, image thumbnail and list of all items for this category")]
        public void ThenTheListOfCategoriesWillDisplayWithTheNameDescriptionStatusImageThumbnailAndListOfAllItemsForThisCategory()
        {
            Assert.AreEqual(0, _categoryTranslationDtos.Count(x => string.IsNullOrEmpty(x.CategoryName)
                                                               || x.CategoryId < 0));
        }

        public RestaurantAdminViewListOfAllCategoriesForMenuSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
