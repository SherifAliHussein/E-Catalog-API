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
    public class fakeSizeService:Service<Size>,ISizeService
    {
        private fakeData dbFakeData;

        public fakeSizeService()
        {
            dbFakeData = new fakeData();
        }

        public override void Insert(Size entity)
        {
            dbFakeData._Sizes.Add(entity);
        }

        public override Size Find(params object[] keyValues)
        {
            return dbFakeData._Sizes.FirstOrDefault(x => x.SizeId == (long) keyValues[0]);
        }
        public override void Update(Size entity)
        {
            var size = dbFakeData._Sizes.FirstOrDefault(x => x.SizeId == entity.SizeId);
            size = entity;
        }
    }
}
