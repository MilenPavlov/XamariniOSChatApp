using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PushSharp.Client;
using XamChat.Core;
using XamChat.Core.ViewModels;
using XamChat.Droid.Activities;

namespace XamChat.Droid.GCM
{
    public class PushHandlerService : PushHandlerServiceBase
    {
        protected override void OnMessage(Context context, Intent intent)
        {
            //get notification details
            string title = intent.Extras.GetString("title");
            string message = intent.Extras.GetString("message");

            //create intent
            intent = new Intent(this, typeof(ConversationsActivity));

            //create notification
            var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);
            notification.Flags = NotificationFlags.AutoCancel;
            notification.SetLatestEventInfo(this, title, message, PendingIntent.GetActivity(this, 0, intent,0));

            //send notification through the Notification Manager
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            notificationManager.Notify(1, notification);
        }

        protected override void OnError(Context context, string errorId)
        {
            Console.WriteLine("Push error " + errorId);
        }

        protected async override void OnRegistered(Context context, string registrationId)
        {
            Console.WriteLine("Push successfully registered");

            var loginViewModel = ServiceContainer.Resolve<LoginViewModel>();

            try
            {
                await loginViewModel.RegisterPush(registrationId);
            }
            catch (Exception ex)
            {
                  Console.WriteLine("Error registering push " + ex);            
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Console.WriteLine("PushUnregistered");
        }
    }
}