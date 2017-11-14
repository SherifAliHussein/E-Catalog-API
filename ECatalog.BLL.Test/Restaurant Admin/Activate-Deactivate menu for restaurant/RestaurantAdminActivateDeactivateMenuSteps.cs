using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Activate_Deactivate_menu_for_restaurant
{
    [Binding]
    public class RestaurantAdminActivateDeactivateMenuSteps : BaseStep
    {
        private UserDto _userDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
        }
        [Given(@"I am logged in as a restaurant admin to activate/deactivate menu")]
        public void GivenIAmLoggedInAsARestaurantAdminToActivateDeactivateMenu()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [When(@"I activate the selected menu")]
        public void WhenIActivateTheSelectedMenu()
        {
            _MenuFacade.ActivateMenu(2);
        }
        
        [When(@"I activate the selected menu that has no activated category")]
        public void WhenIActivateTheSelectedMenuThatHasNoActivatedCategory()
        {
            try
            {
                _MenuFacade.ActivateMenu(1);
            }
            catch (ValidationException e)
            {
                _exception = e;
            }
        }
        
        [When(@"I deactivate the selected menu")]
        public void WhenIDeactivateTheSelectedMenu()
        {
            _MenuFacade.DeActivateMenu(1);
        }
        
        [Then(@"the menu will be activated")]
        public void ThenTheMenuWillBeActivated()
        {
            var menu = _MenuFacade.GetMenu(1, Strings.DefaultLanguage);
            Assert.IsTrue(menu.IsActive);
        }
        
        [Then(@"menu hasn’t activated category validation message will return")]
        public void ThenMenuHasnTActivatedCategoryValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.MenuCategoriesDoesNotActivated);
        }

        [Then(@"menu will be deactivate")]
        public void ThenMenuWillBeDeactivate()
        {
            var menu = _MenuFacade.GetMenu(1, Strings.DefaultLanguage);
            Assert.IsFalse(menu.IsActive);
        }

        public RestaurantAdminActivateDeactivateMenuSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
