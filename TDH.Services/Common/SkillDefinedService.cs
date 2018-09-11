using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Common;
using Utils;

namespace TDH.Services.Common
{
    public class SkillDefinedService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services/Common/SkillDefinedService.cs";

        #endregion

        /// <summary>
        /// Get item by id and owner
        /// </summary>
        /// <param name="model">skill defined model</param>
        /// <returns>MoneyAccountModel. Throw exception if not found or get some error</returns>
        public SkillDefinedModel GetItemByID(SkillDefinedModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    CM_SKILL_DEFINDED _md = _context.CM_SKILL_DEFINDED.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _return = new SkillDefinedModel()
                    {
                        ID = _md.id,
                        Name = _md.name,
                        Ordering = _md.ordering,
                        Description = _md.description,
                        Point = _md.point
                    };
                    return _return;
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetItemByID", model.CreateBy, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get all item without deleted by skill id
        /// </summary>
        /// <param name="skillID">The skill identifier</param>
        /// <param name="userID">The user identifier</param>
        /// <returns></returns>
        public List<SkillDefinedModel> GetAll(Guid skillID, Guid userID)
        {
            try
            {
                List<SkillDefinedModel> _return = new List<SkillDefinedModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.CM_SKILL_DEFINDED
                                 where !m.deleted && m.publish && m.created_by == userID && m.skill_id == skillID
                                 orderby m.created_date ascending
                                 select new
                                 {
                                     m.id,
                                     m.name,
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new SkillDefinedModel() { ID = item.id, Name = item.name });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Skill defined model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(SkillDefinedModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    CM_SKILL_DEFINDED _md = new CM_SKILL_DEFINDED();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.CM_SKILL_DEFINDED.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new FieldAccessException();
                        }
                    }
                    _md.skill_id = model.SkillID;
                    _md.name = model.Name;
                    _md.ordering = model.Ordering;
                    _md.description = model.Description;
                    _md.publish = true;
                    //Create or edit, only change the name and type
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        _context.CM_SKILL_DEFINDED.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        _context.CM_SKILL_DEFINDED.Attach(_md);
                        _context.Entry(_md).State = EntityState.Modified;
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                throw new ApplicationException();
            }
            if (model.Insert)
            {
                Notifier.Notification(model.CreateBy, Message.InsertSuccess, Notifier.TYPE.Success);
            }
            else
            {
                Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            }
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">Skill defined model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(SkillDefinedModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            CM_SKILL_DEFINDED _md = _context.CM_SKILL_DEFINDED.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                           
                            _context.CM_SKILL_DEFINDED.Remove(_md);
                            _context.Entry(_md).State = EntityState.Deleted;

                            _context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model.CreateBy, Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

    }
}
