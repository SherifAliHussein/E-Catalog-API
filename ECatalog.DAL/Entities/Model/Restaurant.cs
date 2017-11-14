using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class Restaurant:Entity
    {
        public Restaurant()
        {
            RestaurantTranslations = new List<RestaurantTranslation>();
            Menus = new List<Menu>();
            SideItems = new List<SideItem>();
            Sizes = new List<Size>();

            RestaurantWaiters = new List<RestaurantWaiter>();
        }
        public long RestaurantId { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<RestaurantTranslation> RestaurantTranslations { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public long RestaurantTypeId { get; set; }
        public virtual RestaurantType RestaurantType{ get; set; }

        [ForeignKey("RestaurantAdmin")]
        public long RestaurantAdminId { get; set; }
        public virtual RestaurantAdmin RestaurantAdmin{ get; set; }
        public bool IsDeleted { get; set; }
        public bool IsReady { get; set; }
        public virtual ICollection<SideItem> SideItems{ get; set; }
        public virtual ICollection<Size> Sizes{ get; set; }

        //[ForeignKey("RestaurantWaiter")]
        //public long RestaurantWaiterId { get; set; }
        public virtual ICollection<RestaurantWaiter> RestaurantWaiters { get; set; }
    }
}
