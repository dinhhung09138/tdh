using System;
using System.Collections.Generic;
using System.Linq;
using TDH.DataAccess;
using TDH.Model.Personal;
using TDH.Common.UserException;

namespace TDH.Services.Personal
{
    /// <summary>
    /// Education type service
    /// </summary>
    public class EducationTypeService
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private readonly string FILE_NAME = "Services.Personal/EducationTypeService.cs";

        #endregion

        /// <summary>
        /// Get all item without deleted
        /// </summary>
        /// <param name="userID">The user identifier</param>
        /// <returns>List<EducationTypeModel></returns>
        public List<EducationTypeModel> GetAll(Guid userID)
        {
            try
            {
                List<EducationTypeModel> _return = new List<EducationTypeModel>();
                using (var _context = new TDHEntities())
                {
                    var _list = (from m in _context.PN_EDUCATION_TYPE
                                 where !m.deleted && m.publish
                                 orderby m.ordering descending
                                 select new
                                 {
                                     m.code,
                                     m.name
                                 }).ToList();
                    foreach (var item in _list)
                    {
                        _return.Add(new EducationTypeModel() { Code = item.code, Name = item.name });
                    }
                }
                return _return;
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, "GetAll", userID, ex);
            }
        }

    }
}
