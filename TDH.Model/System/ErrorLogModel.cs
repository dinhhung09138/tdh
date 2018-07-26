using System;

namespace TDH.Model.System
{
    /// <summary>
    /// Error log model
    /// </summary>
    public class ErrorLogModel : Utils.Database.BaseModel
    {
        /// <summary>
        /// THe identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Method name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Inner exception
        /// </summary>
        public string InnerException { get; set; }

        /// <summary>
        /// Stack trace
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Date happend
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Date, format as string
        /// </summary>
        public string DateString { get; set; }

    }
}
