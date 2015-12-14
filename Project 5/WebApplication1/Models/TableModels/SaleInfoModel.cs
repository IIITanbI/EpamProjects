using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.folder;

namespace WebApplication1.Models
{
    public class SaleInfoModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomDate]
        public DateTime Date { get; set; }

        [Display(Name = "Client first Name")]
        [StringLength(64, ErrorMessage = "Client first Name length up to 64 symbol")]
        public string ClientFirstName { get; set; }

        [Required]
        [Display(Name = "Client second Name")]
        [StringLength(64, ErrorMessage = "Client second Name length up to 64 symbol")]
        public string ClientSecondName { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(64, ErrorMessage = "Product Name length up to 64 symbol")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "File Name")]
        [StringLength(64, ErrorMessage = "FileName length up to 64 symbol")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "Manager Name")]
        [StringLength(64, ErrorMessage = "Manager Name length up to 64 symbol")]
        public string ManagerName { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal Cost { get; set; }
    }
}