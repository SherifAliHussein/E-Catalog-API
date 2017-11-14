using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using ECatalog.API.Infrastructure;
using ECatalog.API.Models;
using ECatalog.API.Providers;
using ECatalog.BLL.DTOs;
using ECatalog.BLL.Services.Interfaces;
using ECatalog.Common;

namespace ECatalog.API.Controllers
{
    public class MenusController : BaseApiController
    {
        private IMenuFacade _menuFacade;
        private ICategoryFacade _categoryFacade;

        public MenusController(IMenuFacade menuFacade, ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
            _menuFacade = menuFacade;
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus", Name = "AddMenu")]
        [HttpPost]
        public IHttpActionResult AddMenu([FromBody] MenuModel menuModel)
        {
            _menuFacade.AddMenu(Mapper.Map<MenuDTO>(menuModel),UserId,Language);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/{MenuId:long}", Name = "GetMenu")]
        [HttpGet]
        [ResponseType(typeof(MenuModel))]
        public IHttpActionResult GetMenu(long menuId)
        {
            return Ok(Mapper.Map<MenuModel>(_menuFacade.GetMenu(menuId,Language)));
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin,Enums.RoleType.Waiter)]
        [Route("api/Menus/", Name = "GetAllMenuForRestaurant")]
        [HttpGet]
        [ResponseType(typeof(List<MenuModel>))]
        public IHttpActionResult GetAllMenuForRestaurant( int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto menus;
            menus = UserRole == Enums.RoleType.RestaurantAdmin.ToString() ? _menuFacade.GetAllMenusByRestaurantId(Language, UserId, page, pagesize) : _menuFacade.GetActivatedMenusByRestaurantId(Language, UserId, page, pagesize);
            return PagedResponse("GetAllMenuForRestaurant", page, pagesize, menus.TotalCount, Mapper.Map<List<MenuModel>>(menus.Data), menus.IsParentTranslated);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/{menuId:long}", Name = "DeleteMenu")]
        [HttpDelete]
        public IHttpActionResult DeleteMenu(long menuId)
        {
            _menuFacade.DeleteMenu(menuId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/{menuId:long}/Activate", Name = "ActivateMenu")]
        [HttpGet]
        public IHttpActionResult ActivateMenu(long menuId)
        {
            _menuFacade.ActivateMenu(menuId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/{menuId:long}/DeActivate", Name = "DeActivateMenu")]
        [HttpGet]
        public IHttpActionResult DeActivateMenu(long menuId)
        {
            _menuFacade.DeActivateMenu(menuId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus", Name = "UpdateMenu")]
        [HttpPut]
        public IHttpActionResult UpdateMenu([FromBody] MenuModel menuModel)
        {
            _menuFacade.UpdateMenu(Mapper.Map<MenuDTO>(menuModel), UserId, Language);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin, Enums.RoleType.Waiter)]
        [Route("api/Menus/{menuId:long}/Categories", Name = "GetAllCategoriesForMenu")]
        [HttpGet]
        [ResponseType(typeof(List<CategoryModel>))]
        public IHttpActionResult GetAllCategoriesForMenu(long menuId, int page = Page, int pagesize = PageSize)
        {
            //var categories = _categoryFacade.GetAllCategoriesByMenuId(Language, menuId, page, pagesize);
            PagedResultsDto categories;
            categories = UserRole == Enums.RoleType.RestaurantAdmin.ToString() ? _categoryFacade.GetAllCategoriesByMenuId(Language, menuId, page, pagesize) : _categoryFacade.GetActivatedCategoriesByMenuId(Language, menuId, page, pagesize);
            var data = Mapper.Map<List<CategoryModel>>(categories.Data);
            foreach (var category in data)
            {
                category.ImageURL = Url.Link("CategoryImage", new { category.RestaurantId, category.MenuId, category.CategoryId });
            }
            return PagedResponse("GetAllCategoriesForMenu", page, pagesize, categories.TotalCount, data, categories.IsParentTranslated);
        }
    }
}
