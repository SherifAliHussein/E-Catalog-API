using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class MenuTranslation:Entity
    {
        public long  MenuTranslationId { get; set; }
        public string Language { get; set; }
        public string MenuName { get; set; }
        public long MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
