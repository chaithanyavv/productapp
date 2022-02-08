using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace productapp.Models
{
    public class login
    {
        [Key]
        public int login_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string type { get; set; }

        public int u_id { get; set; } //foreginkey
        public user user { get; set; }
        
        
    }
}
