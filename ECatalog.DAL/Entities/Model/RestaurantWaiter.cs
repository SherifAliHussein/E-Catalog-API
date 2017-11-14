using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class RestaurantWaiter: RestaurantAdmin
    {
        public string Name { get; set; }

        //[ForeignKey("Restaurant")]
        //public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
