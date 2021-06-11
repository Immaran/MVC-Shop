using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Tax
    {
        public int TaxID { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}