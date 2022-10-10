using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookMate.Models;

namespace BookMate.Controllers
{
    public class RatingsController : Controller
    {
        private BookMateDBEntities db = new BookMateDBEntities();


        //*******************************************************************************************************************************

        public ActionResult AddRating()
        {
            Books book = db.Books.Find(Convert.ToInt32(Session["BookId"].ToString()));
            ViewBag.BookName = book.BName;
            ViewBag.UserName = Session["Username"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRating([Bind(Include = "RRating,RComments")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                rating.UId = Convert.ToInt32(Session["UserId"].ToString());
                rating.BId = Convert.ToInt32(Session["BookId"].ToString());
                db.Rating.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index", "Purchases");
            }

            return View(rating);
        }

        //*******************************************************************************************************************************


        // GET: Ratings
        public ActionResult Index()
        {
            var rating = db.Rating.Include(r => r.Books).Include(r => r.Users);
            return View(rating.ToList());
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Rating.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.BId = new SelectList(db.Books, "BId", "BName");
            ViewBag.UId = new SelectList(db.Users, "UId", "UUserName");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RId,BId,UId,RRating,RComments")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Rating.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BId = new SelectList(db.Books, "BId", "BName", rating.BId);
            ViewBag.UId = new SelectList(db.Users, "UId", "UUserName", rating.UId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Rating.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.BId = new SelectList(db.Books, "BId", "BName", rating.BId);
            ViewBag.UId = new SelectList(db.Users, "UId", "UUserName", rating.UId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RId,BId,UId,RRating,RComments")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BId = new SelectList(db.Books, "BId", "BName", rating.BId);
            ViewBag.UId = new SelectList(db.Users, "UId", "UUserName", rating.UId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Rating.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Rating.Find(id);
            db.Rating.Remove(rating);
            db.SaveChanges();
            return RedirectToAction("Index", "Purchase");
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
