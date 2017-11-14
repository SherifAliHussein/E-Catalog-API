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
    public class SizesController : BaseApiController
    {
        private ISizeFacade _sizeFacade;

        public SizesController(ISizeFacade sizeFacade)
        {
            _sizeFacade = sizeFacade;
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Sizes", Name = "AddSize")]
        [HttpPost]
        public IHttpActionResult AddSize([FromBody] SizeModel sizeModel)
        {
            _sizeFacade.AddSize(Mapper.Map<SizeDto>(sizeModel), UserId, Language);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Sizes/", Name = "GetAllSizes")]
        [HttpGet]
        [ResponseType(typeof(List<SizeModel>))]
        public IHttpActionResult GetAllSizes(int page = Page, int pagesize = PageSize)
        {
            var sizes = _sizeFacade.GetAllSizes(Language,UserId, page, pagesize);
            return PagedResponse("GetAllSizes", page, pagesize, sizes.TotalCount, Mapper.Map<List<SizeModel>>(sizes.Data),true);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Sizes/{sizeId:long}", Name = "DeleteSize")]
        [HttpDelete]
        public IHttpActionResult DeleteSize(long sizeId)
        {
            _sizeFacade.DeleteSize(sizeId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Sizes", Name = "UpdateSize")]
        [HttpPut]
        public IHttpActionResult UpdateSize([FromBody] SizeModel sizeModel)
        {
            _sizeFacade.UpdateSize(Mapper.Map<SizeDto>(sizeModel),UserId, Language);
            return Ok();
        }
    }
}
