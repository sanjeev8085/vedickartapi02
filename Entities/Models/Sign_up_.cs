using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("Sign_up_")]
    public class Sign_up_
    {          
        public string? First_name { get; set; }
        public string? Last_name { get; set; }
        public DateOnly? Date_of_birth { get; set; } 
        public string? EMail { get; set; }       
        public string? UserName { get; set; }
        public Guid ID { get; set; } = new Guid(); // Primary Key
        public string? Password { get; set; }
       
    }
}
