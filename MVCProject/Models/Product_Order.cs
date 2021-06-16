using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Product_Order
    {
        public int Product_OrderID { get; set; }
        public int ProductID { get; set; }
        public int ProductQuantity { get; set; }
        public int OrderID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}