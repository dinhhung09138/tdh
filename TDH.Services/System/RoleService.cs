using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.System;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.System
{
    /// <summary>
    /// Role service
    /// </summary>
    public class RoleService
    {

        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.System/RoleService.cs";

        private string SessionID = "";

        public RoleService(string sessionID)
        {
            this.SessionID = sessionID;
        }

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
                using (var _context = new TDHEntities())
                {
                    var _lData = _context.PROC_SYS_ROLE_List(this.SessionID, userID).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.description.ToLower().Contains(searchValue) ||
                                         m.name.ToLower().Contains(searchValue)).ToList();
                    }

                    foreach (var item in _lData)
                    {
                        _list.Add(new RoleModel()
                        {
                            ID = (Guid)item.id,
                            Name = item.name,
                            Description = item.description,
                            Publish = item.publish,
                            Count = item.count,
                            CountString = item.count.NumberToString()
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
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
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
                using (var _context = new TDHEntities())
                {
                    var _list = _context.PROC_SYS_ROLE_List(this.SessionID, userID).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new RoleModel() { ID = (Guid)item.id, Name = item.name });
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
        /// Get item
        /// </summary>
        /// <param name="model">Role Model</param>
        /// <returns>RoleModel</returns>
        public RoleModel GetItemByID(RoleModel model)
        {
            RoleModel _return = new RoleModel() { ID = Guid.NewGuid(), Insert = model.Insert };
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _lFunc = _context.PROC_SYS_FUNCTION_List(this.SessionID, model.CreateBy).ToList();
                    foreach (var item in _lFunc)
                    {
                        _return.Detail.Add(new RoleDetailModel()
                        {
                            FunctionCode = item.code,
                            FunctionName = item.name,
                            View = item.view,
                            Add = item.add,
                            Edit = item.edit,
                            Delete = item.delete
                        });
                    }

                    var _md = _context.PROC_SYS_ROLE_ById(model.ID, this.SessionID, model.CreateBy).FirstOrDefault();
                    if (_md != null)
                    {
                        _return.ID = (Guid)_md.id;
                        _return.Name = _md.name;
                        _return.Description = _md.description;
                        _return.Publish = _md.publish;
                        //
                        var _lPers = _context.PROC_SYS_ROLE_DETAIL_ByRoleId(_md.id, this.SessionID, model.CreateBy).ToList();
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
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
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
                            SYS_ROLE _md = new SYS_ROLE();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                            }
                            else
                            {
                                _md = context.SYS_ROLE.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                                }
                            }
                            _md.name = model.Name;
                            _md.description = model.Description;
                            _md.publish = model.Publish;
                            if (model.Insert)
                            {
                                _md.created_by = model.CreateBy;
                                _md.created_date = DateTime.Now;
                                context.SYS_ROLE.Add(_md);
                                context.Entry(_md).State = EntityState.Added;
                            }
                            else
                            {
                                _md.updated_by = model.UpdateBy;
                                _md.updated_date = DateTime.Now;
                                context.SYS_ROLE.Attach(_md);
                                context.Entry(_md).State = EntityState.Modified;
                                //
                                var _lDetaiPerm = context.SYS_ROLE_DETAIL.Where(m => m.role_id == _md.id);
                                if (_lDetaiPerm.Count() > 0)
                                {
                                    context.SYS_ROLE_DETAIL.RemoveRange(_lDetaiPerm);
                                }
                            }
                            foreach (var item in model.Detail)
                            {
                                SYS_ROLE_DETAIL _dt = new SYS_ROLE_DETAIL()
                                {
                                    id = Guid.NewGuid(),
                                    function_code = item.FunctionCode,
                                    role_id = _md.id,
                                    view = item.View,
                                    add = item.Add,
                                    edit = item.Edit,
                                    delete = item.Delete
                                };
                                context.SYS_ROLE_DETAIL.Add(_dt);
                                context.Entry(_dt).State = EntityState.Added;
                            }
                            context.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
                        }
                    }
                }
            }
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
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
        /// <param name="model">Role Model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(RoleModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    ObjectParameter _status = new ObjectParameter("STATUS", typeof(int));
                    int _result = _context.PROC_SYS_ROLE_Publish(model.ID, model.Publish, this.SessionID, model.UpdateBy, _status);
                    if (_status.Value.ToString() == "0")
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
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
        /// <param name="model">Role Model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(RoleModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    ObjectParameter _status = new ObjectParameter("STATUS", typeof(int));
                    var _return = _context.PROC_SYS_ROLE_Delete(model.ID, this.SessionID, model.DeleteBy, _status);
                    if (_status.Value.ToString() == "0")
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
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
        /// Check delete
        /// </summary>
        /// <param name="model">Role Model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper CheckDelete(RoleModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    ObjectParameter _status = new ObjectParameter("STATUS", typeof(int));
                    var _return = _context.PROC_SYS_ROLE_CheckDelete(model.ID, this.SessionID, model.DeleteBy, _status);
                    if (_status.Value.ToString() == "0")
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
                    }
                    if (_status.Value.ToString() == "-1")
                    {
                        Notifier.Notification(model.CreateBy, Message.CheckExists, Notifier.TYPE.Warning);
                        return ResponseStatusCodeHelper.NG;
                    }
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
            return ResponseStatusCodeHelper.OK;
        }
        
    }
}
