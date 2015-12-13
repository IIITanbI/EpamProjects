using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using DAL.Models;

namespace WebApplication1.Models
{
    public class ClientModel
    {
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(64, ErrorMessage = "First Name length up to 64 symbol")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Second Name length up to 64 symbol")]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }


    }
}