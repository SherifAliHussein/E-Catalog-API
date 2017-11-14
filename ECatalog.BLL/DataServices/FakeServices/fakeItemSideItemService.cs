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
    public class fakeItemSideItemService:Service<ItemSideItem>,IItemSideItemService
    {
        private fakeData dbFakeData;

        public fakeItemSideItemService()
        {
            dbFakeData = new fakeData();
        }

        public override void InsertRange(IEnumerable<ItemSideItem> entities)
        {
            dbFakeData._ItemSideItems.AddRange(entities);
        }
    }
}
