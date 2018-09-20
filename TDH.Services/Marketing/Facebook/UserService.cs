using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
using TDH.Common.UserException;
using TDH.DataAccess;
using TDH.Model.Marketing.Facebook;
using Utils;

namespace TDH.Services.Marketing.Facebook
{
    /// <summary>
    /// Facebook user service
    /// </summary>
    public class UserService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Marketing/UserService.cs";

        #endregion

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetAll(Guid userID)
        {
            try
            {
                List<UserModel> _return = new List<UserModel>();
                using (var context = new TDHEntities())
                {
                    var _list = context.FB_USER.Where(m => !m.deleted && m.created_by == userID).OrderByDescending(m => m.created_date).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new UserModel()
                        {
                            UID = item.uid,
                            Name = item.name
                        });
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
        /// <returns>UserModel. Throw exception if not found or get some error</returns>
        public UserModel GetItemByID(UserModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_USER _md = context.FB_USER.FirstOrDefault(m => m.uid == model.UID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new UserModel()
                    {
                        UID = _md.uid,
                        Name = _md.name
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
        /// Get item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>UserModel. Throw exception if not found or get some error</returns>
        public string GetTokenByItem(UserModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_USER _md = context.FB_USER.FirstOrDefault(m => m.uid == model.UID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetTokenByItem", model.CreateBy);
                    }
                    if (_md.expires_on <= DateTime.Now || _md.start_on >= DateTime.Now)
                        return "";
                    return _md.auth_token;
                }
            }
            catch (DataAccessException fieldEx)
            {
                throw fieldEx;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetTokenByItem", model.CreateBy, ex);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(UserModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    model.Insert = false;
                    FB_USER _md = new FB_USER();
                    _md = context.FB_USER.FirstOrDefault(m => m.uid == model.UID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        _md = new FB_USER();
                        _md.uid = model.UID;
                        model.Insert = true;
                    }
                    _md.name = model.Name;
                    _md.mobile = model.Mobile;
                    _md.email = model.Email;
                    _md.auth_token = model.AuthToken;
                    _md.start_on = model.StartOn;
                    _md.last_execute = model.LastExecute;
                    _md.expires_on = model.ExpiresOn;
                    _md.ordering = model.Ordering;
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        context.FB_USER.Add(_md);
                        context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        context.FB_USER.Attach(_md);
                        context.Entry(_md).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                }
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
        /// Update Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper UpdateToken(UserModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_USER _md = context.FB_USER.FirstOrDefault(m => m.uid == model.UID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "UpdateToken", model.CreateBy);
                    }
                    _md.auth_token = model.AuthToken;
                    _md.start_on = model.StartOn;
                    _md.last_execute = model.LastExecute;
                    _md.expires_on = model.ExpiresOn;
                    _md.updated_by = model.UpdateBy;
                    _md.updated_date = DateTime.Now;
                    context.FB_USER.Attach(_md);
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
                throw new ServiceException(FILE_NAME, "UpdateToken", model.CreateBy, ex);
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
        /// last execute time
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper LastExecute(UserModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_USER _md = context.FB_USER.FirstOrDefault(m => m.uid == model.UID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "LastExecute", model.CreateBy);
                    }
                    _md.last_execute = model.LastExecute;
                    _md.updated_by = model.UpdateBy;
                    _md.updated_date = DateTime.Now;
                    context.FB_USER.Attach(_md);
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
                throw new ServiceException(FILE_NAME, "LastExecute", model.CreateBy, ex);
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
        public ResponseStatusCodeHelper Delete(UserModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_USER _md = context.FB_USER.FirstOrDefault(m => m.uid == model.UID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    context.FB_USER.Remove(_md);
                    context.Entry(_md).State = EntityState.Deleted;
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
