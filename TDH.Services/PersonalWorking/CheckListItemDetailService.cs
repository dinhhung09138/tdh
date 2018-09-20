using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.PersonalWorking;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.PersonalWorking
{
    /// <summary>
    /// Check list item detail service
    /// </summary>
    public class CheckListItemDetailService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.PersonalWorking/CheckListItemDetailService.cs";

        #endregion

        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userID">User id</param>
        /// <returns><string, object></returns>
        /// 
        public Dictionary<string, object> List(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<CheckListItemDetailModel> _itemResponse = new DataTableResponse<CheckListItemDetailModel>();
                //List of data
                List<CheckListItemDetailModel> _list = new List<CheckListItemDetailModel>();

                using (var context = new TDHEntities())
                {
                    var _lData = context.WK_CHECKLIST_ITEM_DETAIL.Where(m => !m.deleted).OrderByDescending(m => m.create_date).Select(m => new
                    {
                        m.id,
                        m.title,
                        m.check_id,
                        full_name = context.SYS_USER.FirstOrDefault(u => u.id == m.create_by).full_name,
                        m.create_date

                    }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.title.ToLower().Contains(searchValue) || m.full_name.ToLower().Contains(searchValue) || m.create_date.ToString().Contains(searchValue)).ToList();
                    }
                    int _count = 0;
                    foreach (var item in _lData)
                    {
                        _count = context.WK_CHECKLIST_ITEM.Count(m => m.id == item.id && !m.deleted);
                        _list.Add(new CheckListItemDetailModel()
                        {
                            ID = item.id,
                            CheckID = item.check_id,
                            title = item.title,
                            CreateDate = item.create_date,
                            updateDate = DateTime.Today

                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<CheckListItemDetailModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Title":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.title) : _sortList.Sort(col.Dir, m => m.title);
                                    break;
                                case "CreateDate":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.CreateDate) : _sortList.Sort(col.Dir, m => m.CreateDate);
                                    break;
                                case "UpdateDate":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.updateDate) : _sortList.Sort(col.Dir, m => m.updateDate);
                                    break;
                                case "DeleteDate":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.deleteDate) : _sortList.Sort(col.Dir, m => m.deleteDate);
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
        public List<CheckListItemDetailModel> GetAll(Guid userID)
        {
            try
            {
                List<CheckListItemDetailModel> _return = new List<CheckListItemDetailModel>();
                using (var context = new TDHEntities())
                {
                    var _list = context.WK_CHECKLIST_ITEM_DETAIL.Where(m => !m.deleted).OrderByDescending(m => m.create_date).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new CheckListItemDetailModel() { ID = item.id, title = item.title });
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
        /// <returns>CheckListItemDetailModel. Throw exception if not found or get some error</returns>
        public CheckListItemDetailModel GetItemByID(CheckListItemDetailModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    WK_CHECKLIST_ITEM_DETAIL _md = context.WK_CHECKLIST_ITEM_DETAIL.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new CheckListItemDetailModel()
                    {
                        ID = _md.id,
                        CheckID = _md.check_id,
                        title = _md.title,
                        CreateDate = _md.create_date,
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
        public ResponseStatusCodeHelper Save(CheckListItemDetailModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    WK_CHECKLIST_ITEM_DETAIL _md = new WK_CHECKLIST_ITEM_DETAIL();
                    if (model.Insert)
                    {
                        _md.id = Guid.NewGuid();
                    }
                    else
                    {
                        _md = context.WK_CHECKLIST_ITEM_DETAIL.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                        if (_md == null)
                        {
                            throw new DataAccessException(FILE_NAME, "Save", model.CreateBy);
                        }
                    }
                    _md.title = model.title;
                    _md.check_id = model.CheckID;
                    _md.update_date = model.updateDate;

                    if (model.Insert)
                    {
                        _md.create_by = model.CreateBy;
                        _md.create_date = DateTime.Now;
                        context.WK_CHECKLIST_ITEM_DETAIL.Add(_md);
                        context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.update_by = model.UpdateBy;
                        _md.update_date = DateTime.Now;
                        context.WK_CHECKLIST_ITEM_DETAIL.Attach(_md);
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
        public ResponseStatusCodeHelper Delete(CheckListItemDetailModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    WK_CHECKLIST_ITEM_DETAIL _md = context.WK_CHECKLIST_ITEM_DETAIL.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    _md.deleted = true;
                    _md.delete_by = model.DeleteBy;
                    _md.check_id = model.CheckID;
                    _md.delete_date = DateTime.Now;
                    context.WK_CHECKLIST_ITEM_DETAIL.Attach(_md);
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
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(CheckListItemDetailModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    WK_CHECKLIST_ITEM_DETAIL _md = context.WK_CHECKLIST_ITEM_DETAIL.FirstOrDefault(m => m.id == model.ID && !m.deleted);
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
