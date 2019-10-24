using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class Register
    {
        [Key]
        public string Id { get; set; }
        //[DisplayName("Kullanıcı Adınız")]
        [Required(ErrorMessage = "*Lütfen kullanıcı adınızı giriniz.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*Lütfen adınızı giriniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*Lütfen soyadınızı giriniz.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*Lütfen iletişim bilgilerinizi giriniz.")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "*Lütfen e-posta adresinizi giriniz.")]
        [EmailAddress(ErrorMessage ="E-posta adresinizi düzgün giriniz.")]
        public string EMail { get; set; }
        [Required(ErrorMessage = "*Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "*Lütfen şifrenizi giriniz.")]
        [Compare("Password",ErrorMessage="Şifreleriniz uyuşmuyor")]
        public string rePassword { get; set; }
        //public List<Questions> Questions { get; set; }
        //public List<Answers> Answers { get; set; }
        //public List<Demand> Demands { get; set; }
    }
}