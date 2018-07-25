using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.System;
using Utils;
using Utils.JqueryDatatable;

namespace System
{
    public class RoleService
    {

        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services/RoleService.cs";
        
        #endregion

        /// <summary>
        /// Get list data using jquery datatable
        /// </summary>
        /// <param name="request">Jquery datatable request</param>
        /// <param name="userID">User identifier</param>
        /// <returns><string, object></returns>
        public Dictionary<string, object> List(CustomDataTableRequestHelper request, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                DataTableResponse<RoleModel> _itemResponse = new DataTableResponse<RoleModel>();
                //List of data
                List<RoleModel> _list = new List<RoleModel>();
                using (var context = new TDHEntities())
                {
                    var _lData = context.ROLEs.Where(m => !m.deleted).Select(m => new { m.id, m.name, m.description, m.publish }).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.description.ToLower().Contains(searchValue) ||
                                         m.name.ToLower().Contains(searchValue)).ToList();
                    }
                    int _count = 0;
                    foreach (var item in _lData)
                    {
                        _count = context.USER_ROLE.Count(m => m.role_id == item.id);
                        _list.Add(new RoleModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            Description = item.description,
                            Publish = item.publish,
                            Count = _count,
                            CountString = _count.NumberToString()
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<RoleModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Name":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
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
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "List", userID, ex);
                throw new ApplicationException();
            }
            return _return;
        }

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<RoleModel></returns>
        public List<RoleModel> GetAll(Guid userID)
        {
            try
            {
                List<RoleModel> _return = new List<RoleModel>();
                using (var context = new TDHEntities())
                {
                    var _list = context.ROLEs.Where(m => !m.deleted).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new RoleModel() { ID = item.id, Name = item.name });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetAll", userID, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">Role Model</param>
        /// <returns>RoleModel. Throw exception if not found or get some error</returns>
        public RoleModel GetItemByID(RoleModel model)
        {
            RoleModel _return = new RoleModel() { ID = Guid.NewGuid(), Insert = model.Insert };
            try
            {
                using (var context = new TDHEntities())
                {
                    List<FUNCTION> _lFunc = context.FUNCTIONs.OrderByDescending(m => m.ordering).ToList();
                    foreach (var item in _lFunc)
                    {
                        _return.Detail.Add(new RoleDetailModel()
                        {
                            FunctionCode = item.code,
                            FunctionName = item.name,
                            View = false,
                            Add = false,
                            Edit = false,
                            Delete = false
                        });
                    }

                    ROLE _md = context.ROLEs.FirstOrDefault(m => m.id == model.ID);
                    if (_md != null)
                    {
                        _return.ID = _md.id;
                        _return.Name = _md.name;
                        _return.Description = _md.description;
                        _return.Publish = _md.publish;
                        //
                        var _lPers = context.ROLE_DETAIL.Where(m => m.role_id == _md.id).ToList();
                        foreach (var item in _lPers)
                        {
                            var _tmp = _return.Detail.FirstOrDefault(m => m.FunctionCode == item.function_code);
                            if (_tmp != null)
                            {
                                _tmp.Add = item.add;
                                _tmp.View = item.view;
                                _tmp.Edit = item.edit;
                                _tmp.Delete = item.delete;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetItemByID", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return _return;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model">Role Model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(RoleModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            ROLE _md = new ROLE();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                            }
                            else
                            {
                                _md = context.ROLEs.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
                                }
                            }
                            _md.name = model.Name;
                            _md.description = model.Description;
                            _md.publish = model.Publish;
                            if (model.Insert)
                            {
                                _md.created_by = model.CreateBy;
                                _md.created_date = DateTime.Now;
                                context.ROLEs.Add(_md);
                                context.Entry(_md).State = Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                _md.updated_by = model.UpdateBy;
                                _md.updated_date = DateTime.Now;
                                context.ROLEs.Attach(_md);
                                context.Entry(_md).State = Data.Entity.EntityState.Modified;
                                //
                                var _lDetaiPerm = context.ROLE_DETAIL.Where(m => m.role_id == _md.id);
                                if (_lDetaiPerm.Count() > 0)
                                {
                                    context.ROLE_DETAIL.RemoveRange(_lDetaiPerm);
                                }
                            }
                            foreach (var item in model.Detail)
                            {
                                ROLE_DETAIL _dt = new ROLE_DETAIL()
                                {
                                    id = Guid.NewGuid(),
                                    function_code = item.FunctionCode,
                                    role_id = _md.id,
                                    view = item.View,
                                    add = item.Add,
                                    edit = item.Edit,
                                    delete = item.Delete
                                };
                                context.ROLE_DETAIL.Add(_dt);
                                context.Entry(_dt).State = System.Data.Entity.EntityState.Added;
                            }
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "Save", model.CreateBy, ex);
                throw new ApplicationException();
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
        /// <param name="model">Role Model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(RoleModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            ROLE _md = context.ROLEs.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.publish = model.Publish;
                            _md.updated_by = model.UpdateBy;
                            _md.updated_date = DateTime.Now;
                            context.ROLEs.Attach(_md);
                            context.Entry(_md).State = Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            Log.WriteLog(FILE_NAME, "Publish", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "Publish", model.CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model.CreateBy, Message.UpdateSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model">Role Model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(RoleModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    using (var trans = context.Database.BeginTransaction())
                    {
                        try
                        {
                            ROLE _md = context.ROLEs.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.deleted = true;
                            _md.deleted_by = model.DeleteBy;
                            _md.deleted_date = DateTime.Now;
                            context.ROLEs.Attach(_md);
                            context.Entry(_md).State = Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                            trans.Rollback();
                            Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                            throw new ApplicationException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            Notifier.Notification(model.CreateBy, Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Check delete
        /// </summary>
        /// <param name="model">Role Model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(RoleModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    ROLE _md = context.ROLEs.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _user = context.USER_ROLE.FirstOrDefault(m => m.role_id == _md.id);
                    if (_user != null)
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "CheckDelete", model.CreateBy, ex);
                throw new ApplicationException();
            }
            return ResponseStatusCodeHelper.OK;
        }

        /// <summary>
        /// Check access into method
        /// </summary>
        /// <param name="userID">User identifier</param>
        /// <param name="functionCode">Function code</param>
        /// <returns>RoleDetailModel</returns>
        public RoleDetailModel AllowAccess(Guid userID, string functionCode)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    var _permision = (from dt in context.ROLE_DETAIL
                                      join r in context.ROLEs on dt.role_id equals r.id
                                      join ur in context.USER_ROLE on r.id equals ur.role_id
                                      where ur.user_id == userID && dt.function_code == functionCode && !r.deleted && r.publish
                                      select dt).FirstOrDefault();
                    if (_permision != null)
                    {
                        return new RoleDetailModel()
                        {
                            View = _permision.view,
                            Add = _permision.add,
                            Edit = _permision.edit,
                            Delete = _permision.delete
                        };
                    }
                    return new RoleDetailModel()
                    {
                        View = false,
                        Add = false,
                        Edit = false,
                        Delete = false
                    };
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "AllowAccess", userID, ex);
                throw new ApplicationException();
            }
        }
    }
}
