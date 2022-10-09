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
    public class PurchasesController : Controller
    {
        private BookMateDBEntities db = new BookMateDBEntities();

        //****************************************************************************************
        [UserAuth]
        public ActionResult Index()
        {
            Users user = db.Users.Find(Convert.ToInt32(Session["UserId"].ToString()));

            List<Address> addressList = db.Address.Where(x => x.UId == user.UId).ToList();
            List<Purchase> purchaseList = new List<Purchase>();
            foreach (Address a in addressList)
            {
                purchaseList.AddRange(db.Purchase.Where(x => x.AId == a.AId).ToList());
            }
            return View(purchaseList);
        }

        public ActionResult AddRating(int? id)
        {
            Session["BookId"] = id;
            return RedirectToAction("AddRating", "Ratings");
        }

        public ActionResult ConfirmOrder(int? cid)
        {
            if (cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cartItem = db.Cart.Find(cid);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            Purchase item = new Purchase { UId = cartItem.UId, BId = cartItem.BId, PAmount = cartItem.Books.BPrice, PQuantity = 1 };
            ViewBag.Quantity = cartItem.Books.BQuantity;
            ViewBag.BookName = cartItem.Books.BName;
            ViewBag.Price = cartItem.Books.BPrice;
            ViewBag.AId = new SelectList(db.Address.Where(x => x.UId == cartItem.UId), "AId", "AAddressL1");
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmOrder([Bind(Include = "PId,UId,BId,AId,PDateTime,PQuantity,PAmount")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                purchase.PDateTime = DateTime.Now; 
                db.Purchase.Add(purchase);
                db.SaveChanges();
                
                return RedirectToAction("AllPurchases");
            }
            ViewBag.BookName = purchase.Books.BName;
            ViewBag.Price = purchase.Books.BPrice;
            ViewBag.Quantity = purchase.Books.BQuantity;
            ViewBag.AId = new SelectList(db.Address.Where(x => x.UId == purchase.UId), "AId", "AAddressL1");
            return View(purchase);
        }

        //****************************************************************************************

        // GET: Purchases
        public ActionResult AllPurchases()
        {
            var purchase = db.Purchase.Include(p => p.Address).Include(p => p.Books);
            return View(purchase.ToList());
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchase.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.AId = new SelectList(db.Address, "AId", "AAddressL1");
            ViewBag.BId = new SelectList(db.Books, "BId", "BName");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PId,BId,AId,PDateTime,PQuantity,PAmount")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Purchase.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AId = new SelectList(db.Address, "AId", "AAddressL1", purchase.AId);
            ViewBag.BId = new SelectList(db.Books, "BId", "BName", purchase.BId);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchase.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.AId = new SelectList(db.Address, "AId", "AAddressL1", purchase.AId);
            ViewBag.BId = new SelectList(db.Books, "BId", "BName", purchase.BId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PId,BId,AId,PDateTime,PQuantity,PAmount")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AId = new SelectList(db.Address, "AId", "AAddressL1", purchase.AId);
            ViewBag.BId = new SelectList(db.Books, "BId", "BName", purchase.BId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchase.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchase.Find(id);
            db.Purchase.Remove(purchase);
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
