using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class Item:Entity
    {
        public Item()
        {
            ItemTranslations = new List<ItemTranslation>();
            ItemSizes = new List<ItemSize>();
            ItemSideItems = new List<ItemSideItem>();
        }
        public long ItemId { get; set; }
        public virtual ICollection<ItemTranslation> ItemTranslations{ get; set; }
        public virtual ICollection<ItemSize> ItemSizes { get; set; }
        public virtual ICollection<ItemSideItem> ItemSideItems { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category{ get; set; }
        //public double Price { get; set; }
        public bool IsDeleted { get; set; }
        public int  MaxSideItemValue { get; set; }
        public bool IsActive { get; set; }
    }
}
