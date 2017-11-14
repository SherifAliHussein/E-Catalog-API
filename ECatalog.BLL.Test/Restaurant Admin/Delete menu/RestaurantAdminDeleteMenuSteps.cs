using System;
using System.Collections.Generic;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Delete_menu
{
    [Binding]
    public class RestaurantAdminDeleteMenuSteps:BaseStep
    {
        private UserDto _userDto;
        private long _menuId;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
        }
        [Given(@"I am logged in as a restaurant admin to delete menu")]
        public void GivenIAmLoggedInAsARestaurantAdminToDeleteMenu()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I select the menu to deleted")]
        public void GivenISelectTheMenuToDeleted()
        {
            _menuId = 1;
        }
        
        [When(@"I click on delete menu")]
        public void WhenIClickOnDeleteMenu()
        {
            _MenuFacade.DeleteMenu(_menuId);
        }
        
        [Then(@"the menu will be deleted")]
        public void ThenTheMenuWillBeDeleted()
        {
            var exception = Assert.Catch<NotFoundException>(() =>
            {
                _MenuFacade.GetMenu(_menuId, Strings.DefaultLanguage);
            });
            Assert.AreEqual(exception.ErrorCode, ErrorCodes.MenuDeleted);
        }

        public RestaurantAdminDeleteMenuSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
