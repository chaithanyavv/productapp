using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace productapp.Models
{
    public class product
    {
        [Key]
        public int product_id { get; set; }
        public string prduct_name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string photo { get; set; }

        public int cat_id { get; set; } //foreginkey
        public category category { get; set; }
        [NotMapped]
        public string cat_name { get; set; }

        public List<cart> carts { get; set; }
    }
}
