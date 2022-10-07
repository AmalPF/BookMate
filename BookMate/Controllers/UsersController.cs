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
    public class UsersController : Controller
    {
        private BookMateDBEntities db = new BookMateDBEntities();

        //*******************************************************************************************************************************

        // ------------------- User Sign In Page -------------------
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "UUsername,UPassword")] Users user)
        {
            if (ModelState.IsValid)
            {

                var query = from u in db.Users
                            where u.UUserName.Contains(user.UUserName)
                            select u;
                Users temp = query.FirstOrDefault();
                if (temp == null)
                {
                    ModelState.AddModelError("", "Login Failed! No such user exists!!!");
                }
                else if (temp.UUserName == user.UUserName && temp.UPassword == user.UPassword)
                {
                    Session["Username"] = temp.UUserName;
                    Session["UserId"] = temp.UId;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Failed! Incorrect Password!!!");
                }
            }

            return View(user);
        }

        // ------------------- User Sign Up Page -------------------
        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["Username"] = null;
            return RedirectToAction("Index", "Home");
        }

        // ------------------- User Sign Up Page -------------------
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SignUp([Bind(Include = "UId,UUserName,UPassword,UFName,ULName,UDOB,UEmail,UPhone")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // ------------------- User Profile Page -------------------

        // GET: Users/Profile/5
        [UserAuth]
        public ActionResult Profile()
        {
            if (string.IsNullOrEmpty(Session["UserId"].ToString()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(Session["UserId"]);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //*******************************************************************************************************************************

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
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

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UId,UUserName,UPassword,UFName,ULName,UDOB,UEmail,UPhone")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UId,UUserName,UPassword,UFName,ULName,UDOB,UEmail,UPhone")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
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

        // POST: Users/Delete/5
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
