using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class Login
    {
        
        [Required(ErrorMessage = "*Lütfen kullanıcı adınızı giriniz.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }
        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}