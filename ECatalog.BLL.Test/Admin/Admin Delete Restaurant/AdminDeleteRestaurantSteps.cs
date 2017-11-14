using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Admin.Admin_Delete_Restaurant
{
    [Binding]
    public class AdminDeleteRestaurantSteps:BaseStep
    {
        private UserDto _userDto;
        private ValidationException _exception;
        private long restaurantId;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
        }
        [Given(@"I am logged in as a admin to delete restaurant")]
        public void GivenIAmLoggedInAsAAdminToDeleteRestaurant()
        {
            _userDto.UserId = 1;
            _userDto.Role = Enums.RoleType.GlobalAdmin;
        }
        
        [Given(@"Select the restaurant to delete")]
        public void GivenSelectTheRestaurantToDelete()
        {
            restaurantId = 1;
        }
        
        [When(@"I delete restaurant")]
        public void WhenIDeleteRestaurant()
        {
            _RestaurantFacade.DeleteRestaurant(restaurantId);
        }

        [Then(@"restaurant will be deleted")]
        public void ThenRestaurantWillBeDeleted()
        {
            var exception = Assert.Catch<NotFoundException>(() =>
            {
                _RestaurantFacade.GetRestaurant(1, Strings.DefaultLanguage);
            });
            Assert.AreEqual(exception.ErrorCode, ErrorCodes.RestaurantDeleted);
        }

        public AdminDeleteRestaurantSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
