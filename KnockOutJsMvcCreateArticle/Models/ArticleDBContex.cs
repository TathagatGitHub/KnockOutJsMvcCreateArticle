using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace KnockOutJsMvcCreateArticle.Models
{
    public class ArticleDBContex: DbContext
    {
       
        public ArticleDBContex(): base("ArticleDBContexConnection") // "DefaultConnection" is from web.config file.
            {
                Database.SetInitializer<ArticleDBContex>(new DropCreateDatabaseIfModelChanges<ArticleDBContex>());
                // Above line is important otherwise when you make any change in the model, it will not work.
                this.Configuration.LazyLoadingEnabled = false;
            } 
        public DbSet<Article> ArticleDB { get; set; }
        public DbSet<Book> BookDB { get; set; }
        public DbSet<Author> AuthorDB { get; set; }
        //public DbSet<Products> ProductDB { get; set; }
    }



   
          

  

}