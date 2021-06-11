using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Invoice Invoice { get; set; }
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }
        public int DeliveryMethodID { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }
        public int PaymentMethodID { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<Product_Order> Product_Orders { get; set; }
    }
}