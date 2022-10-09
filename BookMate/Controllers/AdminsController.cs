using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookMate.Filters;
using BookMate.Models;

namespace BookMate.Controllers
{
    public class AdminsController : Controller
    {
        private BookMateDBEntities db = new BookMateDBEntities();

        //**************************************************

        public ActionResult Index()
        {
            return RedirectToAction("SignIn", "Admins");
        }

        // ------------------- Admin SignIn Page -------------------
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "AUsername,APassword")] Admin admin)
        {
            if (ModelState.IsValid)
            {

                var query = from a in db.Admin
                            where a.AUserName.Contains(admin.AUserName)
                            select a;
                Admin temp = query.FirstOrDefault();
                if (temp == null)
                {
                    ModelState.AddModelError("", "Login Failed! No such user exists!!!");
                }
                else if (temp.AUserName == admin.AUserName && temp.APassword == admin.APassword)
                {
                    Session["AdminName"] = temp.AUserName;
                    Session["Username"] = null;
                    Session["UserId"] = null;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Admin Login Failed! Incorrect Password!!!");
                }
            }

            return View(admin);
        }

        // ------------------- Admin Logout Page -------------------
        [AdminAuth]
        public ActionResult Logout()
        {
            Session["AdminName"] = null;
            return RedirectToAction("Index", "Home");
        }

        //********************************************************

        
       
       
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
