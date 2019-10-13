using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class PetAnnouncementDTO
    {
        public int AnnouncementID { get; set; }
        public int TypeId { get; set; }
        public PetType Type { get; set; }
        public string Breed { get; set; }
        public string Age { get; set; }
        public int CityId { get; set; }
        public Cities City { get; set; }
        public string Photo { get; set; }
        public int IDforUser { get; set; }
        public string Date { get; set; }
        public string Explanation { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public List<Demand> Demands { get; set; }
    }
}