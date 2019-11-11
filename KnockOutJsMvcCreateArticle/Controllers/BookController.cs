using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnockOutJsMvcCreateArticle.Models;

namespace KnockOutJsMvcCreateArticle.Controllers
{
    public class BookController : Controller
    {
        private ArticleDBContex db = new ArticleDBContex();
        //
        // GET: /Book/

        public ActionResult Index()
        {
            
            List<Book> allBooks = db.BookDB.ToList();
            //return View("IndexDatatableV2", allBooks);
            return View("IndexDatatable", allBooks);
        }
        public ActionResult AuhorBookList()
        {

            return View();
        }
        public ActionResult WebAPIIndex()
        {
            return View();
        }
        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
           // var allBooks = db.BookDB.ToList();

            var allBooks = (from s in db.BookDB
                                    where s.Isbn == "dd"
                                    select s).ToList().Skip(param.iDisplayStart).Take(param.iDisplayLength);

            // var result = from c in db.BookDB
            //           select new [] { c.Id, c.AuthorId, c.Isbn,c.Description,c.Synopsis, c.ImageUrl };

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = allBooks.Count(),
                iTotalDisplayRecords = allBooks.Count(),
                aaData = allBooks
            },
                            JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Book/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.AuthorDB, "Id", "FullName");
            if (Request.IsAjaxRequest())
            {
               // return PartialView("_Create");
                return PartialView("_KOCreate");
            }
            else
                return View();
        }
        public ActionResult CreateWizard()
        {
            ViewBag.AuthorId = new SelectList(db.AuthorDB, "Id", "FullName");
            if (Request.IsAjaxRequest())
            {
                // return PartialView("_Create");
                return PartialView("_KOCreateBookWizard");
            }
            else
                return View("_KOCreateBookWizard");
        }
        public ActionResult CreateWizardStepJs()
        {
            ViewBag.AuthorId = new SelectList(db.AuthorDB, "Id", "FullName");
            if (Request.IsAjaxRequest())
            {
                // return PartialView("_Create");
                return PartialView("_KOCreateBookSetpJs");
            }
            else
                return View("_KOCreateBookSetpJs");
        }
        //
        // POST: /Book/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
               /**/
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CreateAjaxBook([Bind(Include = "Id,AuthorId,Title,Isbn,Synopsis,Description,ImageUrl")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.BookDB.Add(book);
                db.SaveChanges();
                var books = db.BookDB.ToList();
                if (Request.IsAjaxRequest())
                {
                   // return PartialView("IndexBooks", books);
                    return PartialView("_Index", books);
                }
                else
                    return View(books);
            }

            return View();

        }

        //
        // GET: /Book/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }
        // GET: /Book/Edit/5

        public ActionResult KOEdit(int id = 0)
        {
            Book book = db.BookDB.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            //return View(article);
           return PartialView("_KOEdit", book);
            
        }

        //
        // POST: /Book/Edit/5

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
        // GET: /Book/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Book/Delete/5

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



        // [System.Web.Http.HttpPost]
        public ActionResult GetBookspost(JQueryDataTableParams param)
        {
            int i = 1;
            JsonResult JsonResult = new JsonResult();
            try
            {
                int recFilter = db.BookDB.Count();
              
               //var displayedMembers = db.BookDB.ToList().Skip(param.iDisplayStart).Take(param.iDisplayLength);
                var displayedMembers = (from s in db.BookDB
                                       where s.Isbn == "dd"
                                       select s).ToList().Skip(param.iDisplayStart).Take(param.iDisplayLength);


                JsonResult = Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = recFilter,
                    iTotalDisplayRecords = recFilter,
                    aaData = displayedMembers
                },
                   JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                // Info     
                Console.Write(ex);
            }
            // Return info.     
            return JsonResult;

        }
    }
}
