using System;
using System.Collections.Generic;
using System.Linq;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Admin.Update_Restaurant
{
    [Binding]
    public class AdminUpdateRestaurantSteps:BaseStep
    {
        private UserDto _userDto;
        private RestaurantDTO _restaurantDto;
        


        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _restaurantDto = new RestaurantDTO();
        }
        [Given(@"I am logged in as a admin to update restaurant")]
        public void GivenIAmLoggedInAsAAdminToUpdateRestaurant()
        {
            _userDto.UserId = 1;
            _userDto.Role = Enums.RoleType.GlobalAdmin;
        }

        [Given(@"select restaurant to update")]
        public void GivenSelectRestaurantToUpdate()
        {
            _restaurantDto.RestaurantId = 1;
        }
        
        [When(@"I update restaurant name with new restaurant name")]
        public void WhenIUpdateRestaurantNameWithNewRestaurantName()
        {
            _restaurantDto.RestaurantId = 1;
            _restaurantDto.RestaurantName = "UpdatedRestaurant";
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP";
            _restaurantDto.RestaurantDescription ="Main restaurant is the main restaurant in lobby serve any thing" ;
            _restaurantDto.RestaurantTypeId = 1;
            try
            {
                _RestaurantFacade.UpdateRestaurant(_restaurantDto, Strings.DefaultLanguage,"");
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update restaurant name with empty restaurant name")]
        public void WhenIUpdateRestaurantNameWithEmptyRestaurantName()
        {
            _restaurantDto.RestaurantId = 1;
            _restaurantDto.RestaurantName =  "";
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP";
            _restaurantDto.RestaurantDescription =  "Main restaurant is the main restaurant in lobby serve any thing" ;
            _restaurantDto.RestaurantTypeId = 1;
            try
            {
                _RestaurantFacade.UpdateRestaurant(_restaurantDto, Strings.DefaultLanguage,"");
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update restaurant name with existing restaurant name")]
        public void WhenIUpdateRestaurantNameWithExistingRestaurantName()
        {
            _restaurantDto.RestaurantId = 1;
            _restaurantDto.RestaurantName = "restaurant2" ;
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP";
            _restaurantDto.RestaurantDescription ="Main restaurant is the main restaurant in lobby serve any thing" ;
            _restaurantDto.RestaurantTypeId = 1;
            try
            {
                _RestaurantFacade.UpdateRestaurant(_restaurantDto, Strings.DefaultLanguage,"");
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [Then(@"restaurant name will be changed")]
        public void ThenRestaurantNameWillBeChanged()
        {
            var restaurant = _RestaurantFacade.GetRestaurant(_restaurantDto.RestaurantId, Strings.DefaultLanguage);
            Assert.AreEqual("UpdatedRestaurant",restaurant.RestaurantName);
        }

        [Then(@"Missing restaurant name validation message will return for updated restaurant")]
        public void ThenMissingRestaurantNameValidationMessageWillReturnForUpdatedRestaurant()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyRestaurantName);
        }

        [Then(@"repeated restaurant name validation message will return for updated restaurant")]
        public void ThenRepeatedRestaurantNameValidationMessageWillReturnForUpdatedRestaurant()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.RestaurantNameAlreadyExist);
        }


        public AdminUpdateRestaurantSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
