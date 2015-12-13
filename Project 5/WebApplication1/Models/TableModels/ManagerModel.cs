using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ManagerModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Second Name")]
        [StringLength(64, ErrorMessage = "Second Name length up to 64 symbol")]
        public string SecondName { get; set; }
    }
}