using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class ItemSize:Entity
    {
        public long ItemSizeId { get; set; }
        [ForeignKey("Item")]
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }
        [ForeignKey("Size")]
        public long SizeId { get; set; }
        public virtual Size Size { get; set; }
        public double Price { get; set; }
    }
}
