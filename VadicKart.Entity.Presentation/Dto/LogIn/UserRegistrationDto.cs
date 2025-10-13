using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VadicKart.Entity.Presentation.Dto.LogIn
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "ID is required")]
        public Guid ID { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Email cannot be empty")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }


    }
}
