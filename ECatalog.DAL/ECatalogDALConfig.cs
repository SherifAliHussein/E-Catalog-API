using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.DAL.Entities;
using ECatalog.DAL.Entities.Model;
using Microsoft.Practices.Unity;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace ECatalog.DAL
{
    public static  class ECatalogDALConfig
    {

        public static void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<IDataContextAsync, ECatalogContext>(new PerResolveLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryProvider, RepositoryProvider>(
                    new PerResolveLifetimeManager(),
                    new InjectionConstructor(new object[] { new RepositoryFactories() })
                )
                .RegisterType<IRepositoryAsync<User>, Repository<User>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantAdmin>, Repository<RestaurantAdmin>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantWaiter>, Repository<RestaurantWaiter>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Category>, Repository<Category>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<CategoryTranslation>, Repository<CategoryTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Item>, Repository<Item>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<ItemTranslation>, Repository<ItemTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Menu>, Repository<Menu>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<MenuTranslation>, Repository<MenuTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Restaurant>, Repository<Restaurant>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantTranslation>, Repository<RestaurantTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantType>, Repository<RestaurantType>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantTypeTranslation>, Repository<RestaurantTypeTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RefreshToken>, Repository<RefreshToken>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Size>, Repository<Size>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SizeTranslation>, Repository<SizeTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SideItem>, Repository<SideItem>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SideItemTranslation>, Repository<SideItemTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<ItemSideItem>, Repository<ItemSideItem>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<ItemSize>, Repository<ItemSize>>(new PerResolveLifetimeManager());


        }
    }
}
