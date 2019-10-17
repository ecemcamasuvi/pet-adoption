using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Animals.Models
{
    public class PetAnnouncement
    {
        [Key]
        public int AnnouncementID { get; set; }
        [Required]
        public string Age { get; set; }
        public string Photo { get; set; }
        public int IDforUser { get; set; }
        public string Date { get; set; }
        public string Explanation { get; set; }
        [Required]
        public string Title { get; set; }
        public bool Active { get; set; }
        public int BreedId { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int CityId { get; set; }
        public PetBreed Breed { get; set; }
        public Cities City { get; set; }
        public PetType Type { get; set; }
        public List<Demand> Demands { get; set; }
    }
}