using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class SideItem: Entity
    {
        public SideItem()
        {
            SideItemTranslations = new List<SideItemTranslation>();
        }
        public long SideItemId { get; set; }
        public bool IsDeleted { get; set; }
        public int Value { get; set; }
        public virtual ICollection<SideItemTranslation> SideItemTranslations{ get; set; }
        public virtual ICollection<ItemSideItem> ItemSideItems{ get; set; }

        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
