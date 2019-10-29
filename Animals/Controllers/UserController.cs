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

        // GET: User/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Message()
        {
            string userID = HttpContext.User.Identity.GetUserId();
            var myAnnouncements = db.Announcements.Where(i => i.IDforUser.Equals(userID)).Include(i=>i.Demands);
            
            return View(myAnnouncements);
        }
     
        public ActionResult Announcements()
        {
            string userID = HttpContext.User.Identity.GetUserId();
            var myAnnouncements = db.Announcements.Where(i => i.IDforUser.Equals(userID));
            return View(myAnnouncements);
        }

    }
}
