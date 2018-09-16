using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
 

namespace KnockOutJsMvcCreateArticle.Models
{
    public class Article
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Excerpts { get; set; }
        public string Content { get; set; }
    }
}