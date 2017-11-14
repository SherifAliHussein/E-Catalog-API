using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using BoDi;
using ECatalog.BLL.DataServices.FakeServices;
using ECatalog.BLL.Services;
using ECatalog.BLL.Services.Interfaces;
using ECatalog.BLL.Services.ManageStorage;
using ECatalog.Common.CustomException;
using TechTalk.SpecFlow;

namespace ECatalog.BLL.Test
{
    public class BaseStep
    {
        private IObjectContainer objectContainer;
        public IRestaurantFacade _RestaurantFacade;
        public IMenuFacade _MenuFacade;
        public ICategoryFacade _CategoryFacade;
        public IitemFacade _ItemFacade;
        public ISizeFacade _SizeFacade;
        public ISideItemFacade _SideItemFacade;
        public ValidationException _exception;

        public BaseStep(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }
        [BeforeScenario]
        public void Setup()
        {
            ECatalogBLLConfig.RegisterMappings(new MapperConfigurationExpression());
            _RestaurantFacade = new RestaurantFacade(new fakeRestaurantTypeService(),new fakeRestaurantTypeTranslationService(),new fakeRestaurantService(), new fakeRestaurantTranlationService(),new fakeUserService(),new fakeRestaurantAdminService(),new fakeManageStorage());
            _MenuFacade = new MenuFacade(new fakeMenuService(),new fakeMenuTranslationService(),new fakeRestaurantService(),new fakeRestaurantTranlationService(),new fakeRestaurantWaiterService());
            _CategoryFacade = new CategoryFacade(new fakeCategoryService(), new fakeCategoryTranslationService(), new fakeMenuService(),new fakeManageStorage(),new fakeMenuTranslationService(),new fakeRestaurantService());
            _ItemFacade = new ItemFacade(new fakeCategoryService(), new fakeItemService(), new fakeItemTranslationService(),new fakeItemSizeService(), new fakeItemSideItemService(),new fakeManageStorage(), new fakeSizeTranslation(),new fakeSideItemTranslationService(), new fakeCategoryTranslationService(),new fakeMenuService(), new fakeRestaurantService());
            _SizeFacade = new SizeFacade(new fakeSizeService(), new fakeSizeTranslation(),new fakeRestaurantService());
            _SideItemFacade = new SideItemFacade(new fakeSideItemService(),new fakeSideItemTranslationService(),new fakeRestaurantService());
        }
    }
}
