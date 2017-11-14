using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECatalog.API.Models
{
    public class MenuModel
    {
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public bool IsActive { get; set; }
    }
}