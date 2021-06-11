using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public bool Visibility { get; set; }
        [ForeignKey("Categories")]
        public int? ParentID { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}