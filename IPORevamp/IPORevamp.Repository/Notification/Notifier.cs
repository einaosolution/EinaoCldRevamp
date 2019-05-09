using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Notification
{
    public class FCMNotifier:IFCMNotifier
    {
        public async Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data)
        {
            return true;
        }
    }
}
