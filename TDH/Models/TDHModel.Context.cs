﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TDH.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class chacd26d_trandinhhungEntities : DbContext
    {
        public chacd26d_trandinhhungEntities()
            : base("name=chacd26d_trandinhhungEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ABOUT> ABOUTs { get; set; }
        public virtual DbSet<CATEGORY> CATEGORies { get; set; }
        public virtual DbSet<CONFIGURATION> CONFIGURATIONs { get; set; }
        public virtual DbSet<ERROR_LOG> ERROR_LOG { get; set; }
        public virtual DbSet<FUNCTION> FUNCTIONs { get; set; }
        public virtual DbSet<NAVIGATION> NAVIGATIONs { get; set; }
        public virtual DbSet<POST> POSTs { get; set; }
        public virtual DbSet<ROLE> ROLEs { get; set; }
        public virtual DbSet<ROLE_DETAIL> ROLE_DETAIL { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
        public virtual DbSet<USER_ROLE> USER_ROLE { get; set; }
        public virtual DbSet<HOME_CATEGORY> HOME_CATEGORY { get; set; }
        public virtual DbSet<HOME_NAVIGATION> HOME_NAVIGATION { get; set; }
        public virtual DbSet<DAILY_TASK> DAILY_TASK { get; set; }
        public virtual DbSet<IDEA> IDEAs { get; set; }
        public virtual DbSet<IDEA_DETAIL> IDEA_DETAIL { get; set; }
        public virtual DbSet<REPORT> REPORTs { get; set; }
        public virtual DbSet<REPORT_COMMENT> REPORT_COMMENT { get; set; }
        public virtual DbSet<TARGET> TARGETs { get; set; }
        public virtual DbSet<TARGET_TASK> TARGET_TASK { get; set; }
        public virtual DbSet<MN_CATEGORY> MN_CATEGORY { get; set; }
        public virtual DbSet<MN_CATEGORY_SETTING> MN_CATEGORY_SETTING { get; set; }
        public virtual DbSet<MN_GROUP> MN_GROUP { get; set; }
        public virtual DbSet<MN_GROUP_SETTING> MN_GROUP_SETTING { get; set; }
        public virtual DbSet<MN_ACCOUNT> MN_ACCOUNT { get; set; }
        public virtual DbSet<MN_ACCOUNT_SETTING> MN_ACCOUNT_SETTING { get; set; }
        public virtual DbSet<MN_ACCOUNT_TYPE> MN_ACCOUNT_TYPE { get; set; }
        public virtual DbSet<MN_INCOME> MN_INCOME { get; set; }
        public virtual DbSet<MN_PAYMENT> MN_PAYMENT { get; set; }
        public virtual DbSet<MN_TRANSFER> MN_TRANSFER { get; set; }
    }
}
