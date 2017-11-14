using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Delete_size
{
    [Binding]
    public class RestaurantAdminDeleteSizeSteps:BaseStep
    {
        private UserDto _userDto;
        private long _sizeId;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
        }
        [Given(@"I am logged in as a restaurant admin to delete size")]
        public void GivenIAmLoggedInAsARestaurantAdminToDeleteSize()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I select the size to deleted")]
        public void GivenISelectTheSizeToDeleted()
        {
            _sizeId = 1;
        }
        
        [When(@"I click on delete size")]
        public void WhenIClickOnDeleteSize()
        {
            _SizeFacade.DeleteSize(_sizeId);
        }
        
        [Then(@"the size will be deleted")]
        public void ThenTheSizeWillBeDeleted()
        {
            var exception = Assert.Catch<NotFoundException>(() =>
            {
                _SizeFacade.GetSize(_sizeId, Strings.DefaultLanguage);
            });
            Assert.AreEqual(exception.ErrorCode, ErrorCodes.SizeDeleted);
        }

        public RestaurantAdminDeleteSizeSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
