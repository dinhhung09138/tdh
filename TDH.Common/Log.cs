using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDH.DataAccess;

namespace TDH.Common
{
    /// <summary>
    /// Log service
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Write error log to database
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="methodName">method name</param>
        /// <param name="userID">user action</param>
        /// <param name="ex">Exception</param>
        public static void WriteLog(string fileName, string methodName, Guid userID, Exception ex)
        {
            try
            {
                using (var context = new TDHEntities())
                {
                    ERROR_LOG _md = new ERROR_LOG();
                    _md.id = Guid.NewGuid();
                    _md.file_name = fileName;
                    _md.method_name = methodName;
                    _md.source = ex.Source;
                    _md.stack_trace = ex.StackTrace;
                    _md.inner_exception = ex.InnerException != null ? ex.InnerException.Message : "";
                    _md.message = ex.Message;
                    _md.date = DateTime.Now;
                    _md.create_date = DateTime.Now;
                    _md.create_by = userID;
                    context.ERROR_LOG.Add(_md);
                    context.Entry(_md).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Utils.ErrorLogger.TextLoggerHelper.OutputLog(e.Message);
            }
        }
    }
}
