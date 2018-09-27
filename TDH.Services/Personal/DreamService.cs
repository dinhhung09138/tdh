using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Utils.JqueryDatatable;
using Utils;
using TDH.Common;
using TDH.Model.Personal;
using TDH.DataAccess;
using TDH.Common.UserException;

namespace TDH.Services.Personal
{
    public class DreamService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Personal/IdeaService.cs";

        #endregion

        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userID">User id</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> List(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<DreamModel> _itemResponse = new DataTableResponse<DreamModel>();
                //List of data
                List<DreamModel> _list = new List<DreamModel>();
                using (var context = new TDHEntities())
                {
                    var _lData = context.PN_DREAM.Where(m => !m.deleted && m.created_by == userID)
                                                .OrderByDescending(m => m.ordering)
                                                .Select(m => new
                                                {
                                                    m.id,
                                                    m.title,
                                                    m.finish,
                                                    m.ordering,
                                                    m.finish_time
                                                }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue)).ToList();
                    }

                    foreach (var item in _lData)
                    {
                        _list.Add(new DreamModel()
                        {
                            ID = item.id,
                            Title = item.title,
                            Finish = item.finish,
                            Ordering = item.ordering,
                            FinishTime = item.finish_time,
                            FinishTimeString = item.finish_time.HasValue ? item.finish_time.Value.DateToString("dd/MM/yyyy") : ""
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<DreamModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Title":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Title) : _sortList.Sort(col.Dir, m => m.Title);
                                    break;
                                case "Ordering":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Ordering) : _sortList.Sort(col.Dir, m => m.Ordering);
                                    break;
                                case "FinishTimeString":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.FinishTimeString) : _sortList.Sort(col.Dir, m => m.FinishTimeString);
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
        /// <returns>List<DreamModel></returns>
        public List<DreamModel> GetAll(Guid userID)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    return context.PN_DREAM
                                .Where(m => !m.deleted && m.created_by == userID)
                                .OrderByDescending(m => m.created_date)
                                .Select(m => new DreamModel() { ID = m.id, Title = m.title })
                                .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">Dream model</param>
        /// <returns>DreamModel</returns>
        public DreamModel GetItemByID(DreamModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    PN_DREAM _md = context.PN_DREAM.FirstOrDefault(m => m.id == model.ID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new DreamModel()
                    {
                        ID = _md.id,
                        Title = _md.title,
                        Finish = _md.finish,
                        FinishTime = _md.finish_time,
                        FinishTimeString = _md.finish_time.HasValue ? _md.finish_time.Value.DateToString("dd/MM/yyyy") : "",
                        Notes = _md.notes
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
        /// <param name="model">Dream model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(DreamModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    PN_DREAM _md = new PN_DREAM();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = context.PN_DREAM.FirstOrDefault(m => m.id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
                        }
                    }
                    _md.title = model.Title;
                    _md.finish = model.Finish;
                    _md.finish_time = model.FinishTime;
                    _md.notes = model.Notes;
                    _md.ordering = model.Ordering;
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        context.PN_DREAM.Add(_md);
                        context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        context.PN_DREAM.Attach(_md);
                        context.Entry(_md).State = EntityState.Modified;
                    }
                    context.SaveChanges();
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
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(DreamModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    PN_DREAM _md = context.PN_DREAM.FirstOrDefault(m => m.id == model.ID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.deleted_by = model.DeleteBy;
                    _md.deleted_date = DateTime.Now;
                    context.PN_DREAM.Attach(_md);
                    context.Entry(_md).State = EntityState.Modified;
                    context.SaveChanges();
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
