using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrMnager_mvc.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        
    }
}