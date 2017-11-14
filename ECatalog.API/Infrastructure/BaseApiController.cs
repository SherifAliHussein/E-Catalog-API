using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using ECatalog.API.Infrastructure.ActionResult;
using ECatalog.API.Infrastructure.Filters;
using ECatalog.Common;

namespace ECatalog.API.Infrastructure
{
    [CustomExceptionFilter]
    public class BaseApiController : ApiController
    {
        public string Language;
        public string UserRole { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

        public const int PageSize = 10;
        public const int Page = 1;

        public BaseApiController()
        {
            Language = Thread.CurrentThread.CurrentCulture.Name;
            ReadAuthenticationData();
        }

        private void ReadAuthenticationData()
        {
            if (User.Identity.IsAuthenticated)
            {
                UserId = GetTokenValue<int>(Strings.userID);
                UserName = GetTokenValue<string>(Strings.userName);
                UserRole = GetTokenValue<string>(ClaimTypes.Role);
            }
        }

        public List<string> GetTokenValues(string param)
        {
            var user = User.Identity as ClaimsIdentity;
            return user.Claims.Where(e => e.Type.EndsWith(param)).Select(c => c.Value).ToList<string>();
        }
        public T GetTokenValue<T>(string value)
        {
            var tokenValues = GetTokenValues(value);
            if (tokenValues != null)
            {
                return (T)Convert.ChangeType(tokenValues[0], typeof(T));
            }
            return default(T);
        }

        protected IHttpActionResult PagedResponse(string routeName, int currentPage, int pageSize, long totalCount, dynamic results,bool isParentTranslated)
        {
            return new PagedResponseActionResult(Request, routeName, currentPage, pageSize, totalCount, results, isParentTranslated);
        }
    }
}
