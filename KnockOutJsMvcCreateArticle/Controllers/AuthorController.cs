using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using KnockOutJsMvcCreateArticle.Models;
using System.Web.ModelBinding;

namespace KnockOutJsMvcCreateArticle.Controllers
{
    public class AuthorController : Controller
    {
        private ArticleDBContex db = new ArticleDBContex();
        AuthorService _repository = new AuthorService();
       
        // AutoMapper if using ViewModel
        // GET: api/Authors
        
        public ActionResult Index([Form] QueryOptions queryOptions)
        {
            //queryOptions.PageSize = 1;
            var start = (queryOptions.CurrentPage - 1) * queryOptions.PageSize;

            var authors = db.AuthorDB.OrderBy(queryOptions.Sort).Skip(start).Take(queryOptions.PageSize);
            queryOptions.TotalPages =(int) Math.Ceiling((double)db.ArticleDB.Count() / queryOptions.PageSize);
            
            ViewBag.QueryOptions = queryOptions;
            return View(db.AuthorDB.ToList());
        }
      

        public ActionResult Details(int id = 0)
        {
            Author author = db.AuthorDB.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        public ActionResult Create()
        {
           // ViewBag.AuthorId = new SelectList(db.AuthorDB, "Id", "FullName");
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Create");
            }
            else
                return View();
            
        }

      
        public ActionResult CreateAjaxAuthor([Bind(Include = "Id,FirstName,LastName,Biography")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.AuthorDB.Add(author);
                db.SaveChanges();
                var authors = db.AuthorDB.ToList();
                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/Shared/_Author.cshtml", authors);
                }
                else
                    return RedirectToAction("Index");
            }
            return View(author);
        }
       
        public ActionResult Edit(int id = 0)
        {
            Author author = db.AuthorDB.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            //public ActionResult Edit(
//[Bind(Include = "Id,FirstName,LastName,Biography")] Author author)
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxEditAuthor(Author author)
        {
            //public ActionResult Edit(
            //[Bind(Include = "Id,FirstName,LastName,Biography")] Author author)
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();

                var authors = db.AuthorDB.ToList();
                if (Request.IsAjaxRequest())
                {
                    // return PartialView("IndexBooks", books);
                    return PartialView("_IndexMVCModelKOMapping", authors);
                }
                else
                    return View();
            }
            return View();
        }
        

        public ActionResult AjaxEdit(int id = 0)
        {
            Author author = db.AuthorDB.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            return PartialView("_EditMVCModelKOMapping", author);

        }

        public ActionResult Delete(int id = 0)
        {
            Author author = db.AuthorDB.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.AuthorDB.Find(id);
            db.AuthorDB.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}