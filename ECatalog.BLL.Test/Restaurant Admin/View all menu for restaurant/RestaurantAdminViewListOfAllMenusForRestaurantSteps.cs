using System;
using System.Collections.Generic;
using System.Linq;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.View_all_menu_for_restaurant
{
    [Binding]
    public class RestaurantAdminViewListOfAllMenusForRestaurantSteps:BaseStep
    {
        private UserDto _userDto;
        private List<MenuDTO> _menuTranslationDtos;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _menuTranslationDtos = new List<MenuDTO>();
        }
        [Given(@"I am logged in as a restaurant admin to view list of all menus for restaurant")]
        public void GivenIAmLoggedInAsARestaurantAdminToViewListOfAllMenusForRestaurant()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [When(@"I list all menus for restaurant")]
        public void WhenIListAllMenusForRestaurant()
        {
            _menuTranslationDtos = (List<MenuDTO>) _MenuFacade.GetAllMenusByRestaurantId(Strings.DefaultLanguage, 1, 1, 10).Data;
        }
        
        [Then(@"the list of menu will display with the menu name, description, status and list of all categories for this menu")]
        public void ThenTheListOfMenuWillDisplayWithTheMenuNameDescriptionStatusAndListOfAllCategoriesForThisMenu()
        {
            Assert.AreEqual(0, _menuTranslationDtos.Count(x => string.IsNullOrEmpty(x.MenuName)
                                                                    || x.MenuId < 0));
        }

        public RestaurantAdminViewListOfAllMenusForRestaurantSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
