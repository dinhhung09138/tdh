using System;

namespace TDH.Common.UserException
{
    /// <summary>
    /// Service exception
    /// This excetion happen when has any error in services
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// File name path
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Action name
        /// </summary>
        public string ActionName { get; set; }
        
        /// <summary>
        /// ServiceException
        /// </summary>
        public ServiceException() : base("")
        {

        }

        /// <summary>
        /// ServiceException
        /// </summary>
        /// <param name="fileName">Service file name</param>
        /// <param name="functionName">Function from service file</param>
        /// <param name="userID">User id</param>
        /// <param name="ex">Exception</param>
        public ServiceException(string fileName, string functionName, Guid userID, Exception ex) : base("", ex)
        {
            this.FileName = fileName;
            this.ActionName = functionName;
            this.UserID = userID;
            Log.WriteLog(FileName, ActionName, UserID, ex);
            Notifier.Notification(userID, Common.Message.Error, Notifier.TYPE.Error);
        }
    }
}
