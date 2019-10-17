using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Animals.Models
{
    public class PetBreed
    {
        [Key]
        public int BreedID { get; set; }
        public string Breed { get; set; }
        public int IdofType { get; set; }
        public PetType Type { get; set; }
        public List<PetAnnouncement> Announcements { get; set; }
    }
}