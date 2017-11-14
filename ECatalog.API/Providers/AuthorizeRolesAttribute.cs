using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ECatalog.Common;

namespace ECatalog.API.Providers
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
        public AuthorizeRolesAttribute(params Enums.RoleType[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}