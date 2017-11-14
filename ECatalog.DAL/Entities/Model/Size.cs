using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class Size:Entity
    {
        public Size()
        {
            SizeTranslations = new List<SizeTranslation>();
        }
        public long SizeId { get; set; }

        public bool IsDeleted { get; set; }
        public virtual ICollection<SizeTranslation> SizeTranslations { get; set; }
        public virtual ICollection<ItemSize> ItemSizes { get; set; }


        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
