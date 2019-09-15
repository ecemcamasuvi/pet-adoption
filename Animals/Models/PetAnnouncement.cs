using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class PetAnnouncement
    {
        [Key]
        public int AnnouncementID { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Age { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public int IDforUser { get; set; }
        public string Date { get; set; }
        public string Explanation { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public List<Demand> Demands { get; set; }
    }
}