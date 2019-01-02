using System;
using System.Collections.Generic;
using System.Linq;
using TDH.DataAccess;
using TDH.Model.Personal;
using TDH.Common.UserException;
using System.Reflection;

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
                using (var _context = new TDHEntities())
                {
                    return (from m in _context.PN_EDUCATION_TYPE
                            where !m.deleted && m.publish
                            orderby m.ordering descending
                            select new EducationTypeModel()
                            {
                                Code = m.code,
                                Name = m.name
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(FILE_NAME, MethodInfo.GetCurrentMethod().Name, userID, ex);
            }
        }

    }
}
