using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Product Name length up to 64 symbol")]
        public string Name { get; set; }
    }
}