using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class RestaurantTypeTranslation:Entity
    {
        public long RestaurantTypeTranslationId { get; set; }
        public string Language { get; set; }
        public string TypeName { get; set; }
        public long RestaurantTypeId { get; set; }
        public virtual RestaurantType RestaurantType { get; set; }
    }
}
