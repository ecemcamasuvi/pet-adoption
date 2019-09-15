using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class Questions
    {
        [Key]
        public int QuestionID { get; set; }
        public int IDforUser { get; set; }
        public int IDforDestination { get; set; } //In case of a direct question
        public string Content { get; set; }
        public string Date { get; set; }
        public string Topic { get; set; }
        public string Title { get; set; }
        public bool IsPublic { get; set; } //1 for public question ; 0 for direct question
        public List<Answers> Answers { get; set; } 
    }
}