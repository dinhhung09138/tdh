using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.Personal;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.Personal
{
    /// <summary>
    /// Skill service
    /// </summary>
    public class SkillService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Personal/SkillService.cs";

        #endregion

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
                short _level = 0;
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
                            _level = 0;
                            //
                            var _df = (from m in _context.PN_SKILL_DEFINDED
                                      join s in _context.PN_SKILL on m.skill_id equals s.skill_id
                                      where m.defined_id == d.id && s.created_by == userID
                                      select new
                                      {
                                          m.point
                                      }).FirstOrDefault();
                            //
                            if(_df != null)
                            {
                                _level = _df.point;
                            }
                            md.Defined.Add(new SkillDefinedModel() { ID = d.id, SkillID = item.id, Name = d.name, Level = _level });
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
        /// Save skill defined level
        /// </summary>
        /// <param name="model">Skill defined model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper SaveSkillDefined(SkillDefinedModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _skill = _context.CM_SKILL.FirstOrDefault(m => m.id == model.SkillID);
                    if(_skill == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    var _pnSkill = _context.PN_SKILL.FirstOrDefault(m => m.skill_id == model.SkillID && m.created_by == model.CreateBy);
                    if(_pnSkill == null)
                    {
                        _pnSkill = new PN_SKILL()
                        {
                            skill_id = model.SkillID,
                            level = 100,
                            ordering = 1,
                            publish = true,
                            created_by = model.CreateBy,
                            created_date = DateTime.Now
                        };
                        _context.PN_SKILL.Add(_pnSkill);
                        _context.Entry(_pnSkill).State = EntityState.Added;
                        _context.SaveChanges();
                    }
                    var _defined = _context.PN_SKILL_DEFINDED.FirstOrDefault(m => m.defined_id == model.ID && m.skill_id == model.SkillID);
                    if(_defined == null)
                    {
                        _defined = new PN_SKILL_DEFINDED()
                        {
                            defined_id = model.ID,
                            skill_id = model.SkillID,
                            point = model.Level
                        };
                        _context.PN_SKILL_DEFINDED.Add(_defined);
                        _context.Entry(_defined).State = EntityState.Added;
                    }
                    else
                    {
                        _defined.point = model.Level;
                        _context.PN_SKILL_DEFINDED.Attach(_defined);
                        _context.Entry(_defined).State = EntityState.Modified;
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
        /// Get all skill item by user
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns></returns>
        public List<SkillDefinedModel> MySkill(Guid userID)
        {
            try
            {
                short _level = 0;
                List<SkillDefinedModel> _return = new List<SkillDefinedModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from pd in _context.PN_SKILL_DEFINDED
                                 join ps in _context.PN_SKILL on pd.skill_id equals ps.skill_id
                                 join cd in _context.CM_SKILL_DEFINDED on pd.defined_id equals cd.id
                                 join cs in _context.CM_SKILL on cd.skill_id equals cs.id
                                 where !cs.deleted && cs.publish && ps.created_by == userID && pd.point > 0
                                 orderby ps.skill_id, cs.ordering descending
                                 select new
                                 {
                                    pd.defined_id,
                                    pd.skill_id,
                                    cd.name,
                                    pd.point
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new SkillDefinedModel() { ID = item.defined_id, SkillID = item.skill_id, Name = item.name, Level = item.point });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
        }


    }
}
