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
    }
}
