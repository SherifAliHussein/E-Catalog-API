using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECatalog.API.Models
{
    public class SideItemModel
    {
        public long SideItemId { get; set; }
        public string SideItemName { get; set; }
        public int Value { get; set; }
    }
}