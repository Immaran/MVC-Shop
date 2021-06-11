using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Product_Tag
    {
        public int Product_TagID { get; set; }
        public int ProductID { get; set; }
        public int TagID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Tag Tag { get; set; }
    }
}