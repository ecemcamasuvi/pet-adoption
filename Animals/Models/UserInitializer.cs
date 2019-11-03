﻿using Animals.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Animals.Models
{
    public class UserInitializer : CreateDatabaseIfNotExists<AdoptionContext>
    {
        protected override void Seed(AdoptionContext context)
        {
            //2 role 2 user 1 admin 2 person...
            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole(){
                    Name="admin",Description= "yönetici rolü" };
                manager.Create(role);
            }
            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name="user", Description="user rolü" };
                manager.Create(role);
            }
            if (!context.Users.Any(i => i.UserName == "ecemcamasuvi"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
                {Name="Ecem",
                Lastname="Çamaşuvi",
                UserName="ecemcamasuvi",
                Email="e@gmail.com",
                Contact="Leave Ecem alone...",
                };
                manager.Create(user,"Ecem123");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }
            if (!context.Users.Any(i => i.UserName == "realLifeZombie"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
                {Name="Ecem",
                Lastname="The Zombie",
                UserName= "realLifeZombie",
                Email="herewegoagain@gmail.com",
                Contact="Ecem wants to sleep",
                };
                manager.Create(user,"Ecem123");
                manager.AddToRole(user.Id, "user");
            }

            //List<Register> users = new List<Register>()
            //{
            //    new Register(){UserName="ecemcmsv",Name="Ecem",LastName="Çamaşuvi",Contact="Do bir külah dondurma 5594949933",EMail="ecemcamasuvi@gmail.com",Password="Ecem123" },
            //    new Register(){UserName="summerSun",Name="Summer",LastName="Time",Contact="Re masmavi bir dere 4343434434",EMail="summer4fun_97_notFake@gmail.com",Password="Ecem123" },
            //    new Register(){UserName="gang_53",Name="Respect",LastName="Lol",Contact="Mi denizde bir gemi 232332122",EMail="e@gmail.com" ,Password="Ecem123"},
            //};
            //foreach (var person in users)
            //{
            //    context.Users.Add(person);
            //}
            //context.SaveChanges();
            List<PetType> petTypes = new List<PetType>()
            {
                new PetType(){Type="Kedi"},
                new PetType(){Type="Köpek"},
                new PetType(){Type="Balık"},
                new PetType(){Type="Kuş"},
                new PetType(){Type="Diğer"}
            };
            foreach (var type in petTypes)
            {
                context.PetTypes.Add(type);
            }
            context.SaveChanges();
            List<PostTopics> postTopics = new List<PostTopics>()
            {
                new PostTopics(){Topic="Kedi Bakımı"},
                new PostTopics(){Topic="Köpek Bakımı"},
                new PostTopics(){Topic="Balık Bakımı"},
                new PostTopics(){Topic="Kuş Bakımı"},
                new PostTopics(){Topic="Diğer Hayvanların Bakımı"},
                new PostTopics(){Topic="Evcil Hayvan Aksesuarları"},
                new PostTopics(){Topic="Kampanya"},
                new PostTopics(){Topic="Diğer"},
               
            };
            foreach (var topic in postTopics)
            {
                context.PostTopics.Add(topic);
            }
            context.SaveChanges();
            List<Cities> cities = new List<Cities>()
            {
                new Cities(){City="Adana"},
                new Cities(){City="Adıyaman"},
                new Cities(){City="Afyon"},
                new Cities(){City="Ağrı"},
                new Cities(){City="Amasya"},
                new Cities(){City="Ankara"},
                new Cities(){City="Antalya"},
                new Cities(){City="Artvin"},
                new Cities(){City="Aydın"},
                new Cities(){City="Balıkesir"},
                new Cities(){City="Bilecik"},
                new Cities(){City="Bingöl"},
                new Cities(){City="Bitlis"},
                new Cities(){City="Bolu"},
                new Cities(){City="Burdur"},
                new Cities(){City="Bursa"},
                new Cities(){City="Çanakkale"},
                new Cities(){City="Çankırı"},
                new Cities(){City="Çorum"},
                new Cities(){City="Denizli"},
                new Cities(){City="Diyarbakır"},
                new Cities(){City="Edirne"},
                new Cities(){City="Elazığ"},
                new Cities(){City="Erzincan"},
                new Cities(){City="Erzurum"},
                new Cities(){City="Eskişehir"},
                new Cities(){City="Gaziantep"},
                new Cities(){City="Giresun"},
                new Cities(){City="Gümüşhane"},
                new Cities(){City="Hakkari"},
                new Cities(){City="Hatay"},
                new Cities(){City="Isparta"},
                new Cities(){City="Mersin"},
                new Cities(){City="İstanbul"},
                new Cities(){City="İzmir"},
                new Cities(){City="Kars"},
                new Cities(){City="Kastamonu"},
                new Cities(){City="Kayseri"},
                new Cities(){City="Kırklareli"},
                new Cities(){City="Kırşehir"},
                new Cities(){City="Kocaeli"},
                new Cities(){City="Konya"},
                new Cities(){City="Kütahya"},
                new Cities(){City="Malatya"},
                new Cities(){City="Manisa"},
                new Cities(){City="Kahramanmaraş"},
                new Cities(){City="Mardin"},
                new Cities(){City="Muğla"},
                new Cities(){City="Muş"},
                new Cities(){City="Nevşehir"},
                new Cities(){City="Niğde"},
                new Cities(){City="Ordu"},
                new Cities(){City="Rize"},
                new Cities(){City="Sakarya"},
                new Cities(){City="Samsun"},
                new Cities(){City="Siirt"},
                new Cities(){City="Sinop"},
                new Cities(){City="Sivas"},
                new Cities(){City="Tekirdağ"},
                new Cities(){City="Tokat"},
                new Cities(){City="Trabzon"},
                new Cities(){City="Tunceli"},
                new Cities(){City="Şanlıurfa"},
                new Cities(){City="Uşak"},
                new Cities(){City="Van"},
                new Cities(){City="Yozgat"},
                new Cities(){City="Zonguldak"},
                new Cities(){City="Aksaray"},
                new Cities(){City="Bayburt"},
                new Cities(){City="Karaman"},
                new Cities(){City="Kırıkkale"},
                new Cities(){City="Batman"},
                new Cities(){City="Şırnak"},
                new Cities(){City="Bartın"},
                new Cities(){City="Ardahan"},
                new Cities(){City="Iğdır"},
                new Cities(){City="Yalova"},
                new Cities(){City="Karabük"},
                new Cities(){City="Kilis"},
                new Cities(){City="Osmaniye"},
                new Cities(){City="Düzce"}
            };
            foreach (var i in cities)
            {
                context.Cities.Add(i);
            }
            context.SaveChanges();
            List<PetBreed> petBreeds = new List<PetBreed>()
            {
                new PetBreed(){Breed="Bilinmiyor",IdofType=1},
                new PetBreed(){Breed="British shorthair",IdofType=1},
                new PetBreed(){Breed="Mavi Rus kedisi",IdofType=1},
                new PetBreed(){Breed="Van kedisi",IdofType=1},
                new PetBreed(){Breed="Siyam kedisi",IdofType=1},
                new PetBreed(){Breed="Scottish fold",IdofType=1},
                new PetBreed(){Breed="İran kedisi",IdofType=1},
                new PetBreed(){Breed="Bengal kedisi",IdofType=1},
                new PetBreed(){Breed="Sfenks kedisi",IdofType=1},
                new PetBreed(){Breed="Chinchilla kedisi",IdofType=1},
                new PetBreed(){Breed="Ankara kedisi",IdofType=1},
                new PetBreed(){Breed="Norveç orman kedisi",IdofType=1},
                new PetBreed(){Breed="Bilinmiyor",IdofType=2},
                new PetBreed(){Breed="Kangal Köpeği",IdofType=2},
                new PetBreed(){Breed="İngiliz Bulldog Köpeği",IdofType=2},
                new PetBreed(){Breed="Beagle Köpeği",IdofType=2},
                new PetBreed(){Breed="İngiliz Pointer Köpeği",IdofType=2},
                new PetBreed(){Breed="Fransız Bulldog Köpeği",IdofType=2},
                new PetBreed(){Breed="Jack Russell Terrier Köpeği",IdofType=2},
                new PetBreed(){Breed="Neapolitan Mastiff Köpeği",IdofType=2},
                new PetBreed(){Breed="Doberman Köpeği",IdofType=2},
                new PetBreed(){Breed="Maltese Köpeği",IdofType=2},
                new PetBreed(){Breed="Labrador Retriever Köpeği",IdofType=2},
                new PetBreed(){Breed="Golden Retriever Köpeği",IdofType=2},
                new PetBreed(){Breed="Pekingese Köpeği",IdofType=2},
                new PetBreed(){Breed="Bilinmiyor",IdofType=3},
                new PetBreed(){Breed="Japon balığı",IdofType=3},
                new PetBreed(){Breed="Lepistes",IdofType=3},
                new PetBreed(){Breed="Beta balığı",IdofType=3},
                new PetBreed(){Breed="Melek",IdofType=3},
                new PetBreed(){Breed="Neon tetra",IdofType=3},
                new PetBreed(){Breed="Sarı prenses",IdofType=3},
                new PetBreed(){Breed="Plati",IdofType=3},
                new PetBreed(){Breed="Bilinmiyor",IdofType=4},
                new PetBreed(){Breed="Muhabbet Kuşu",IdofType=4},
                new PetBreed(){Breed="Hint Bülbülü",IdofType=4},
                new PetBreed(){Breed="Papağan",IdofType=4},
                new PetBreed(){Breed="Kanarya",IdofType=4},
                new PetBreed(){Breed="Bengal İspinozu",IdofType=4},
                new PetBreed(){Breed="Bilinmiyor",IdofType=5},
                new PetBreed(){Breed="Bukalemun",IdofType=5},
                new PetBreed(){Breed="Hamster",IdofType=5},
                new PetBreed(){Breed="Kirpi",IdofType=5},
                new PetBreed(){Breed="Maymun",IdofType=5},
                new PetBreed(){Breed="Tarantula",IdofType=5},
                new PetBreed(){Breed="Tavşan",IdofType=5},
                new PetBreed(){Breed="Yılan",IdofType=5},
                new PetBreed(){Breed="İguana",IdofType=5},

            };
            foreach (var type in petBreeds)
            {
                context.Breeds.Add(type);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}