using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Common
{
    /// <summary>
    /// Notification
    /// </summary>
    public class Notifier
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private static readonly string FILE_NAME = "Common/Notifier.cs";

        /// <summary>
        /// Hub url
        /// </summary>
        private static string HUB_URL = Configuration.SettingValue("setting_hubconnect");

        /// <summary>
        /// Hub name
        /// </summary>
        private static string HUB_NAME = Configuration.SettingValue("setting_hubname");

        /// <summary>
        /// Type of notification: success, error, warning
        /// </summary>
        public class TYPE
        {
            /// <summary>
            /// Success
            /// </summary>
            public static readonly string Success = "success";

            /// <summary>
            /// Error
            /// </summary>
            public static readonly string Error = "error";

            /// <summary>
            /// Warning
            /// </summary>
            public static readonly string Warning = "warning";
        }

        #endregion

        /// <summary>
        /// Notification to user
        /// None async
        /// </summary>
        /// <param name="userID">The user identifier, who receive message</param>
        /// <param name="message">Message</param>
        /// <param name="type">type: success, error, warning</param>
        public static void Notification(Guid userID, string message, string type)
        {
            try
            {
                var _hubConnection = new HubConnection(HUB_URL)
                {
                    Credentials = CredentialCache.DefaultCredentials,
                    TraceLevel = TraceLevels.All,
                    TraceWriter = Console.Out
                };
                IHubProxy _hub = _hubConnection.CreateHubProxy(HUB_NAME);
                _hubConnection.Start().Wait();
                _hub.Invoke("sendNotification", userID.ToString(), message, type);
                _hubConnection.Stop();
            }
            catch (Exception ex)
            {
                Log.WriteLog(FILE_NAME, "Notification", userID, ex);
            }
        }

        //public static async Task Notification(Guid userID, string message, string type)
        //{
        //    try
        //    {
        //        var _hubConnection = new HubConnection(HUB_URL)
        //        {
        //            Credentials = CredentialCache.DefaultCredentials,
        //            TraceLevel = TraceLevels.All,
        //            TraceWriter = Console.Out
        //        };
        //        IHubProxy _hub = _hubConnection.CreateHubProxy(HUB_NAME);
        //        _hubConnection.Start().Wait();
        //        await _hub.Invoke("SendNotification", userID.ToString(), message, type);
        //        _hubConnection.Stop();

        //    }
        //    catch (Exception ex)
        //    {
        //        TDH.Services.Log.WriteLog(FILE_NAME, "Notification", userID, ex);
        //    }
        //}
    }
}
