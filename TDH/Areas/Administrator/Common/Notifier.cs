using System;
using Microsoft.AspNet.SignalR.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net;

namespace TDH.Areas.Administrator.Common
{
    public class Notifier
    {
        #region " [ Properties ] "

        /// <summary>
        /// File name
        /// </summary>
        private static readonly string FILE_NAME = "Administrator/Common/Notifier.cs";

        private static string HUB_URL = Configuration.SettingValue("setting_hubconnect");

        private static string HUB_NAME = Configuration.SettingValue("setting_hubname");

        /// <summary>
        /// Type of notification: success, error, warning
        /// </summary>
        public class TYPE
        {
            public static readonly string Success = "success";
            public static readonly string Error = "error";
            public static readonly string Warning = "warning";
        }

        #endregion

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
                _hub.Invoke("SendNotification", userID.ToString(), message, type);
                _hubConnection.Stop();

            }
            catch (Exception ex)
            {
                TDH.Services.Log.WriteLog(FILE_NAME, "Notification", userID, ex);
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