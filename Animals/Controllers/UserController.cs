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

            if (model.Password == null || model.rePassword == null)
            {
                ApplicationUser info = UserManager.FindById(model.Id);
                var user = UserManager.Find(info.UserName, model.Password);
                if (model != null && user != null)
                {
                    var entity = UserManager.FindById(model.Id);
                    if (entity != null)
                    {
                        entity.Name = model.Name;
                        entity.Lastname = model.LastName;
                        entity.Contact = model.Contact;
                        entity.Email = model.EMail;
                        UserManager.Update(entity);
                        TempData["User"] = entity;
                        return View(entity);
                    }
                    //db.Entry(users).State = EntityState.Modified;

                }
                else
                {
                    ViewBag.PasswordError = "Girdiğiniz şifre doğru değil.";
                }
                return View(model);
            }
            else
            {
                ApplicationUser info = UserManager.FindById(model.Id);
                var user = UserManager.Find(info.UserName, model.oldPassword);
                if (user != null)
                {
                    if (model.Password.Equals(model.rePassword))
                    {
                        if (!model.Password.Equals(model.oldPassword))
                        {
                            if (ModelState.IsValid)
                            {
                                UserManager.ChangePassword(info.Id, model.oldPassword, model.Password);
                                return View(user);
                            }
                        }
                        else
                        {
                            ViewBag.PasswordChangeError = "Girdiğiniz şifre eskisiyle aynı.";
                        }
                        //db.Entry(users).State = EntityState.Modified;
                    }
                    else
                    {
                        ViewBag.PasswordChangeError = "Girdiğiniz şifreler uyuşmamaktadır.";
                    }
                }
                else
                {
                    ViewBag.PasswordChangeError = "Girdiğiniz şifre doğru değil.";
                }
                return View(model);
            }
            //return RedirectToAction("Profile", "Home", users);
        }

        /*     [HttpPost]
             [ValidateAntiForgeryToken]
             public ActionResult EditPassword(int UserID, string PasswordEx, string PasswordNew, string PasswordNew2)
             {
                 if (PasswordEx.Equals(PasswordNew) || !PasswordNew.Equals(PasswordNew2))
                 {
                 }
                 else
                 {
                     var entity = db.Users.Find();

                     if (ModelState.IsValid)
                     {
                         db.Entry(users).State = EntityState.Modified;
                         db.SaveChanges();
                         return RedirectToAction("Index");
                     }
                 }
             }
                 return View();
         }
         */
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
    }
}
