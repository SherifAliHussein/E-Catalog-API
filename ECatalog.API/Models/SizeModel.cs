using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECatalog.API.Models
{
    public class SizeModel
    {
        public long SizeId { get; set; }
        public string SizeName { get; set; }
        public double Price { get; set; }
    }
}