using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model;
using TDH.Model.System;
using Utils;
using Utils.JqueryDatatable;

namespace TDH.Services.System
{

    /// <summary>
    /// User service class
    /// </summary>
    public class UserService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.System/UserService.cs";

        private string SessionID = "";

        public UserService(string sessionID)
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
                DataTableResponse<UserModel> _itemResponse = new DataTableResponse<UserModel>();
                //List of data
                List<UserModel> _list = new List<UserModel>();
                using (var _context = new TDHEntities())
                {
                    var _lData = _context.PROC_SYS_USER_List(this.SessionID, userID).ToList();

                    _itemResponse.draw = request.draw;
                    _itemResponse.recordsTotal = _lData.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        _lData = _lData.Where(m => m.user_name.ToLower().Contains(searchValue) ||
                                         m.full_name.ToLower().Contains(searchValue) ||
                                         m.role_name.ToLower().Contains(searchValue) ||
                                         m.last_login.ToString().ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in _lData)
                    {
                        _list.Add(new UserModel()
                        {
                            ID = item.id,
                            Locked = item.locked,
                            FullName = item.full_name,
                            UserName = item.user_name,
                            LastLoginString = item.last_login,
                            RoleName = item.role_name
                        });
                    }
                    _itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<UserModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "FullName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.FullName) : _sortList.Sort(col.Dir, m => m.FullName);
                                    break;
                                case "UserName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.UserName) : _sortList.Sort(col.Dir, m => m.UserName);
                                    break;
                                case "LastLoginString":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.LastLoginString) : _sortList.Sort(col.Dir, m => m.LastLoginString);
                                    break;
                                case "RoleName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.RoleName) : _sortList.Sort(col.Dir, m => m.RoleName);
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
        /// Get item
        /// </summary>
        /// <param name="model">User Model</param>
        /// <returns>UserModel</returns>
        public UserModel GetItemByID(UserModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _md = _context.PROC_SYS_USER_ById(model.ID, this.SessionID, model.CreateBy).FirstOrDefault();
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new UserModel()
                    {
                        ID = _md.id,
                        FullName = _md.full_name,
                        UserName = _md.user_name,
                        Locked = _md.locked,
                        RoleID = _md.role_id,
                        RoleName = _md.role_name,
                        Insert = false
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
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(UserModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    string _password = null;
                    if (model.Password != null && model.Password.Length > 0)
                    {
                        _password = Utils.Security.PasswordSecurityHelper.GetHashedPassword(model.Password);
                    }
                    ObjectParameter _status = new ObjectParameter("STATUS", typeof(int));
                    int _return = _context.PROC_SYS_USER_Save(model.ID, model.FullName, model.UserName, _password, model.Locked, model.Notes, model.RoleID, model.CreateBy, model.UpdateBy, model.Insert, this.SessionID, model.CreateBy, _status);
                    if(_status.Value.ToString() == "0")
                    {
                        throw new DataAccessException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy);
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
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(UserModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    ObjectParameter _status = new ObjectParameter("STATUS", typeof(int));
                    int _result = _context.PROC_SYS_USER_Publish(model.ID, model.Locked, model.UpdateBy, this.SessionID, model.CreateBy, _status);
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
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(UserModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    ObjectParameter _status = new ObjectParameter("STATUS", typeof(int));
                    int _result = _context.PROC_SYS_USER_Delete(model.ID, model.DeleteBy, this.SessionID, model.CreateBy, _status);
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
            catch (ServiceException serviceEx)
            {
                throw serviceEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, model.CreateBy, ex);
            }
            Notifier.Notification(model.CreateBy, Message.DeleteSuccess, Notifier.TYPE.Success);
            return ResponseStatusCodeHelper.Success;
        }

        /// <summary>
        /// Login function
        /// </summary>
        /// <param name="model">Login model</param>
        /// <returns>UserModel</returns>
        public UserModel Login(LoginModel model)
        {
            UserModel _return = new UserModel() { };
            try
            {
                using (var _context = new TDHEntities())
                {
                    string _password = Utils.Security.PasswordSecurityHelper.GetHashedPassword(model.Password);
                    var _md = _context.PROC_SYS_LOGIN(model.UserName, _password).FirstOrDefault();
                    if (_md == null)
                    {
                        return _return;
                    }
                    return new UserModel()
                    {
                        ID = (Guid)_md.user_id,
                        UserName = _md.user_name,
                    };
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, new Guid(), ex);
            }
        }

        /// <summary>
        /// Get sidebar data, permission based on user identifier
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<ModuleSideBarViewModel></returns>
        public List<ModuleSideBarViewModel> GetSidebar(Guid userID)
        {
            List<ModuleSideBarViewModel> _return = new List<ModuleSideBarViewModel>();
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _listFunction = _context.PROC_SYS_SIDEBAR_ByUserID(userID).ToList();

                    var _listModule = _listFunction.Select(m => m.module_code).Distinct().ToList();
                    foreach (var m in _listModule)
                    {
                        var _module = _listFunction.FirstOrDefault(f => f.module_code == m);

                        ModuleSideBarViewModel _md = new ModuleSideBarViewModel()
                        {
                            Icon = _module.icon,
                            Title = _module.module_title,
                            DefaultUrl = _module.default_action
                        };
                        if (m == "marketing")
                        {
                            _md.functions = _listFunction.Where(f => f.module_code == m).Select(s => new SideBarViewModel() { Code = s.code, Title = s.title, Action = s.url }).ToList();
                        }
                        _return.Add(_md);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
            return _return;
        }

        /// <summary>
        /// Get sidebar data, permission based on user identifier
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <param name="moduleCode">Module code</param>
        /// <returns>List<SideBarViewModel></returns>
        public List<SideBarViewModel> GetSidebar(Guid userID, string moduleCode)
        {
            List<SideBarViewModel> _return = new List<SideBarViewModel>();
            try
            {
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.PROC_SYS_SIDEBAR_ByUserID(userID)
                                 where m.module_code == moduleCode
                                 select new { m.code, m.title, m.url }).ToList();

                    foreach (var item in _list)
                    {
                        _return.Add(new SideBarViewModel()
                        {
                            Code = item.code,
                            Title = item.title,
                            Action = item.url
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
            return _return;
        }

    }
}

