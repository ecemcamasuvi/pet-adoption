using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "*Lütfen kullanıcı adınızı giriniz.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*Lütfen adınızı giriniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*Lütfen soyadınızı giriniz.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*Lütfen iletişim bilgilerinizi giriniz.")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "*Lütfen e-posta adresinizi giriniz.")]
        public string EMail { get; set; }
        [Required(ErrorMessage = "*Lütfen şifrenizi giriniz.")]
        public string Password { get; set; }
        public List<Questions> Questions { get; set; }
        public List<Answers> Answers { get; set; }
        public List<Demand> Demands { get; set; }
    }
}