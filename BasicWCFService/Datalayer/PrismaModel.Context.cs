﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TwoToWin.Prisma.BasicWCFService.Datalayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Prisma_FastighetEntities : DbContext
    {
        public Prisma_FastighetEntities()
            : base("name=Prisma_FastighetEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BLbuilding> BLbuilding { get; set; }
        public DbSet<BLfloor> BLfloor { get; set; }
        public DbSet<BLroom> BLroom { get; set; }
        public DbSet<WOrequest> WOrequest { get; set; }
        public DbSet<WOaction> WOaction { get; set; }
        public DbSet<BLzone> BLzone { get; set; }
    }
}
