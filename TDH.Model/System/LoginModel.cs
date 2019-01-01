using System;

namespace TDH.Model.System
{
    /// <summary>
    /// Login model
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// Remember me option
        /// </summary>
        public bool RememberMe { get; set; } = false;

        /// <summary>
        /// Browser from mobile or computer
        /// </summary>
        public bool IsMobile { get; set; } = false;

        /// <summary>
        /// Browser platform
        /// </summary>
        public string PlatForm { get; set; } = "";

        /// <summary>
        /// Browser version
        /// </summary>
        public string Version { get; set; } = "";

        /// <summary>
        /// User agent
        /// </summary>
        public string UserAgent { get; set; } = "";

        /// <summary>
        /// Host name
        /// </summary>
        public string HostName { get; set; } = "";

        /// <summary>
        /// Host address
        /// </summary>
        public string HostAddress { get; set; } = "";

        /// <summary>
        /// Current session id
        /// </summary>
        public string SessionID { get; set; } = "";
    }
}
