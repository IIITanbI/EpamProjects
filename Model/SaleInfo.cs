//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SaleInfo()
        {
            this.Cost = 0m;
        }
    
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public decimal Cost { get; set; }
        public int FileId { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Client Client { get; set; }
        public virtual FileInformation File { get; set; }
    }
}
