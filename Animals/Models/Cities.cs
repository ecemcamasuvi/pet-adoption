using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Animals.Models
{
    public class Cities
    {
        [Key]
        public int CityID { get; set; }
        public string City { get; set; }
        public List<PetAnnouncement> Announcements{ get; set; }
    }
}