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
    public class CartsController : Controller
    {
        private BookMateDBEntities db = new BookMateDBEntities();


        //**************************************************

        public ActionResult Index()
        {
            Users user = db.Users.Find(Convert.ToInt32(Session["UserId"].ToString()));
            List<Cart> CartList = new List<Cart>();
            CartList.AddRange(db.Cart.Where(x => x.UId == user.UId).ToList());
            return View(CartList);
        }
        [UserAuth]
        public ActionResult BuyNow(int? id)
        {
            return RedirectToAction("ConfirmOrder", "Purchases", new { cid=id });
        }

        //**************************************************



        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Cart.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.BId = new SelectList(db.Books, "BId", "BName");
            ViewBag.UId = new SelectList(db.Users, "UId", "UUserName");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CId,UId,BId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Cart.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BId = new SelectList(db.Books, "BId", "BName", cart.BId);
            ViewBag.UId = new SelectList(db.Users, "UId", "UUserName", cart.UId);
            return View(cart);
        }
        
        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Cart.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Cart.Find(id);
            db.Cart.Remove(cart);
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
