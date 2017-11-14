using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ECatalog.BLL.DataService.Interfaces;
using ECatalog.BLL.DTOs;
using ECatalog.Common;
using ECatalog.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace ECatalog.BLL.DataService
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IRepositoryAsync<User> repository) : base(repository)
        {
            _repository = repository;
        }
        public User ValidateUser(string userName, string password)
        {
            return _repository.Query(u => u.UserName.ToLower() ==  userName.ToLower() && u.Password == password && !u.IsDeleted).Select().FirstOrDefault();

        }
        public User CheckUserIsDeleted(string userName, string password)
        {
            return _repository.Query(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password).Select().FirstOrDefault();

        }
        public bool CheckUserNameDuplicated(string userName)
        {
            return _repository.Queryable().Any(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDeleted && (u.Role == Common.Enums.RoleType.GlobalAdmin || u.Role == Enums.RoleType.Waiter));
        }
        public bool CheckUserNameDuplicatedForWaiter(string userName)
        {
            return _repository.Queryable().Any(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDeleted && (u.Role == Common.Enums.RoleType.GlobalAdmin || u.Role == Enums.RoleType.RestaurantAdmin));
        }

    }
}