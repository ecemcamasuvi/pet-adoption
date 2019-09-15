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
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public List<Questions> Questions { get; set; }
        public List<Answers> Answers { get; set; }
        public List<Demand> Demands { get; set; }
    }
}