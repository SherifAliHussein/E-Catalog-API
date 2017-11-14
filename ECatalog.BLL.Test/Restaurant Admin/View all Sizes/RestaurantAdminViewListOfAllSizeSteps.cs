using System;
using System.Collections.Generic;
using System.Linq;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Restaurant_Admin.View_all_Sizes
{
    [Binding]
    public class RestaurantAdminViewListOfAllSizeSteps:BaseStep
    {
        private UserDto _userDto;
        private List<SizeDto> _sizes;

        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _sizes = new List<SizeDto>();
        }

        [Given(@"I am logged in as a restaurant admin to view list of all sizes")]
        public void GivenIAmLoggedInAsARestaurantAdminToViewListOfAllSizes()
        {
            _userDto.UserId = 2;
            _userDto.Role = Enums.RoleType.RestaurantAdmin;
        }
        
        [When(@"I list all sizes")]
        public void WhenIListAllSizes()
        {
            _sizes = (List<SizeDto>) _SizeFacade.GetAllSizes(Strings.DefaultLanguage,_userDto.UserId, 1, 10).Data;
        }
        
        [Then(@"the list of sizes will display with the name")]
        public void ThenTheListOfSizesWillDisplayWithTheName()
        {
            Assert.AreEqual(0, _sizes.Count(x => string.IsNullOrEmpty(x.SizeName)
                                                               || x.SizeId < 0));
        }

        public RestaurantAdminViewListOfAllSizeSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
