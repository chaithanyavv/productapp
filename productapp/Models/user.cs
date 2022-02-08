using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace productapp.Models
{
    public class user
    {
        [Key]
        public int user_id { get; set; }
        public string name { get; set; }
        public string place { get; set; }
        public long phone { get; set; }
        public List<login> logins { get; set; }

        public List<cart> carts { get; set; }
    }
}
