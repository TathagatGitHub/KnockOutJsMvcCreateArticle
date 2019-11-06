using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using KnockOutJsMvcCreateArticle.Models;
using System.Collections.Specialized;
using System.Net.Http.Formatting;
using System.Web.Mvc;

namespace KnockOutJsMvcCreateArticle.Controllers
{
    public class BookApiController : ApiController
    {
        private ArticleDBContex db = new ArticleDBContex();

       
        [Queryable]
        //   [ActionName("GetBooks")]
        public IQueryable<Book> GetBooks()
        {
            // var bookdb = db.BookDB.Include(b => b.Author);
            // return db.ArticleDB.AsEnumerable();
            return db.BookDB;
        }

    
        [Queryable]
        public IQueryable<Book> GetBooksSearch()
        {
            // var bookdb = db.BookDB.Include(b => b.Author);
            // return db.ArticleDB.AsEnumerable();
            return db.BookDB;
        }

        // GET api/BookApi/5
        //  [ActionName("GetBook")]
        public Book GetBook(int id)
        {
            Book book = db.BookDB.Find(id);
            if (book == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return book;
        }

        // PUT api/BookApi/5
        public HttpResponseMessage PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != book.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/BookApi
        public HttpResponseMessage PostBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.BookDB.Add(book);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, book);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = book.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        public HttpResponseMessage PostBookStep(FormDataCollection form)
        {
            string customerid = form.Get("email");
           

    return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage PostBookStepJsSubmission(Book book)
        {
            if (ModelState.IsValid)
            {
                db.BookDB.Add(book);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, book);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = book.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/BookApi/5
        public HttpResponseMessage DeleteBook(int id)
        {
            Book book = db.BookDB.Find(id);
            if (book == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.BookDB.Remove(book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, book);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}