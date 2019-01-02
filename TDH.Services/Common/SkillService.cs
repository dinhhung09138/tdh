using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.Common;
using Utils;

namespace TDH.Services.Common
{
    public class SkillService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Common/SkillService.cs";

        #endregion

        /// <summary>
        /// Get item by id and owner
        /// </summary>
        /// <param name="model">skill model</param>
        /// <returns>MoneyAccountModel. Throw exception if not found or get some error</returns>
        public SkillModel GetItemByID(SkillModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    CM_SKILL _md = _context.CM_SKILL.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    var _return = new SkillModel()
                    {
                        ID = _md.id,
                        Name = _md.name,
                        Ordering = _md.ordering,
                        Notes = _md.notes
                    };
                    return _return;
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
            }
        }

        /// <summary>
        /// Get all item without deleted by group id
        /// </summary>
        /// <param name="groupID">The group identifier</param>
        /// <param name="userID">The user identifier</param>
        /// <returns></returns>
        public List<SkillModel> GetAll(Guid groupID, Guid userID)
        {
            try
            {
                List<SkillModel> _return = new List<SkillModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.CM_SKILL
                                 where !m.deleted && m.publish && m.created_by == userID && m.group_id == groupID
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.id,
                                     m.name,
                                     defined = (_context.CM_SKILL_DEFINDED.Where(s => s.skill_id == m.id && !s.deleted && s.created_by == userID).Select(s => new { s.id, s.name, s.skill_id }))
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        SkillModel md = new SkillModel() { ID = item.id, Name = item.name };
                        foreach (var d in item.defined)
                        {
                            md.Defined.Add(new SkillDefinedModel() { ID = d.id, Name = d.name });
                        }
                        _return.Add(md);
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Skill model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(SkillModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    CM_SKILL _md = new CM_SKILL();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.CM_SKILL.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                        }
                    }
                    _md.group_id = model.GroupID;
                    _md.name = model.Name;
                    _md.ordering = model.Ordering;
                    _md.notes = model.Notes;
                    _md.publish = true;
                    //Create or edit, only change the name and type
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        _context.CM_SKILL.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        _context.CM_SKILL.Attach(_md);
                        _context.Entry(_md).State = EntityState.Modified;
                    }
                    _context.SaveChanges();
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
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
        /// <param name="model">Skill model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(SkillModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    CM_SKILL _md = _context.CM_SKILL.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.deleted_by = model.DeleteBy;
                    _md.deleted_date = DateTime.Now;
                    _context.CM_SKILL.Attach(_md);
                    _context.Entry(_md).State = EntityState.Modified;

                    var _lPerson = _context.PN_SKILL.Where(m => m.skill_id == _md.id).ToList();
                    if (_lPerson.Count > 0)
                    {
                        _context.PN_SKILL.RemoveRange(_lPerson);
                    }

                    _context.SaveChanges();
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Check Delete
        /// </summary>
        /// <param name="model">Skill model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(SkillModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    CM_SKILL _md = _context.CM_SKILL.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    var _skills = _context.PN_SKILL.FirstOrDefault(m => m.skill_id == model.ID && !m.deleted);
                    if (_skills != null)
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
            }
            return ResponseStatusCodeHelper.OK;
        }

    }
}
