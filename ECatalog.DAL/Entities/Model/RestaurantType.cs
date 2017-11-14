using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class RestaurantType:Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long RestaurantTypeId { get; set; }
        public virtual ICollection<RestaurantTypeTranslation> RestaurantTypeTranslations { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public bool IsDeleted { get; set; }
    }
}
