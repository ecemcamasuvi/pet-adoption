﻿using Animals.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class AdoptionContext : IdentityDbContext<ApplicationUser>
    {
        public AdoptionContext() : base("dbConnection")
        {
            Database.SetInitializer(new UserInitializer());
        }
        [Key]
        public DbSet<Register> Users { get; set; }
        public DbSet<PetAnnouncement> Announcements { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<LikeDislikeAnswer> LikeDislikeAnswers { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<PetBreed> Breeds { get; set; }
        public DbSet<PostTopics> PostTopics { get; set; }
        
    }
}