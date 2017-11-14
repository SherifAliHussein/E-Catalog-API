using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECatalog.DAL.Entities.Model
{
    public class RefreshToken:Entity
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; } 
        public System.DateTime IssuedUtc { get; set; }
        public System.DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
    }
}
