using System;
using System.Collections.Generic;
using System.Linq;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.View_all_items_for_category
{
    [Binding]
    public class RestaurantAdminViewListOfAllItemsForCategorySteps : BaseStep
    {
        private UserDto _userDto;
        private List<ItemDTO> _itemTranslationDtos;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _itemTranslationDtos = new List<ItemDTO>();
        }
        [Given(@"I am logged in as a restaurant admin to view list of all items for category")]
        public void GivenIAmLoggedInAsARestaurantAdminToViewListOfAllItemsForCategory()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [When(@"I list all items")]
        public void WhenIListAllItems()
        {
            _itemTranslationDtos = (List<ItemDTO>) _ItemFacade.GetAllItemsByCategoryId(Strings.DefaultLanguage, 1, 1, 10).Data;
        }
        
        [Then(@"the list of items will display with the name, description, image and price")]
        public void ThenTheListOfItemsWillDisplayWithTheNameDescriptionImageAndPrice()
        {
            Assert.AreEqual(0, _itemTranslationDtos.Count(x => string.IsNullOrEmpty(x.ItemName)
                                                               || string.IsNullOrEmpty(x.ItemDescription)
                                                               || x.Sizes.Count(s=>s.Price<=0) == 0
                                                               || x.ItemID < 0));
        }

        public RestaurantAdminViewListOfAllItemsForCategorySteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
