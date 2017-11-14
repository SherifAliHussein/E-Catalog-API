using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class Menu:Entity
    {
        public Menu()
        {
            MenuTranslations = new List<MenuTranslation>();
            Categories = new List<Category>();
        }
        public long MenuId { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<MenuTranslation> MenuTranslations { get; set; }
        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public bool IsDeleted { get; set; }
    }
}
