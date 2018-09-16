using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KnockOutJsMvcCreateArticle.Models;

namespace KnockOutJsMvcCreateArticle.Controllers
{
    public class ArticleApiController : ApiController
    {
        private ArticleDBContex db = new ArticleDBContex();

        // GET api/Default1
        [System.Web.Mvc.OutputCache(Duration = 30000, Location = System.Web.UI.OutputCacheLocation.Client, VaryByParam = "none")]
        public IEnumerable<Article> GetArticles()
        {
            return db.ArticleDB.AsEnumerable();
        }

        // GET api/Default1/5
         //[System.Web.Mvc.OutputCache(Duration = 30, Location = System.Web.UI.OutputCacheLocation.Client, VaryByParam = "id")]
         [System.Web.Mvc.OutputCache(Duration = 300000, Location = System.Web.UI.OutputCacheLocation.Client, VaryByParam = "id")]
        public Article GetArticle(int id)
        {
            System.Threading.Thread.Sleep(1000);
            Article article = db.ArticleDB.Find(id);
            if (article == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return article;
        }

        // PUT api/Default1/5
        public HttpResponseMessage PutArticle(int id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != article.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(article).State = EntityState.Modified;

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

        // POST api/Default1
        public HttpResponseMessage PostArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                db.ArticleDB.Add(article);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, article);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = article.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Default1/5
        public HttpResponseMessage DeleteArticle(int id)
        {
            Article article = db.ArticleDB.Find(id);
            if (article == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ArticleDB.Remove(article);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, article);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}