using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECatalog.BLL.DTOs
{
    public class ItemDTO
    {
        public long ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public long CategoryId { get; set; }
        public long MenuId { get; set; }
        public long RestaurantId { get; set; }
        //public double Price { get; set; }
        public List<SizeDto> Sizes { get; set; }
        public List<SideItemDTO> SideItems{ get; set; }
        public MemoryStream Image { get; set; }
        public bool IsImageChange { get; set; }
        public int MaxSideItemValue { get; set; }
        public bool IsActive { get; set; }
    }
}
