using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        public int ParentPostID { get; set; }
        public Post ThePost { get; set; }
        public string IDforUser { get; set; }
        public string IDforDestination { get; set; } //In case of a direct message
        public string Content { get; set; }
        public string Date { get; set; }
        public string Topic { get; set; }
        public string Title { get; set; }
        public bool IsPublic { get; set; } //1 for public post ; 0 for direct message
        public List<Post> Posts { get; set; } 
    }
}