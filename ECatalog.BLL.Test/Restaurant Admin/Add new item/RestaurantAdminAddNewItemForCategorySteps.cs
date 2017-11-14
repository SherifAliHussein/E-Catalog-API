using System;
using System.Collections.Generic;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Add_new_item
{
    [Binding]
    public class RestaurantAdminAddNewItemForCategorySteps:BaseStep
    {
        private UserDto _userDto;
        private ItemDTO _itemDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _itemDto = new ItemDTO();
        }

        [Given(@"I am logged in as a restaurant admin to add new item for category")]
        public void GivenIAmLoggedInAsARestaurantAdminToAddNewItemForCategory()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I entered item name, price and description and select the image")]
        public void GivenIEnteredItemNamePriceAndDescriptionAndSelectTheImage()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "Grilled chicken";
            _itemDto.ItemDescription = "Grilled chicken descc";
            //_itemDto.Price = 1.9;
            _itemDto.ItemID = 3;
            _itemDto.Sizes = new List<SizeDto> {new SizeDto {SizeId = 1,Price = 1.9}};
            _itemDto.SideItems = new List<SideItemDTO> {new SideItemDTO {SideItemId = 1}};
        }
        
        [Given(@"I left item name")]
        public void GivenILeftItemName()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "";
            _itemDto.ItemDescription = "Grilled chicken descc";
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 1.9 } };
        }
        
        [Given(@"I entered existing item name for the same category")]
        public void GivenIEnteredExistingItemNameForTheSameCategory()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "item1";
            _itemDto.ItemDescription = "Grilled chicken descc" ;
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 1.9 } };
        }
        
        [Given(@"I entered item name with more than (.*) characters")]
        public void GivenIEnteredItemNameWithMoreThanCharacters(int p0)
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken";
            _itemDto.ItemDescription = "Grilled chicken descc";
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 1.9 } };
        }
        
        [Given(@"I left item description")]
        public void GivenILeftItemDescription()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "Grilled chicken";
            _itemDto.ItemDescription = "";
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 1.9 } };
        }
        
        [Given(@"I entered item name, description and image and price less than or equal zero")]
        public void GivenIEnteredItemNameDescriptionAndImageAndPriceLessThanOrEqualZero()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "Grilled chicken";
            _itemDto.ItemDescription = "Grilled chicken descc";
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = -1.9 } };
        }
        
        [When(@"I click on add item")]
        public void WhenIClickOnAddItem()
        {
            try
            {
                _ItemFacade.AddItem(_itemDto,Strings.DefaultLanguage,"");
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [Then(@"the item will be added successfully")]
        public void ThenTheItemWillBeAddedSuccessfully()
        {
            var item = _ItemFacade.GetItem(_itemDto.ItemID, Strings.DefaultLanguage);
            Assert.IsNotNull(item);
        }
        
        [Then(@"Missing item name validation message will return")]
        public void ThenMissingItemNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyItemName);
        }
        
        [Then(@"repeated item name validation message will return")]
        public void ThenRepeatedItemNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.ItemNameAlreadyExist);
        }
        
        [Then(@"Maximum length for item name validation message will return")]
        public void ThenMaximumLengthForItemNameValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.ItemNameExceedLength);
        }
        
        [Then(@"Missing item description validation message will return")]
        public void ThenMissingItemDescriptionValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyItemDescription);
        }
        
        [Then(@"item price should be positive validation message will return")]
        public void ThenItemPriceShouldBePositiveValidationMessageWillReturn()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.InvalidItemPrice);
        }

        public RestaurantAdminAddNewItemForCategorySteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
