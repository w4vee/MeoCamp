using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.BusinessModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        [Display(Name = "Fullname")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Username không được để trống")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password không được để trống")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}
