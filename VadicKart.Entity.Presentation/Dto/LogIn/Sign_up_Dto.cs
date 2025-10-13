using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VadicKart.Entity.Presentation.Dto.LogIn
{
    public class Sign_up_Dto
    {
        public required string? First_name { get; set; }
        public required string? Last_name { get; set; }
        public DateOnly? Date_of_birth { get; set; }
        public required string? EMail { get; set; }
        public required string? UserName { get; set; }
        //public Guid ID { get; set; }  // FK
        public required string? Password { get; set; }
    }
}
