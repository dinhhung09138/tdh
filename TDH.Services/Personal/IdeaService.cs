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
    /// <summary>
    /// Idea service
    /// </summary>
    public class IdeaService
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
        /// <param name="request">Jquery datatable request</param>
        /// <param name="userID">The user id</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> List(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<IdeaModel> _itemResponse = new DataTableResponse<IdeaModel>();
                //List of data
                List<IdeaModel> _list = new List<IdeaModel>();
                using (var context = new TDHEntities())
                {
                    var _lData = context.PN_IDEA.Where(m => !m.deleted && m.created_by == userID)
                                                .OrderByDescending(m => m.created_date)
                                                .Select(m => new
                                                {
                                                    m.id,
                                                    m.title,
                                                    m.created_date
                                                }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue) ||
                                              m.created_date.ToString().Contains(searchValue)).ToList();
                    }
                    int _count = 0;
                    foreach (var item in _lData)
                    {
                        _count = context.PN_TARGET.Count(m => m.idea_id == item.id && !m.deleted && m.created_by == userID);
                        _list.Add(new IdeaModel()
                        {
                            ID = item.id,
                            Title = item.title,
                            CreateDate = item.created_date,
                            CreateDateString = item.created_date.DateToString(),
                            Targets = _count.NumberToString()
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<IdeaModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Title":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Title) : _sortList.Sort(col.Dir, m => m.Title);
                                    break;
                                case "CreateDateString":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.CreateDate) : _sortList.Sort(col.Dir, m => m.CreateDate);
                                    break;
                                case "Targets":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Targets) : _sortList.Sort(col.Dir, m => m.Targets);
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
        /// <returns>List<IdeaModel></returns>
        public List<IdeaModel> GetAll(Guid userID)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    return context.PN_IDEA
                                    .Where(m => !m.deleted && m.created_by == userID)
                                    .OrderByDescending(m => m.created_date)
                                    .Select(m => new IdeaModel() { ID = m.id, Title = m.title })
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
        /// <param name="model">Idea model</param>
        /// <returns>IdeaModel</returns>
        public IdeaModel GetItemByID(IdeaModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    PN_IDEA _md = context.PN_IDEA.FirstOrDefault(m => m.id == model.ID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new IdeaModel()
                    {
                        ID = _md.id,
                        Title = _md.title,
                        Content = _md.content
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
        /// <param name="model">Idea model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(IdeaModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    PN_IDEA _md = new PN_IDEA();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = context.PN_IDEA.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
                        }
                    }
                    _md.title = model.Title;
                    _md.content = model.Content;
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        context.PN_IDEA.Add(_md);
                        context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        context.PN_IDEA.Attach(_md);
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
        /// <param name="model">Idea model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(IdeaModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    PN_IDEA _md = context.PN_IDEA.FirstOrDefault(m => m.id == model.ID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.deleted_by = model.DeleteBy;
                    _md.deleted_date = DateTime.Now;
                    context.PN_IDEA.Attach(_md);
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

        /// <summary>
        /// Check Delete item
        /// </summary>
        /// <param name="model">Idea model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(IdeaModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    PN_TARGET _md = context.PN_TARGET.FirstOrDefault(m => m.idea_id == model.ID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        return ResponseStatusCodeHelper.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "CheckDelete", model.CreateBy, ex);
            }
            return ResponseStatusCodeHelper.NG;
        }

    }
}
