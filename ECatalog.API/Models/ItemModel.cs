using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ECatalog.API.Models
{
    public class ItemModel
    {
        public long ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public long CategoryId { get; set; }
        public long MenuId { get; set; }
        public long RestaurantId { get; set; }
        public List<SizeModel> Sizes { get; set; }
        public List<SideItemModel> SideItems { get; set; }
        public string ImageURL { get; set; }
        public bool IsImageChange { get; set; }
        public int MaxSideItemValue { get; set; }
        public bool IsActive { get; set; }
    }
}