using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.BLL.DataService.Interfaces;
using ECatalog.Common;
using ECatalog.DAL.Entities;
using ECatalog.DAL.Entities.Model;
using Service.Pattern;

namespace ECatalog.BLL.DataServices.FakeServices
{
    public class fakeUserService:Service<User>,IUserService
    {
        private fakeData dbFakeData;
        public fakeUserService()
        {
            dbFakeData = new fakeData();
        }
        public User ValidateUser(string userName, string password)
        {
            return dbFakeData._Users.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password &&
                                          !u.IsDeleted);
        }

        public bool CheckUserNameDuplicated(string userName)
        {
            return dbFakeData._Users.Any(u => u.UserName.ToLower() == userName);
        }

        public User CheckUserIsDeleted(string userName, string password)
        {
            return dbFakeData._Users.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password );
        }

        public bool CheckUserNameDuplicatedForWaiter(string userName)
        {
            return dbFakeData._Users.Any(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDeleted && (u.Role == Common.Enums.RoleType.GlobalAdmin || u.Role == Enums.RoleType.RestaurantAdmin));
        }

        public override void Insert(User entity)
        {
            dbFakeData._Users.Add(entity);
        }
    }
}
