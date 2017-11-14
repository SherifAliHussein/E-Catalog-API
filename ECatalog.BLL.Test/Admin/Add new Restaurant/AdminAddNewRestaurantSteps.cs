using System;
using System.Collections.Generic;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test
{
    [Binding]
    public class AdminAddNewRestaurantSteps:BaseStep
    {
        private UserDto _userDto;
        private RestaurantDTO _restaurantDto;
        
        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _restaurantDto = new RestaurantDTO();
        }

        [Given(@"I am logged in as a admin to add new restaurant")]
        public void GivenIAmLoggedInAsAAdminToAddNewRestaurant()
        {
            _userDto.UserId = 1;
            _userDto.Role = Enums.RoleType.GlobalAdmin;
        }

        [When(@"I entered restaurant name and description and logo and select restaurant type and username for restaurant admin and password")]
        public void WhenIEnteredRestaurantNameAndDescriptionAndLogoAndSelectRestaurantTypeAndUsernameForRestaurantAdminAndPassword()
        {
            _restaurantDto.RestaurantId = 2;
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP@ssw0rd";
            _restaurantDto.RestaurantName = "Main restaurant" ;
            _restaurantDto.RestaurantDescription = "Main restaurant is the main restaurant in lobby serve any thing" ;
            _restaurantDto.RestaurantTypeId = 1;
        }

        [When(@"I click on add restaurant")]
        public void WhenIClickOnAddRestaurant()
        {
            try
            {
                _RestaurantFacade.AddRestaurant(_restaurantDto,Strings.DefaultLanguage,"");
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }

        [When(@"I left restaurant name and select restaurant type")]
        public void WhenILeftRestaurantNameAndSelectRestaurantType()
        {
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP@ssw0rd";
            _restaurantDto.RestaurantDescription = "Main restaurant is the main restaurant in lobby serve any thing";
            _restaurantDto.RestaurantTypeId = 1;
        }
        
        [When(@"I entered existing restaurant name")]
        public void WhenIEnteredExistingRestaurantName()
        {
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP@ssw0rd";
            _restaurantDto.RestaurantName = "restaurant1";
            _restaurantDto.RestaurantDescription = "Main restaurant is the main restaurant in lobby serve any thing" ;
            _restaurantDto.RestaurantTypeId = 1;
        }
        
        [When(@"I entered restaurant name with more than (.*) characters")]
        public void WhenIEnteredRestaurantNameWithMoreThanCharacters(int p0)
        {
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP@ssw0rd";
            _restaurantDto.RestaurantName = "Main restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurantMain restaurant" ;
            _restaurantDto.RestaurantDescription ="Main restaurant is the main restaurant in lobby serve any thing" ;
            _restaurantDto.RestaurantTypeId = 1;
        }
        
        [When(@"I left restaurant description and select restaurant type")]
        public void WhenILeftRestaurantDescriptionAndSelectRestaurantType()
        {
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP@ssw0rd";
            _restaurantDto.RestaurantName ="Main restaurant" ;
            _restaurantDto.RestaurantTypeId = 1;
        }
        
        [When(@"I left username for restaurant admin")]
        public void WhenILeftUsernameForRestaurantAdmin()
        {
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP@ssw0rd";
            _restaurantDto.RestaurantName = "Main restaurant";
            _restaurantDto.RestaurantDescription = "Main restaurant is the main restaurant in lobby serve any thing" ;
            _restaurantDto.RestaurantTypeId = 1;
        }
        
        [When(@"I left password for restaurant admin")]
        public void WhenILeftPasswordForRestaurantAdmin()
        {
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantName = "Main restaurant" ;
            _restaurantDto.RestaurantDescription = "Main restaurant is the main restaurant in lobby serve any thing";
            _restaurantDto.RestaurantTypeId = 1;
        }
        
        [When(@"I entered existing username for restaurant admin")]
        public void WhenIEnteredExistingUsernameForRestaurantAdmin()
        {
            _restaurantDto.RestaurantAdminUserName = "adminstartor";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP@ssw0rd";
            _restaurantDto.RestaurantName = "Main restaurant" ;
            _restaurantDto.RestaurantDescription = "Main restaurant is the main restaurant in lobby serve any thing";
            _restaurantDto.RestaurantTypeId = 1;
            _restaurantDto.RestaurantId = 1;
        }
        
        [When(@"I entered short password for restaurant admin")]
        public void WhenIEnteredShortPasswordForRestaurantAdmin()
        {
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw";
            _restaurantDto.RestaurantName = "Main restaurant" ;
            _restaurantDto.RestaurantDescription = "Main restaurant is the main restaurant in lobby serve any thing";
            _restaurantDto.RestaurantTypeId = 1;
        }
        
        [When(@"I entered long password for restaurant admin")]
        public void WhenIEnteredLongPasswordForRestaurantAdmin()
        {
            _restaurantDto.RestaurantAdminUserName = "admin";
            _restaurantDto.RestaurantAdminPassword = "P@ssw0rdP@ssw0rdP@ssw0rdP@ssw0rdP@ssw0rdP@ssw0rd";
            _restaurantDto.RestaurantName = "Main restaurant";
            _restaurantDto.RestaurantDescription = "Main restaurant is the main restaurant in lobby serve any thing" ;
            _restaurantDto.RestaurantTypeId = 1;
        }
        
        [Then(@"the restaurant will be added successfully deactivated")]
        public void ThenTheRestaurantWillBeAddedSuccessfullyDeactivated()
        {
            var restaurant = _RestaurantFacade.GetRestaurant(_restaurantDto.RestaurantId, Strings.DefaultLanguage);
            Assert.IsFalse(restaurant.IsActive);
        }
        
        [Then(@"Missing restaurant name validation message will return")]
        public void ThenMissingRestaurantNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyRestaurantName);
        }
        
        [Then(@"repeated restaurant name validation message will return")]
        public void ThenRepeatedRestaurantNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.RestaurantNameAlreadyExist);
        }

        [Then(@"Maximum length for restaurant name validation message will return")]
        public void ThenMaximumLengthForRestaurantNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.RestaurantNameExceedLength);
        }

        [Then(@"Missing restaurant description validation message will return")]
        public void ThenMissingRestaurantDescriptionValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyRestaurantDescription);
        }

        [Then(@"Missing admin username validation message will return")]
        public void ThenMissingAdminUsernameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyRestaurantAdminUserName);
        }

        [Then(@"Missing admin password validation message will return")]
        public void ThenMissingAdminPasswordValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyRestaurantAdminPassword);
        }

        [Then(@"repeated username for restaurant admin validation message will return")]
        public void ThenRepeatedUsernameForRestaurantAdminValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.RestaurantAdminUserNameAlreadyExist);
        }
        
        [Then(@"Minimum password length (.*) characters validation message will return")]
        public void ThenMinimumPasswordLengthCharactersValidationMessageWillReturn(int p0)
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.RestaurantAdminPasswordLengthNotMatched);
        }
        
        [Then(@"Maximum password length (.*) characters validation message will return")]
        public void ThenMaximumPasswordLengthCharactersValidationMessageWillReturn(int p0)
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.RestaurantAdminPasswordLengthNotMatched);
        }

        public AdminAddNewRestaurantSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
