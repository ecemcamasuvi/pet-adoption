using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class LikeDislikeAnswer
    {
        [Key]
        public int LikeDislikeID { get; set; }
        public int IDforAnswer { get; set; }
        public bool LikeOrDislike { get; set; } //1 for like 0 for dislike
        public bool IDforUser { get; set; }
    }
}