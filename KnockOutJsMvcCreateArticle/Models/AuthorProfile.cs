using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KnockOutJsMvcCreateArticle.Models
{
    public class AuthorProfile
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Email required")] // datepicker will show
        public string Email { get; set; }

        [Required(ErrorMessage = "DOB required")] // datepicker will show
        [Display(Name = "DOB :")]
        [DataType(DataType.Date)]
       
        public string Phone { get; set; }

        public string Address { get; set; }
    }
}