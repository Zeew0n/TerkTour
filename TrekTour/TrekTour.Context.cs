﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrekTour
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TrekTourEntities : DbContext
    {
        public TrekTourEntities()
            : base("name=TrekTourEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ContentFunctions> ContentFunctions { get; set; }
        public DbSet<ContentImages> ContentImages { get; set; }
        public DbSet<Contents> Contents { get; set; }
        public DbSet<Functions> Functions { get; set; }
        public DbSet<NewsRecords> NewsRecords { get; set; }
        public DbSet<QuotesRecords> QuotesRecords { get; set; }
        public DbSet<TestimonialsRecords> TestimonialsRecords { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<ContentTags> ContentTags { get; set; }
        public DbSet<Menus> Menus { get; set; }
    }
}
