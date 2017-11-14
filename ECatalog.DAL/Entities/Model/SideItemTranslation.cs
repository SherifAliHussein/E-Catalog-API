using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class SideItemTranslation:Entity
    {
        public long SideItemTranslationId { get; set; }
        public string Language { get; set; }
        public string SideItemName { get; set; }
        public long SideItemId { get; set; }
        public virtual SideItem SideItem { get; set; }
    }
}
