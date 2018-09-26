using System;

namespace TDH.Common.UserException
{
    /// <summary>
    /// Controller exception
    /// This will happen when has any error happend in controller
    /// Only use in area module
    /// </summary>
    public class ControllerException : Exception
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
        
        public ControllerException(string fileName, string functionName, Guid userID, Exception ex = null) : base("", ex)
        {
            this.FileName = fileName;
            this.ActionName = functionName;
            this.UserID = userID;
            Log.WriteLog(FileName, ActionName, UserID, new Exception() { Source = ex.Source });
            Notifier.Notification(userID, Common.Message.Error, Notifier.TYPE.Error);
        }
    }
}
