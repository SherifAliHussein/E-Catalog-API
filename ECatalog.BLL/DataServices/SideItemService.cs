using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DataServices.Interfaces;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace ECatalog.BLL.DataServices
{
    public class SideItemService:Service<SideItem>,ISideItemService
    {
        public SideItemService(IRepositoryAsync<SideItem> repository):base(repository)
        {
            
        }
    }
}
