using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockOutJsMvcCreateArticle.Models
{
    public class BookAuthorViewModel
    {
        public List<Author> Authors { get; set; }
        public List<Book> Books { get; set; }
    }
}