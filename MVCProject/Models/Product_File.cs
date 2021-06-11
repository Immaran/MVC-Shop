using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Product_File
    {
        public int Product_FileID { get; set; }
        public int ProductID { get; set; }
        public int FileID { get; set; }
        public virtual Product Product { get; set; }
        public virtual File File { get; set; }
    }
}