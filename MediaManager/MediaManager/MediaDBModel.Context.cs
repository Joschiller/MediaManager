﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediaManager
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MediaDBEntities : DbContext
    {
        public MediaDBEntities()
            : base("name=MediaDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Catalogue> Catalogues { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<MT_Relation> MT_Relation { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<PT_Relation> PT_Relation { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
    }
}
