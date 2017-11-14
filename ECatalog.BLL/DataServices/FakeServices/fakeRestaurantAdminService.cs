using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.DAL.Entities;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;

namespace ECatalog.BLL.DataServices.FakeServices
{
    public class fakeRestaurantAdminService:Service<RestaurantAdmin>,IRestaurantAdminService
    {
        private fakeData dbFakeData;
        public fakeRestaurantAdminService()
        {
            dbFakeData = new fakeData();
        }
        public bool CheckUserNameDuplicated(string userName, long restaurantId)
        {
            return dbFakeData._RestaurantAdmins.Any(u => u.UserName.ToLower() == userName.ToLower() && u.RestaurantId != restaurantId && !u.IsDeleted);
        }

        public override void Insert(RestaurantAdmin entity)
        {
            dbFakeData._RestaurantAdmins.Add(entity);
        }

        public override void Update(RestaurantAdmin entity)
        {
            var restaurantAdmin = dbFakeData._RestaurantAdmins.FirstOrDefault(x => x.UserId == entity.UserId);
            restaurantAdmin = entity;
        }
    }
}
