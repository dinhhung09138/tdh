using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.Personal;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.Personal
{
    /// <summary>
    /// Education service
    /// </summary>
    public class EducationService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Personal/EducationService.cs";

        #endregion

        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request">Jquery datatable request</param>
        /// <param name="userID">The user identifier</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> List(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<EducationModel> _itemResponse = new DataTableResponse<EducationModel>();
                //List of data
                List<EducationModel> _list = new List<EducationModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.PN_EDUCATION
                                  join t in _context.PN_EDUCATION_TYPE on m.education_type equals t.code
                                  where !m.deleted && m.created_by == userID &&
                                        m.education_type == (request.Parameter1 == "" ? m.education_type : request.Parameter1)
                                  orderby m.date descending
                                  select new
                                  {
                                      m.id,
                                      m.name,
                                      m.school,
                                      m.duration,
                                      m.is_plan,
                                      m.is_finish,
                                      m.is_cancel,
                                      type_name = t.name
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.name.ToLower().Contains(searchValue) ||
                                                   m.type_name.ToLower().Contains(searchValue) ||
                                                   m.school.ToLower().Contains(searchValue) ||
                                                   m.duration.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in _lData)
                    {
                        _list.Add(new EducationModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            School = item.school,
                            Duration = item.duration,
                            IsPlan = item.is_plan,
                            IsFinish = item.is_finish,
                            IsCancel = item.is_cancel,
                            TypeName = item.type_name
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<EducationModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Name":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
                                    break;
                                case "School":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.School) : _sortList.Sort(col.Dir, m => m.School);
                                    break;
                                case "Duration":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Duration) : _sortList.Sort(col.Dir, m => m.Duration);
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
                throw new ServiceException(FILE_NAME, "List", userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<EducationModel></returns>
        public List<EducationModel> GetAll(Guid userID)
        {
            try
            {
                List<EducationModel> _return = new List<EducationModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.PN_EDUCATION
                                 where !m.deleted && m.publish && m.created_by == userID
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.id,
                                     m.name,
                                     m.description
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new EducationModel() { ID = item.id, Name = item.name, Description = item.description });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
            }
        }
        
        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>MoneyEducationModel. Throw exception if not found or get some error</returns>
        public EducationModel GetItemByID(EducationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_EDUCATION _md = _context.PN_EDUCATION.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new EducationModel()
                    {
                        ID = _md.id,
                        Name = _md.name,
                        School = _md.school,
                        Duration = _md.duration,
                        Date = _md.date,
                        DateString = _md.date.DateToString(),
                        Description = _md.description,
                        Content = _md.content,
                        TypeCode = _md.education_type,
                        IsPlan = _md.is_plan,
                        IsFinish = _md.is_finish,
                        IsCancel = _md.is_cancel,
                        Ordering = _md.ordering,
                        Publish = _md.publish
                    };
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetItemByID", model.CreateBy, ex);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(EducationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_EDUCATION _md = new PN_EDUCATION();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.PN_EDUCATION.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
                        }
                    }
                    _md.name = model.Name;
                    _md.school = model.School;
                    _md.duration = model.Duration;
                    _md.description = model.Description;
                    _md.content = model.Content;
                    _md.date = model.Date;
                    _md.is_plan = model.IsPlan;
                    _md.is_finish = model.IsFinish;
                    _md.is_cancel = model.IsCancel;
                    _md.education_type = model.TypeCode;
                    _md.ordering = model.Ordering;
                    _md.publish = model.Publish;
                    //Setting value don't allow change when create or edit
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        _context.PN_EDUCATION.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        _context.PN_EDUCATION.Attach(_md);
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
                throw new ServiceException(FILE_NAME, "Save", model.CreateBy, ex);
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
        public ResponseStatusCodeHelper Publish(EducationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_EDUCATION _md = _context.PN_EDUCATION.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Publish", model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.updated_by = model.UpdateBy;
                    _md.updated_date = DateTime.Now;
                    _context.PN_EDUCATION.Attach(_md);
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
                throw new ServiceException(FILE_NAME, "Publish", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(EducationModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_EDUCATION _md = _context.PN_EDUCATION.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.deleted_by = model.DeleteBy;
                    _md.deleted_date = DateTime.Now;
                    _context.PN_EDUCATION.Attach(_md);
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
                throw new ServiceException(FILE_NAME, "Delete", model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

    }
}
