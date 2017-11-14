using System;
using System.Collections.Generic;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.Update_item
{
    [Binding]
    public class RestaurantAdminUpdateItemSteps:BaseStep
    {
        private UserDto _userDto;
        private ItemDTO _itemDto;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _itemDto = new ItemDTO();
        }
        [Given(@"I am logged in as a restaurant admin to update item")]
        public void GivenIAmLoggedInAsARestaurantAdminToUpdateItem()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [Given(@"I update the current item name with new name")]
        public void GivenIUpdateTheCurrentItemNameWithNewName()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName ="new Grilled chicken" ;
            _itemDto.ItemDescription = "Grilled chicken descc";
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 1.9 } };
            _itemDto.SideItems = new List<SideItemDTO> {new SideItemDTO {SideItemId = 1} };
            _itemDto.ItemID = 1; 
        }
        
        [Given(@"I update the current item name with empty name")]
        public void GivenIUpdateTheCurrentItemNameWithEmptyName()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName ="";
            _itemDto.ItemDescription = "Grilled chicken descc";
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 1.9 } };
            _itemDto.SideItems = new List<SideItemDTO> { new SideItemDTO { SideItemId = 1 } };
            _itemDto.ItemID = 3;
        }
        
        [Given(@"I update the current item name with exist name")]
        public void GivenIUpdateTheCurrentItemNameWithExistName()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "item1";
            _itemDto.ItemDescription = "Grilled chicken descc";
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 1.9 } };
            _itemDto.SideItems = new List<SideItemDTO> { new SideItemDTO { SideItemId = 1 } };
            _itemDto.ItemID = 3;
        }
        
        [Given(@"I update the current item with long name")]
        public void GivenIUpdateTheCurrentItemWithLongName()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken Grilled chicken";
            _itemDto.ItemDescription = "Grilled chicken descc";
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 1.9 } };
            _itemDto.SideItems = new List<SideItemDTO> { new SideItemDTO { SideItemId = 1 } };
            _itemDto.ItemID = 3;
        }
        
        //[Given(@"I click on update item")]
        //public void GivenIClickOnUpdateItem()
        //{
        //    ScenarioContext.Current.Pending();
        //}
        
        [Given(@"I update the current item price with new price less than or equal zero")]
        public void GivenIUpdateTheCurrentItemPriceWithNewPriceLessThanOrEqualZero()
        {
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "Grilled chicken";
            _itemDto.ItemDescription = "Grilled chicken descc";
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 0 } };
            _itemDto.SideItems = new List<SideItemDTO> { new SideItemDTO { SideItemId = 1 } };
            _itemDto.ItemID = 3;
        }
        
        [Given(@"I update the current item description with empty")]
        public void GivenIUpdateTheCurrentItemDescriptionWithEmpty()
        { 
            _itemDto.CategoryId = 1;
            _itemDto.ItemName = "Grilled chicken";
            _itemDto.ItemDescription = "" ;
            _itemDto.Sizes = new List<SizeDto> { new SizeDto { SizeId = 1, Price = 1.9 } };
            _itemDto.SideItems = new List<SideItemDTO> { new SideItemDTO { SideItemId = 1 } };
            _itemDto.ItemID = 3;
        }
        
        [When(@"I click on update item")]
        public void WhenIClickOnUpdateItem()
        {
            try
            {
                _ItemFacade.UpdateItem(_itemDto,Strings.DefaultLanguage,"");
            }
            catch (ValidationException ex)
            {
                _exception = ex;
            }
        }
        
        [Then(@"item name will update successfully")]
        public void ThenItemNameWillUpdateSuccessfully()
        {
            var item = _ItemFacade.GetItem(_itemDto.ItemID, Strings.DefaultLanguage);
            Assert.AreEqual("new Grilled chicken", item.ItemName);
        }
        
        [Then(@"Missing item name validation message will return for the update item")]
        public void ThenMissingItemNameValidationMessageWillReturnForTheUpdateItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyItemName);
        }
        
        [Then(@"repeated item name validation message will return for the update item")]
        public void ThenRepeatedItemNameValidationMessageWillReturnForTheUpdateItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.ItemNameAlreadyExist);
        }
        
        [Then(@"Maximum length for item name validation message will return for the update item")]
        public void ThenMaximumLengthForItemNameValidationMessageWillReturnForTheUpdateItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.ItemNameExceedLength);
        }
        
        [Then(@"item price should be positive validation message will return for the update item")]
        public void ThenItemPriceShouldBePositiveValidationMessageWillReturnForTheUpdateItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.InvalidItemPrice);
        }
        
        [Then(@"Missing item description validation message will return for the update item")]
        public void ThenMissingItemDescriptionValidationMessageWillReturnForTheUpdateItem()
        {
            Assert.AreEqual(_exception.ErrorCode, ErrorCodes.EmptyItemDescription);
        }

        public RestaurantAdminUpdateItemSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
