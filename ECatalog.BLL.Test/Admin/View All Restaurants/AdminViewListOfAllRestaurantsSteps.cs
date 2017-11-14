using System;
using System.Collections.Generic;
using System.Linq;
using BoDi;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.Common.CustomException;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test.Admin.View_All_Restaurants
{
    [Binding]
    public class AdminViewListOfAllRestaurantsSteps:BaseStep
    {
        private UserDto _userDto;
        private List<RestaurantDTO> _restaurantTranslationDto;


        [BeforeScenario()]
        public void Init()
        {
            _userDto = new UserDto();
            _restaurantTranslationDto = new List<RestaurantDTO>();
        }
        [Given(@"I am logged in as a admin to view list of all restaurants")]
        public void GivenIAmLoggedInAsAAdminToViewListOfAllRestaurants()
        {
            _userDto.UserId = 1;
            _userDto.Role = Enums.RoleType.GlobalAdmin;
        }
        
        [When(@"I list all restaurants")]
        public void WhenIListAllRestaurants()
        {
            _restaurantTranslationDto =(List<RestaurantDTO>) _RestaurantFacade.GetAllRestaurant(Strings.DefaultLanguage, 1, 10).Data;
        }
        
        [Then(@"the list of restaurant will display with the restaurant name, description, restaurant admin info, restaurant type name and status and thumbnail image")]
        public void ThenTheListOfRestaurantWillDisplayWithTheRestaurantNameDescriptionRestaurantAdminInfoRestaurantTypeNameAndStatusAndThumbnailImage()
        {
            Assert.AreEqual(0, _restaurantTranslationDto.Count(x => string.IsNullOrEmpty(x.RestaurantAdminUserName)
                                                       || x.RestaurantId < 0
                                                       || string.IsNullOrEmpty(x.RestaurantDescription)
                                                       || string.IsNullOrEmpty(x.RestaurantName)
                                                       || string.IsNullOrEmpty(x.RestaurantTypeName)));
        }

        public AdminViewListOfAllRestaurantsSteps(IObjectContainer objectContainer) : base(objectContainer)
        {
        }
    }
}
