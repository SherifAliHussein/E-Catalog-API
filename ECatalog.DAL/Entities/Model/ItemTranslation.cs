using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class ItemTranslation:Entity
    {
        public long ItemTranslationId { get; set; }
        public string Language { get; set; }
        public string  ItemName { get; set; }
        public string ItemDescription { get; set; }
        public long ItemId { get; set; }
        public virtual Item  Item { get; set; }
    }
}
