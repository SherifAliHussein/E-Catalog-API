using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class CategoryTranslation:Entity
    {
        public long CategoryTranslationId { get; set; }
        public string Language { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
