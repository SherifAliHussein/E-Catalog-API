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
    public class WaitersController : BaseApiController
    {
        private IUserFacade _userFacade;

        public WaitersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters", Name = "GetAllRestaurantWaiters")]
        [HttpGet]
        [ResponseType(typeof(List<RestaurantWaiterModel>))]
        public IHttpActionResult GetAllRestaurantWaiters(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto waiters;
            waiters = _userFacade.GetAllRestaurantWaiters(UserId, page, pagesize);
            var data = Mapper.Map<List<RestaurantWaiterModel>>(waiters.Data);
            return PagedResponse("GetAllItemsForCategory", page, pagesize, waiters.TotalCount, data, waiters.IsParentTranslated);
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters", Name = "AddRestaurantWaiter")]
        [HttpPost]
        public IHttpActionResult AddRestaurantWaiter([FromBody] RestaurantWaiterModel restaurantWaiterModel)
        { 
            _userFacade.AddRestaurantWaiter(Mapper.Map<RestaurantWaiterDTO>(restaurantWaiterModel),UserId);
            return Ok();
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters/{waiterId:long}", Name = "GetRestaurantWaiter")]
        [HttpGet]
        public IHttpActionResult GetRestaurantWaiter(long waiterId)
        {
            _userFacade.GetRestaurantWaiter(waiterId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters/{waiterId:long}", Name = "DeleteRestaurantWaiter")]
        [HttpDelete]
        public IHttpActionResult DeleteRestaurantWaiter(long waiterId)
        {
            _userFacade.DeleteRestaurantWaiter(waiterId);
            return Ok();
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters", Name = "UpdateRestaurantWaiter")]
        [HttpPut]
        public IHttpActionResult UpdateRestaurantWaiter([FromBody] RestaurantWaiterModel restaurantWaiterModel)
        {
            _userFacade.UpdateRestaurantWaiter(Mapper.Map<RestaurantWaiterDTO>(restaurantWaiterModel));
            return Ok();
        }

    }
}
