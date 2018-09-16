using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KnockOutJsMvcCreateArticle.Models;
using System.Data.Entity;
namespace KnockOutJsMvcCreateArticle.Controllers
{
    public class BookAuthorAPIController : ApiController
    {
        BookAuthorViewModel objBookAuthorViewModel = new BookAuthorViewModel();
        ArticleDBContex db = new ArticleDBContex();
        List <Book> books = new List<Book>();
        List<Author> authors = new List<Author>();
        // GET api/bookauthorapi
        public BookAuthorViewModel Get()
        {
            books = db.BookDB.ToList();
            authors = db.AuthorDB.ToList();
            objBookAuthorViewModel.Authors = authors;
            objBookAuthorViewModel.Books = books;
            return (objBookAuthorViewModel);
         //   return new string[] { "value1", "value2" };
        }
        
        public IQueryable<viewModelBookAuthor> Getvm2(string bookIsbn, int authorId)
        {

            var query = from x in db.BookDB
                        join y in db.AuthorDB on x.AuthorId equals y.Id
                        where x.Isbn.Equals(bookIsbn)
                        select new viewModelBookAuthor
                        {
                            BookIsbn = x.Isbn,
                            BookTitle = x.Title,
                            AuthorName = y.FirstName,
                            BookImage = x.ImageUrl,
                            BookDescription = x.Description,
                            AuthorID = y.Id
                        };
            return query;
        }

        [HttpGet]
        [Queryable]
        public IQueryable<viewModelBookAuthor> GetvmBookAuthorSearch(string bookIsbn)
        {
           
            var query = from x in db.BookDB
                    join y in db.AuthorDB on x.AuthorId equals y.Id
                    where x.Isbn.Equals(bookIsbn)
                    select new viewModelBookAuthor
                    {
                        BookIsbn = x.Isbn,
                        BookTitle = x.Title,
                        AuthorName = y.FirstName,
                        BookImage = x.ImageUrl,
                        BookDescription = x.Description,
                        AuthorID = y.Id 
                    };
                return query;
        }

        // GET api/bookauthorapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/bookauthorapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/bookauthorapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bookauthorapi/5
        public void Delete(int id)
        {
        }
    }
}
