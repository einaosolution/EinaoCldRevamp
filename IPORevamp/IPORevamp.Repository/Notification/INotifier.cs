using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Notification
{
    public interface IFCMNotifier
    {
        Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data);
    }
}
