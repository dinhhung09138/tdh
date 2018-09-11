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
    public class SkillGroupService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services/Common/SkillGroupService.cs";

        #endregion

        /// <summary>
        /// Get item by id and owner
        /// </summary>
        /// <param name="model">Group model</param>
        /// <returns>MoneyAccountModel. Throw exception if not found or get some error</returns>
        public SkillGroupModel GetItemByID(SkillGroupModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    CM_SKILL_GROUP _md = _context.CM_SKILL_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _return = new SkillGroupModel()
                    {
                        ID = _md.id,
                        Name = _md.name,
                        Ordering = _md.ordering,
                        Notes = _md.notes
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
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns></returns>
        public List<SkillGroupModel> GetAll(Guid userID)
        {
            try
            {
                List<SkillGroupModel> _return = new List<SkillGroupModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.CM_SKILL_GROUP
                                 where !m.deleted && m.publish && m.created_by == userID
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.id,
                                     m.name,
                                     count = (_context.CM_SKILL.Count(s => s.group_id == m.id && !s.deleted && s.created_by == userID))
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new SkillGroupModel() { ID = item.id, Name = item.name, CountSkill = item.count });
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
        /// <param name="model">Group model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(SkillGroupModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            CM_SKILL_GROUP _md = new CM_SKILL_GROUP();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                            }
                            else
                            {
                                _md = _context.CM_SKILL_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
                                }
                            }
                            _md.name = model.Name;
                            _md.ordering = model.Ordering;
                            _md.notes = model.Notes;
                            _md.publish = true;
                            //Create or edit, only change the name and type
                            if (model.Insert)
                            {
                                _md.created_by = model.CreateBy;
                                _md.created_date = DateTime.Now;
                                _context.CM_SKILL_GROUP.Add(_md);
                                _context.Entry(_md).State = EntityState.Added;
                            }
                            else
                            {
                                _md.updated_by = model.UpdateBy;
                                _md.updated_date = DateTime.Now;
                                _context.CM_SKILL_GROUP.Attach(_md);
                                _context.Entry(_md).State = EntityState.Modified;
                            }
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
        /// <param name="model">Group model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(SkillGroupModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    CM_SKILL_GROUP _md = _context.CM_SKILL_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    _md.deleted = true;
                    _md.deleted_by = model.DeleteBy;
                    _md.deleted_date = DateTime.Now;
                    _context.CM_SKILL_GROUP.Attach(_md);
                    _context.Entry(_md).State = EntityState.Modified;
                    _context.SaveChanges();
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

        /// <summary>
        /// Check Delete
        /// </summary>
        /// <param name="model">Group model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(SkillGroupModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    CM_SKILL_GROUP _md = _context.CM_SKILL_GROUP.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _skills = _context.CM_SKILL.FirstOrDefault(m => m.group_id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_skills != null)
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "CheckDelete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.OK;
        }

    }
}
