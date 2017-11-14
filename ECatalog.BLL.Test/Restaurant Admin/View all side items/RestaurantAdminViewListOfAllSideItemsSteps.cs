using System;
using System.Collections.Generic;
using System.Linq;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.View_all_side_items
{
    [Binding]
    public class RestaurantAdminViewListOfAllSideItemsSteps:BaseStep
    {
        private UserDto _userDto;
        private List<SideItemDTO> _sideItem;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _sideItem = new List<SideItemDTO>();
        }

        [Given(@"I am logged in as a restaurant admin to view list of all side items")]
        public void GivenIAmLoggedInAsARestaurantAdminToViewListOfAllSideItems()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [When(@"I list all side items")]
        public void WhenIListAllSideItems()
        {
            _sideItem = (List<SideItemDTO>)_SideItemFacade.GetAllSideItems(Strings.DefaultLanguage, _userDto.UserId, 1, 10).Data;
        }

        [Then(@"the list of side items will display with the name and value")]
        public void ThenTheListOfSideItemsWillDisplayWithTheNameAndValue()
        {
            Assert.AreEqual(0, _sideItem.Count(x => string.IsNullOrEmpty(x.SideItemName)
                                                 || x.SideItemId < 0 || x.Value < 0));
        }

        public RestaurantAdminViewListOfAllSideItemsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
