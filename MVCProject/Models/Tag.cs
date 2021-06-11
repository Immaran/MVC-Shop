using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product_Tag> Product_Tags { get; set; }
    }
}