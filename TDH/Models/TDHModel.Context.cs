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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
        public virtual DbSet<V_ACCOUNT_HISTORY> V_ACCOUNT_HISTORY { get; set; }
        public virtual DbSet<V_MONEY_FLOW> V_MONEY_FLOW { get; set; }
        public virtual DbSet<V_CATEGORY_HISTORY> V_CATEGORY_HISTORY { get; set; }
        public virtual DbSet<WK_CHECKLIST_GROUP> WK_CHECKLIST_GROUP { get; set; }
        public virtual DbSet<WK_CHECKLIST_ITEM> WK_CHECKLIST_ITEM { get; set; }
        public virtual DbSet<WK_CHECKLIST_ITEM_DETAIL> WK_CHECKLIST_ITEM_DETAIL { get; set; }
    
        [DbFunction("chacd26d_trandinhhungEntities", "FNC_REPORT_SUMMARY_BY_YEAR")]
        public virtual IQueryable<FNC_REPORT_SUMMARY_BY_YEAR_Result> FNC_REPORT_SUMMARY_BY_YEAR(Nullable<int> i_Year)
        {
            var i_YearParameter = i_Year.HasValue ?
                new ObjectParameter("I_Year", i_Year) :
                new ObjectParameter("I_Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FNC_REPORT_SUMMARY_BY_YEAR_Result>("[chacd26d_trandinhhungEntities].[FNC_REPORT_SUMMARY_BY_YEAR](@I_Year)", i_YearParameter);
        }
    
        [DbFunction("chacd26d_trandinhhungEntities", "FNC_REPORT_INCOME_BY_CATEGORY")]
        public virtual IQueryable<FNC_REPORT_INCOME_BY_CATEGORY_Result> FNC_REPORT_INCOME_BY_CATEGORY()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FNC_REPORT_INCOME_BY_CATEGORY_Result>("[chacd26d_trandinhhungEntities].[FNC_REPORT_INCOME_BY_CATEGORY]()");
        }
    
        [DbFunction("chacd26d_trandinhhungEntities", "FNC_MN_GROUP_SETTING_GET_BY_GROUP")]
        public virtual IQueryable<FNC_MN_GROUP_SETTING_GET_BY_GROUP_Result> FNC_MN_GROUP_SETTING_GET_BY_GROUP(Nullable<System.Guid> i_GroupID, Nullable<int> i_Year, Nullable<System.Guid> i_UserID)
        {
            var i_GroupIDParameter = i_GroupID.HasValue ?
                new ObjectParameter("I_GroupID", i_GroupID) :
                new ObjectParameter("I_GroupID", typeof(System.Guid));
    
            var i_YearParameter = i_Year.HasValue ?
                new ObjectParameter("I_Year", i_Year) :
                new ObjectParameter("I_Year", typeof(int));
    
            var i_UserIDParameter = i_UserID.HasValue ?
                new ObjectParameter("I_UserID", i_UserID) :
                new ObjectParameter("I_UserID", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FNC_MN_GROUP_SETTING_GET_BY_GROUP_Result>("[chacd26d_trandinhhungEntities].[FNC_MN_GROUP_SETTING_GET_BY_GROUP](@I_GroupID, @I_Year, @I_UserID)", i_GroupIDParameter, i_YearParameter, i_UserIDParameter);
        }
    
        [DbFunction("chacd26d_trandinhhungEntities", "FNC_REPORT_SUMMARY")]
        public virtual IQueryable<FNC_REPORT_SUMMARY_Result> FNC_REPORT_SUMMARY()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FNC_REPORT_SUMMARY_Result>("[chacd26d_trandinhhungEntities].[FNC_REPORT_SUMMARY]()");
        }
    }
}
