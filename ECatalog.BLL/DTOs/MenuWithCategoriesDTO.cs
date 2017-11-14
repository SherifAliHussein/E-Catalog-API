using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECatalog.BLL.DTOs
{
    public class MenuWithCategoriesDTO
    {
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public bool IsActive { get; set; }

        public List<CategoryDTO> Categories { get; set; }
    }
}
