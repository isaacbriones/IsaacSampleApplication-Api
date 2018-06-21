using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySampleApplication.Models.Request
{
    public class LoginAddRequest
    {
        [Required,MaxLength(128)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
