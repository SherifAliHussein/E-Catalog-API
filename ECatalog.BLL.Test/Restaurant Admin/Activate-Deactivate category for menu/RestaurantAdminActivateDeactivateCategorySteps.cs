using System;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Activate_Deactivate_category_for_menu
{
    [Binding]
    public class RestaurantAdminActivateDeactivateCategorySteps:BaseStep
    {
        private UserDto _userDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
        }
        [Given(@"I am logged in as a restaurant admin to activate/deactivate category")]
        public void GivenIAmLoggedInAsARestaurantAdminToActivateDeactivateCategory()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I am logged in as a admin to activate/deactivate category")]
        public void GivenIAmLoggedInAsAAdminToActivateDeactivateCategory()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [When(@"I activate the selected category")]
        public void WhenIActivateTheSelectedCategory()
        {
            _CategoryFacade.ActivateCategory(1);
        }
        
        [When(@"I activate the selected category that has no items")]
        public void WhenIActivateTheSelectedCategoryThatHasNoItems()
        {
            try
            {
                _CategoryFacade.ActivateCategory(4);
            }
            catch (ValidationException e)
            {
                _exception = e;
            }
        }
        
        [When(@"I deactivate the selected category")]
        public void WhenIDeactivateTheSelectedCategory()
        {
            _CategoryFacade.DeActivateCategory(2);
        }
        
        [Then(@"the category will be activated")]
        public void ThenTheCategoryWillBeActivated()
        {
            var category = _CategoryFacade.GetCategory(1, Strings.DefaultLanguage);
            Assert.IsTrue(category.IsActive);
        }
        
        [Then(@"category hasn’t items validation message will return")]
        public void ThenCategoryHasnTItemsValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.CategoryItemsDoesNotActivated);
        }

        [Then(@"category will be deactivate")]
        public void ThenCategoryWillBeDeactivate()
        {
            var category = _CategoryFacade.GetCategory(2, Strings.DefaultLanguage);
            Assert.IsFalse(category.IsActive);
        }

        public RestaurantAdminActivateDeactivateCategorySteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
