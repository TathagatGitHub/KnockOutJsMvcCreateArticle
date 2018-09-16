using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnockOutJsMvcCreateArticle.Models;



namespace KnockOutJsMvcCreateArticle.Controllers
{
    public class BookAuthorController : Controller
    {
        BookAuthorViewModel objBookAuthorViewModel = new BookAuthorViewModel();
        ArticleDBContex db = new ArticleDBContex();
        List<Book> Books = new List<Book>();
        List<Author> Authors = new List<Author>();
        //
        // GET: /BookAuthor/
         [System.Web.Mvc.OutputCache(Duration = 30, Location = System.Web.UI.OutputCacheLocation.Client, VaryByParam = "none")]
        public ActionResult Index()
        {
            System.Threading.Thread.Sleep(5000);
            Books = db.BookDB.ToList();
            Authors = db.AuthorDB.ToList();
            
            objBookAuthorViewModel.Authors = Authors;
            objBookAuthorViewModel.Books = Books;
            return View("~/Views/Book/Index.cshtml", objBookAuthorViewModel);
        }

        
        public ActionResult IndexMVCModelKOMapping()
        {
            System.Threading.Thread.Sleep(5000);
            Books = db.BookDB.ToList();
            Authors = db.AuthorDB.ToList();

            objBookAuthorViewModel.Authors = Authors;
            objBookAuthorViewModel.Books = Books;
            return View("~/Views/Book/IndexMVCModelKOMapping.cshtml", objBookAuthorViewModel);
        }

        //
        // GET: /BookAuthor/Details/5
        [System.Web.Mvc.OutputCache(Duration = 3000, Location = System.Web.UI.OutputCacheLocation.Client, VaryByParam = "id")]
        public ActionResult Details(int id)
         {
          
            Author author = db.AuthorDB.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }


            return View(author);

           
        }
         [System.Web.Mvc.OutputCache(Duration = 3000, Location = System.Web.UI.OutputCacheLocation.Client, VaryByParam = "id")]
        public JsonResult DetailsJson(int id)
         {
             System.Threading.Thread.Sleep(5000);
            return Json(db.AuthorDB.Find(id), JsonRequestBehavior.AllowGet);

           
        }
        

        //
        // GET: /BookAuthor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BookAuthor/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BookAuthor/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BookAuthor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BookAuthor/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BookAuthor/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
