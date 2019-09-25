using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Animals.Models;

namespace Animals.Controllers
{
    public class UserController : Controller
    {
       

        private AdoptionContext db = new AdoptionContext();
        // GET: User
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Name,LastName,Contact,EMail")] Users users)
        {
            if (ModelState.IsValid)
            {
                var entity = db.Users.Find(users.UserID);
                if (entity != null)
                {
                    entity.Name = users.Name;
                    entity.LastName = users.LastName;
                    entity.Contact = users.Contact;
                    entity.EMail = users.EMail;
                    db.SaveChanges();
                    TempData["User"] = users;
                    return RedirectToAction("Profile", "Home");
                }
                //db.Entry(users).State = EntityState.Modified;
            }
            return RedirectToAction("Profile", "Home",users);
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
        Users users = db.Users.Find(id);
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
        Users users = db.Users.Find(id);
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
