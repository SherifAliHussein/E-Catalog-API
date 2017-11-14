using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DataService.Interfaces;
using ECatalog.DAL.Entities;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;

namespace ECatalog.BLL.DataServices.FakeServices
{
    public class fakeRestaurantTypeService:Service<RestaurantType>, IRestaurantTypeService
    {
        private fakeData dbFakeData;

        public fakeRestaurantTypeService()
        {
            dbFakeData = new fakeData();
        }

        public override void Insert(RestaurantType entity)
        {
            dbFakeData._RestaurantTypes.Add(entity);
        }

        public long GetLastRecordId()
        {
            var lastType = dbFakeData._RestaurantTypes.OrderBy(x => x.RestaurantTypeId).LastOrDefault();
            return lastType?.RestaurantTypeId ?? 0;
        }
    }
}
