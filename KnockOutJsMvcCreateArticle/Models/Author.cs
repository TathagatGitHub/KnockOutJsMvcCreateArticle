using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace KnockOutJsMvcCreateArticle.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
       
        public string LastName { get; set; }
        public string Biography { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }

     //   public virtual ICollection<Book> Books { get; set; }
    }
}