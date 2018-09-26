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
    }
}
