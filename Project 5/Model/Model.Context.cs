﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModelContainer : DbContext
    {
        public ModelContainer()
            : base("ModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Manager> ManagerSet { get; set; }
        public virtual DbSet<Client> ClientSet { get; set; }
        public virtual DbSet<FileInformation> FileInformationSet { get; set; }
        public virtual DbSet<Product> ProductSet { get; set; }
        public virtual DbSet<SaleInfo> SaleInfoSet { get; set; }
    }
}
