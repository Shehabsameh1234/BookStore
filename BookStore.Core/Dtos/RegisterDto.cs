using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[a-zA-Z0-9]).{8,}$",
        ErrorMessage = "password must contain at least one number , one upper case charachter ,one alphanumeric, one special charachter")]
        public string Password { get; set; }
    }
}
