using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace MVCProject.Models
{
    public class ProductFileViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}