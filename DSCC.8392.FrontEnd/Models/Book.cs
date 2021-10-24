using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSCC._8392.FrontEnd.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Title not specified")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage ="Author not specified")]
        public string Author { get; set; }
        [Required(ErrorMessage ="Issue year not specified")]
        public int IssueYear { get; set; }
        [Required(ErrorMessage ="Genre not specified")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public SelectList Genres { get; set; }
        public string ErrorMessage { get; set; }
    }
}