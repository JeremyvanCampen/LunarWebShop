﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LunarWebShop.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LunarEntities2 : DbContext
    {
        public LunarEntities2()
            : base("name=LunarEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Bestelling> Bestelling { get; set; }
        public virtual DbSet<Gebruiker> Gebruiker { get; set; }
        public virtual DbSet<Keycode> Keycode { get; set; }
        public virtual DbSet<Klant> Klant { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Table> Table { get; set; }
    }
}
