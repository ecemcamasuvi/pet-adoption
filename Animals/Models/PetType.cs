using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Animals.Models
{
    public class PetType
    {
        [Key]
        public int TypeID { get; set; }
        public string Type { get; set; }
        public List<PetAnnouncement> Announcements { get; set; }
    }
}