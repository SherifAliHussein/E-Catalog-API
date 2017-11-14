using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Configuration;
using ECatalog.API.Models;
using ECatalog.BLL;
using ECatalog.BLL.DTOs;

namespace ECatalog.API.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {

            var mapperConfiguration = new MapperConfigurationExpression();
            mapperConfiguration.CreateMap<RestaurantTypeModel, RestaurantTypeDto>();
            mapperConfiguration.CreateMap<RestaurantTypeDto, RestaurantTypeModel>();
            mapperConfiguration.CreateMap<RestaurantModel, RestaurantDTO>();
            mapperConfiguration.CreateMap<RestaurantDTO, RestaurantModel>();

            mapperConfiguration.CreateMap<MenuModel, MenuDTO>();
            mapperConfiguration.CreateMap<MenuDTO, MenuModel>();

            mapperConfiguration.CreateMap<CategoryModel, CategoryDTO>();
            mapperConfiguration.CreateMap<CategoryDTO, CategoryModel>();

            mapperConfiguration.CreateMap<SizeModel, SizeDto>();
            mapperConfiguration.CreateMap<SizeDto, SizeModel>();

            mapperConfiguration.CreateMap<SideItemModel, SideItemDTO>();
            mapperConfiguration.CreateMap<SideItemDTO, SideItemModel>();


            mapperConfiguration.CreateMap<ItemModel, ItemDTO>();
            mapperConfiguration.CreateMap<ItemDTO, ItemModel>();

            mapperConfiguration.CreateMap<ItemNamesDto, ItemNameModel>();

            mapperConfiguration.CreateMap<RestaurantWaiterModel, RestaurantWaiterDTO>();
            mapperConfiguration.CreateMap<RestaurantWaiterDTO, RestaurantWaiterModel>();

            ECatalogBLLConfig.RegisterMappings(mapperConfiguration);
            //Mapper.Initialize(m =>
            //{
            //    m.CreateMap<RestaurantTypeModel, RestaurantTypeDto>();
            //    m.CreateProfile("ff",expression => {});
            //    //m.AddProfile(ECatalogBLLConfig);
            //});
            
        }
    }
}