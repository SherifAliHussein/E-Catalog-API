using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Admin.Admin_activate_deactivate_restaurant
{
    [Binding]
    public class AdminActivateDeactivateRestaurantSteps:BaseStep
    {

        private UserDto _userDto;
        private ValidationException _exception;


        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
        }
        [Given(@"I am logged in as a admin to activate/deactivate restaurant")]
        public void GivenIAmLoggedInAsAAdminToActivateDeactivateRestaurant()
        {
            _userDto.UserId = 1;
            _userDto.Role = Enums.RoleType.GlobalAdmin;
        }
        
        [When(@"I activate the selected restaurant")]
        public void WhenIActivateTheSelectedRestaurant()
        {
            _RestaurantFacade.ActivateRestaurant(1);
        }
        
        [When(@"I activate the selected restaurant that has no activated menu")]
        public void WhenIActivateTheSelectedRestaurantThatHasNoActivatedMenu()
        {
            try
            {
                _RestaurantFacade.ActivateRestaurant(2);
            }
            catch (ValidationException e)
            {
                _exception = e;
            }
        }
        
        [When(@"I deactivate the selected restaurant")]
        public void WhenIDeactivateTheSelectedRestaurant()
        {
            _RestaurantFacade.DeActivateRestaurant(1);
        }
        
        [Then(@"the restaurant will be activated")]
        public void ThenTheRestaurantWillBeActivated()
        {
            var restaurant = _RestaurantFacade.GetRestaurant(1, Strings.DefaultLanguage);
            Assert.IsTrue(restaurant.IsActive);
        }
        
        [Then(@"restaurant hasn’t activated menu validation message will return")]
        public void ThenRestaurantHasnTActivatedMenuValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.RestaurantMenuDoesNotActivated);
        }

        [Then(@"restaurant will be deactivate")]
        public void ThenRestaurantWillBeDeactivate()
        {
            var restaurant = _RestaurantFacade.GetRestaurant(1, Strings.DefaultLanguage);
            Assert.IsFalse(restaurant.IsActive);
        }

        public AdminActivateDeactivateRestaurantSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
