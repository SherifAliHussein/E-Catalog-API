using System;
using System.Collections.Generic;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Update_Menu
{
    [Binding]
    public class RestaurantAdminUpdateMenuSteps:BaseStep
    {
        private UserDto _userDto;
        private MenuDTO _menuDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _menuDto = new MenuDTO();
        }
        [Given(@"I am logged in as a restaurant admin to update menu")]
        public void GivenIAmLoggedInAsARestaurantAdminToUpdateMenu()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I update the current menu name with new name")]
        public void GivenIUpdateTheCurrentMenuNameWithNewName()
        {
            _menuDto.MenuId = 2;
            //_menuDto.RestaurantId = 1;
            _menuDto.MenuName = "New menu";
        }
        
        [Given(@"I update the current menu name with empty name")]
        public void GivenIUpdateTheCurrentMenuNameWithEmptyName()
        {
            _menuDto.MenuId = 2;
            //_menuDto.RestaurantId = 1;
            _menuDto.MenuName = "";
        }
        
        [Given(@"I update the current menu name with exist name")]
        public void GivenIUpdateTheCurrentMenuNameWithExistName()
        {
            _menuDto.MenuId = 2;
            //_menuDto.RestaurantId = 1;
            _menuDto.MenuName = "Menu2";
        }
        
        [Given(@"I update the current menu with long name")]
        public void GivenIUpdateTheCurrentMenuWithLongName()
        {
            _menuDto.MenuId = 2;
           // _menuDto.RestaurantId = 1;
            _menuDto.MenuName =
                "menu1 menu1menu1 menu1 menu1menu1 menu1 menu1menu1 menu1 menu1menu1 menu1 menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1menu1 menu1 menu1menu1 menu1menu1 menu1 menu1menu1 menu1menu1 menu1 menu1menu1 menu1menu1 menu1";
        }
        
        [Given(@"I click on update menu")]
        public void GivenIClickOnUpdateMenu()
        {
            try
            {
                _MenuFacade.UpdateMenu(_menuDto,2,Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        //[When(@"I click on update menu")]
        //public void WhenIClickOnUpdateMenu()
        //{
        //    ScenarioContext.Current.Pending();
        //}
        
        [Then(@"menu name will update successfully")]
        public void ThenMenuNameWillUpdateSuccessfully()
        {
            var menu = _MenuFacade.GetMenu(_menuDto.MenuId, Strings.DefaultLanguage);
            Assert.AreEqual("New menu", menu.MenuName);
        }
        
        [Then(@"Missing menu name validation message will return for the updated menu")]
        public void ThenMissingMenuNameValidationMessageWillReturnForTheUpdatedMenu()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyMenuName);
        }
        
        [Then(@"repeated menu name validation message will return for the updated menu")]
        public void ThenRepeatedMenuNameValidationMessageWillReturnForTheUpdatedMenu()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.MenuNameAlreadyExist);
        }
        
        [Then(@"Maximum length for menu name validation message will return for the updated menu")]
        public void ThenMaximumLengthForMenuNameValidationMessageWillReturnForTheUpdatedMenu()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.MenuNameExceedLength);
        }

        public RestaurantAdminUpdateMenuSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
