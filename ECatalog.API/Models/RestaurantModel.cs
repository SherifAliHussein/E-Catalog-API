using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECatalog.API.Models
{
    public class RestaurantModel
    {
        public long RestaurantId { get; set; }
        public string RestaurantAdminUserName { get; set; }
        public string RestaurantAdminPassword { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantTypeName { get; set; }
        public long RestaurantTypeId { get; set; }
        public bool IsActive { get; set; }

        public string LogoURL { get; set; }
        public bool IsReady { get; set; }
        public bool IsLogoChange { get; set; }
    }
}