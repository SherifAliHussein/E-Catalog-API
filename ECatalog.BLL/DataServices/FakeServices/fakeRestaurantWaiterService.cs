using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.DAL.Entities;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;

namespace ECatalog.BLL.DataServices.FakeServices
{
    public class fakeRestaurantWaiterService:Service<RestaurantWaiter>,IRestaurantWaiterService
    {
        private fakeData dbFakeData;

        public fakeRestaurantWaiterService()
        {
            dbFakeData = new fakeData();
        }

        public bool CheckUserNameDuplicated(string userName, long restaurantId)
        {
            return dbFakeData.RestaurantWaiters.Any(u => u.UserName.ToLower() == userName.ToLower() && u.RestaurantId != restaurantId && !u.IsDeleted);
        }

        public PagedResultsDto GetAllRestaurantWaiters(long restaurantId,int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = dbFakeData.RestaurantWaiters.Count(x => !x.IsDeleted && x.RestaurantId == restaurantId);
            results.Data = Mapper.Map<List<RestaurantWaiter>, List<RestaurantWaiterDTO>>(dbFakeData.RestaurantWaiters.Where(x => !x.IsDeleted && x.RestaurantId == restaurantId)
                .OrderBy(x => x.RestaurantId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList());
            return results;
        }
    }
}
