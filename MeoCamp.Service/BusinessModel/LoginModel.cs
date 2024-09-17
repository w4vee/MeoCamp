using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.BusinessModel
{
    public class LoginModel
    {
        
        [Display(Name = "Username or Email address")]
        public string Username { get; set; } = "";

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";
    }
}
