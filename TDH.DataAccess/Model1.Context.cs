﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TDH.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TDHEntities : DbContext
    {
        public TDHEntities()
            : base("name=TDHEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MN_ACCOUNT> MN_ACCOUNT { get; set; }
        public virtual DbSet<MN_ACCOUNT_SETTING> MN_ACCOUNT_SETTING { get; set; }
        public virtual DbSet<MN_ACCOUNT_TYPE> MN_ACCOUNT_TYPE { get; set; }
        public virtual DbSet<MN_CATEGORY> MN_CATEGORY { get; set; }
        public virtual DbSet<MN_CATEGORY_SETTING> MN_CATEGORY_SETTING { get; set; }
        public virtual DbSet<MN_GROUP> MN_GROUP { get; set; }
        public virtual DbSet<MN_GROUP_SETTING> MN_GROUP_SETTING { get; set; }
        public virtual DbSet<MN_INCOME> MN_INCOME { get; set; }
        public virtual DbSet<MN_PAYMENT> MN_PAYMENT { get; set; }
        public virtual DbSet<MN_TRANSFER> MN_TRANSFER { get; set; }
        public virtual DbSet<WK_CHECKLIST_GROUP> WK_CHECKLIST_GROUP { get; set; }
        public virtual DbSet<WK_CHECKLIST_ITEM> WK_CHECKLIST_ITEM { get; set; }
        public virtual DbSet<WK_CHECKLIST_ITEM_DETAIL> WK_CHECKLIST_ITEM_DETAIL { get; set; }
        public virtual DbSet<V_MONEY_FLOW> V_MONEY_FLOW { get; set; }
        public virtual DbSet<PN_IDEA> PN_IDEA { get; set; }
        public virtual DbSet<PN_REPORT> PN_REPORT { get; set; }
        public virtual DbSet<PN_REPORT_KIND> PN_REPORT_KIND { get; set; }
        public virtual DbSet<PN_TARGET> PN_TARGET { get; set; }
        public virtual DbSet<PN_TARGET_PLANNING> PN_TARGET_PLANNING { get; set; }
        public virtual DbSet<PN_TARGET_PLANNING_REPORT> PN_TARGET_PLANNING_REPORT { get; set; }
        public virtual DbSet<PN_TARGET_PLANNING_SPRINT> PN_TARGET_PLANNING_SPRINT { get; set; }
        public virtual DbSet<PN_TARGET_PRIORITY> PN_TARGET_PRIORITY { get; set; }
        public virtual DbSet<PN_TARGET_QUESTION> PN_TARGET_QUESTION { get; set; }
        public virtual DbSet<PN_TARGET_TYPE> PN_TARGET_TYPE { get; set; }
        public virtual DbSet<PN_TASK> PN_TASK { get; set; }
        public virtual DbSet<PN_TASK_STATUS> PN_TASK_STATUS { get; set; }
        public virtual DbSet<PN_TASK_TYPE> PN_TASK_TYPE { get; set; }
        public virtual DbSet<SYS_FUNCTION> SYS_FUNCTION { get; set; }
        public virtual DbSet<SYS_ROLE> SYS_ROLE { get; set; }
        public virtual DbSet<SYS_ROLE_DETAIL> SYS_ROLE_DETAIL { get; set; }
        public virtual DbSet<SYS_USER> SYS_USER { get; set; }
        public virtual DbSet<SYS_USER_ROLE> SYS_USER_ROLE { get; set; }
        public virtual DbSet<WEB_ABOUT> WEB_ABOUT { get; set; }
        public virtual DbSet<WEB_CATEGORY> WEB_CATEGORY { get; set; }
        public virtual DbSet<WEB_CONFIGURATION> WEB_CONFIGURATION { get; set; }
        public virtual DbSet<WEB_ERROR_LOG> WEB_ERROR_LOG { get; set; }
        public virtual DbSet<WEB_HOME_CATEGORY> WEB_HOME_CATEGORY { get; set; }
        public virtual DbSet<WEB_HOME_NAVIGATION> WEB_HOME_NAVIGATION { get; set; }
        public virtual DbSet<WEB_NAVIGATION> WEB_NAVIGATION { get; set; }
        public virtual DbSet<WEB_POST> WEB_POST { get; set; }
        public virtual DbSet<V_ACCOUNT_HISTORY> V_ACCOUNT_HISTORY { get; set; }
        public virtual DbSet<V_CATEGORY_HISTORY> V_CATEGORY_HISTORY { get; set; }
        public virtual DbSet<SYS_MODULE> SYS_MODULE { get; set; }
        public virtual DbSet<PN_DREAM> PN_DREAM { get; set; }
        public virtual DbSet<CM_SKILL> CM_SKILL { get; set; }
        public virtual DbSet<CM_SKILL_DEFINDED> CM_SKILL_DEFINDED { get; set; }
        public virtual DbSet<CM_SKILL_GROUP> CM_SKILL_GROUP { get; set; }
        public virtual DbSet<PN_EDUCATION> PN_EDUCATION { get; set; }
        public virtual DbSet<PN_EDUCATION_TYPE> PN_EDUCATION_TYPE { get; set; }
        public virtual DbSet<PN_EVENT> PN_EVENT { get; set; }
        public virtual DbSet<PN_EVENT_TYPE> PN_EVENT_TYPE { get; set; }
        public virtual DbSet<PN_SKILL> PN_SKILL { get; set; }
        public virtual DbSet<V_TIMELINE> V_TIMELINE { get; set; }
    
        [DbFunction("TDHEntities", "FNC_REPORT_INCOME_BY_CATEGORY")]
        public virtual IQueryable<FNC_REPORT_INCOME_BY_CATEGORY_Result> FNC_REPORT_INCOME_BY_CATEGORY()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FNC_REPORT_INCOME_BY_CATEGORY_Result>("[TDHEntities].[FNC_REPORT_INCOME_BY_CATEGORY]()");
        }
    
        [DbFunction("TDHEntities", "FNC_REPORT_INCOME_BY_CATEGORY_BY_YEAR")]
        public virtual IQueryable<FNC_REPORT_INCOME_BY_CATEGORY_BY_YEAR_Result> FNC_REPORT_INCOME_BY_CATEGORY_BY_YEAR(Nullable<int> i_Year)
        {
            var i_YearParameter = i_Year.HasValue ?
                new ObjectParameter("I_Year", i_Year) :
                new ObjectParameter("I_Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FNC_REPORT_INCOME_BY_CATEGORY_BY_YEAR_Result>("[TDHEntities].[FNC_REPORT_INCOME_BY_CATEGORY_BY_YEAR](@I_Year)", i_YearParameter);
        }
    
        [DbFunction("TDHEntities", "FNC_REPORT_SUMMARY")]
        public virtual IQueryable<FNC_REPORT_SUMMARY_Result> FNC_REPORT_SUMMARY()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FNC_REPORT_SUMMARY_Result>("[TDHEntities].[FNC_REPORT_SUMMARY]()");
        }
    
        [DbFunction("TDHEntities", "FNC_REPORT_SUMMARY_BY_YEAR")]
        public virtual IQueryable<FNC_REPORT_SUMMARY_BY_YEAR_Result> FNC_REPORT_SUMMARY_BY_YEAR(Nullable<int> i_Year)
        {
            var i_YearParameter = i_Year.HasValue ?
                new ObjectParameter("I_Year", i_Year) :
                new ObjectParameter("I_Year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FNC_REPORT_SUMMARY_BY_YEAR_Result>("[TDHEntities].[FNC_REPORT_SUMMARY_BY_YEAR](@I_Year)", i_YearParameter);
        }
    
        [DbFunction("TDHEntities", "FNC_MN_GROUP_SETTING_GET_BY_GROUP")]
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
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<FNC_MN_GROUP_SETTING_GET_BY_GROUP_Result>("[TDHEntities].[FNC_MN_GROUP_SETTING_GET_BY_GROUP](@I_GroupID, @I_Year, @I_UserID)", i_GroupIDParameter, i_YearParameter, i_UserIDParameter);
        }
    }
}
