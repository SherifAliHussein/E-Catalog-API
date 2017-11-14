using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using AutoMapper;
using ECatalog.API.Infrastructure;
using ECatalog.API.Models;
using ECatalog.API.Providers;
using ECatalog.BLL.DTOs;
using ECatalog.BLL.Services.Interfaces;
using ECatalog.Common;
using ECatalog.Common.CustomException;

namespace ECatalog.API.Controllers
{
    public class ItemsController : BaseApiController
    {
        private IitemFacade _itemFacade;
        public ItemsController(IitemFacade itemFacade)
        {
            _itemFacade = itemFacade;
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items", Name = "AddItem")]
        [HttpPost]
        public IHttpActionResult AddItem()
        {

            if (!HttpContext.Current.Request.Files.AllKeys.Any())
                throw new ValidationException(ErrorCodes.EmptyItemImage);
            var httpPostedFile = HttpContext.Current.Request.Files[0];

            var itemModel = new JavaScriptSerializer().Deserialize<ItemModel>(HttpContext.Current.Request.Form.Get(0));

            if (httpPostedFile == null)
                throw new ValidationException(ErrorCodes.EmptyItemImage);

            if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                throw new ValidationException(ErrorCodes.ImageExceedSize);


            if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);

            var itemDto = Mapper.Map<ItemDTO>(itemModel);

            itemDto.Image = new MemoryStream();
            httpPostedFile.InputStream.CopyTo(itemDto.Image);

            _itemFacade.AddItem(itemDto, Language, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/{itemId:long}", Name = "GetItem")]
        [HttpGet]
        [ResponseType(typeof(ItemModel))]
        public IHttpActionResult GetItem(long itemId)
        {
            var item = Mapper.Map<ItemModel>(_itemFacade.GetItem(itemId, Language));
            item.ImageURL = Url.Link("ItemImage", new { item.RestaurantId, item.MenuId, item.CategoryId, item.ItemID });
            return Ok(item);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items", Name = "UpdateItem")]
        [HttpPut]
        public IHttpActionResult UpdateItem()
        {
            var itemModel =
                new JavaScriptSerializer().Deserialize<ItemModel>(HttpContext.Current.Request.Form.Get(0));
            var itemDto = Mapper.Map<ItemDTO>(itemModel);
            if (itemModel.IsImageChange)
            {
                if (!HttpContext.Current.Request.Files.AllKeys.Any())
                    throw new ValidationException(ErrorCodes.EmptyItemImage);
                var httpPostedFile = HttpContext.Current.Request.Files[0];


                if (httpPostedFile == null)
                    throw new ValidationException(ErrorCodes.EmptyCategoryImage);

                if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);

                //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
                itemDto.Image = new MemoryStream();
                httpPostedFile.InputStream.CopyTo(itemDto.Image);
            }
            _itemFacade.UpdateItem(itemDto, Language, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/{itemId:long}", Name = "DeleteItem")]
        [HttpDelete]
        public IHttpActionResult DeleteItem(long itemId)
        {
            _itemFacade.DeleteItem(itemId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/Translate", Name = "TranslateItem")]
        [HttpPut]
        public IHttpActionResult TranslateItem([FromBody] ItemModel itemModel)
        {
            _itemFacade.TranslateItem(Mapper.Map<ItemDTO>(itemModel), Language);
            return Ok();
        }



        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/{itemId:long}/Activate", Name = "ActivateItem")]
        [HttpGet]
        public IHttpActionResult ActivateItem(long itemId)
        {
            _itemFacade.ActivateItem(itemId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/{itemId:long}/DeActivate", Name = "DeActivateItem")]
        [HttpGet]
        public IHttpActionResult DeActivateItem(long itemId)
        {
            _itemFacade.DeActivateItem(itemId);
            return Ok();
        }

    }
}
