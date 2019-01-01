using System;
using System.Collections.Generic;
using System.Linq;
using TDH.DataAccess;
using TDH.Model.Personal;
using TDH.Common.UserException;

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
        private readonly string FILE_NAME = "Services.Personal/EventTypeService.cs";

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
                using (var _context = new TDHEntities())
                {
                    return (from m in _context.PN_EVENT_TYPE
                            where !m.deleted && m.publish
                            orderby m.ordering descending
                            select new EventTypeModel()
                            {
                                Code = m.code,
                                Name = m.name
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
            }
        }

    }
}
