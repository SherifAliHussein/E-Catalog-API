using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class Category:Entity
    {
        public Category()
        {
            CategoryTranslations = new List<CategoryTranslation>();
            Items = new List<Item>();
        }
        public long CategoryId { get; set; }
        public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual ICollection<Item>  Items{ get; set; }
        [ForeignKey("Menu")]
        public long MenuId { get; set; }
        public virtual Menu Menu{ get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
