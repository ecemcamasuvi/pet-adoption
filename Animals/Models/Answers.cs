using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class Answers
    {
        [Key]
        public int AnswerID { get; set; }
        public int IDforUser { get; set; }
        public int IDforQuestion { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public bool IsPublic { get; set; } //1 for public answer ; 0 for direct answer
        //In case of a direct answer, like and dislike doesn't mean anything.
        public int Like { get; set; }
        public int Dislike { get; set; }
        public List<LikeDislikeAnswer> LikenDislike { get; set; }

    }
}