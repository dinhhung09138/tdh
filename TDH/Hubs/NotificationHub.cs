using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TDH.Common;

namespace TDH
{
    public class NotificationHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        /// Send notification to user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public void SendNotification(string userID, string message, string type)
        {
            Clients.All.notificationToUser(userID, message, type);
        }
    }
}