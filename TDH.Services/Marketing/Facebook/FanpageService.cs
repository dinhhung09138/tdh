using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Marketing.Facebook;
using Utils;

namespace TDH.Services.Marketing.Facebook
{
    public class FanpageService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Marketing.Facebook/FanpageService.cs";

        #endregion

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <returns></returns>
        public List<FanpageModel> GetAll(Guid userID)
        {
            try
            {
                List<FanpageModel> _return = new List<FanpageModel>();
                using (var context = new TDHEntities())
                {
                    var _list = context.FB_FANPAGE.Where(m => !m.deleted && m.created_by == userID).OrderByDescending(m => m.created_date).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new FanpageModel()
                        {
                            UID = item.uid,
                            UserName = item.user_name,
                            Link = item.link,
                            DisplayName = item.display_name
                        });
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
        /// <param name="model"></param>
        /// <returns>FanpageModel. Throw exception if not found or get some error</returns>
        public FanpageModel GetItemByID(FanpageModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_FANPAGE _md = context.FB_FANPAGE.FirstOrDefault(m => m.uid == model.UID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    return new FanpageModel()
                    {
                        UID = _md.uid,
                        UserName = _md.user_name,
                        Link = _md.link,
                        DisplayName = _md.display_name
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
        /// Get item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>FanpageModel. Throw exception if not found or get some error</returns>
        public string GetTokenByItem(FanpageModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_FANPAGE _md = context.FB_FANPAGE.FirstOrDefault(m => m.uid == model.UID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    if (_md.expires_on <= DateTime.Now || _md.start_on >= DateTime.Now)
                        return "";
                    return _md.auth_token;
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "GetTokenByItem", model.CreateBy, ex);
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Save(FanpageModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_FANPAGE _md = new FB_FANPAGE();
                    if (model.Insert)
                    {
                        _md.uid = model.UID;
                    }
                    else
                    {
                        _md = context.FB_FANPAGE.FirstOrDefault(m => m.uid == model.UID && !m.deleted && m.created_by == model.CreateBy);
                        if (_md == null)
                        {
                            throw new FieldAccessException();
                        }
                    }
                    _md.link = model.Link;
                    _md.user_name = model.UserName;
                    _md.display_name = model.DisplayName;
                    _md.phone = model.Phone;
                    _md.email = model.Email;
                    _md.website = model.Website;
                    _md.auth_token = model.AuthToken;
                    _md.start_on = model.StartOn;
                    _md.last_execute = model.LastExecute;
                    _md.expires_on = model.ExpiresOn;
                    _md.ordering = model.Ordering;
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        context.FB_FANPAGE.Add(_md);
                        context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        context.FB_FANPAGE.Attach(_md);
                        context.Entry(_md).State = EntityState.Modified;
                    }
                    context.SaveChanges();
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
        /// Update Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper UpdateToken(FanpageModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_FANPAGE _md = context.FB_FANPAGE.FirstOrDefault(m => m.uid == model.UID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    _md.auth_token = model.AuthToken;
                    _md.start_on = model.StartOn;
                    _md.last_execute = model.LastExecute;
                    _md.expires_on = model.ExpiresOn;
                    _md.updated_by = model.UpdateBy;
                    _md.updated_date = DateTime.Now;
                    context.FB_FANPAGE.Attach(_md);
                    context.Entry(_md).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "UpdateToken", model.CreateBy, ex);
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
        /// last execute time
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper LastExecute(FanpageModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_FANPAGE _md = context.FB_FANPAGE.FirstOrDefault(m => m.uid == model.UID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    _md.last_execute = model.LastExecute;
                    _md.updated_by = model.UpdateBy;
                    _md.updated_date = DateTime.Now;
                    context.FB_FANPAGE.Attach(_md);
                    context.Entry(_md).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Notifier.Notification(model.CreateBy, Message.Error, Notifier.TYPE.Error);
                Log.WriteLog(FILE_NAME, "LastExecute", model.CreateBy, ex);
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
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(FanpageModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_FANPAGE _md = context.FB_FANPAGE.FirstOrDefault(m => m.uid == model.UID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new FieldAccessException();
                    }
                    context.FB_FANPAGE.Remove(_md);
                    context.Entry(_md).State = EntityState.Deleted;
                    context.SaveChanges();
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

    }
}
