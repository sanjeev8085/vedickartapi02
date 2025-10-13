using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("UserRegistration")]
    public class UserRegistration
    {
        public Guid ID { get; set; }         // Primary Key
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? EMail { get; set; }
        public int?  IsActive { get; set; }
    }
}
