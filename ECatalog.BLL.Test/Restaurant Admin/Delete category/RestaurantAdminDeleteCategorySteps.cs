using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Delete_category
{
    [Binding]
    public class RestaurantAdminDeleteCategorySteps:BaseStep
    {
        private UserDto _userDto;
        private long _categoryId;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
        }
        [Given(@"I am logged in as a restaurant admin to delete category")]
        public void GivenIAmLoggedInAsARestaurantAdminToDeleteCategory()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"Select the category to delete")]
        public void GivenSelectTheCategoryToDelete()
        {
            _categoryId = 1;
        }
        
        [When(@"I delete category")]
        public void WhenIDeleteCategory()
        {
            _CategoryFacade.DeleteCategory(_categoryId);
        }
        
        [Then(@"category will be deleted")]
        public void ThenCategoryWillBeDeleted()
        {
            var exception = Assert.Catch<NotFoundException>(() =>
            {
                _CategoryFacade.GetCategory(_categoryId, Strings.DefaultLanguage);
            });
            Assert.AreEqual(exception.ErrorCode, ErrorCodes.CategoryDeleted);
        }

        public RestaurantAdminDeleteCategorySteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
