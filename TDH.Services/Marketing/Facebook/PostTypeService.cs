using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.Marketing.Facebook;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.Marketing.Facebook
{
    /// <summary>
    /// Post type service
    /// </summary>
    public class PostTypeService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Marketing/PostService.cs";

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
                DataTableResponse<PostTypeModel> _itemResponse = new DataTableResponse<PostTypeModel>();
                //List of data
                List<PostTypeModel> _list = new List<PostTypeModel>();
                using (var context = new TDHEntities())
                {
                    var _lData = context.FB_POST_TYPE.Where(m => !m.deleted)
                                                .OrderByDescending(m => m.name)
                                                .Select(m => new
                                                {
                                                    m.code,
                                                    m.name,
                                                    m.on_group,
                                                    m.on_fanpage,
                                                    m.on_profile
                                                }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.code.ToLower().Contains(searchValue) ||
                                              m.name.ToLower().Contains(searchValue)).ToList();
                    }

                    foreach (var item in _lData)
                    {
                        _list.Add(new PostTypeModel()
                        {
                            Code = item.code,
                            Name = item.name,
                            OnFanpage = item.on_fanpage,
                            OnGroup = item.on_group,
                            OnProfile = item.on_profile
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<PostTypeModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Code":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Code) : _sortList.Sort(col.Dir, m => m.Code);
                                    break;
                                case "Name":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
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
        /// <returns></returns>
        public List<PostTypeModel> GetAll(Guid userID)
        {
            try
            {
                List<PostTypeModel> _return = new List<PostTypeModel>();
                using (var context = new TDHEntities())
                {
                    var _list = context.FB_POST_TYPE.Where(m => !m.deleted).OrderByDescending(m => m.name).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new PostTypeModel() { Code = item.code, Name = item.name });
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
        /// <param name="model"></param>
        /// <returns>PostTypeModel. Throw exception if not found or get some error</returns>
        public PostTypeModel GetItemByID(PostTypeModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_POST_TYPE _md = context.FB_POST_TYPE.FirstOrDefault(m => m.code == model.Code && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new PostTypeModel()
                    {
                        Code = _md.code,
                        Name = _md.name,
                        OnFanpage = _md.on_fanpage,
                        OnProfile = _md.on_profile,
                        OnGroup = _md.on_group,
                        Ordering = _md.ordering,
                        Publish = _md.publish,
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
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(PostTypeModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_POST_TYPE _md = new FB_POST_TYPE();
                    if (model.Insert)
                    {
                        _md.code = "none";
                    }
                    else
                    {
                        _md = context.FB_POST_TYPE.FirstOrDefault(m => m.code == model.Code && !m.deleted);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
                        }
                    }
                    _md.name = model.Name;
                    _md.on_fanpage = model.OnFanpage;
                    _md.on_group = model.OnGroup;
                    _md.on_profile = model.OnProfile;
                    _md.notes = model.Notes;
                    _md.publish = model.Publish;
                    _md.ordering = model.Ordering;
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        context.FB_POST_TYPE.Add(_md);
                        context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        context.FB_POST_TYPE.Attach(_md);
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
        public ResponseStatusCodeHelper Delete(PostTypeModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_POST_TYPE _md = context.FB_POST_TYPE.FirstOrDefault(m => m.code == model.Code && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.deleted_by = model.DeleteBy;
                    _md.deleted_date = DateTime.Now;
                    context.FB_POST_TYPE.Attach(_md);
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
