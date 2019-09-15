using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class UserInitializer : DropCreateDatabaseIfModelChanges<AdoptionContext>
    {
        protected override void Seed(AdoptionContext context)
        {
            List<Users> users = new List<Users>()
            {
                new Users(){UserName="ecemcmsv",Name="Ecem",LastName="Çamaşuvi",Contact="ecemcamasuvi@gmail.com" },
                new Users(){UserName="summerSun",Name="Summer",LastName="Time",Contact="summer4fun_97_notFake@gmail.com" },
                new Users(){UserName="gang_53",Name="Respect",LastName="Lol",Contact="real53_cityOfLightsLOL@gmail.com" },
            };
            foreach(var person in users)
            {
                context.Users.Add(person);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}