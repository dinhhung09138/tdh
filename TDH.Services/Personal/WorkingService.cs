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
    /// Woring service
    /// </summary>
    public class WorkingService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Personal/WorkingService.cs";

        #endregion

        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request">Jquery datatable request</param>
        /// <param name="userID">The user identifier</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> ListProject(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<ProjectModel> _itemResponse = new DataTableResponse<ProjectModel>();
                //List of data
                List<ProjectModel> _list = new List<ProjectModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.PN_WORKING_PROJECT
                                  where !m.deleted && m.created_by == userID && m.created_by == userID
                                  orderby m.ordering descending
                                  select new
                                  {
                                      m.id,
                                      m.name,
                                      m.description,
                                      m.during_time,
                                      m.image,
                                      m.ordering,
                                      m.publish,
                                      m.is_hot,
                                      m.is_other
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.name.ToLower().Contains(searchValue) ||
                                                   m.description.ToLower().Contains(searchValue) ||
                                                   m.during_time.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in _lData)
                    {
                        _list.Add(new ProjectModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            DuringTime = item.during_time,
                            Image = item.image,
                            Description = item.description,
                            Ordering = item.ordering,
                            Publish = item.publish,
                            IsHot = item.is_hot,
                            IsOther = item.is_other
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<ProjectModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Name":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
                                    break;
                                case "DuringTime":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.DuringTime) : _sortList.Sort(col.Dir, m => m.DuringTime);
                                    break;
                                case "Image":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Image) : _sortList.Sort(col.Dir, m => m.Image);
                                    break;
                                case "Description":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Description) : _sortList.Sort(col.Dir, m => m.Description);
                                    break;
                            }
                        }
                        _itemResponse.data = _sortList.Skip(request.start).Take(request.length).ToList();
                    }
                    else
                    {
                        _itemResponse.data = _list.Skip(request.start).Take(request.length).ToList();
                    }
                    _return.Add(DatatableCommonSetting.Response.DATA, _itemResponse);
                }
                _return.Add(DatatableCommonSetting.Response.STATUS, ResponseStatusCodeHelper.OK);
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<EventModel></returns>
        public List<ProjectModel> GetAllProject(Guid userID)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return (from m in _context.PN_WORKING_PROJECT
                            where !m.deleted && m.publish && m.created_by == userID
                            orderby m.ordering descending
                            select new ProjectModel()
                            {
                                ID = m.id,
                                Name = m.name,
                                Image = m.image,
                                DuringTime = m.during_time,
                                Description = m.description,
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>MoneyEventModel</returns>
        public ProjectModel GetProjectItemByID(ProjectModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_WORKING_PROJECT _md = _context.PN_WORKING_PROJECT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    return new ProjectModel()
                    {
                        ID = _md.id,
                        Name = _md.name,
                        DuringTime = _md.during_time,
                        Image = _md.image,
                        Description = _md.description,
                        Content = _md.content,
                        Ordering = _md.ordering,
                        Publish = _md.publish,
                        IsHot = _md.is_hot,
                        IsOther = _md.is_other
                    };
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
        /// Save
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper SaveProject(ProjectModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_WORKING_PROJECT _md = new PN_WORKING_PROJECT();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.PN_WORKING_PROJECT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                        }
                    }
                    _md.name = model.Name;
                    _md.during_time = model.DuringTime;
                    _md.description = model.Description;
                    _md.content = model.Content;
                    _md.image = model.Image;
                    _md.publish = model.Publish;
                    _md.ordering = model.Ordering;
                    _md.is_hot = model.IsHot;
                    _md.is_other = model.IsOther;
                    //Setting value don't allow change when create or edit
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        _context.PN_WORKING_PROJECT.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        _context.PN_WORKING_PROJECT.Attach(_md);
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
        /// Publish
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper PublishProject(ProjectModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {

                    PN_WORKING_PROJECT _md = _context.PN_WORKING_PROJECT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.updated_by = model.UpdateBy;
                    _md.updated_date = DateTime.Now;
                    _context.PN_WORKING_PROJECT.Attach(_md);
                    _context.Entry(_md).State = EntityState.Modified;
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
            Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper DeleteProject(ProjectModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_WORKING_PROJECT _md = _context.PN_WORKING_PROJECT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.deleted_by = model.DeleteBy;
                    _md.deleted_date = DateTime.Now;
                    _context.PN_WORKING_PROJECT.Attach(_md);
                    _context.Entry(_md).State = EntityState.Modified;
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
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request">Jquery datatable request</param>
        /// <param name="userID">The user identifier</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> ListExperience(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<ExperienceModel> _itemResponse = new DataTableResponse<ExperienceModel>();
                //List of data
                List<ExperienceModel> _list = new List<ExperienceModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.PN_WORKING_EXPERIENCE
                                  where !m.deleted && m.created_by == userID && m.created_by == userID
                                  orderby m.ordering descending
                                  select new
                                  {
                                      m.id,
                                      m.position,
                                      m.company_name,
                                      m.during_time,
                                      m.description,
                                      m.ordering
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.position.ToLower().Contains(searchValue) ||
                                                   m.company_name.ToLower().Contains(searchValue) ||
                                                   m.description.ToLower().Contains(searchValue) ||
                                                   m.during_time.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in _lData)
                    {
                        _list.Add(new ExperienceModel()
                        {
                            ID = item.id,
                            Position = item.position,
                            CompanyName = item.company_name,
                            DuringTime = item.during_time,
                            Description = item.description,
                            Ordering = item.ordering
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<ExperienceModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Position":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Position) : _sortList.Sort(col.Dir, m => m.Position);
                                    break;
                                case "CompanyName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.CompanyName) : _sortList.Sort(col.Dir, m => m.CompanyName);
                                    break;
                                case "DuringTime":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.DuringTime) : _sortList.Sort(col.Dir, m => m.DuringTime);
                                    break;
                                case "Description":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Description) : _sortList.Sort(col.Dir, m => m.Description);
                                    break;
                            }
                        }
                        _itemResponse.data = _sortList.Skip(request.start).Take(request.length).ToList();
                    }
                    else
                    {
                        _itemResponse.data = _list.Skip(request.start).Take(request.length).ToList();
                    }
                    _return.Add(DatatableCommonSetting.Response.DATA, _itemResponse);
                }
                _return.Add(DatatableCommonSetting.Response.STATUS, ResponseStatusCodeHelper.OK);
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<EventModel></returns>
        public List<ExperienceModel> GetAllExperience(Guid userID)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    return (from m in _context.PN_WORKING_EXPERIENCE
                            where !m.deleted && m.publish && m.created_by == userID
                            orderby m.ordering descending
                            select new ExperienceModel()
                            {
                                ID = m.id,
                                Position = m.position,
                                DuringTime = m.during_time,
                                CompanyName = m.company_name,
                                Description = m.description
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>MoneyEventModel</returns>
        public ExperienceModel GetExperienceItemByID(ExperienceModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_WORKING_EXPERIENCE _md = _context.PN_WORKING_EXPERIENCE.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    return new ExperienceModel()
                    {
                        ID = _md.id,
                        Position = _md.position,
                        DuringTime = _md.during_time,
                        CompanyName = _md.company_name,
                        Description = _md.description,
                        Publish = _md.publish,
                        Ordering = _md.ordering
                    };
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
        /// Save
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper SaveExperience(ExperienceModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_WORKING_EXPERIENCE _md = new PN_WORKING_EXPERIENCE();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.PN_WORKING_EXPERIENCE.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                        }
                    }
                    _md.company_name = model.CompanyName;
                    _md.during_time = model.DuringTime;
                    _md.position = model.Position;
                    _md.description = model.Description;
                    _md.publish = model.Publish;
                    _md.ordering = model.Ordering;
                    //Setting value don't allow change when create or edit
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        _context.PN_WORKING_EXPERIENCE.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        _context.PN_WORKING_EXPERIENCE.Attach(_md);
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
        /// Publish
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper PublishExperience(ExperienceModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {

                    PN_WORKING_EXPERIENCE _md = _context.PN_WORKING_EXPERIENCE.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.updated_by = model.UpdateBy;
                    _md.updated_date = DateTime.Now;
                    _context.PN_WORKING_EXPERIENCE.Attach(_md);
                    _context.Entry(_md).State = EntityState.Modified;
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
            Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper DeleteExperience(ExperienceModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_WORKING_EXPERIENCE _md = _context.PN_WORKING_EXPERIENCE.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.deleted_by = model.DeleteBy;
                    _md.deleted_date = DateTime.Now;
                    _context.PN_WORKING_EXPERIENCE.Attach(_md);
                    _context.Entry(_md).State = EntityState.Modified;
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

    }
}
