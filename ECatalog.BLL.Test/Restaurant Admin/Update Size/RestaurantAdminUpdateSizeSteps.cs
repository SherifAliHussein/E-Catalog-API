using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Update_Size
{
    [Binding]
    public class RestaurantAdminUpdateSizeSteps:BaseStep
    {
        private UserDto _userDto;
        private SizeDto _sizeDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _sizeDto = new SizeDto();
        }
        [Given(@"I am logged in as a restaurant admin to update size")]
        public void GivenIAmLoggedInAsARestaurantAdminToUpdateSize()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [When(@"I update the current menu name with new name")]
        public void WhenIUpdateTheCurrentMenuNameWithNewName()
        {
            _sizeDto.SizeName = "new size";
            _sizeDto.SizeId = 1;
            try
            {
                _SizeFacade.UpdateSize(_sizeDto,_userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update the current menu name with empty name")]
        public void WhenIUpdateTheCurrentMenuNameWithEmptyName()
        {
            _sizeDto.SizeName = "";
            _sizeDto.SizeId = 1;
            try
            {
                _SizeFacade.UpdateSize(_sizeDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update the current menu name with exist name")]
        public void WhenIUpdateTheCurrentMenuNameWithExistName()
        {
            _sizeDto.SizeName = "regualr";
            _sizeDto.SizeId = 2;
            try
            {
                _SizeFacade.UpdateSize(_sizeDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update the current menu with long name")]
        public void WhenIUpdateTheCurrentMenuWithLongName()
        {
            _sizeDto.SizeName = "regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr regualr ";
            _sizeDto.SizeId = 1;
            try
            {
                _SizeFacade.UpdateSize(_sizeDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I update the current menu with short name")]
        public void WhenIUpdateTheCurrentMenuWithShortName()
        {
            _sizeDto.SizeName = "rg";
            _sizeDto.SizeId = 1;
            try
            {
                _SizeFacade.UpdateSize(_sizeDto, _userDto.UserId, Strings.DefaultLanguage);
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [Then(@"size name will update successfully")]
        public void ThenSizeNameWillUpdateSuccessfully()
        {
            var size = _SizeFacade.GetSize(_sizeDto.SizeId, Strings.DefaultLanguage);
            Assert.AreEqual(_sizeDto.SizeName, size.SizeName);
        }
        
        [Then(@"Missing size name validation message will return for the updated size")]
        public void ThenMissingSizeNameValidationMessageWillReturnForTheUpdatedSize()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptySizeName);
        }
        
        [Then(@"repeated size name validation message will return for the updated size")]
        public void ThenRepeatedSizeNameValidationMessageWillReturnForTheUpdatedSize()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SizeNameAlreadyExist);
        }
        
        [Then(@"Maximum length for size name validation message will return for the updated size")]
        public void ThenMaximumLengthForSizeNameValidationMessageWillReturnForTheUpdatedSize()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SizeNameExceedLength);
        }
        [Then(@"Minimum length for size name validation message will return for the updated size")]
        public void ThenMinimumLengthForSizeNameValidationMessageWillReturnForTheUpdatedSize()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.SizeNameMinimumLength);
        }


        public RestaurantAdminUpdateSizeSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
