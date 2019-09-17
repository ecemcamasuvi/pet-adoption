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
                new Users(){UserName="ecemcmsv",Name="Ecem",LastName="Çamaşuvi",Contact="Do bir külah dondurma 5594949933",EMail="ecemcamasuvi@gmail.com",Password="Ecem123" },
                new Users(){UserName="summerSun",Name="Summer",LastName="Time",Contact="Re masmavi bir dere 4343434434",EMail="summer4fun_97_notFake@gmail.com",Password="Ecem123" },
                new Users(){UserName="gang_53",Name="Respect",LastName="Lol",Contact="Mi denizde bir gemi 232332122",EMail="real53_cityOfLightsLOL@gmail.com" ,Password="Ecem123"},
            };
            foreach (var person in users)
            {
                context.Users.Add(person);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}