using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECatalog.BLL.DTOs
{
    public class CategoryDTO
    {
        public long CategoryId { get; set; }
        public long MenuId { get; set; }
        public long RestaurantId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public MemoryStream Image { get; set; }
        public bool IsImageChange { get; set; }
    }
}
