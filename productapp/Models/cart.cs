using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace productapp.Models
{
    public class cart
    {
        [Key]
        public int cart_id { get; set; }
        public string quantity { get; set; }

        public int prdct_id { get; set; }
        public product product { get; set; }

        public int u_id { get; set; }
        public user user { get; set; }

    }
    // its a trial
//this change was made from github
    //it is second commit
}
