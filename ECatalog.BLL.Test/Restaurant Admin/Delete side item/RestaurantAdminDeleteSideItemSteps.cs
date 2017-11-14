using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Delete_side_item
{
    [Binding]
    public class RestaurantAdminDeleteSideItemSteps:BaseStep
    {
        private UserDto _userDto;
        private long _sideItemId;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
        }
        [Given(@"I am logged in as a restaurant admin to delete side item")]
        public void GivenIAmLoggedInAsARestaurantAdminToDeleteSideItem()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I select the side item to deleted")]
        public void GivenISelectTheSideItemToDeleted()
        {
            _sideItemId = 1;
        }
        
        [When(@"I click on delete side item")]
        public void WhenIClickOnDeleteSideItem()
        {
            _SideItemFacade.DeleteSideItem(_sideItemId);
        }
        
        [Then(@"the side item will be deleted")]
        public void ThenTheSideItemWillBeDeleted()
        {
            var exception = Assert.Catch<NotFoundException>(() =>
            {
                _SideItemFacade.GetSideItem(_sideItemId, Strings.DefaultLanguage);
            });
            Assert.AreEqual(exception.ErrorCode, ErrorCodes.SideItemDeleted);
        }

        public RestaurantAdminDeleteSideItemSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
