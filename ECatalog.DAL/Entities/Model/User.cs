using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECatalog.Common;
using Repository.Pattern.Ef6;

namespace ECatalog.DAL.Entities.Model
{
    public class User:Entity
    {
        [Key]
        public long UserId { get; set; }
        
        //[Index("Index_UserName", IsUnique = true)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Enums.RoleType Role { get; set; }

        public bool IsDeleted { get; set; }

       // public virtual Restaurant Restaurant { get; set; }
    }
}
