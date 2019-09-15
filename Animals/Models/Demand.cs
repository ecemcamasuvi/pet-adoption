using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class Demand
    {
        [Key]
        public int DemandID { get; set; }
        public int IDforPet { get; set; }
        public int IDforUser { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public bool State { get; set; } //1 for approval; during the evaluation 0
        public bool Active { get; set; } //0 for rejection; during the evaluation 1
    }
}