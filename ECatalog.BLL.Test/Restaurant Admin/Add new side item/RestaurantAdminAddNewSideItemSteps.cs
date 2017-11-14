using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Add_new_side_item
{
    [Binding]
    public class RestaurantAdminAddNewSideItemSteps:BaseStep
    {
        private UserDto _userDto;
        private SideItemDTO _sideItemDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _sideItemDto = new SideItemDTO();
        }
        [Given(@"I am logged in as a restaurant admin to add new side item")]
        public void GivenIAmLoggedInAsARestaurantAdminToAddNewSideItem()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I entered side item name and value")]
        public void GivenIEnteredSideItemNameAndValue()
        {
            _sideItemDto.SideItemName = "Sautee";
            _sideItemDto.Value = 1;
        }
        
        [Given(@"I left side item name")]
        public void GivenILeftSideItemName()
        {
            _sideItemDto.SideItemName = "";
            _sideItemDto.Value = 1;
            _sideItemDto.SideItemId = 3;
        }
        
        [Given(@"I entered existing side item name")]
        public void GivenIEnteredExistingSideItemName()
        {
            _sideItemDto.SideItemName = "Fries";
            _sideItemDto.Value = 1;
            _sideItemDto.SideItemId = 3;
        }
        
        [Given(@"I entered side item name with less than (.*) characters")]
        public void GivenIEnteredSideItemNameWithLessThanCharacters(int p0)
        {
            _sideItemDto.SideItemName = "Fr";
            _sideItemDto.Value = 1;
            _sideItemDto.SideItemId = 3;
        }
        
        [Given(@"I entered side item name with more than (.*) characters")]
        public void GivenIEnteredSideItemNameWithMoreThanCharacters(int p0)
        {
            _sideItemDto.SideItemName = "Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries Fries ";
            _sideItemDto.Value = 1;
            _sideItemDto.SideItemId = 3;
        }
        
        [Given(@"I left side item value")]
        public void GivenILeftSideItemValue()
        {
            _sideItemDto.SideItemName = "Fries";
            _sideItemDto.SideItemId = 3;
        }
        
        [Given(@"I entered side item name and invalid number for value")]
        public void GivenIEnteredSideItemNameAndInvalidNumberForValue()
        {
            _sideItemDto.SideItemName = "Fries";
            _sideItemDto.Value = 0;
            _sideItemDto.SideItemId = 3;
        }
        
        [When(@"I click on add side item")]
        public void WhenIClickOnAddSideItem()
        {
            try
            {
                _SideItemFacade.AddSideItem(_sideItemDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException e)
            {
                _exception = e;
            }
        }
        
        [Then(@"the side item will be added successfully deactivated")]
        public void ThenTheSideItemWillBeAddedSuccessfullyDeactivated()
        {
            var sideItem = _SideItemFacade.GetSideItem(_sideItemDto.SideItemId, Strings.DefaultLanguage);
            Assert.NotNull(sideItem);
        }
        
        [Then(@"Missing side item name validation message will return")]
        public void ThenMissingSideItemNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptySideItemName);
        }
        
        [Then(@"repeated side item name validation message will return")]
        public void ThenRepeatedSideItemNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SideItemNameAlreadyExist);
        }
        
        [Then(@"Minimum length for side item name validation message will return")]
        public void ThenMinimumLengthForSideItemNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SideItemNameMinimumLength);
        }
        
        [Then(@"Maximum length for side item name validation message will return")]
        public void ThenMaximumLengthForSideItemNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SideItemNameExceedLength);
        }
        
        [Then(@"Missing side item value validation message will return")]
        public void ThenMissingSideItemValueValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.InvalidSideItemValue);
        }
        
        [Then(@"Invalid side item value validation message will return")]
        public void ThenInvalidSideItemValueValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.InvalidSideItemValue);
        }

        public RestaurantAdminAddNewSideItemSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
