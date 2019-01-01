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
    /// Facebook group service
    /// </summary>
    public class GroupService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Marketing/PostService.cs";

        #endregion

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <returns></returns>
        public List<GroupModel> GetAll(Guid userID)
        {
            try
            {
                List<GroupModel> _return = new List<GroupModel>();
                using (var context = new TDHEntities())
                {
                    var _list = context.FB_GROUP.Where(m => !m.deleted && m.created_by == userID).OrderByDescending(m => m.created_date).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new GroupModel()
                        {
                            UID = item.uid,
                            Name = item.name,
                            Link = item.link
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
        /// <returns>GroupModel. Throw exception if not found or get some error</returns>
        public GroupModel GetItemByID(GroupModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_GROUP _md = context.FB_GROUP.FirstOrDefault(m => m.uid == model.UID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "GetItemByID", model.CreateBy);
                    }
                    return new GroupModel()
                    {
                        UID = _md.uid,
                        Name = _md.name,
                        Link = _md.link
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
        public ResponseStatusCodeHelper Save(GroupModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    model.Insert = false;
                    FB_GROUP _md = new FB_GROUP();
                    _md = context.FB_GROUP.FirstOrDefault(m => m.uid == model.UID && !m.deleted && m.created_by == model.CreateBy);
                    if (_md == null)
                    {
                        model.Insert = true;
                        _md = new FB_GROUP();
                        _md.uid = model.UID;
                    }
                    _md.link = model.Link;
                    _md.name = model.Name;
                    _md.ordering = model.Ordering;
                    if (model.Insert)
                    {
                        _md.created_by = model.CreateBy;
                        _md.created_date = DateTime.Now;
                        context.FB_GROUP.Add(_md);
                        context.Entry(_md).State = EntityState.Added;
                    }
                    else
                    {
                        _md.updated_by = model.UpdateBy;
                        _md.updated_date = DateTime.Now;
                        context.FB_GROUP.Attach(_md);
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
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResponseStatusCodeHelper</returns>
        public ResponseStatusCodeHelper Delete(GroupModel model)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    FB_GROUP _md = context.FB_GROUP.FirstOrDefault(m => m.uid == model.UID && m.created_by == model.CreateBy && !m.deleted);
                    if (_md == null)
                    {
                        throw new DataAccessException(FILE_NAME, "Delete", model.CreateBy);
                    }
                    context.FB_GROUP.Remove(_md);
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
