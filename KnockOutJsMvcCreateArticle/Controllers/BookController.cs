﻿using System;
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

        public ActionResult AdvanceDataTable ()
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
           // int i = 1;
           if (Request.Form["action"] == "edit")
            {
                return Json(new
                {
                    success = true,
                    responseText = 1000,
                    responseCode = "Success"
                });
            }
            JsonResult JsonResult = new JsonResult();
            try
            {
                int recFilter = db.BookDB.Count();

                //var displayedMembers = db.BookDB.ToList().Skip(param.iDisplayStart).Take(param.iDisplayLength);
                if (param.iDisplayLength!=-1)
                { var displayedMembers = (from s in db.BookDB
                                          where s.Title == "Title12"
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
                else
                {
                    var displayedMembers = (from s in db.BookDB
                                            where s.Isbn == "Isbn12"
                                            select s).ToList();
                    JsonResult = Json(new
                    {
                        sEcho = param.sEcho,
                        iTotalRecords = recFilter,
                        iTotalDisplayRecords = recFilter,
                        aaData = displayedMembers
                    },
                JsonRequestBehavior.AllowGet);

                }
             

            }

            catch (Exception ex)
            {
                // Info     
                Console.Write(ex);
            }
            // Return info.     
            return JsonResult;

        }

        public JsonResult DataProviderAction(string sEcho, int iDisplayStart, int iDisplayLength)
        {
            var idFilter = Convert.ToString(Request["sSearch_0"]);
            var nameFilter = Convert.ToString(Request["sSearch_2"]);
        //    var  dateFilter = Convert.ToString(Request["sSearch_2"]);
            var townFilter = Convert.ToString(Request["sSearch_3"]);
            var synopsisFilter = Convert.ToString(Request["sSearch_4"]);

            var fromID = 0;
            var toID = 0;
            if (idFilter.Contains('~'))
            {
                //Split number range filters with ~
                fromID = idFilter.Split('~')[0] == "" ? 0 : Convert.ToInt32(idFilter.Split('~')[0]);
                toID = idFilter.Split('~')[1] == "" ? 0 : Convert.ToInt32(idFilter.Split('~')[1]);
            }
            var city = "";
            if (townFilter.Contains('^'))
            {
                //Split number range filters with ~
                city = townFilter.Substring(1, townFilter.Length-2); // == "" ? 0 : townFilter.Split('~')[0];
                if (city == "All")
                {
                    townFilter = "";
                }
             //   toID = idFilter.Split('~')[1] == "" ? 0 : Convert.ToInt32(townFilter.Split('~')[1]);
            }
            var synopsis = "";
            if (synopsisFilter.Contains('^'))
            {
                synopsis= synopsisFilter.Substring(1, synopsisFilter.Length - 2); // == "" ? 0 : townFilter.Split('~')[0];
                if (synopsis == "All")
                {
                    synopsisFilter = "";
                }
            }
       

            var filteredCompanies = db.BookDB
                                    .Where(c => (fromID == 0 || fromID < c.Id)
                                                &&
                                                (toID == 0 || c.Id < toID)
                                                &&
                                                (nameFilter == "" || c.Title.ToLower().Contains(nameFilter.ToLower()))
                                                &&
                                                (townFilter == "" || c.Isbn == city)
                                                &&
                                                (synopsisFilter == "" || c.Synopsis == synopsis)
                                                //&&
                                                //(fromDate == DateTime.MinValue || fromDate < c.DateCreated)
                                                //&&
                                                //(toDate == DateTime.MaxValue || c.DateCreated < toDate)
                                            );

            //Extract only current page
            //var displayedCompanies = filteredCompanies.Skip(iDisplayStart).Take(iDisplayLength);
            var result = (from s in filteredCompanies//db.BookDB
                        //  where s.Title == "Title12"
                                    select s).ToList().Skip(iDisplayStart).Take(iDisplayLength);

         
            return Json(new
            {
                sEcho = sEcho,
                iTotalRecords = db.BookDB.Count(),
                iTotalDisplayRecords = filteredCompanies.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

        public string EditDatatableMethod(int Id)
      //    public string EditDatatableMethod(JQueryDataTableParams param)
        {
           string isbnID;// = Request.Form["data[" + Id.ToString() + "[Isbn]"].ToString();
           string Synopsis;
            string ImageUrl;
            if (Request.Form["action"] == "edit")
            {
                Book book = db.BookDB.Find(Id);

                isbnID = Request.Form["data[" + Id.ToString() + "][Isbn]"].ToString();
                Synopsis = Request.Form["data[" + Id.ToString() + "][Synopsis]"].ToString();
                book.Synopsis = Synopsis;
                db.SaveChanges();
                return "Sucess";


            }

            else
            {
                ImageUrl = Request.Form["data[" + Id.ToString() + "][ImageUrl]"].ToString();
                return "Failure";
            }



            }
    }
}
