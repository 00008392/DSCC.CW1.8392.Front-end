using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSCC._8392.FrontEnd.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Title not specified")]
        public string Title { get; set; }
    }
}