using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public bool Visibility { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Add_date { get; set; }
        public int Sold_units { get; set; }
        public int? TaxID { get; set; }
        public virtual Tax Tax { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public int? ProducerID { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual ICollection<Product_Order> Product_Orders { get; set; }
        public virtual ICollection<Product_File> Product_Files { get; set; }
        public virtual ICollection<Product_Tag> Product_Tags { get; set; }
    }
}