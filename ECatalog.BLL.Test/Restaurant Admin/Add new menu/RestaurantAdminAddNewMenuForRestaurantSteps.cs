using System;
using System.Collections.Generic;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Add_new_menu
{
    [Binding]
    public class RestaurantAdminAddNewMenuForRestaurantSteps:BaseStep
    {
        private UserDto _userDto;
        private MenuDTO _menuDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _menuDto = new MenuDTO();
        }

        [Given(@"I am logged in as a restaurant admin to add new menu for restaurant")]
        public void GivenIAmLoggedInAsARestaurantAdminToAddNewMenuForRestaurant()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I entered menu name")]
        public void GivenIEnteredMenuName()
        {
            _menuDto.MenuId = 2;
            //_menuDto.RestaurantId = 1;
            _menuDto.MenuName = "Main menu";
        }
        
        [Given(@"I left menu name")]
        public void GivenILeftMenuName()
        {
            _menuDto.MenuId = 2;
            //_menuDto.RestaurantId = 1;
            _menuDto.MenuName = "";
        }
        
        [Given(@"I am logged in as a restaurant admin to add new menu")]
        public void GivenIAmLoggedInAsARestaurantAdminToAddNewMenu()
        {
            _menuDto.MenuId = 2;
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [When(@"I click on add menu")]
        public void WhenIClickOnAddMenu()
        {
            try
            {
                _MenuFacade.AddMenu(_menuDto,2, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I entered existing menu name for the same restaurant")]
        public void WhenIEnteredExistingMenuNameForTheSameRestaurant()
        {
            _menuDto.MenuId = 2;
            //_menuDto.RestaurantId = 1;
            _menuDto.MenuName = "menu1" ;
        }
        
        [When(@"I entered menu name with more than (.*) characters")]
        public void WhenIEnteredMenuNameWithMoreThanCharacters(int p0)
        {
            _menuDto.MenuId = 2;
            //_menuDto.RestaurantId = 1;
            _menuDto.MenuName =
                "menu1 menu1menu1 menu1 menu1menu1 menu1 menu1menu1 menu1 menu1menu1 menu1 menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1 menu1menu1 menu1menu1 menu1 menu1menu1 menu1menu1 menu1 menu1menu1 menu1menu1 menu1";
        }
        
        [Then(@"the menu will be added successfully deactivated")]
        public void ThenTheMenuWillBeAddedSuccessfullyDeactivated()
        {
            var menu = _MenuFacade.GetMenu(_menuDto.MenuId, Strings.DefaultLanguage);
            Assert.IsFalse(menu.IsActive);
        }
        
        [Then(@"Missing menu name validation message will return")]
        public void ThenMissingMenuNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyMenuName);
        }
        
        [Then(@"repeated menu name validation message will return")]
        public void ThenRepeatedMenuNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.MenuNameAlreadyExist);
        }
        
        [Then(@"Maximum length for menu name validation message will return")]
        public void ThenMaximumLengthForMenuNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.MenuNameExceedLength);
        }

        public RestaurantAdminAddNewMenuForRestaurantSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
