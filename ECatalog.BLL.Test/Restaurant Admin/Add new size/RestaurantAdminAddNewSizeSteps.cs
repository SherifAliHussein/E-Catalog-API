using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Add_new_size
{
    [Binding]
    public class RestaurantAdminAddNewSizeSteps:BaseStep
    {
        private UserDto _userDto;
        private SizeDto _sizeDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _sizeDto = new SizeDto();
        }
        [Given(@"I am logged in as a restaurant admin to add new size")]
        public void GivenIAmLoggedInAsARestaurantAdminToAddNewSize()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I entered size name")]
        public void GivenIEnteredSizeName()
        {
            _sizeDto.SizeName = "medium";
        }
        
        [Given(@"I left size name")]
        public void GivenILeftSizeName()
        {
            _sizeDto.SizeName = "";
            _sizeDto.SizeId = 3;
        }
        
        [Given(@"I entered existing size name for the same restaurant")]
        public void GivenIEnteredExistingSizeNameForTheSameRestaurant()
        {
            _sizeDto.SizeName = "regualr";
            _sizeDto.SizeId = 3;
        }
        
        [Given(@"I entered size name with less than (.*) characters")]
        public void GivenIEnteredSizeNameWithLessThanCharacters(int p0)
        {
            _sizeDto.SizeName = "re";
            _sizeDto.SizeId = 3;
        }
        
        [Given(@"I entered size name with more than (.*) characters")]
        public void GivenIEnteredSizeNameWithMoreThanCharacters(int p0)
        {
            _sizeDto.SizeName = "regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr ";
            _sizeDto.SizeId = 3;
        }
        
        [When(@"I click on add size")]
        public void WhenIClickOnAddSize()
        {
            try
            {
                _SizeFacade.AddSize(_sizeDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [Then(@"the size will be added successfully deactivated")]
        public void ThenTheSizeWillBeAddedSuccessfullyDeactivated()
        {
            var size = _SizeFacade.GetSize(_sizeDto.SizeId, Strings.DefaultLanguage);
            Assert.NotNull(size);
        }
        
        [Then(@"Missing size name validation message will return")]
        public void ThenMissingSizeNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptySizeName);
        }
        
        [Then(@"repeated size name validation message will return")]
        public void ThenRepeatedSizeNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SizeNameAlreadyExist);
        }
        
        [Then(@"Minimum length for size name validation message will return")]
        public void ThenMinimumLengthForSizeNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SizeNameMinimumLength);
        }
        
        [Then(@"Maximum length for size name validation message will return")]
        public void ThenMaximumLengthForSizeNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SizeNameExceedLength);
        }

        public RestaurantAdminAddNewSizeSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
