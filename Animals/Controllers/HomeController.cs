using Animals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Animals.Controllers
{
    public class HomeController : Controller
    {
        private AdoptionContext context = new AdoptionContext();
        // GET: Home

        public ActionResult Index()
        {
            return View(context.Users.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "EMail,Password")] Users users)
        {
            Users user = context.Users.Where(i => i.EMail.Equals(users.EMail) && i.Password.Equals(users.Password)).FirstOrDefault();
            if (user != null)
            {
                Session["UserID"] = user.UserID;
                return RedirectToAction("Profile","User",user);
            }
            else if(users.EMail!=null&&users.Password!=null)
            {
                ViewBag.Error = "E-mail adresi ya da parola hatalı.";
            }
            return View(users);
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
        public ActionResult CreateUser([Bind(Include = "UserID,UserName,Name,LastName,Contact,EMail,Password")] Users users)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(users);
                context.SaveChanges();
                Session["UserID"] = users.UserID;
                int userID = Convert.ToInt32(Session["UserID"].ToString());
                Users user = context.Users.Where(i => i.UserID == userID).FirstOrDefault();
                return RedirectToAction("Profile","User", user);
            }

            return View(users);
        }

        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
     
        public PartialViewResult LoginPartial()
        {
            int userID = Convert.ToInt32(Session["UserID"].ToString());
            Users user = context.Users.Where(i => i.UserID == userID).FirstOrDefault();
            return PartialView(user);
        }
       
    }
}