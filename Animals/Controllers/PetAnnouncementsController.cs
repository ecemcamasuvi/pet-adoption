using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Animals.Models;
using AutoMapper;

namespace Animals.Controllers
{
    public class PetAnnouncementsController : Controller
    {
        private AdoptionContext db = new AdoptionContext();

        // GET: PetAnnouncements
        public PartialViewResult AnimalTypeList()
        {
            return PartialView(db.PetTypes.ToList());
        }
        public PartialViewResult AnimalBreedList()
        {
            return PartialView();
        }
        public ActionResult Index()
        {
            return View(db.Announcements.ToList());

        }
        public ActionResult List(int? type)
        {
            List<PetAnnouncement> announcements = db.Announcements.ToList();
            List<PetAnnouncementDTO> destinationAnnouncements = new List<PetAnnouncementDTO>();
            var config = new MapperConfiguration(i => { i.CreateMap<PetAnnouncement, PetAnnouncementDTO>(); });
            IMapper mapper = config.CreateMapper();
            foreach (var i in announcements)
            {
                destinationAnnouncements.Add(mapper.Map<PetAnnouncement, PetAnnouncementDTO>(i));
            }
            var posts = destinationAnnouncements
                .Where(i => i.Active == true)
                .Select(i => new PetAnnouncementDTO()
                {
                    IDforUser = i.IDforUser,
                    Age = i.Age,
                    AnnouncementID = i.AnnouncementID,
                    Breed = i.Breed,
                    City = i.City,
                    Date = i.Date,
                    Demands = i.Demands,
                    Explanation = i.Explanation,
                    Title = i.Title,
                    Type = i.Type,
                    Photo = i.Photo
                }).AsQueryable();

            if (type != null)
            {
                posts = posts.Where(i => i.Type.Equals(type));
            }
            return View(posts.ToList());
        }


        // GET: PetAnnouncements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetAnnouncement petAnnouncement = db.Announcements.Find(id);
            if (petAnnouncement == null)
            {
                return HttpNotFound();
            }
            TempData["user"] = db.Users.Find(petAnnouncement.IDforUser);
            return View(petAnnouncement);
        }

        // GET: PetAnnouncements/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.PetTypes, "TypeID", "Type");
            ViewBag.CityId = new SelectList(db.Cities, "CityID", "City");
            return View();
        }

        // POST: PetAnnouncements/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnnouncementID,TypeId,Breed,Age,CityId,Explanation,Title")] PetAnnouncement petAnnouncement, HttpPostedFileBase Photo)
        {
           
            ViewBag.TypeId = new SelectList(db.PetTypes, "TypeID", "Type",petAnnouncement.TypeId);
            ViewBag.CityId = new SelectList(db.Cities, "CityID", "City",petAnnouncement.CityId);
            if (ModelState.IsValid)
            {
                var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
                string foto = uid + ".jpg";
                Photo.SaveAs(Server.MapPath(@"~\Image\") + foto);
                petAnnouncement.IDforUser = Convert.ToInt32(Session["UserID"].ToString());
                string format = "dd.MM.yyyy";
                petAnnouncement.Date = DateTime.Now.ToString(format);
                petAnnouncement.Active = true;
                petAnnouncement.Photo = foto;
                db.Announcements.Add(petAnnouncement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petAnnouncement);
        }
        // GET: PetAnnouncements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetAnnouncement petAnnouncement = db.Announcements.Find(id);
            if (petAnnouncement == null)
            {
                return HttpNotFound();
            }
            return View(petAnnouncement);
        }

        // POST: PetAnnouncements/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnnouncementID,Type,Breed,Age,City,Photo,IDforUser,Date,Explanation,Title,Active")] PetAnnouncement petAnnouncement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petAnnouncement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petAnnouncement);
        }

        // GET: PetAnnouncements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetAnnouncement petAnnouncement = db.Announcements.Find(id);
            if (petAnnouncement == null)
            {
                return HttpNotFound();
            }
            return View(petAnnouncement);
        }

        // POST: PetAnnouncements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetAnnouncement petAnnouncement = db.Announcements.Find(id);
            db.Announcements.Remove(petAnnouncement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
