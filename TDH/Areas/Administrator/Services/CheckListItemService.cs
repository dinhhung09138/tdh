using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDH.Areas.Administrator.Common;
using Utils.JqueryDatatable;
using TDH.Areas.Administrator.Models;
using Utils;
using TDH.Models;

namespace TDH.Areas.Administrator.Services
{
    public class CheckListItemService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Administrator/Services/CheckListItemService.cs";

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
                DataTableResponse<CheckListItemModel> _itemResponse = new DataTableResponse<CheckListItemModel>();
                //List of data
                List<CheckListItemModel> _list = new List<CheckListItemModel>();

                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _lData = context.WK_CHECKLIST_ITEM.Where(m => !m.deleted).OrderByDescending(m => m.create_date).Select(m => new
                    {
                        m.id,
                        m.title,
                        m.description,
                        full_name = context.USERs.FirstOrDefault(u => u.id == m.create_by).full_name,
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
                        _list.Add(new CheckListItemModel()
                        {
                            ID = item.id,
                            title = item.title,
                            description = item.description,
                            CreateDate = item.create_date,
                            updateDate = DateTime.Today

                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<CheckListItemModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Title":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.title) : _sortList.Sort(col.Dir, m => m.title);
                                    break;
                                case "Description":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.description) : _sortList.Sort(col.Dir, m => m.description);
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
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "List", userID, ex);
                throw new ApplicationException();
            }
            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <returns></returns>
        public List<CheckListItemModel> GetAll(Guid userID)
        {
            try
            {
                List<CheckListItemModel> _return = new List<CheckListItemModel>();
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    var _list = context.WK_CHECKLIST_ITEM.Where(m => !m.deleted).OrderByDescending(m => m.create_date).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new CheckListItemModel() { ID = item.id, title = item.title });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>CheckListGruopModel. Throw exception if not found or get some error</returns>
        public CheckListItemModel GetItemByID(CheckListItemModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    WK_CHECKLIST_ITEM _md = context.WK_CHECKLIST_ITEM.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    return new CheckListItemModel()
                    {
                        ID = _md.id,
                        title = _md.title,
                        description=_md.description,
                        CreateDate = _md.create_date,
                    };
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "GetItemByID", model.CreateBy, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(CheckListItemModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            WK_CHECKLIST_ITEM _md = new WK_CHECKLIST_ITEM();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                            }
                            else
                            {
                                _md = context.WK_CHECKLIST_ITEM.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
                                }
                            }
                            _md.title = model.title;
                            _md.description = model.description;
                            _md.update_date = model.updateDate;
                           
                            if (model.Insert)
                            {
                                _md.create_by = model.CreateBy;
                                _md.create_date = DateTime.Now;
                                context.WK_CHECKLIST_ITEM.Add(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _md.update_by = model.UpdateBy;
                                _md.update_date = DateTime.Now;
                                context.WK_CHECKLIST_ITEM.Attach(_md);
                                context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                            }
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                throw new ApplicationException();
            }
            if (model.Insert)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.InsertSuccess, Notifier.TYPE.Success);
            }
            else
            {
                Notifier.Notification(model.CreateBy, Resources.Message.UpdateSuccess, Notifier.TYPE.Success);
            }
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(CheckListItemModel model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            WK_CHECKLIST_ITEM _md = context.WK_CHECKLIST_ITEM.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.deleted = true;
                            _md.delete_by = model.DeleteBy;
                            _md.description = model.description;
                            _md.delete_date = DateTime.Now;
                            context.WK_CHECKLIST_ITEM.Attach(_md);
                            context.Entry(_md).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            TDH.Services.Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model.CreateBy, Resources.Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Check Delete item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(WK_CHECKLIST_ITEM model)
        {
            try
            {
                using (var context = new chacd26d_trandinhhungEntities())
                {
                    WK_CHECKLIST_ITEM _md = context.WK_CHECKLIST_ITEM.FirstOrDefault(m => m.id == model.id && !m.deleted);
                    if (_md == null)
                    {
                        return ResponseStatusCodeHelper.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.create_by, Resources.Message.Error, Notifier.TYPE.Error);
                TDH.Services.Log.WriteLog(FILE_NAME, "CheckDelete", model.create_by, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.NG;
        }
    }
}