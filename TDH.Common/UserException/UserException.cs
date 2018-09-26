using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Common.UserException
{
    /// <summary>
    /// Exception happend in user webpage module
    /// </summary>
    public class UserException : Exception
    {
        /// <summary>
        /// Status of exception
        /// 204 No Content
        /// 403 Forbidden
        /// 404 Not Found
        /// 500 Internal Server Error
        /// 501 Not Implemented
        /// </summary>
        public int Status { get; set; } = 0;

        public UserException()
        {

        }

        public UserException(string message) : base(message)
        {

        }

        /// <summary>
        /// UserException
        /// </summary>
        /// <param name="fileName">Service file name</param>
        /// <param name="functionName">Function from service file</param>
        /// <param name="ex">Exception</param>
        public UserException(string fileName, string functionName, Exception ex) : base("", ex)
        {
            Log.WriteLog(fileName, functionName, new Guid(), ex);
        }

        /// <summary>
        /// UserException
        /// </summary>
        /// <param name="fileName">Service file name</param>
        /// <param name="functionName">Function from service file</param>
        /// <param name="status">status code</param>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        public UserException(string fileName, string functionName, int status, string message, Exception ex) : base(message, ex)
        {
            this.Status = status;
            Log.WriteLog(fileName, functionName, new Guid(), ex);
        }
    }
}
