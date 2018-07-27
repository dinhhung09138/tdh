﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
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
        private readonly string FILE_NAME = "Services/UserService.cs";

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
                    var _lData = (from m in _context.SYS_USER
                                  join ur in _context.SYS_USER_ROLE on m.id equals ur.user_id
                                  join r in _context.SYS_ROLE on ur.role_id equals r.id
                                  where !m.deleted
                                  select new
                                  {
                                      m.id,
                                      m.user_name,
                                      m.full_name,
                                      m.last_login,
                                      m.locked,
                                      role_name = r.name
                                  }).ToList();

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
                            LastLoginString = item.last_login == null ? "" : ((DateTime)item.last_login).DateToString("dd/MM/yyyy hh:mm"),
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
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "List", userID, ex);
                throw new ApplicationException();
            }
            return _return;
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="model">User Model</param>
        /// <returns>UserModel. Throw exception if not found or get some error</returns>
        public UserModel GetItemByID(UserModel model)
        {
            UserModel _return = new UserModel() { ID = Guid.NewGuid(), Insert = model.Insert };
            try
            {
                using (var _context = new TDHEntities())
                {
                    SYS_USER _md = _context.SYS_USER.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    var _role = (from m in _context.SYS_ROLE
                                 join r in _context.SYS_USER_ROLE on m.id equals r.role_id
                                 where r.user_id == _md.id
                                 select new { m.id, m.name }).FirstOrDefault();
                    return new UserModel()
                    {
                        ID = _md.id,
                        FullName = _md.full_name,
                        UserName = _md.user_name,
                        Locked = _md.locked,
                        RoleID = _role.id,
                        RoleName = _role.name
                    };
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetItemByID", model.CreateBy, ex);
                throw new ApplicationException();
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
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            SYS_USER _md = new SYS_USER();
                            if (model.Insert)
                            {
                                _md.id = Guid.NewGuid();
                                _md.user_name = model.UserName;
                                _md.notes = model.Notes;
                                _md.password = Utils.Security.PasswordSecurityHelper.GetHashedPassword(model.Password);
                            }
                            else
                            {
                                _md = _context.SYS_USER.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                                if (_md == null)
                                {
                                    throw new FieldAccessException();
                                }
                                if (model.Password != null && model.Password.Length > 0)
                                {
                                    _md.password = Utils.Security.PasswordSecurityHelper.GetHashedPassword(model.Password);
                                }
                            }
                            _md.full_name = model.FullName;
                            _md.locked = model.Locked;
                            if (model.Insert)
                            {
                                _md.create_by = model.CreateBy;
                                _md.create_date = DateTime.Now;
                                _context.SYS_USER.Add(_md);
                                _context.Entry(_md).State = EntityState.Added;
                                //
                                SYS_USER_ROLE role = new SYS_USER_ROLE()
                                {
                                    id = Guid.NewGuid(),
                                    user_id = _md.id,
                                    role_id = model.RoleID
                                };
                                _context.SYS_USER_ROLE.Add(role);
                                _context.Entry(role).State = EntityState.Added;
                            }
                            else
                            {
                                _md.update_by = model.UpdateBy;
                                _md.update_date = DateTime.Now;
                                _context.SYS_USER.Attach(_md);
                                _context.Entry(_md).State = EntityState.Modified;
                                var role = _context.SYS_USER_ROLE.FirstOrDefault(m => m.user_id == model.ID);
                                role.role_id = model.RoleID;
                                _context.SYS_USER_ROLE.Attach(role);
                                _context.Entry(role).State = EntityState.Modified;
                            }
                            _context.SaveChanges();
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
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Publish(UserModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            SYS_USER _md = _context.SYS_USER.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.locked = model.Locked;
                            _md.update_by = model.UpdateBy;
                            _md.update_date = DateTime.Now;
                            _context.SYS_USER.Attach(_md);
                            _context.Entry(_md).State = EntityState.Modified;
                            _context.SaveChanges();
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
        /// <param name="model">User model</param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(UserModel model)
        {
            try
            {
                using (var _context = new TDHEntities())
                {
                    using (var trans = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            SYS_USER _md = _context.SYS_USER.FirstOrDefault(m => m.id == model.ID && !m.deleted);
                            if (_md == null)
                            {
                                throw new FieldAccessException();
                            }
                            _md.deleted = true;
                            _md.delete_by = model.DeleteBy;
                            _md.delete_date = DateTime.Now;
                            _context.SYS_USER.Attach(_md);
                            _context.Entry(_md).State = EntityState.Modified;
                            //Role
                            var _lRole = _context.SYS_USER_ROLE.Where(m => m.user_id == model.ID);
                            if (_lRole.Count() > 0)
                            {
                                _context.SYS_USER_ROLE.RemoveRange(_lRole);
                            }
                            _context.SaveChanges();
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
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "Delete", model.CreateBy, ex);
                throw new ApplicationException();
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
                    SYS_USER _md = _context.SYS_USER.FirstOrDefault(m => m.user_name == model.UserName && m.password == _password && !m.deleted && !m.locked);
                    if (_md == null)
                    {
                        return _return;
                    }
                    _md.last_login = DateTime.Now;
                    _context.SYS_USER.Attach(_md);
                    _context.Entry(_md).State = EntityState.Modified;
                    _context.SaveChanges();
                    //
                    return new UserModel()
                    {
                        ID = _md.id,
                        UserName = _md.user_name,
                    };
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Login", new Guid(), ex);
                throw new ApplicationException();
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
                    var _listFunction = (from u in _context.SYS_USER
                                         join ur in _context.SYS_USER_ROLE on u.id equals ur.user_id
                                         join r in _context.SYS_ROLE on ur.role_id equals r.id
                                         join dt in _context.SYS_ROLE_DETAIL on r.id equals dt.role_id
                                         join f in _context.SYS_FUNCTION on dt.function_code equals f.code
                                         where dt.view && u.id == userID && r.publish & !r.deleted
                                         orderby f.ordering descending
                                         select new { f.module_code }).Distinct().ToList();

                    var _list = _context.SYS_MODULE.OrderByDescending(m => m.ordering).ToList();

                    foreach (var item in _list)
                    {
                        if (_listFunction.Count(m => m.module_code == item.code && m.module_code != "none") > 0)
                        {
                            _return.Add(new ModuleSideBarViewModel()
                            {
                                Icon = item.icon,
                                Title = item.title,
                                DefaultUrl = item.default_action
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetSidebar", userID, ex);
                throw new ApplicationException();
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
                    var _list = (from u in _context.SYS_USER
                                 join ur in _context.SYS_USER_ROLE on u.id equals ur.user_id
                                 join r in _context.SYS_ROLE on ur.role_id equals r.id
                                 join dt in _context.SYS_ROLE_DETAIL on r.id equals dt.role_id
                                 join f in _context.SYS_FUNCTION on dt.function_code equals f.code
                                 where dt.view && u.id == userID && r.publish && !r.deleted && f.code.Contains(moduleCode)
                                 orderby f.ordering descending
                                 select new { f.code, f.title, f.area, f.controller, f.action }).ToList();

                    foreach (var item in _list)
                    {
                        _return.Add(new SideBarViewModel()
                        {
                            Code = item.code,
                            Title = item.title,
                            Area = item.area,
                            Controller = item.controller,
                            Action = item.action
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(userID, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetSidebar", userID, ex);
                throw new ApplicationException();
            }
            return _return;
        }
    }
}

