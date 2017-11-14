using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Update_side_item
{
    [Binding]
    public class RestaurantAdminUpdateSideItemSteps:BaseStep
    {
        private UserDto _userDto;
        private SideItemDTO _sideItemDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _sideItemDto = new SideItemDTO();
        }
        [Given(@"I am logged in as a restaurant admin to update side item")]
        public void GivenIAmLoggedInAsARestaurantAdminToUpdateSideItem()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [When(@"I update the current side item name with new name")]
        public void WhenIUpdateTheCurrentSideItemNameWithNewName()
        {
            _sideItemDto.SideItemName = "new side item";
            _sideItemDto.SideItemId = 1;
            _sideItemDto.Value = 1;
            try
            {
                _SideItemFacade.UpdateSideItem(_sideItemDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update the current side item name with empty name")]
        public void WhenIUpdateTheCurrentSideItemNameWithEmptyName()
        {
            _sideItemDto.SideItemName = "";
            _sideItemDto.SideItemId = 1;
            _sideItemDto.Value = 1;
            try
            {
                _SideItemFacade.UpdateSideItem(_sideItemDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update the current side item name with exist name")]
        public void WhenIUpdateTheCurrentSideItemNameWithExistName()
        {
            _sideItemDto.SideItemName = "Pasta";
            _sideItemDto.SideItemId = 1;
            _sideItemDto.Value = 1;
            try
            {
                _SideItemFacade.UpdateSideItem(_sideItemDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update the current side item with long name")]
        public void WhenIUpdateTheCurrentSideItemWithLongName()
        {
            _sideItemDto.SideItemName = "Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta Pasta";
            _sideItemDto.SideItemId = 1;
            _sideItemDto.Value = 1;
            try
            {
                _SideItemFacade.UpdateSideItem(_sideItemDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update the current side item with short name")]
        public void WhenIUpdateTheCurrentSideItemWithShortName()
        {
            _sideItemDto.SideItemName = "n";
            _sideItemDto.SideItemId = 1;
            _sideItemDto.Value = 1;
            try
            {
                _SideItemFacade.UpdateSideItem(_sideItemDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update the current side item with invalid value")]
        public void WhenIUpdateTheCurrentSideItemWithInvalidValue()
        {
            _sideItemDto.SideItemName = "new side item";
            _sideItemDto.SideItemId = 1;
            _sideItemDto.Value = -1;
            try
            {
                _SideItemFacade.UpdateSideItem(_sideItemDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [Then(@"side item name will update successfully")]
        public void ThenSideItemNameWillUpdateSuccessfully()
        {
            var sideItem = _SideItemFacade.GetSideItem(_sideItemDto.SideItemId, Strings.DefaultLanguage);
            Assert.AreEqual(_sideItemDto.SideItemName, sideItem.SideItemName);
        }
        
        [Then(@"Missing side item name validation message will return for the updated side item")]
        public void ThenMissingSideItemNameValidationMessageWillReturnForTheUpdatedSideItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptySideItemName);
        }
        
        [Then(@"repeated side item name validation message will return for the updated side item")]
        public void ThenRepeatedSideItemNameValidationMessageWillReturnForTheUpdatedSideItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SideItemNameAlreadyExist);
        }
        
        [Then(@"Maximum length for size name validation message will return for the updated side item")]
        public void ThenMaximumLengthForSizeNameValidationMessageWillReturnForTheUpdatedSideItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SideItemNameExceedLength);
        }
        
        [Then(@"Minimum length for size name validation message will return for the updated side item")]
        public void ThenMinimumLengthForSizeNameValidationMessageWillReturnForTheUpdatedSideItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SideItemNameMinimumLength);
        }
        
        [Then(@"Invalid side item value validation message will return for the updated side item")]
        public void ThenInvalidSideItemValueValidationMessageWillReturnForTheUpdatedSideItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.InvalidSideItemValue);
        }

        public RestaurantAdminUpdateSideItemSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
