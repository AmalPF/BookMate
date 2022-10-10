using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookMate.Filters;
using BookMate.Models;
using static System.Net.WebRequestMethods;

namespace BookMate.Controllers
{
    public class BooksController : Controller
    {
        private BookMateDBEntities db = new BookMateDBEntities();

        //*******************************************************************************************************************************

        // ------------------- index page: search for book -------------------
        public ActionResult Index(string SearchBy, string search)
        {
            ViewBag.res = "";
            if (SearchBy == "BookName")
            {
                var books = db.Books.Where(model => model.BName == search).ToList();
                if (books.Count == 0)
                {
                    ViewBag.res = "No Book Found";
                }
                return View(books);
            }
            else if (SearchBy == "Category")
            {
                var books = db.Books.Where(model => model.BCategory == search).ToList();
                if (books.Count == 0)



                {
                    ViewBag.res = "No Book Found on this category";
                }
                return View(books);
            }
            else
            {
                var books = db.Books.Include(b => b.Category);
                return View(books.ToList());
            }
        }

        // ------------------- BookList Page: displays all books to Admin -------------------
        [AdminAuth]
        public ActionResult BookList()
        {
            var books = db.Books.Include(b => b.Category);
            return View(books.ToList());
        }

        // ------------------- AddToCart Page -------------------
        [UserAuth]
        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = db.Users.Find(Convert.ToInt32(Session["UserId"].ToString()));
            Books book = db.Books.Find(id);
            Cart newItem = new Cart { UId = user.UId, BId = id };

            db.Cart.Add(newItem);
            db.SaveChanges();
            //Response.Write("<script> alert('" + book.BName + " Added to Cart')</script>");
            return RedirectToAction("Index", "Carts");
        }

        // ------------------- Add Page -------------------
        [AdminAuth]
        public ActionResult Add()
        {
            ViewBag.BCategory = new SelectList(db.Category, "CCategoryName", "CCategoryName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "BId,BName,BAuthor,BPublisher,BYearOfPublication,BCategory,BImage,BPrice,BQuantity")] Books books, HttpPostedFileBase BImage)
        {
            if (ModelState.IsValid)
            {
                if (BImage != null)
                {
                    int newId = getLastBookId() + 1;
                    string newFileName = newId + Path.GetExtension(BImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/Book Images"), newFileName);
                    books.BImage = "~/Uploads/Book Images/" + newFileName;
                    BImage.SaveAs(path);
                    db.Books.Add(books);
                    db.SaveChanges();
                    return RedirectToAction("BookList");
                }
            }

            ViewBag.BCategory = new SelectList(db.Category, "CCategoryName", "CCategoryName", books.BCategory);
            return View(books);
        }



        // ------------------- getting the last id from books -------------------
        private int getLastBookId()
        {
            int id = db.Books.Max(b => b.BId);
            return id;
        }



        //*******************************************************************************************************************************


        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            Session["image1"] = books.BImage.ToString();
            return View(books);
        }


        // GET: Books/Edit/5
        [AdminAuth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            ViewBag.BCategory = new SelectList(db.Category, "CCategoryName", "CCategoryName", books.BCategory);
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AdminAuth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BId,BName,BAuthor,BPublisher,BYearOfPublication,BCategory,BImage,BPrice,BQuantity,BNPurchases,BRating")] Books books, HttpPostedFileBase BImage)
        {
            if (ModelState.IsValid)
            {
                int newId = books.BId;
                string newFileName = newId + Path.GetExtension(BImage.FileName);
                string path = Path.Combine(Server.MapPath("~/Uploads/Book Images"), newFileName);
                books.BImage = "~/Uploads/Book Images/" + newFileName;
                BImage.SaveAs(path);
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BCategory = new SelectList(db.Category, "CCategoryName", "CCategoryName", books.BCategory);
            return View(books);
        }

        // GET: Books/Delete/5
        [AdminAuth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Books books = db.Books.Find(id);
            db.Books.Remove(books);
            db.SaveChanges();
            return RedirectToAction("BookList");
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
