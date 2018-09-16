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
using KnockOutJsMvcCreateArticle.Controllers;
namespace KnockOutJsMvcCreateArticle.Controllers
{
    public class AuthorApiController : ApiController
    {
        private ArticleDBContex db = new ArticleDBContex();

        AuthorService _repository = new AuthorService();

        public IEnumerable<Author> Get()
        {
            return _repository.GetAll();
        }

       // private authService = new AuthorService();
        // GET api/AuthorApi
        public IEnumerable<Author> GetAuthors()
        {
            return _repository.GetAll();
           
        }

        // GET api/AuthorApi/5
        public Author GetAuthor(int id)
        {
            Author author = db.AuthorDB.Find(id);
            if (author == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return author;
        }

        // PUT api/AuthorApi/5
     //   public HttpResponseMessage PutAuthor(int id, Author author)
        public HttpResponseMessage PutAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

           // if (id != author.Id)
          //  {
            //    return Request.CreateResponse(HttpStatusCode.BadRequest);
            //}

            db.Entry(author).State = EntityState.Modified;

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

        // POST api/AuthorApi
        public HttpResponseMessage PostAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(author);
            //    db.AuthorDB.Add(author);
              //  db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, author);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = author.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AuthorApi/5
        public HttpResponseMessage DeleteAuthor(int id)
        {
            Author author = db.AuthorDB.Find(id);
            if (author == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AuthorDB.Remove(author);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, author);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}