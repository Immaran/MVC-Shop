using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Status
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}