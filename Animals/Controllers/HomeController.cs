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
        public ActionResult Login(string EMail, string Password)
        {
            Users user = context.Users.Where(i => i.EMail.Equals(EMail) && i.Password.Equals(Password)).FirstOrDefault();
            Session["UserID"] = user.UserID;
            return View("Profile", user);
        }
        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
        public ActionResult Profile()
        {
            int userID = Convert.ToInt32(Session["UserID"].ToString());
            Users user = context.Users.Where(i => i.UserID == userID).FirstOrDefault();
            return View("Profile",user);
        }

        public PartialViewResult LoginPartial()
        {
            int userID = Convert.ToInt32(Session["UserID"].ToString());
            Users user = context.Users.Where(i => i.UserID == userID).FirstOrDefault();
            return PartialView(user);
        }
    }
}