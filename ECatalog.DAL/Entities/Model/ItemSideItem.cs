using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class ItemSideItem:Entity
    {
        public long ItemSideItemId { get; set; }
        [ForeignKey("Item")]
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }
        [ForeignKey("SideItem")]
        public long SideItemId { get; set; }
        public virtual SideItem SideItem { get; set; }
    }
}
