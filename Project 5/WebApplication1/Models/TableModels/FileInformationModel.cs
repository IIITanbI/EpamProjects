using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.folder;

namespace WebApplication1.Models
{
    public class FileInformationModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "File name")]
        [StringLength(64, ErrorMessage = "File name length up to 64 symbol")]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [CustomDate]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Manager Name")]
        [StringLength(64, ErrorMessage = "Manager name length up to 64 symbol")]
        public string ManagerName { get; set; }
    }
}