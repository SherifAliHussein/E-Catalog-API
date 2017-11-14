using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Admin.Add_new_restaurant_type
{
    [Binding]
    public class AddNewRestaurantTypeSteps:BaseStep
    {
        private UserDto _userDto;
        private RestaurantTypeDto _restaurantTypeDto;
        private ValidationException _exception;
        private bool IsSuccess;


        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _restaurantTypeDto = new RestaurantTypeDto();
        }
        [Given(@"I am logged in as a admin to add new restaurant type")]
        public void GivenIAmLoggedInAsAAdminToAddNewRestaurantType()
        {
            _userDto.UserId = 1;
            _userDto.Role =Enums.RoleType.GlobalAdmin;
          
        }
        
        [When(@"I entered restaurant type name")]
        public void WhenIEnteredRestaurantTypeName()
        {
            _restaurantTypeDto.TypeName =  "Italian";
        }
        
        [When(@"I click on add restaurant type")]
        public void WhenIClickOnAddRestaurantType()
        {
            try
            {
                IsSuccess = _RestaurantFacade.AddRestaurantType(_restaurantTypeDto,Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I left restaurant type name")]
        public void WhenILeftRestaurantTypeName()
        {
            _restaurantTypeDto.TypeName = "";
        }
        
        [When(@"I entered existing restaurant type name")]
        public void WhenIEnteredExistingRestaurantTypeName()
        {
            _restaurantTypeDto.TypeName = "chinese";
        }
        
        [When(@"I entered restaurant type name with more than (.*) characters")]
        public void WhenIEnteredRestaurantTypeNameWithMoreThanCharacters(int p0)
        {
            _restaurantTypeDto.TypeName = "chinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinesechinese";
        }

        [Then(@"restaurant type will added successfully")]
        public void ThenRestaurantTypeWillAddedSuccessfully()
        {
            Assert.IsTrue(IsSuccess);
        }
        
        [Then(@"Missing Name validation message will return")]
        public void ThenMissingNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyRestaurantType);
        }
        
        [Then(@"Repeated Name validation message will return")]
        public void ThenRepeatedNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.RestaurantTypeAlreadyExist);
        }
        
        [Then(@"Maximum length validation message will return")]
        public void ThenMaximumLengthValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.RestaurantTypeExceedLength);
        }

        public AddNewRestaurantTypeSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
