using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class RestaurantTranslation:Entity
    {
        public long RestaurantTranslationId { get; set; }
        public string Language { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
