using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Animals.Identity;
using Animals.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Animals.Controllers
{
    public class UserController : Controller
    {

        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public UserController()
        {
            var userStore = new UserStore<ApplicationUser>(new AdoptionContext());
            var roleStore = new RoleStore<ApplicationRole>(new AdoptionContext());
            UserManager = new UserManager<ApplicationUser>(userStore);
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        private AdoptionContext db = new AdoptionContext();
        // GET: User
        public ActionResult Index()
        {
            return View(UserManager.Users.ToList());

        }



        public ActionResult Edit()
        {
            return View();
        }

        // GET: User/Edit/5

        // POST: User/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.

        public ActionResult Profile()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            string userID = authManager.User.Identity.GetUserId();
            var user = UserManager.FindById(userID);
            return View("Profile", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(Register model)
        {
            ApplicationUser info = UserManager.FindById(model.Id);
            if (model.Password != null)
            {
                var entity = UserManager.Find(info.UserName, model.Password);
                if (model != null && entity != null)
                {
                    if (entity != null)
                    {
                        entity.Name = model.Name;
                        entity.Lastname = model.LastName;
                        entity.Contact = model.Contact;
                        entity.Email = model.EMail;
                        UserManager.Update(entity);
                        TempData["User"] = entity;
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Girdiğiniz şifre doğru değil.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Şifre alanı boş bırakılamaz.";
            }
            return RedirectToAction("Profile", info);
        }
        //return RedirectToAction("Profile", "Home", users);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfilePassword(Register model)
        {
            ApplicationUser info = UserManager.FindById(model.Id);
            if (model.Password == null || model.oldPassword == null || model.rePassword == null)
            {
                TempData["ErrorMessage"] = "Şifre alanı boş bırakılamaz.";
            }
            else
            {
                if (UserManager.Find(info.UserName, model.oldPassword) != null)
                {
                    if (model.Password.Equals(model.rePassword))
                    {
                        if (!model.Password.Equals(model.oldPassword))
                        {
                            if (ModelState.IsValid)
                            {
                                UserManager.ChangePassword(info.Id, model.oldPassword, model.Password);
                                TempData["User"] = info;
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Girdiğiniz şifre eskisiyle aynı.";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Girdiğiniz şifreler uyuşmamaktadır.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Girdiğiniz şifre doğru değil.";
                }
            }
            return RedirectToAction("Profile", info);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Register users = db.Users.Find(id);
            db.Users.Remove(users);
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
        public PartialViewResult IncomingMessage()
        {
            string userID = HttpContext.User.Identity.GetUserId();
            var myAnnouncements = db.Announcements.Where(i => i.IDforUser.Equals(userID)).Include(i => i.Demands).OrderByDescending(i => i.AnnouncementID);
            return PartialView(myAnnouncements);
        }
        public PartialViewResult IncomingDirectMessage()
        {
            string userID = HttpContext.User.Identity.GetUserId();
            var yourPosts = db.Posts.Include(i => i.Posts).Where(i => i.IDforDestination.Equals(userID)).OrderByDescending(i=>i.PostID);//someone else's messages
            return PartialView(yourPosts);
        }
        public PartialViewResult OutgoingMessage()
        {
            string userID = HttpContext.User.Identity.GetUserId();
            var myDemands = db.Demands.Where(i => i.IDforUser.Equals(userID)).OrderByDescending(i => i.DemandID);
            return PartialView(myDemands);
        }
        public PartialViewResult OutgoingDirectMessage()
        {
            string userID = HttpContext.User.Identity.GetUserId();
            var myPosts = db.Posts.Where(i => i.IDforUser.Equals(userID)).OrderByDescending(i => i.PostID);
            return PartialView(myPosts);
        }
        public ActionResult Message()
        {
            return View();
        }
        public ActionResult Announcements(string userID)
        {
            if (userID == null)
            {
                userID = HttpContext.User.Identity.GetUserId();
            }
            var myAnnouncements = db.Announcements.Include(i => i.Demands).Where(i => i.IDforUser.Equals(userID));
            return View(myAnnouncements);
        }

        public PartialViewResult Who(string personID)
        {
            var user = UserManager.FindById(personID);
            return PartialView(user);
        }
        public ActionResult PersonsProfile(string personID)
        {
            var user = UserManager.FindById(personID);
            return View(user);
        }
        public ActionResult DeleteDemand(int? demandID)
        {
            var demand = db.Demands.Find(demandID);
            demand.Active = false;
            ViewBag.Reject = "Talebi başarıyla reddettiniz.";
            db.SaveChanges();
            return RedirectToAction("Message");
        }
        public ActionResult AcceptDemand(int? demandID)
        {
            var otherDemands = db.Demands.Where(i => i.DemandID != demandID);
            foreach (var item in otherDemands)
            {
                item.Active = false; //rejected
            }
            var demand = db.Demands.Find(demandID);
            demand.State = true;
            var announcement = db.Announcements.Find(demand.IDforPet);
            announcement.Active = false;
            ViewBag.Success = "Evlat edinme işlemini onayladınız.";
            db.SaveChanges();
            return RedirectToAction("Message");
        }
        public PartialViewResult MessageDetails(int? messageID)
        {
            var demand = db.Demands.Find(messageID);
            return PartialView(demand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MessageDetails(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Posts = new List<Post>();
                post.ParentPostID = null;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Message");
            }
            return PartialView(post);
        }
        public PartialViewResult DirectMessageDetails(int? postID)
        {
            var post = db.Posts.Find(postID);
            post.Content = "";
            return PartialView(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DirectMessageDetails(Post post)
        {
            if (ModelState.IsValid)
            {
                var parent=db.Posts.Include(i => i.Posts).Where(i => i.PostID == post.ParentPostID).First();
                parent.Posts.Add(post);
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Message");
            }
            return PartialView(post);
        }
        public PartialViewResult Demand(int? id)
        {
            var demand = db.Demands.Find(id);
            return PartialView(demand);
        }

    }
}
