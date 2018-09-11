using System;
using System.Collections.Generic;
using System.Linq;
using Utils.JqueryDatatable;
using Utils;
using TDH.Common;
using TDH.DataAccess;
using TDH.Model.Personal;
using System.Data.Entity;

namespace TDH.Services.Personal
{
    /// <summary>
    /// Event type service
    /// </summary>
    public class EventTypeService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services/Personal/EventTypeService.cs";

        #endregion

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<EventTypeModel></returns>
        public List<EventTypeModel> GetAll(Guid userID)
        {
            try
            {
                List<EventTypeModel> _return = new List<EventTypeModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.PN_EVENT_TYPE
                                 where !m.deleted && m.publish
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.code,
                                     m.name
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new EventTypeModel() { Code = item.code, Name = item.name });
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

    }
}
