using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace productapp.Models
{
    public class category
    {
        [Key]
        public int category_id { get; set; }
        public string category_name { get; set; }

        public List<product> products { get; set; }
    }
}
