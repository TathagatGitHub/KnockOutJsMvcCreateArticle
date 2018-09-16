using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockOutJsMvcCreateArticle.Models
{
    public class viewModelBookAuthor // Does not need to be added into DBContext 
    {
        public string BookIsbn {get;set;}
        public int AuthorID {get; set;}
        public string AuthorName{get;set;}
        public string BookTitle{get;set;}
        public string BookDescription {get;set;}
        public string BookImage { get; set; }
         
    }
}