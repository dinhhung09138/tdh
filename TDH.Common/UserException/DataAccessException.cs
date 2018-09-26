using System;

namespace TDH.Common.UserException
{
    /// <summary>
    /// Data access permision exception
    /// When user trying to access data with out owner. This exception will happen
    /// Only use in area module
    /// </summary>
    public class DataAccessException : Exception
    {
        /// <summary>
        /// Default message
        /// </summary>
        private static string message = "Attempted to access a field that is not accessible by the caller.";

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
        /// DataAccessException
        /// </summary>
        public DataAccessException() : base(message)
        {

        }
        /// <summary>
        /// DataAccessException
        /// </summary>
        /// <param name="fileName">Service file name</param>
        /// <param name="functionName">Function name</param>
        /// <param name="userID">user id</param>
        public DataAccessException(string fileName, string functionName, Guid userID) : base(message)
        {
            this.FileName = fileName;
            this.ActionName = functionName;
            this.UserID = userID;
            Log.WriteLog(FileName, ActionName, UserID, new Exception() { Source = message });
            Notifier.Notification(userID, Common.Message.Error, Notifier.TYPE.Error);
        }
    }
}
