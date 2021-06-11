using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class File
    {
        public int FileID { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product_File> Product_Files { get; set; }
    }
}