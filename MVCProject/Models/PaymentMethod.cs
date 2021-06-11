using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}