using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;

namespace ECatalog.BLL.DataService.Interfaces
{
    public interface IUserService:IService<User>
    {
        User ValidateUser(string userName, string password);
        bool CheckUserNameDuplicated(string userName);
        User CheckUserIsDeleted(string userName, string password);
        bool CheckUserNameDuplicatedForWaiter(string userName);
    }
}
