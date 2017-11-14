using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECatalog.BLL.DTOs
{
    public class RestaurantWaiterDTO
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        //public long RestaurantId { get; set; }
    }
}
