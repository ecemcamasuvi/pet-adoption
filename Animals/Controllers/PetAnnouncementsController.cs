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
        public PartialViewResult CityList()
        {
            return PartialView(db.Cities.ToList());
        }
        public PartialViewResult AnimalTypeList()
        {
            return PartialView(db.PetTypes.ToList());
        }
        public PartialViewResult AnimalBreedList(int? typeID)
        {
            //if (Session["CityID"] != null)
            //{
            //    ViewBag.CityID = Session["CityID"].ToString();
            //}
            var breedList = db.Breeds.Where(i => i.IdofType == typeID);
            return PartialView(breedList.ToList());
        }
        public ActionResult Index()
        {
            var announcements = db.Announcements.Include(i => i.Type);
            announcements.Include(i => i.Breed);
            announcements.Include(i => i.City);
            return View(announcements.ToList());

        }
        [HttpGet]
        [Route("PetAnnouncements/List/{typeID?}/{breedID?}")]
        public ActionResult List(int? typeID, int? breedID)
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
                    BreedId = i.BreedId,
                    CityId = i.CityId,
                    Date = i.Date,
                    Demands = i.Demands,
                    Explanation = i.Explanation,
                    Title = i.Title,
                    TypeId = i.TypeId,
                    Photo = i.Photo
                }).AsQueryable();
            if (Session["CityPosts"] != null)
            {
                posts = (IQueryable<PetAnnouncementDTO>)Session["CityPosts"];
            }
            if (typeID != null)
            {
                posts = posts.Where(i => i.TypeId == typeID);
                Session["TypeID"] = typeID;
                ViewBag.Type = typeID;
                Session["ListPosts"] = posts;
            }
            if (breedID != null)
            {
                posts = posts.Where(i => i.BreedId == breedID);
                Session["ListPosts"] = posts;
            }
            if (typeID == breedID && typeID == null)
            {
                Session["TypeID"] = null;
                Session["ListPosts"] = null;
            }
            return View(posts.ToList());
        }

        [HttpGet]
        [Route("PetAnnouncements/ListforCity/{cityID?}")]
        public ActionResult ListforCity(int? cityID)
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
                    BreedId = i.BreedId,
                    CityId = i.CityId,
                    Date = i.Date,
                    Demands = i.Demands,
                    Explanation = i.Explanation,
                    Title = i.Title,
                    TypeId = i.TypeId,
                    Photo = i.Photo
                }).AsQueryable();
            if (Session["ListPosts"] != null)
            {
                posts = (IQueryable<PetAnnouncementDTO>)Session["ListPosts"];
                ViewBag.Type = Session["TypeID"];
            }
            if (cityID != null)
            {
                posts = posts.Where(i => i.CityId == cityID);
                Session["CityPost"] = posts;
            }
            else
            {
                Session["CityPost"] = null;

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
            petAnnouncement.City = db.Cities.Find(petAnnouncement.CityId);
            petAnnouncement.Breed = db.Breeds.Find(petAnnouncement.BreedId);
            petAnnouncement.Type = db.PetTypes.Find(petAnnouncement.TypeId);
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
            List<SelectListItem> listTypes = new List<SelectListItem>();
            listTypes.Add(new SelectListItem() { Text = "Lütfen seçim yapınız.", Value = "0" });
            foreach (var item in db.PetTypes)
            {
                listTypes.Add(new SelectListItem() { Text = item.Type, Value = item.TypeID.ToString() });
            }
            ViewBag.TypeId = new SelectList(listTypes, "Value", "Text");
            ViewBag.CityId = new SelectList(db.Cities, "CityID", "City");
            return View();
        }

        public JsonResult GetBreeds(int? petType)
        {
            var breeds = db.Breeds.Where(i => i.IdofType == petType).ToList();
            List<SelectListItem> listBreeds = new List<SelectListItem>();
            //listBreeds.Add(new SelectListItem() { Text = "select state", Value = "0" });
            foreach (var item in breeds)
            {
                listBreeds.Add(new SelectListItem() { Text = item.Breed, Value = item.BreedID.ToString() });
            }
            return Json(new SelectList(listBreeds, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        // POST: PetAnnouncements/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnnouncementID,TypeId,Age,CityId,Explanation,Title")] PetAnnouncement petAnnouncement, HttpPostedFileBase Photo, int BreedId)
        {

            ViewBag.TypeId = new SelectList(db.PetTypes, "TypeID", "Type", petAnnouncement.TypeId);
            ViewBag.CityId = new SelectList(db.Cities, "CityID", "City", petAnnouncement.CityId);

            if (ModelState.IsValid)
            {
                var uid = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
                string foto = uid + ".jpg";
                Photo.SaveAs(Server.MapPath(@"~\Image\") + foto);
                petAnnouncement.BreedId = BreedId;
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
