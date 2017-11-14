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
    public class fakeItemSizeService:Service<ItemSize>,IItemSizeService
    {
        private fakeData dbFakeData;

        public fakeItemSizeService()
        {
            dbFakeData = new fakeData();
        }

        public override void InsertRange(IEnumerable<ItemSize> entities)
        {
            dbFakeData._ItemSizes.AddRange(entities);
        }
    }
}
