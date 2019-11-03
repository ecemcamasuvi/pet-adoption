using Animals.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Animals.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        private AdoptionContext db = new AdoptionContext();

        public ActionResult Index()
        {
            var postList = db.Posts.Where(i => i.IsPublic == true && i.Topic.Equals("Question")).ToList();
            return View(postList);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        public PartialViewResult Answers(int? parentID)
        {
            var answers = db.Posts.Where(i => i.ParentPostID == parentID && i.Topic.Equals("Answer"));
            return PartialView(answers);
        }
        public PartialViewResult CreateAnswer(int? parentID)
        {
            return PartialView(parentID);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Post answer)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(answer);
                db.SaveChanges();
            }
            return View(db.Posts.Where(i=>i.PostID==answer.ParentPostID).First());
        }
        public ActionResult Create()
        {
            List<SelectListItem> listTypes = new List<SelectListItem>();
            listTypes.Add(new SelectListItem() { Text = "Lütfen seçim yapınız.", Value = "0" });
            foreach (var item in db.PostTopics)
            {
                listTypes.Add(new SelectListItem() { Text = item.Topic, Value = item.TopicID.ToString() });
            }
            ViewBag.TopicId = new SelectList(listTypes, "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            ViewBag.TopicId = new SelectList(db.PostTopics, "TopicID", "Topic", post.TopicId);

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }
        public PartialViewResult Topic()
        {
            return PartialView(db.PostTopics.ToList());
        }
        [HttpGet]
        [Route("Post/List/{topicID?}")]
        public ActionResult List(int? topicID)
        {
            List<Post> posts = db.Posts.ToList();
            List<PostDTO> destinationPosts = new List<PostDTO>();
            var config = new MapperConfiguration(i => { i.CreateMap<Post, PostDTO>(); });
            IMapper mapper = config.CreateMapper();
            foreach (var i in posts)
            {
                destinationPosts.Add(mapper.Map<Post, PostDTO>(i));
            }
            var questions = destinationPosts
                .Where(i => i.IsPublic == true&&i.Topic.Equals("Question"))
                .Select(i => new PostDTO()
                {
                    ParentPostID=i.ParentPostID,
                    PostID=i.PostID,
                    TopicId=i.TopicId,
                    Title=i.Title,
                    Content=i.Content,
                    Date=i.Date,
                    IDforDestination=i.IDforDestination,
                    IDforUser=i.IDforUser,
                    IsPublic=i.IsPublic,
                    Posts=i.Posts,
                    PostTopics=i.PostTopics,
                    ThePost=i.ThePost,
                    Topic=i.Topic
                }).AsQueryable();
            
            if (topicID != null)
            {
                questions = questions.Where(i => i.TopicId == topicID);
            }
          
            return View(questions.ToList());
        }
        //// GET: PetAnnouncements/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    var petAnnouncementforEdit = db.Announcements.Find(id);
        //    List<SelectListItem> listTypes = new List<SelectListItem>();
        //    listTypes.Add(new SelectListItem() { Text = "Lütfen seçim yapınız.", Value = "0" });
        //    foreach (var item in db.PetTypes)
        //    {
        //        listTypes.Add(new SelectListItem() { Text = item.Type, Value = item.TypeID.ToString() });
        //    }
        //    ViewBag.vbTypeId = new SelectList(listTypes, "Value", "Text", petAnnouncementforEdit.TypeId);
        //    ViewBag.vbCityId = new SelectList(db.Cities, "CityID", "City", petAnnouncementforEdit.CityId);
        //    var breeds = db.Breeds.Where(i => i.IdofType == petAnnouncementforEdit.TypeId).ToList();
        //    List<SelectListItem> listBreeds = new List<SelectListItem>();
        //    foreach (var item in breeds)
        //    {
        //        listBreeds.Add(new SelectListItem() { Text = item.Breed, Value = item.BreedID.ToString() });
        //    }
        //    ViewBag.vbBreedId = new SelectList(listBreeds, "Value", "Text", petAnnouncementforEdit.BreedId);
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PetAnnouncement petAnnouncement = db.Announcements.Find(id);
        //    if (petAnnouncement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(petAnnouncement);
        //}
    }
}