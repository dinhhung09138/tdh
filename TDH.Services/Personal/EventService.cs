using System;
using System.Collections.Generic;
using System.Linq;
using Utils.JqueryDatatable;
using Utils;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Personal;
using System.Data.Entity;
using TDH.Common.UserException;

namespace TDH.Services.Personal
{
    /// <summary>
    /// Event service
    /// </summary>
    public class EventService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Personal/EventService.cs";

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
                DataTableResponse<EventModel> _itemResponse = new DataTableResponse<EventModel>();
                //List of data
                List<EventModel> _list = new List<EventModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = (from m in _context.PN_EVENT
                                  join t in _context.PN_EVENT_TYPE on m.event_type equals t.code
                                  where !m.deleted && m.created_by == userID && m.created_by == userID &&
                                        m.event_type == (request.Parameter1 == "" ? m.event_type : request.Parameter1)
                                  orderby m.date descending
                                  select new
                                  {
                                      m.id,
                                      m.title,
                                      m.duration,
                                      m.is_plan,
                                      m.is_finish,
                                      m.is_cancel,
                                      t.name
                                  }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue) ||
                                                   m.duration.ToLower().Contains(searchValue) ||
                                                   m.name.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in _lData)
                    {
                        _list.Add(new EventModel()
                        {
                            ID = item.id,
                            Title = item.title,
                            Duration = item.duration,
                            IsPlan = item.is_plan,
                            IsFinish = item.is_finish,
                            IsCancel = item.is_cancel,
                            TypeName = item.name
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<EventModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Title":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Title) : _sortList.Sort(col.Dir, m => m.Title);
                                    break;
                                case "Duration":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Duration) : _sortList.Sort(col.Dir, m => m.Duration);
                                    break;
                                case "TypeName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.TypeName) : _sortList.Sort(col.Dir, m => m.TypeName);
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
        /// <returns>List<EventModel></returns>
        public List<EventModel> GetAll(Guid userID)
        {
            try
            {
                List<EventModel> _return = new List<EventModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.PN_EVENT
                                 where !m.deleted && m.publish && m.created_by == userID
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.id,
                                     m.title,
                                     m.description
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new EventModel() { ID = item.id, Title = item.title, Description = item.description });
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
        /// <returns>MoneyEventModel. Throw exception if not found or get some error</returns>
        public EventModel GetItemByID(EventModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_EVENT _md = _context.PN_EVENT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new EventModel()
                    {
                        ID = _md.id,
                        Title = _md.title,
                        Duration = _md.duration,
                        Date = _md.date,
                        DateString = _md.date.DateToString(),
                        Description = _md.description,
                        Content = _md.content,
                        TypeCode = _md.event_type,
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
        public ResponseStatusCodeHelper Save(EventModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_EVENT _md = new PN_EVENT();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = _context.PN_EVENT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
                        }
                    }
                    _md.title = model.Title;
                    _md.duration = model.Duration;
                    _md.description = model.Description;
                    _md.content = model.Content;
                    _md.date = model.Date;
                    _md.is_plan = model.IsPlan;
                    _md.is_finish = model.IsFinish;
                    _md.is_cancel = model.IsCancel;
                    _md.event_type = model.TypeCode;
                    _md.ordering = model.Ordering;
                    _md.publish = model.Publish;
                    //Setting value don't allow change when create or edit
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        _context.PN_EVENT.Add(_md);
                        _context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        _context.PN_EVENT.Attach(_md);
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
        public ResponseStatusCodeHelper Publish(EventModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {

                    PN_EVENT _md = _context.PN_EVENT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Publish", model.CreateBy);
                    }
                    _md.publish = model.Publish;
                    _md.updated_by = model.UpdateBy;
                    _md.updated_date = DateTime.Now;
                    _context.PN_EVENT.Attach(_md);
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
        public ResponseStatusCodeHelper Delete(EventModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    PN_EVENT _md = _context.PN_EVENT.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.deleted_by = model.DeleteBy;
                    _md.deleted_date = DateTime.Now;
                    _context.PN_EVENT.Attach(_md);
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
