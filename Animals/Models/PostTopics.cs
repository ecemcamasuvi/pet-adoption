using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class PostTopics
    {
        [Key]
        public int TopicID { get; set; }
        public string Topic { get; set; }
        public List<Post> Posts { get; set; }
    }
}