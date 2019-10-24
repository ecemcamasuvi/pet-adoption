using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Animals.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Contact { get; set; }
    }
}