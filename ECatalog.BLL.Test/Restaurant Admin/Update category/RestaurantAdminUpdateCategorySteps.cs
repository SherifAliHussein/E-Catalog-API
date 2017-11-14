using System;
using System.Collections.Generic;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Update_category
{
    [Binding]
    public class RestaurantAdminUpdateCategorySteps:BaseStep
    {
        private UserDto _userDto;
        private CategoryDTO _categoryDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _categoryDto = new CategoryDTO();
        }
        [Given(@"I am logged in as a restaurant admin to update category")]
        public void GivenIAmLoggedInAsARestaurantAdminToUpdateCategory()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I update the current category name with new name")]
        public void GivenIUpdateTheCurrentCategoryNameWithNewName()
        {
            _categoryDto.MenuId = 2;
            _categoryDto.CategoryId = 1;
            _categoryDto.CategoryName = "new chicken";
        }
        
        [Given(@"I update the current category name with empty name")]
        public void GivenIUpdateTheCurrentCategoryNameWithEmptyName()
        {
            _categoryDto.MenuId = 2;
            _categoryDto.CategoryId = 1;
            _categoryDto.CategoryName = "";
        }
        
        [Given(@"I update the current category name with exist name")]
        public void GivenIUpdateTheCurrentCategoryNameWithExistName()
        {
            _categoryDto.MenuId = 2;
            _categoryDto.CategoryId = 1;
            _categoryDto.CategoryName = "category4";
        }
        
        [Given(@"I update the current category with long name")]
        public void GivenIUpdateTheCurrentCategoryWithLongName()
        {
            _categoryDto.MenuId = 2;
            _categoryDto.CategoryId = 1;
            _categoryDto.CategoryName ="chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chickenchicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken   chicken chicken chickenchicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken  ";
        }
        
        [When(@"I click on update category")]
        public void WhenIClickOnUpdateCategory()
        {
            try
            {
                _CategoryFacade.UpdateCategory(_categoryDto,Strings.DefaultLanguage,"");
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [Then(@"category name will update successfully")]
        public void ThenCategoryNameWillUpdateSuccessfully()
        {
            var category = _CategoryFacade.GetCategory(_categoryDto.CategoryId, Strings.DefaultLanguage);
            Assert.AreEqual("new chicken", category.CategoryName);
        }
        
        [Then(@"Missing category name validation message will return for the updated category")]
        public void ThenMissingCategoryNameValidationMessageWillReturnForTheUpdatedCategory()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyCategoryName);
        }

        [Then(@"repeated category name validation message will return for the updated category")]
        public void ThenRepeatedCategoryNameValidationMessageWillReturnForTheUpdatedCategory()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.CategoryNameAlreadyExist);
        }
        
        [Then(@"Maximum length for category name validation message will return for the updated category")]
        public void ThenMaximumLengthForCategoryNameValidationMessageWillReturnForTheUpdatedCategory()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.CategoryNameExceedLength);
        }

        public RestaurantAdminUpdateCategorySteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
