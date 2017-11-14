using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Delete_item
{
    [Binding]
    public class RestaurantAdminDeleteItemSteps:BaseStep
    {
        private UserDto _userDto;
        private long _itemId;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
        }
        [Given(@"I am logged in as a restaurant admin to delete item")]
        public void GivenIAmLoggedInAsARestaurantAdminToDeleteItem()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"Select the item to delete")]
        public void GivenSelectTheItemToDelete()
        {
            _itemId = 1;
        }
        
        [When(@"I delete item")]
        public void WhenIDeleteItem()
        {
            _ItemFacade.DeleteItem(_itemId);
        }
        
        [Then(@"item will be deleted")]
        public void ThenItemWillBeDeleted()
        {
            var exception = Assert.Catch<NotFoundException>(() =>
            {
                _ItemFacade.GetItem(_itemId, Strings.DefaultLanguage);
            });
            Assert.AreEqual(exception.ErrorCode, ErrorCodes.ItemDeleted);
        }

        public RestaurantAdminDeleteItemSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
