using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
using Swashbuckle.Swagger;
using System.Net.Http.Headers;

namespace ECatalog.API.Controllers
{
    public class RestaurantsController : BaseApiController
    {
        private IRestaurantFacade _restaurantFacade;
        public RestaurantsController(IRestaurantFacade restaurantFacade)
        {
            _restaurantFacade = restaurantFacade;
        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants/Type",Name = "GetRestaurantType")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<RestaurantTypeModel>))]
        public IHttpActionResult GetRestaurantType()
        {
            return Ok(Mapper.Map<List<RestaurantTypeModel>>(_restaurantFacade.GetAllRestaurantType(Language)));

        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants/Type", Name = "AddRestaurantType")]
        [HttpPost]
        public IHttpActionResult AddRestaurantType([FromBody] RestaurantTypeModel restaurantType)
        {
            _restaurantFacade.AddRestaurantType(Mapper.Map<RestaurantTypeDto>(restaurantType),Language);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants/Type", Name = "UpdateRestaurantType")]
        [HttpPut]
        public IHttpActionResult UpdateRestaurantType([FromBody] RestaurantTypeModel restaurantType)
        {
            _restaurantFacade.UpdateRestaurantType(Mapper.Map<RestaurantTypeDto>(restaurantType),Language);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants/type/{restaurantTypeId:long}", Name = "DeleteRestaurantType")]
        [HttpDelete]
        public IHttpActionResult DeleteRestaurantType(long restaurantTypeId)
        {
            _restaurantFacade.DeleteRestaurantType(restaurantTypeId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants", Name = "AddRestaurant")]
        [HttpPost]
        public IHttpActionResult AddRestaurant()
        {
            if (!HttpContext.Current.Request.Files.AllKeys.Any())
                throw new ValidationException(ErrorCodes.EmptyRestaurantLogo);
            var httpPostedFile = HttpContext.Current.Request.Files[0];
            
            var restaurant = new JavaScriptSerializer().Deserialize<RestaurantModel>(HttpContext.Current.Request.Form.Get(0));

            if (httpPostedFile == null)
                throw new ValidationException(ErrorCodes.EmptyRestaurantLogo);

            if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                throw new ValidationException(ErrorCodes.ImageExceedSize);
            

            if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);

            var restaurantDto = Mapper.Map<RestaurantDTO>(restaurant);
            //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
            restaurantDto.Image = new MemoryStream();
            httpPostedFile.InputStream.CopyTo(restaurantDto.Image);

            _restaurantFacade.AddRestaurant(restaurantDto, Language,HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }
        [Route("api/Restaurants/{restaurantId:long}/logo", Name = "RestaurantLogo")]
        public HttpResponseMessage GetRestaurantLogo(long restaurantId, string type="orignal")
        {
            try
            {
                string filePath = type == "orignal"
                    ? Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + restaurantId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(restaurantId.ToString()) && !Path.GetFileName(x).Contains("thumb"))
                    : Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + restaurantId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(restaurantId.ToString()) && Path.GetFileName(x).Contains("thumb"));
                

                HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);

                byte[] fileData = File.ReadAllBytes(filePath);

                Response.Content = new ByteArrayContent(fileData);
                Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return Response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage();
            }
        }
        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants", Name = "GetAllRestaurants")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<RestaurantModel>))]
        public IHttpActionResult GetAllRestaurants(int page = Page, int pagesize = PageSize)
        {
            var restaurants = _restaurantFacade.GetAllRestaurant(Language, page, pagesize);
            var data = Mapper.Map<List<RestaurantModel>>(restaurants.Data);
            foreach (var item in data)
            {
                item.LogoURL = Url.Link("RestaurantLogo", new {item.RestaurantId});
            }
            return PagedResponse("GetAllRestaurants", page, pagesize, restaurants.TotalCount, data,true);

        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants/{restaurantId:long}/", Name = "GetRestaurant")]
        [ResponseType(typeof(RestaurantModel))]
        [HttpGet]
        public IHttpActionResult GetRestaurant(long restaurantId)
        {
            var restaurant = Mapper.Map<RestaurantModel>(_restaurantFacade.GetRestaurant(restaurantId, Language));
            restaurant.LogoURL = Url.Link("RestaurantLogo", new {restaurantId});
            return Ok(restaurant);
        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants/{restaurantId:long}/Activate", Name = "ActivateRestaurant")]
        [HttpGet]
        public IHttpActionResult ActivateRestaurant(long restaurantId)
        {
            _restaurantFacade.ActivateRestaurant(restaurantId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants/{restaurantId:long}/DeActivate", Name = "DeActivateRestaurant")]
        [HttpGet]
        public IHttpActionResult DeActivateRestaurant(long restaurantId)
        {
            _restaurantFacade.DeActivateRestaurant(restaurantId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants/{restaurantId:long}", Name = "DeleteRestaurant")]
        [HttpDelete]
        public IHttpActionResult DeleteRestaurant(long restaurantId)
        {
            _restaurantFacade.DeleteRestaurant(restaurantId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.GlobalAdmin)]
        [Route("api/Restaurants", Name = "UpdateRestaurant")]
        [HttpPut]
        public IHttpActionResult UpdateRestaurant()
        {
            var restaurant = new JavaScriptSerializer().Deserialize<RestaurantModel>(HttpContext.Current.Request.Form.Get(0));
            var restaurantDto = Mapper.Map<RestaurantDTO>(restaurant);
            if (restaurant.IsLogoChange)
            {
                if (!HttpContext.Current.Request.Files.AllKeys.Any())
                    throw new ValidationException(ErrorCodes.EmptyRestaurantLogo);
                var httpPostedFile = HttpContext.Current.Request.Files[0];


                if (httpPostedFile == null)
                    throw new ValidationException(ErrorCodes.EmptyRestaurantLogo);

                if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);
                restaurantDto.Image = new MemoryStream();
                httpPostedFile.InputStream.CopyTo(restaurantDto.Image);
            }

            _restaurantFacade.UpdateRestaurant(restaurantDto, Language, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [Route("api/Restaurants/{restaurantId:long}/Menu/{menuId:long}/Category/{categoryId:long}", Name = "CategoryImage")]
        public HttpResponseMessage GetCategoryImage(long restaurantId,long menuId,long categoryId, string type = "orignal")
        {
            try
            {
                string filePath = type == "orignal"
                    ? Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + restaurantId + "\\" + menuId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(categoryId.ToString()) && !Path.GetFileName(x).Contains("thumb"))
                    : Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + restaurantId + "\\" + menuId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(categoryId.ToString()) && Path.GetFileName(x).Contains("thumb"));


                HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);

                byte[] fileData = File.ReadAllBytes(filePath);

                Response.Content = new ByteArrayContent(fileData);
                Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return Response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage();
            }
        }

        [Route("api/Restaurants/{restaurantId:long}/Menu/{menuId:long}/Category/{categoryId:long}/Item/{itemId:long}", Name = "ItemImage")]
        public HttpResponseMessage GetItemImage(long restaurantId, long menuId, long categoryId,long itemId, string type = "orignal")
        {
            try
            {
                string filePath = type == "orignal"
                    ? Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + restaurantId + "\\" + menuId + "\\" + categoryId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(itemId.ToString()) && !Path.GetFileName(x).Contains("thumb"))
                    : Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + restaurantId + "\\" + menuId + "\\" + categoryId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(itemId.ToString()) && Path.GetFileName(x).Contains("thumb"));


                HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);

                byte[] fileData = File.ReadAllBytes(filePath);

                Response.Content = new ByteArrayContent(fileData);
                Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return Response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage();
            }
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Restaurants/Publish", Name = "PublishRestaurant")]
        [HttpGet]
        public IHttpActionResult PublishRestaurant()
        {
            _restaurantFacade.PublishRestaurant(UserId);
            return Ok();
        }
        [AuthorizeRoles(Enums.RoleType.GlobalAdmin,Enums.RoleType.RestaurantAdmin)]
        [Route("api/Restaurants/IsReady", Name = "CheckRestaurantReady")]
        [ResponseType(typeof(RestaurantModel))]
        [HttpGet]
        public IHttpActionResult CheckRestaurantReady()
        {
            return Ok(_restaurantFacade.CheckRestaurantReady(UserId));
        }
    }
}
