using BookMate.Filters;
using BookMate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMate.Controllers
{
    public class HomeController : Controller
    {
        private BookMateDBEntities db = new BookMateDBEntities();

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
                var books = db.Books;
                return View(books.ToList());
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}