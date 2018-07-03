using System;
using System.Collections.Generic;
using System.Linq;
using TDH.Models;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Utils;
using TDH.Areas.Administrator.Common;

namespace TDH.Areas.Administrator.Services
{
    public class TargetService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/TargetService.cs";

        #endregion

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <returns></returns>
        public List<TargetModel> GetAllDisplayOnOverview(Guid userID)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = (from m in context.TARGETs
                                 where !m.deleted
                                 orderby m.create_date descending
                                 select new TargetModel()
                                 {
                                     ID = m.id,
                                     ParentID = m.parent_id,
                                     Title = m.title,
                                     Done = m.done,
                                     EstimateDate = m.estimate_date,
                                     TaskCount = m.task_count,
                                     TaskDone = context.TARGET_TASK.Count(t => t.target_id == m.id && t.done),
                                     Level = m.level
                                 }).ToList();
                    return _list;
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetAllDisplayOnOverview", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CategoryModel. Throw exception if not found or get some error</returns>
        public TargetModel GetItemByID(TargetModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    TARGET _md = context.TARGETs.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    return new TargetModel()
                    {
                        ID = _md.id,
                        Title = _md.title,
                        ParentID = _md.parent_id,
                        Result = _md.result,
                        Done = _md.done,
                        EstimateDate = _md.estimate_date,
                        FinishDate = _md.finish_date,
                        TaskCount = _md.task_count,
                        Level = _md.level
                    };
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetItemByID", model.CreateBy, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(TargetModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            TARGET _md = new TARGET();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                                _md.parent_id = model.ParentID;
                                _md.level = model.Level;
                                _md.done = false;
                            }
                            else
                            {
                                _md = context.TARGETs.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
                                }
                            }
                            _md.title = model.Title;
                            _md.estimate_date = model.EstimateDate;                            
                            if (model.Insert)
                            {
                                _md.create_by = model.CreateBy;
                                _md.create_date = DateTime.Now;
                                context.TARGETs.Add(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _md.update_by = model.UpdateBy;
                                _md.update_date = DateTime.Now;
                                context.TARGETs.Attach(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                            }
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                throw new ApplicationException();
            }
            if (model.Insert)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.InsertSuccess, Notifier.TYPE.Success);
            }
            else
            {
                Notifier.Notification(model.CreateBy, Resources.Message.UpdateSuccess, Notifier.TYPE.Success);
            }
            return ResponseStatusCodeHelper.Success;
        }

    }
}