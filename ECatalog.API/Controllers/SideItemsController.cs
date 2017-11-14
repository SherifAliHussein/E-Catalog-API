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
    public class SideItemsController : BaseApiController
    {
        private ISideItemFacade _sideItemFacade;

        public SideItemsController(ISideItemFacade sideItemFacade)
        {
            _sideItemFacade = sideItemFacade;
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems", Name = "AddSideItem")]
        [HttpPost]
        public IHttpActionResult AddSideItem([FromBody] SideItemModel sideItemModel)
        {
            _sideItemFacade.AddSideItem(Mapper.Map<SideItemDTO>(sideItemModel),UserId, Language);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems/", Name = "GetAllSideItems")]
        [HttpGet]
        [ResponseType(typeof(List<SideItemModel>))]
        public IHttpActionResult GetAllSideItems(int page = Page, int pagesize = PageSize)
        {
            var sideItems = _sideItemFacade.GetAllSideItems(Language,UserId, page, pagesize);
            return PagedResponse("GetAllSideItems", page, pagesize, sideItems.TotalCount, Mapper.Map<List<SideItemModel>>(sideItems.Data),true);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems/{sideItemId:long}", Name = "DeleteSideItem")]
        [HttpDelete]
        public IHttpActionResult DeleteSideItem(long sideItemId)
        {
            _sideItemFacade.DeleteSideItem(sideItemId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems", Name = "UpdateSideItem")]
        [HttpPut]
        public IHttpActionResult UpdateSideItem([FromBody] SideItemModel sideItemModel)
        {
            _sideItemFacade.UpdateSideItem(Mapper.Map<SideItemDTO>(sideItemModel), UserId, Language);
            return Ok();
        }
    }
}
