using System;
using System.Collections.Generic;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Add_new_category
{
    [Binding]
    public class RestaurantAdminAddNewCategoryForMenuSteps:BaseStep
    {
        private UserDto _userDto;
        private CategoryDTO _categoryDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _categoryDto = new CategoryDTO();
        }
        [Given(@"I am logged in as a restaurant admin to add new category for menu")]
        public void GivenIAmLoggedInAsARestaurantAdminToAddNewCategoryForMenu()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I entered category name and select the image")]
        public void GivenIEnteredCategoryNameAndSelectTheImage()
        {
            _categoryDto.MenuId = 2;
            _categoryDto.CategoryId = 3;
            _categoryDto.CategoryName = "chicken";
        }
        
        [Given(@"I left category name")]
        public void GivenILeftCategoryName()
        {
            _categoryDto.MenuId = 2;
            _categoryDto.CategoryId = 3;
            _categoryDto.CategoryName = "";
        }
        
        [When(@"I click on add category")]
        public void WhenIClickOnAddCategory()
        {
            try
            {
                _CategoryFacade.AddCategory(_categoryDto,Strings.DefaultLanguage,"");
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [When(@"I entered existing category name for the same menu")]
        public void WhenIEnteredExistingCategoryNameForTheSameMenu()
        {
            _categoryDto.MenuId = 2;
            _categoryDto.CategoryId = 3;
            _categoryDto.CategoryName = "category2";
        }
        
        [When(@"I entered category name with more than (.*) characters")]
        public void WhenIEnteredCategoryNameWithMoreThanCharacters(int p0)
        {
            _categoryDto.MenuId = 2;
            _categoryDto.CategoryId = 3;
            _categoryDto.CategoryName = "chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chickenchicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken   chicken chicken chickenchicken chicken chicken chicken chicken chicken chicken chicken chicken chicken chicken  ";
        }
        
        [Then(@"the category will be added successfully deactivated")]
        public void ThenTheCategoryWillBeAddedSuccessfullyDeactivated()
        {
            var category = _CategoryFacade.GetCategory(_categoryDto.CategoryId, Strings.DefaultLanguage);
            Assert.IsFalse(category.IsActive);
        }
        
        [Then(@"Missing category name validation message will return")]
        public void ThenMissingCategoryNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyCategoryName);
        }
        
        [Then(@"repeated category name validation message will return")]
        public void ThenRepeatedCategoryNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.CategoryNameAlreadyExist);
        }
        
        [Then(@"Maximum length for category name validation message will return")]
        public void ThenMaximumLengthForCategoryNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.CategoryNameExceedLength);
        }

        public RestaurantAdminAddNewCategoryForMenuSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
