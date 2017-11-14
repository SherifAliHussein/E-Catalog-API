using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class RestaurantAdmin: User
    {

        //[ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
       // public virtual Restaurant Restaurant { get; set; }
    }
}
