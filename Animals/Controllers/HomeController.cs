using Animals.Identity;
using Animals.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Animals.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public HomeController()
        {
            var userStore = new UserStore<ApplicationUser>(new AdoptionContext());
            var roleStore = new RoleStore<ApplicationRole>(new AdoptionContext());
            UserManager = new UserManager<ApplicationUser>(userStore);
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        private AdoptionContext context = new AdoptionContext();
        // GET: Home

        public ActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            var user = UserManager.Find(model.UserName, model.Password);
            if (user != null)
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                var identityClaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                var authProperties = new AuthenticationProperties();
                authProperties.IsPersistent = model.RememberMe;
                authManager.SignIn(authProperties, identityClaims);
                //Session["UserID"] = user.Id;
                return RedirectToAction("Profile", "User", user);
            }
            else
            {
                ModelState.AddModelError("userLoginError", "E-mail adresi ya da parola hatalı.");
            }
            //Register user = context.Users.Where(i => i.EMail.Equals(users.EMail) && i.Password.Equals(users.Password)).FirstOrDefault();
            //if (user != null)
            //{
            //    return RedirectToAction("Profile", "User", user);
            //}
            //else if (users.EMail != null && users.Password != null)
            //{
            //    ViewBag.Error = "E-mail adresi ya da parola hatalı.";
            //}
            return View(model);
        }
        // GET: User/Create
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: User/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Id = model.Id;
                user.Name = model.Name;
                user.Lastname = model.LastName;
                user.UserName = model.UserName;
                user.Contact = model.Contact;
                user.Email = model.EMail;
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("User"))
                    {
                        UserManager.AddToRole(user.Id, "User");
                    }
                    //Session["UserID"] = user.Id;
                    return RedirectToAction("Profile", "User", user);
                }
                else
                {
                    ModelState.AddModelError("userCreateError", "Kullanıcı oluşturma hatası.");
                }
            }

            return View(model);
        }

        public ActionResult Exit()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            //Session.Abandon();
            return RedirectToAction("Index");
        }


    }
}