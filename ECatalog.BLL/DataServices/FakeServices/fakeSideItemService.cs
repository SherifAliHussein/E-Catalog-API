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
    public class fakeSideItemService:Service<SideItem>,ISideItemService
    {
        private fakeData dbFakeData;

        public fakeSideItemService()
        {
            dbFakeData = new fakeData();
        }
        public override void Insert(SideItem entity)
        {
            dbFakeData._SideItems.Add(entity);
        }

        public override SideItem Find(params object[] keyValues)
        {
            return dbFakeData._SideItems.FirstOrDefault(x => x.SideItemId == (long)keyValues[0]);
        }
        public override void Update(SideItem entity)
        {
            var sideItem = dbFakeData._SideItems.FirstOrDefault(x => x.SideItemId == entity.SideItemId);
            sideItem = entity;
        }
    }
}
