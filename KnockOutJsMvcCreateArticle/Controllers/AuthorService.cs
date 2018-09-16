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

namespace KnockOutJsMvcCreateArticle.Controllers
{
    public  class AuthorService : IDisposable
    {
        private ArticleDBContex db = new ArticleDBContex();

        public IEnumerable<Author> GetAll()
        {
            return db.AuthorDB;
        }
        public Author GetAuthor(int id)
        {
            return db.AuthorDB.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Author author)
        {
            db.AuthorDB.Add(author);
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}