using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RegisterDTO
    {

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).*$", ErrorMessage ="Password must have 1 Uppercase, 1 Lower case, 1 numbr, 1 non alphanumeric and at least 8 characters")]
        public string Password { get; set; }
    }
}
